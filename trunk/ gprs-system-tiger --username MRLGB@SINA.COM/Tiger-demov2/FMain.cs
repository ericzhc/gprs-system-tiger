using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Threading;
using System.Linq;
using System.Globalization;
using System.Diagnostics;
using Tiger.Helper;
using Tiger.Gprs;
using Tiger.Properties;

namespace Tiger
{
    public partial class FMain : Form
    {
        #region 主窗口全局变量

        public FServerInfo Serviceinfo = new FServerInfo();
        public FCenterConfig Config;
        public string ServIp;
        public int ConnectTime;
        public int RefreshTime;
        public int ServPort;
        public int ServType;
        public int ServMode;      
        public int ServStart = 0;
        public int Sign;

        public bool Recvdata;
        public bool Threadrun;//控制线程
        public Thread ThreadBlock;

        private readonly Random _rand = new Random();//Produce Data test
        private DoubleQueue _dqueue;
        //protected static readonly log4net.ILog m_Log =LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected log4net.ILog MLog;

        //BindingSource _bsSystem = new BindingSource(); // System object
        //BindingSource bsP = new BindingSource(); // Passengers

        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();//
        #endregion

        public FMain()
        {
            InitializeComponent();
            LoadConfiguration();
            InitQueue();
            UpdateDtuListFromDB();
            //staticcount = 0;
            InitUIDataBinding();
        }

        private void LoadConfiguration()
        {
            //Path.Combine(Directory.GetCurrentDirectory(), "logs");

            //MLog = log4net.LogManager.GetLogger(GetType().FullName);
            //var path = Directory.GetCurrentDirectory();
            //var filename = Path.Combine(path, "App.config");
            //new FileInfo(filename);

            ////log4net.Config.XmlConfigurator.Configure(file);
            ////m_Log.InfoFormat("Application Configuration Completed at {0}", DateTime.Now);
            ////m_Log.InfoFormat("The logs will be placed in '{0}'.", logPath);
            //MLog.InfoFormat("________________________________");
            //MLog.InfoFormat("Application Star logging  at {0}", DateTime.Now);

            timerStore2Db.Interval = Global.TimerStore; //记录实时状态到数据库的时间间隔

        }

        public void InitQueue()
        {
            _dqueue = new DoubleQueue();
            _dqueue.OnResponseData += DoubleQueue_OnResponseData;
            _dqueue.Run();

            Global.Attached = true;
            btn_Atach.Enabled = false;
            
        }

        //add main ui control binding
        private void InitUIDataBinding()
        {
            //add data binding
            txt_System_heat.DataBindings.Add(new Binding("Text", Global.Osystem, "SystemHeat", false, DataSourceUpdateMode.Never));
            txt_System_efficiency.DataBindings.Add(new Binding("Text", Global.Osystem, "SystemEfficiency", false, DataSourceUpdateMode.Never));
            txt_Carbon_emission.DataBindings.Add(new Binding("Text", Global.Osystem, "CarbonEmission", false, DataSourceUpdateMode.Never));
            txt_Sulfur_emission.DataBindings.Add(new Binding("Text", Global.Osystem, "SulfurEmission", false, DataSourceUpdateMode.Never));
            txt_Dust_emission.DataBindings.Add(new Binding("Text", Global.Osystem, "DustEmission", false, DataSourceUpdateMode.Never));
            txt_Solar_assurance_year.DataBindings.Add(new Binding("Text", Global.Osystem, "SolarAssuranceYear", false, DataSourceUpdateMode.Never));
            txt_Solar_assurance_day.DataBindings.Add(new Binding("Text", Global.Osystem, "SolarAssuranceDay", false, DataSourceUpdateMode.Never));
            txt_Energy_alternative.DataBindings.Add(new Binding("Text", Global.Osystem, "EnergyAlternative", false, DataSourceUpdateMode.Never));
            txt_Fee_effect.DataBindings.Add(new Binding("Text", Global.Osystem, "FeeEffect", false, DataSourceUpdateMode.Never));
        }

        private static void UpdateDtuListFromDB()
        {
            var union = MyEntityFramework.GetAllUnits();

            //遍历所有查询结果
            foreach (var unit in union)
            {
                if (!Global.ParameterList.ContainsKey(unit.UnitId))
                {
                    var parao = new ParameterObject(unit.UnitId, unit.Aera_IrradiatedSum, unit.Auxiliary_power, unit.Flow_CollectorSys, unit.Flow_HeatUsing, unit.Volumn_HeatingBox)
                                    {SystemHeat = unit.System_heat};
                    Global.ParameterList.Add(parao.Id, parao);
                }

                if (!Global.DtuList.ContainsKey(unit.UnitId))
                {
                    var dtu = new DtuObject(unit.UnitId);
                      Global.DtuList.Add(dtu.Id, dtu);
                }

                if (!Global.SatisticList.ContainsKey(unit.UnitId))
                {
                    var dtustatistic = new StatisticObject(unit.UnitId) {SystemHeat = unit.System_heat};
                    Global.SatisticList.Add(dtustatistic.Id, dtustatistic);
                }
            
            }
   
        }

        private void DoubleQueue_OnResponseData(ushort id, GprsDataRecord values)
        {
            //消息经过缓冲处理后的处理函数。（显示、存储）
            //Registers a task that will wait for a WaitHandle and will wait forever (-1 means
            //never expire) and has a state object, and should execute only once++++++++
            //DateTime now = DateTime.Now;
            //DateTimeFormatInfo format = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
            //format.DateSeparator = "-";
            //format.ShortDatePattern = @"yyyy/MM/dd";
            //string timestring = now.ToString("d", format);
            //m_Log.Info(timestring + "_" + values.Id.ToString());//LOG4 
            //m_Log.Info(values.m_userid + "---" + values.m_recv_date + "---" + values.m_data_len.ToString() + "\r\n" + global.StrToHex(values.m_data_buf, values.m_data_len) + "\r\n");//LOG4 


            ThreadPool.QueueUserWorkItem(ThreadPoolTask, values);
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++

        }

        // This thread procedure performs the task specified by the 
        // ThreadPool.QueueUserWorkItem
        public void ThreadPoolTask(object message)
        {
            var gprsrecord = (GprsDataRecord)message;
            Global.DtuList[gprsrecord.m_userid].UpdateDtuObject(gprsrecord);//更新DTUList实时状态
            ComputeStatistic();//统计统计要素
            StoreDtuState2Db();//记录状态数据
        }

        private void 数据备份ToolStripMenuItemClick(object sender, EventArgs e)
        {
            var fBackup = new FBackup();
            fBackup.ShowDialog();
        }

        private void 数据清除ToolStripMenuItemClick(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(this, Resources.FMain_数据清除ToolStripMenuItemClick_, Resources.FMain_数据清除ToolStripMenuItemClick_清除确认, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                //try
                //{
                //    da.ExecuteCommand("delete from logininfor");
                //    da.ExecuteCommand("delete from tb_unit");
                //    da.ExecuteCommand("delete from tb_region");
                //    da.ExecuteCommand("delete from tb_unit");
                //    da.ExecuteCommand("insert into logininfor (name,pass,region_name) values('" + "admin" + "','" + "admin" + "','" + "管理员" + "')");
                //    da.ExecuteCommand("insert into tb_region values('" + "管理员" + "')");
                //}
                //catch (System.Exception ex)
                //{
                //    string errorMessage = "Clear fail.\r\n\r\n" + ex.ToString();
                //    MessageBox.Show(errorMessage, "Error");
                //}
                //finally
                //{
                //    MessageBox.Show("清除完成.");
                //}
            }
        }

        private void ToolStripMenuItem2Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void 系统退出ToolStripMenuItemClick(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void 系统配置ToolStripMenuItemClick(object sender, EventArgs e)
        {
            //F_SystemSet f_systemset = new F_SystemSet();
            var fSystemconfig = new FNodeManager();
            fSystemconfig.ShowDialog();
        }

        private void 统计要素历史数据ToolStripMenuItemClick(object sender, EventArgs e)
        {
            var fHistoryView = new FHistory();
            fHistoryView.ShowDialog();
        }

        private void 帮助ToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        private void 显示要素历史数据ToolStripMenuItemClick(object sender, EventArgs e)
        {
            var fHistoryView = new FHistoryUpdate();
            fHistoryView.ShowDialog();
        }

        private void 用户添加ToolStripMenuItemClick(object sender, EventArgs e)
        {
            var fuser = new FUserManager();
            fuser.ShowDialog();
        }


        //**********************************************************
        //获得本机所有IP，并添加到IP列表框		
        //**********************************************************
        public void AddIp(ComboBox comboBox)
        {
            //string hostname = Dns.GetHostName();
            //System.Net.IPHostEntry addr = Dns.GetHostEntry(hostname);
            //IPAddress[] IpAddr = addr.AddressList;
            //for (int i = 0; i < IpAddr.Length; i++)
            //{
            //    string a = IpAddr[i].ToString();
            //    comboBox.Items.Add(IpAddr[i].ToString());
            //}
        }

        //**********************************
        //启动服务
        //**********************************
        private void 启动数据中心ToolStripMenuItemClick(object sender, EventArgs e)
        {
            Sign = 9999;
            Gprs.Gprs.SetCustomIP(Gprs.Gprs.inet_addr(ServIp));
            Gprs.Gprs.SetWorkMode(ServMode);//开发包函数，设置服务模式：2-消息，0-阻塞，1-非阻塞
            Gprs.Gprs.SelectProtocol(ServType);//开发包函数，设置服务类型：0-UDP，1-TCP
            var mess = new StringBuilder(1000);
            if (ServMode == 2)
                Sign = Gprs.Gprs.start_net_service(Handle, Gprs.Gprs.WmDtu, ServPort, mess);//开发包函数，消息模式启动服务
            else
            {
                Sign = Gprs.Gprs.start_net_service(Handle, 0, ServPort, mess);//开发包函数，非消息模式启动服务
                timer3.Interval = 100;
                timer3.Enabled = true;//启动非消息模式下数据读取和处理定时器
            }
            if (ServMode == 0)
            {
                timer3.Enabled = false;
                Threadrun = true;
                Recvdata = false;
                ThreadBlock = new Thread(Pressdata);//创建新的阻塞模式下读取数据线程
                ThreadBlock.Start(); //启动阻塞模式下读取数据线程
            }
            if (Sign == 0)
            {
                //服务启动后，主窗口设置
                timer2.Interval = RefreshTime * 1000;
                timer2.Enabled = true;
                ServStart = 1;
                启动数据中心ToolStripMenuItem.Enabled = false;
                停止服务ToolStripMenuItem.Enabled = true;
                分离DTUToolStripMenuItem.Enabled = true;
                //toolBarButton1.Enabled = false;
                //toolBarButton2.Enabled = true;
                //toolBarButton3.Enabled = true;
                //toolBarButton8.Enabled = true;
                statusBar1.Panels[1].Text = Resources.FMain_启动数据中心ToolStripMenuItemClick_服务启动;
                if (ServType == 0)
                {
                    switch (ServMode)
                    {
                        case 0:
                            AddText("UDP:阻塞模式" + "\r\n");
                            break;
                        case 1:
                            AddText("UDP:非阻塞模式" + "\r\n");
                            break;
                        case 2:
                            AddText("UDP:消息模式" + "\r\n");
                            break;
                    }
                }
                else if (ServType == 1)
                {
                    switch (ServMode)
                    {
                        case 0:
                            AddText("TCP:阻塞模式" + "\r\n");
                            break;
                        case 1:
                            AddText("TCP:非阻塞模式" + "\r\n");
                            break;
                        case 2:
                            AddText("TCP:消息模式" + "\r\n");
                            break;
                    }
                }
            }
            else
                ServStart = 0;
            AddText(mess + "\n");
        }

        //*********************************
        //停止服务
        //*********************************
        private void 停止服务ToolStripMenuItemClick(object sender, EventArgs e)
        {
            var mess = new StringBuilder(1000);
            //停止服务
            Gprs.Gprs.do_close_all_user2(mess);//开发包函数，使所有DTU下线
            timer2.Enabled = false;
            if (ServMode != 2)
                timer3.Enabled = false;
            if (ServMode == 0)
            {
                Gprs.Gprs.cancel_read_block();//阻塞模式下取消阻塞读取
                Threadrun = false;//退出线程处理函数
                ThreadBlock.Abort();//终止线程
            }
            if (Gprs.Gprs.stop_net_service(mess) == 0)//开发包函数，停止服务
            {
                //界面处理
                ServStart = 0;
                //RefreshList();
                启动数据中心ToolStripMenuItem.Enabled = true;
                停止服务ToolStripMenuItem.Enabled = false;
                分离DTUToolStripMenuItem.Enabled = false;
                //toolBarButton1.Enabled = true;
                //toolBarButton2.Enabled = false;
                //toolBarButton3.Enabled = false;
                //toolBarButton8.Enabled = false;
                statusBar1.Panels[1].Text = Resources.FMain_停止服务ToolStripMenuItemClick_服务停止;
            }
            AddText("\n" + mess);
        }

        //*****************************************
        //分离终端菜单响应函数
        //*****************************************
        private void 分离dtuToolStripMenuItemClick(object sender, EventArgs e)
        {
            //StringBuilder mess = new StringBuilder(500);
            //if (textBox2.Text.Length == 11)
            //{
            //    if (MessageBox.Show("确定使该DTU下线？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            //        if (Gprs.Gprs.do_close_one_user2(textBox2.Text, mess) == 0)//开发包函数，使某个DTU下线并发下线指令
            //        {
            //            RefreshList();//刷新终端登陆列表
            //        }
            //}
            //else
            //    MessageBox.Show("请选择DTU！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //*************************************
        //中心参数配置菜单响应函数
        //*************************************
        private void 中心参数设置ToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (ServStart == 0)
            {
                //AddIP(serviceinfo.comboBox1);
               // serviceinfo.comboBox1.SelectedIndex = 0;
                Serviceinfo.comboBox2.Text = ConnectTime.ToString(CultureInfo.InvariantCulture);
                Serviceinfo.comboBox3.Text = RefreshTime.ToString(CultureInfo.InvariantCulture);
                Serviceinfo.textBox1.Text = ServPort.ToString(CultureInfo.InvariantCulture);
                if (ServType == 0)
                    Serviceinfo.radioButton2.Checked = true;
                else if (ServType == 1)
                    Serviceinfo.radioButton1.Checked = true;
                if (ServMode == 0)
                    Serviceinfo.radioButton4.Checked = true;
                else if (ServMode == 1)
                    Serviceinfo.radioButton5.Checked = true;
                else if (ServType == 2)
                    Serviceinfo.radioButton3.Checked = true;
                if (Serviceinfo.ShowDialog() == DialogResult.OK)
                {
                    ServIp = Serviceinfo.comboBox1.Text;
                    ConnectTime = int.Parse(Serviceinfo.comboBox2.Text);
                    RefreshTime = int.Parse(Serviceinfo.comboBox3.Text);
                    ServPort = int.Parse(Serviceinfo.textBox1.Text);
                    if (Serviceinfo.radioButton2.Checked)
                        ServType = 0;
                    else if (Serviceinfo.radioButton1.Checked)
                        ServType = 1;
                    if (Serviceinfo.radioButton4.Checked)
                        ServMode = 0;
                    else if (Serviceinfo.radioButton5.Checked)
                        ServMode = 1;
                    else if (Serviceinfo.radioButton3.Checked)
                        ServMode = 2;
                }
            }
            if (ServStart == 1)
            {
                MessageBox.Show(Resources.FMain_中心参数设置ToolStripMenuItemClick_请先关闭服务_, Resources.FMain_中心参数设置ToolStripMenuItemClick_提示, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //****************************************
        //远程参数配置菜单响应函数
        //****************************************
        private void 服务参数设置ToolStripMenuItemClick(object sender, EventArgs e)
        {
            uint i;
            var userInfo = new GprsUserInfo();
            Config = new FCenterConfig();
            var iDtuAmount = Gprs.Gprs.get_max_user_amount();
            for (i = 0; i < iDtuAmount; i++)
            {
                Gprs.Gprs.get_user_at(i, ref userInfo);//开发包函数，通过DTU顺序号获得DTU信息
                if (userInfo.m_status == 1)//在线
                {
                    //向列表框添加ID号
                    Config.comboBox8.Items.Add(userInfo.m_userid);
                }
            }
            Config.ShowDialog();
        }

        //***************************************************
        //加载窗体时读取服务参数配置文件到各服务参数变量
        //***************************************************
        private void FMainLoad(object sender, EventArgs e)
        {
            //从配置文件Sysconfig.ini读取各项参数到服务参数变量
            //thread = new Thread(new ThreadStart( pressdata ));
            ServStart = 0;
            string path = Directory.GetCurrentDirectory();
            path = path + "\\Sysconfig.ini";
            var ini = new IniFile(path);
            ServIp = ini.IniReadValue("ServerConfig", "serv_ip", "127.0.0.1");
            ConnectTime = int.Parse(ini.IniReadValue("ServerConfig", "connect_time", "30"));
            RefreshTime = int.Parse(ini.IniReadValue("ServerConfig", "refresh_time", "3"));
            ServPort = int.Parse(ini.IniReadValue("ServerConfig", "serv_port", "5002"));
            ServType = int.Parse(ini.IniReadValue("ServerConfig", "serv_type", "0"));
            ServMode = int.Parse(ini.IniReadValue("ServerConfig", "serv_mode", "2"));
            
            //add data binding
            //txt_System_heat.DataBindings.Add(new Binding("Text", global.osystem, "System_heat", false, DataSourceUpdateMode.Never));
            //this.StartPosition = FormStartPosition.CenterScreen;
        }

        //********************************************
        //退出
        //********************************************
        private void FMainClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //new StringBuilder(500);
            if (MessageBox.Show(Resources.FMain_FMainClosing_, Resources.FMain_中心参数设置ToolStripMenuItemClick_提示, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                //MLog.InfoFormat("Application Stop logging  at {0}", DateTime.Now);
                //MLog.InfoFormat("________________________________");
                //如果服务正在启动，调用开发包函数使所有DTU下线并关闭服务
                if (ServStart == 1)
                {
                   停止服务ToolStripMenuItem.PerformClick(); 
                }
                //遍历所有list元素
                foreach (var item in Global.DtuList)
                {
                    using (var context = new DbTigerEntities())
                    {
                        try
                        {
                            union c = context.unions.First(i => i.UnitId == item.Key);
                            c.System_heat = Global.SatisticList[item.Key].SystemHeat;//记录上次保存数据
                            context.SaveChanges();

                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                e.Cancel = false;
            }
            else
                e.Cancel = true;
            Environment.Exit(0);
        }

        //*************************************
        //保存服务参数到配置文件Sysconfig.ini
        //*************************************
        private void FMainClosed(object sender, EventArgs e)
        {
            //保存各项参数到配置文件Sysconfig.ini
            var path = Directory.GetCurrentDirectory();
            path = path + "\\Sysconfig.ini";
            var ini = new IniFile(path);
            ini.IniWriteValue("ServerConfig", "serv_ip", ServIp);
            ini.IniWriteValue("ServerConfig", "connect_time", ConnectTime.ToString(CultureInfo.InvariantCulture));
            ini.IniWriteValue("ServerConfig", "refresh_time", RefreshTime.ToString(CultureInfo.InvariantCulture));
            ini.IniWriteValue("ServerConfig", "serv_port", ServPort.ToString(CultureInfo.InvariantCulture));
            ini.IniWriteValue("ServerConfig", "serv_type", ServType.ToString(CultureInfo.InvariantCulture));
            ini.IniWriteValue("ServerConfig", "serv_mode", ServMode.ToString(CultureInfo.InvariantCulture)); 
        }

        //**********************************
        //定时发送数据定时器
        //**********************************
        private void Timer1Tick(object sender, EventArgs e)
        {
            //StringBuilder mess = new StringBuilder(500);
            //string str;
            //string msg;
            //int len;
            //string s = new string((char)0, 1024);					//分配1024字									节长度的字符串数组
            //byte[] bc = System.Text.Encoding.Default.GetBytes(s);	//转换到字节数组
            //if ((textBox2.TextLength == 11) && textBox3.Text != "")
            //{
            //    bc = System.Text.Encoding.Default.GetBytes(textBox3.Text.ToCharArray(0, textBox3.TextLength), 0, textBox3.TextLength);
            //    if (true/*radioButton2.Checked*/)
            //    {
            //        len = HexToStr(textBox3.Text, bc);
            //        str = System.Text.Encoding.Default.GetString(bc, 0, len);	//将该字节数组转换到字符串进行传送
            //        if (len > 0)
            //            if (Gprs.Gprs.do_send_user_data(textBox2.Text, bc, len, mess) == 0)//开发包函数，发送数据到DTU
            //            {
            //                msg = "\n向 ";
            //                msg = msg + textBox2.Text + " 16进制发送数据:" + textBox3.Text + "\r\n";
            //                AddText(msg);
            //            }
            //    }
            //    //else if (Gprs.Gprs.do_send_user_data(textBox2.Text, bc, textBox3.TextLength, mess) == 0)
            //    //{
            //    //    msg = "\n向 ";
            //    //    msg = msg + textBox2.Text + " 发送数据:" + textBox3.Text + "\r\n";
            //    //    AddText(msg);
            //    //}
            //}
            //else
            //    MessageBox.Show("没有选择DTU或发送内容为空！", "发送信息错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Timer2Tick(object sender, EventArgs e)
        {

        }

        //************************************************
        //非阻塞模式下数据处理定时器
        //************************************************
        private void Timer3Tick(object sender, EventArgs e)
        {
            //int i;
            var recdPtr = new GprsDataRecord();
            var mess = new StringBuilder(100);

            //读取DTU数据		
            if (ServMode == 1)
            {
                if (Gprs.Gprs.do_read_proc(ref recdPtr, mess, true/*checkBox2.Checked*/) >= 0)
                {
                    //RefreshList();
                    switch (recdPtr.m_data_type)
                    {
                        case 0x01:	//注册包												
                            var userInfo = new GprsUserInfo();
                            //ushort usPort;
                            if (Gprs.Gprs.get_user_info(recdPtr.m_userid, ref userInfo) == 0)
                            {
                                //已经注册过	
                                AddText("\n" + recdPtr.m_userid + "---注册" + "\r\n");
                                //for (i = 0; i <	listView1.Items.Count; i++)
                                //    if (listView1.Items[i].Text == user_info.m_userid)
                                //    {		
                                //        listView1.Items[i].SubItems.Add(user_info.m_logon_date);
                                //        listView1.Items[i].SubItems.Add(user_info.m_update_time.ToString());
                                //        listView1.Items[i].SubItems.Add(Gprs.Gprs.inet_ntoa(Gprs.Gprs.ntohl(user_info.m_sin_addr)));
                                //        usPort = user_info.m_sin_port;
                                //        listView1.Items[i].SubItems.Add(usPort.ToString());
                                //        listView1.Items[i].SubItems.Add(Gprs.Gprs.inet_ntoa(Gprs.Gprs.ntohl(user_info.m_local_addr)));
                                //        usPort = user_info.m_local_port;
                                //        listView1.Items[i].SubItems.Add(usPort.ToString());										
                                //        return;
                                //    }	
                                //没有注册过
                            }
                            //RefreshList();
                            break;
                        case 0x02:	//注销包
                            AddText("\n" + recdPtr.m_userid + "---注销" + "\r\n");
                            //for (i = 0; i <	listView1.Items.Count; i++)
                            //    if (listView1.Items[i].Text == recdPtr.m_userid)
                            //    {								
                            //        listView1.Items[i].Remove();
                            //        break;
                            //    }
                            break;
                        case 0x04:	//无效包
                            break;
                        case 0x09:	//数据包
                            //if(checkBox1.Checked)
                            //    AddText("\r\n" + recdPtr.m_userid +"---"+ recdPtr.m_recv_date + "---"+recdPtr.m_data_len.ToString()+"\r\n"
                            //        +StrToHex(recdPtr.m_data_buf,recdPtr.m_data_len)+"\r\n");
                            //else
                            //    AddText("\r\n" + recdPtr.m_userid +"---"+ recdPtr.m_recv_date + "---"+recdPtr.m_data_len.ToString()+"\r\n"
                            //        +System.Text.Encoding.Default.GetString(recdPtr.m_data_buf)+"\r\n");		 							
                            break;
                        case 0x0d:
                            //AddText("\n"+recdPtr.m_userid+"---参数设置成功"+"\r\n");
                            //config.timer3.Enabled = false;++++++++++++++
                            //config.button2.Enabled = true;++++++++++++++
                            MessageBox.Show(Resources.FMain_Timer3Tick_参数设置成功, Resources.FMain_Timer3Tick_信息, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 0x0b:
                            //AddText("\n"+recdPtr.m_userid+"---参数查询成功"+"\r\n");
                            //config.readconf();++++++++++++++
                            //config.timer2.Enabled = false;++++++++++++++
                            //config.button1.Enabled = true;++++++++++++++
                            MessageBox.Show(Resources.FMain_Timer3Tick_参数查询成功, Resources.FMain_Timer3Tick_信息, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 0x06:
                            //AddText("\n"+recdPtr.m_userid+"---断开PPP连接成功"+"\r\n");
                            break;
                        case 0x07:
                            //AddText("\n"+recdPtr.m_userid+"---停止向DSC发送数据"+"\r\n");
                            break;
                        case 0x08:
                            //AddText("\n"+recdPtr.m_userid+"---允许向DSC发送数据"+"\r\n");
                            break;
                        case 0x0A:
                            //AddText("\n"+recdPtr.m_userid+"---丢弃用户数据"+"\r\n");
                            break;
                    }
                }
            }
        }

        //********************************************************
        //阻塞模式下线程处理函数
        //********************************************************
        protected void Pressdata()
        {
            //int i;
            var recdPtr = new GprsDataRecord();
            var mess = new StringBuilder(100);
            var userInfo = new GprsUserInfo();
            //ushort usPort;

            //读取DTU数据				
            while (Threadrun)
            {
                if (Recvdata)
                    continue;
                if (Threadrun == false)
                    break;
                if (Gprs.Gprs.do_read_proc(ref recdPtr, mess, true/*checkBox2.Checked*/) >= 0)
                {
                    //byte a = recdPtr.m_data_type;
                    //RefreshList();
                    Recvdata = true;
                    switch (recdPtr.m_data_type)
                    {
                        case 0x01:	//注册包												
                            if (Gprs.Gprs.get_user_info(recdPtr.m_userid, ref userInfo) == 0)
                            {
                                //已经注册过	
                                //AddText("\n"+recdPtr.m_userid + "---注册"+"\r\n");					
                                //for (i = 0; i <	listView1.Items.Count; i++)
                                //    if (listView1.Items[i].Text == user_info.m_userid)
                                //    {		
                                //        listView1.Items[i].SubItems.Add(user_info.m_logon_date);
                                //        listView1.Items[i].SubItems.Add(user_info.m_update_time.ToString());
                                //        listView1.Items[i].SubItems.Add(Gprs.Gprs.inet_ntoa(Gprs.Gprs.ntohl(user_info.m_sin_addr)));
                                //        usPort = user_info.m_sin_port;
                                //        listView1.Items[i].SubItems.Add(usPort.ToString());
                                //        listView1.Items[i].SubItems.Add(Gprs.Gprs.inet_ntoa(Gprs.Gprs.ntohl(user_info.m_local_addr)));
                                //        usPort = user_info.m_local_port;
                                //        listView1.Items[i].SubItems.Add(usPort.ToString());										
                                //    }	
                                //没有注册过
                            }
                            RefreshList();
                            break;
                        case 0x02:	//注销包
                            //AddText("\n"+recdPtr.m_userid + "---注销"+"\r\n");					
                            //for (i = 0; i <	listView1.Items.Count; i++)
                            //    if (listView1.Items[i].Text == recdPtr.m_userid)
                            //    {								
                            //        listView1.Items[i].Remove();
                            //        break;
                            //    }
                            break;
                        case 0x04:	//无效包
                            break;
                        case 0x09:	//数据包
                            //if(checkBox1.Checked)
                            //AddText("\r\n" + recdPtr.m_userid +"---"+ recdPtr.m_recv_date + "---"+recdPtr.m_data_len.ToString()+"\r\n"
                            //+StrToHex(recdPtr.m_data_buf,recdPtr.m_data_len)+"\r\n");
                            //else
                            //AddText("\r\n" + recdPtr.m_userid +"---"+ recdPtr.m_recv_date + "---"+recdPtr.m_data_len.ToString()+"\r\n"
                            //+System.Text.Encoding.Default.GetString(recdPtr.m_data_buf)+"\r\n");		

                            //AddText("\r\n" + recdPtr.m_userid + "---" + recdPtr.m_recv_date + "---" + recdPtr.m_data_len.ToString() + "\r\n"
                            //+ StrToHex(recdPtr.m_data_buf, recdPtr.m_data_len) + "\r\n");+++++++
                            break;
                        case 0x0d:
                            //AddText("\n"+recdPtr.m_userid+"---参数设置成功"+"\r\n");
                            //config.timer3.Enabled = false;+++++++
                            //config.button2.Enabled = true;+++++++
                            MessageBox.Show(Resources.FMain_Timer3Tick_参数设置成功, Resources.FMain_Timer3Tick_信息, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 0x0b:
                            //AddText("\n"+recdPtr.m_userid+"---参数查询成功"+"\r\n");
                            //config.readconf();+++++++
                            //config.timer2.Enabled = false;+++++++
                            //config.button1.Enabled = true;+++++++
                            MessageBox.Show(Resources.FMain_Timer3Tick_参数查询成功, Resources.FMain_Timer3Tick_信息, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 0x06:
                            //AddText("\n"+recdPtr.m_userid+"---断开PPP连接成功"+"\r\n");
                            break;
                        case 0x07:
                            //AddText("\n"+recdPtr.m_userid+"---停止向DSC发送数据"+"\r\n");
                            break;
                        case 0x08:
                            //AddText("\n"+recdPtr.m_userid+"---允许向DSC发送数据"+"\r\n");
                            break;
                        case 0x0A:
                            //AddText("\n"+recdPtr.m_userid+"---丢弃用户数据"+"\r\n");
                            break;
                    }
                    Recvdata = false;
                }
            }
            //	thread.Abort();
        }

        //*******************************************
        //消息模式时定义消息的响应函数
        //*******************************************
        protected override void WndProc(ref Message m)
        {
            //int i;

            //响应DTU消息
            if (m.Msg == Gprs.Gprs.WmDtu)
            {
                var recdPtr = new GprsDataRecord();
                var mess = new StringBuilder(100);

                //开发包函数，读取DTU数据				
                if (Gprs.Gprs.do_read_proc(ref recdPtr, mess, true/*checkBox2.Checked*/) >= 0)
                {
                    //RefreshList();
                    switch (recdPtr.m_data_type)
                    {
                        case 0x01:	//注册包												
                            var userInfo = new GprsUserInfo();
                            //ushort usPort;
                            if (Gprs.Gprs.get_user_info(recdPtr.m_userid, ref userInfo) == 0)//开发包函数，通过ID获取DTU信息
                            {
                                //已经注册过	
                                //AddText("\n"+recdPtr.m_userid + "---注册"+"\r\n");					
                                //	RefreshList();//刷新终端登陆列表
                            }
                            break;
                        case 0x02:	//注销包
                            //AddText("\n"+recdPtr.m_userid + "---注销"+"\r\n");	
                            //收到的是注销包，从终端登陆列表中删除该DTU信息
                            //for (i = 0; i <	listView1.Items.Count; i++)
                            //    if (listView1.Items[i].Text == recdPtr.m_userid)
                            //    {								
                            //        listView1.Items[i].Remove();
                            //        break;
                            //    }
                            break;
                        case 0x04:	//无效包
                            break;
                        case 0x09:	//数据包
                            //16进制显示收到的数据
                            //if (checkBox1.Checked)
                            {
                                //DTUObject state = new DTUObject();
                                //state.Aera_IrradiatedSum = 10;
                                //Dqueue.EnQueueItem(state);
                                //logger.Info("Receive a message from Gprs Modules!.");
                                //AddText("\r\n" + recdPtr.m_userid + "---" + recdPtr.m_recv_date + "---" + recdPtr.m_data_len.ToString() + "\r\n"
                                //    + StrToHex(recdPtr.m_data_buf, recdPtr.m_data_len) + "\r\n");
                                //    

                                _dqueue.EnQueueItem(recdPtr);
                            }
                            // else
                            //显示数据
                            //AddText("\r\n" + recdPtr.m_userid + "---" + recdPtr.m_recv_date + "---" + recdPtr.m_data_len.ToString() + "\r\n"
                            //    + System.Text.Encoding.Default.GetString(recdPtr.m_data_buf) + "\r\n");	 							
                            break;
                        case 0x0d:
                            //收到参数设置成功数据包，取消参数设置超时定时器
                            //AddText("\n"+recdPtr.m_userid+"---参数设置成功"+"\r\n");
                            //config.timer3.Enabled = false;+++++++
                            //config.button2.Enabled = true;+++++++
                            MessageBox.Show(Resources.FMain_Timer3Tick_参数设置成功, Resources.FMain_Timer3Tick_信息, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 0x0b:
                            //收到查询参数数据包，取消参数查询超时定时器，并读取各项参数到DEMO
                            //AddText("\n"+recdPtr.m_userid+"---参数查询成功"+"\r\n");
                            //config.readconf();//读取各项参数+++++++
                            //config.timer2.Enabled = false;+++++++
                            //config.button1.Enabled = true;+++++++
                            MessageBox.Show(Resources.FMain_Timer3Tick_参数查询成功, Resources.FMain_Timer3Tick_信息, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 0x06:
                            //AddText("\n"+recdPtr.m_userid+"---断开PPP连接成功"+"\r\n");
                            break;
                        case 0x07:
                            //AddText("\n"+recdPtr.m_userid+"---停止向DSC发送数据"+"\r\n");
                            break;
                        case 0x08:
                            //AddText("\n"+recdPtr.m_userid+"---允许向DSC发送数据"+"\r\n");
                            break;
                        case 0x0A:
                            //AddText("\n"+recdPtr.m_userid+"---丢弃用户数据"+"\r\n");
                            break;
                    }
                }
            }
            else
            {
                //缺省消息处理
                base.WndProc(ref m);
            }
        }

        //************************************
        //刷新终端登陆列表
        //************************************
        public void RefreshList()
        {
            //uint i, iDtuAmount;
            //string str = "";
            //long t_update, t_now;
            //StringBuilder mess = new StringBuilder(1000);
            //Gprs_USER_INFO user_info = new Gprs_USER_INFO();
            ////listView1.Items.Clear();//清空终端登陆列表
            //str = str + 0x00 + 0x00 + 0x00;
            //iDtuAmount = Gprs.Gprs.get_max_user_amount();//开发包函数，返回中心最大连接DTU数量
            //for (i = 0; i < iDtuAmount; i++)
            //{
            //    Gprs.Gprs.get_user_at(i, ref user_info);//开发包函数，通过DTU顺序号获取DTU信息
            //    if (user_info.m_status == 1)
            //    {
            //        t_update = (long)(user_info.m_update_time[0])
            //                    + (long)(user_info.m_update_time[1]) * 256
            //                    + (long)(user_info.m_update_time[2]) * 256 * 256
            //                    + (long)(user_info.m_update_time[3]) * 256 * 256 * 256
            //                    + 3600 * 8;
            //        t_now = (long)Math.Round((DateTime.Now.ToOADate() - 25569) * 3600 * 24);
            //        if ((t_now - t_update) > connect_time * 60)//判断DTU最后注册时间与现在时间的差值是否超过设置的超时时间
            //        {										//若超时则认为该DTU不在线，调用开发包函数使其下线
            //            Gprs.Gprs.do_close_one_user2(user_info.m_userid, mess);//开发包函数，使某个DTU下线并发下线指令
            //            continue;
            //        }
            //        ListInsert(user_info, t_update);
            //    }
            //}
        }

        //public void AddText(string str)
        //{
        //    //if (textBox1.Text.Length >= textBox1.MaxLength)
        //    //    textBox1.Text = "";
        //    //textBox1.AppendText(str);
        //}

        //-------------------my 
        public void AddText(string str)
        {
            //if (txt_Gprscontent.Text.Length >= textBox1.MaxLength)
            //    txt_Gprscontent.Text = "";
            //txt_Gprscontent.AppendText(str);
        }

        private void Button2Click(object sender, EventArgs e)
        {
            var fnode = new FNode();
            fnode.ShowDialog();
        }

        private void Button1Click1(object sender, EventArgs e)
        {
            //F_Node_State fnode = new F_Node_State();
            //fnode.ShowDialog();
        }

        private void TimerProduceTick(object sender, EventArgs e)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var item in Global.DtuList)
            {
                var record = new GprsDataRecord();
                _cacheLock.EnterWriteLock();
                try
                {
                    record.Initialize();
                    record.m_userid = item.Key;//消息数据包设置Gprs号码
                    DateTime now = DateTime.Now;
                    DateTimeFormatInfo format = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
                    format.DateSeparator = "/";
                    format.ShortDatePattern = @"yyyy/MM/dd/hh/mm/ss";
                    record.m_recv_date = now.ToString("d", format);//消息数据包设置上报时间
                    item.Value.RecvDate = now;//产生数据时时间

                    string allstring = "T1-" + ((ushort)_rand.Next(1, 100) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "T2-" + ((ushort)_rand.Next(1, 100) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "T3-" + ((ushort)_rand.Next(1, 100) * 0.8).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "T4-" + ((ushort)_rand.Next(1, 100) * 0.79).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "T5-" + ((ushort)_rand.Next(1, 100) * 0.99).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "T6-" + ((ushort)_rand.Next(1, 100) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "F1-" + ((ushort)_rand.Next(1000, 30000) * 6.7).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "F2-" + ((ushort)_rand.Next(2000, 7700) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "A1-" + ((ushort)_rand.Next(1, 100) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "A2-" + ((ushort)_rand.Next(1, 100) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "A3-" + ((ushort)_rand.Next(1, 100) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "P1-" + ((ushort)_rand.Next(1, 1000) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "W1-" + ((ushort)_rand.Next(1, 100) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "v1-" + ((ushort)_rand.Next(1, 10000) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    record.m_data_len = (ushort)allstring.Length;
                    //byte[] byteArray =;
                    record.m_data_buf = System.Text.Encoding.Default.GetBytes(string.Copy(allstring));
                }

                finally
                {
                    _cacheLock.ExitWriteLock();
                }
                _dqueue.EnQueueItem(record);

            }
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.

            //Format and display the TimeSpan value. 
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            //    ts.Hours, ts.Minutes, ts.Seconds,
            //    ts.Milliseconds / 10);
            //MessageBox.Show("produce time:"+elapsedTime.ToString());
            BtnBindingTestClick(null, null);
        }

        private void BtnProduceDataClick(object sender, EventArgs e)
        {
            //timerProduce.Enabled = !timerProduce.Enabled;
            TimerProduceTick(null, null);
        }

        private void BtnAtachClick(object sender, EventArgs e)
        {
            _dqueue.OnResponseData += DoubleQueue_OnResponseData;
            Global.Attached = true;
            btn_Atach.Enabled = false;
            btn_Detach.Enabled = true;
        }

        private void BtnDetachClick(object sender, EventArgs e)
        {
            _dqueue.OnResponseData -= DoubleQueue_OnResponseData;
            Global.Attached = false;
            btn_Atach.Enabled = true;
            btn_Detach.Enabled = false;
        }


        private void CheckBoxStoreCheckedChanged(object sender, EventArgs e)
        { 
            timerStore2Db.Enabled = checkBox_store.Checked;
        }

        private void CheckBoxProduceCheckedChanged(object sender, EventArgs e)
        {
            timerProduce.Enabled = checkBox_Produce.Checked;
        }

        private static void StoreDtuState2Db()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            using (var context = new DbTigerEntities())
            {
                //遍历所有list元素
                foreach (var item in Global.DtuList)
                {
                    try
                    {
                        //DateTime now = DateTime.Now;
                        //DateTimeFormatInfo format = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
                        //format.DateSeparator = "-";
                        //format.ShortDatePattern = @"yyyy/MM/dd/hh/mm/ss";

                        var unitstate = new unitstate
                        {
                            UnitId = item.Key,
                            DateTime_RecvDate = item.Value.RecvDate,//产生数据时时间
                            Temp_HeatingBox = item.Value.Field1[(ushort)(Field1No.TempHeatingBox)],
                            Temp_CollectorBox = item.Value.Field1[(ushort)(Field1No.TempCollectorBox)],
                            Temp_CollectorIn = item.Value.Field1[(ushort)(Field1No.TempCollectorIn)],
                            Temp_CollectorOut = item.Value.Field1[(ushort)(Field1No.TempCollectorOut)],
                            Temp_Ambient = item.Value.Field1[(ushort)(Field1No.TempAmbient)],
                            Humidity_Ambient = item.Value.Field1[(ushort)(Field1No.HumidityAmbient)],
                            //Flow_CollectorSys = (decimal)(item.Value.Field1[(ushort)(Field1NO.Flow_CollectorSys)]),
                            //Flow_HeatUsing = (decimal)(item.Value.Field1[(ushort)(Field1NO.Flow_HeatUsing)]),
                            Amount_Irradiated = item.Value.Field1[(ushort)(Field1No.AmountIrradiated)],
                            Amount_IrradiatedSum = item.Value.Field1[(ushort)(Field1No.AmountIrradiatedSum)],
                            //Aera_IrradiatedSum = (decimal)(item.Value.Field1[(ushort)(Field1NO.Aera_IrradiatedSum)]),
                            Speed_Wind = item.Value.Field1[(ushort)(Field1No.SpeedWind)],
                            //Volumn_HeatingBox = (decimal)(item.Value.Field1[(ushort)(Field1NO.Volumn_HeatingBox)])

                        };
                        context.unitstates.Add(unitstate);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }

                }
            }
            stopWatch.Stop();
        }

        private static void ComputeStatistic() 
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            using (var context = new DbTigerEntities())
            {
                //遍历所有list元素
                foreach (var item in Global.DtuList)
                {
                    try
                    {
                        var single = new singleunitstatistic
                        {
                            UnitId = item.Key,
                            DateTime_Statics = item.Value.RecvDate,//产生数据时时间
                            System_heat = (Global.ParameterList[item.Key].SystemHeat)+(Global.ParameterList[item.Key].DeltaTime)*(Global.DtuList[item.Key].Field1[(ushort)Field1No.FlowCollectorSys]),

                        };
                        Global.SatisticList[item.Key].SystemHeat = single.System_heat;//保存统计数据到全局状态表
                        context.singleunitstatistics.Add(single);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }

                }

                //所有DTU状态统计要素累积
                //foreach (KeyValuePair<string, DTUObject> item in global.DTUList)
                //{
                //    global.osystem.System_heat += global.SatisticList[item.Key].System_heat;
                    
                //}
                //try
                //{
                //    allunitstatistic all = new allunitstatistic
                //    {
                //        DateTime_Statics = System.DateTime.Now,//产生数据时时间
                //        System_heat = global.osystem.System_heat,

                //    };
                //    context.allunitstatistics.Add(all);
                //    context.SaveChanges();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.InnerException.ToString());
                //}

            }
            stopWatch.Stop();
        }

        private void BtnStore2DbClick(object sender, EventArgs e)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            using (var context = new DbTigerEntities())
            {
                //遍历所有list元素
                foreach (var item in Global.DtuList)
                {
                    try
                    {
                        //DateTime now = DateTime.Now;
                        //DateTimeFormatInfo format = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
                        //format.DateSeparator = "-";
                        //format.ShortDatePattern = @"yyyy/MM/dd/hh/mm/ss";

                        var unitstate = new unitstate
                        {
                            UnitId = item.Key,
                            DateTime_RecvDate = item.Value.RecvDate,//产生数据时时间
                            Temp_HeatingBox = item.Value.Field1[(ushort)(Field1No.TempHeatingBox)],
                            Temp_CollectorBox = item.Value.Field1[(ushort)(Field1No.TempCollectorBox)],
                            Temp_CollectorIn = item.Value.Field1[(ushort)(Field1No.TempCollectorIn)],
                            Temp_CollectorOut = item.Value.Field1[(ushort)(Field1No.TempCollectorOut)],
                            Temp_Ambient = item.Value.Field1[(ushort)(Field1No.TempAmbient)],
                            Humidity_Ambient = item.Value.Field1[(ushort)(Field1No.HumidityAmbient)],
                            //Flow_CollectorSys = (decimal)(item.Value.Field1[(ushort)(Field1NO.Flow_CollectorSys)]),
                            //Flow_HeatUsing = (decimal)(item.Value.Field1[(ushort)(Field1NO.Flow_HeatUsing)]),
                            Amount_Irradiated = item.Value.Field1[(ushort)(Field1No.AmountIrradiated)],
                            Amount_IrradiatedSum = item.Value.Field1[(ushort)(Field1No.AmountIrradiatedSum)],
                            //Aera_IrradiatedSum = (decimal)(item.Value.Field1[(ushort)(Field1NO.Aera_IrradiatedSum)]),
                            Speed_Wind = item.Value.Field1[(ushort)(Field1No.SpeedWind)],
                            Delta_Time = Global.ParameterList[item.Key].DeltaTime,

                        };
                        context.unitstates.Add(unitstate);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }

                }
            }
            stopWatch.Stop();
        }

        private void BtnBindingTestClick(object sender, EventArgs e)
        {
            Global.Osystem.SystemHeat = (ushort)_rand.Next(0, 100);
            Global.Osystem.SystemHeat = (ushort)_rand.Next(0, 100);//供热水箱温度
            Global.Osystem.ConventionalEnergy = (ushort)_rand.Next(0, 100);  //系统常规热源耗能量
            Global.Osystem.StorageTank = (ushort)_rand.Next(0, 100); //贮热水箱热损系数
            Global.Osystem.SystemEfficiency = (ushort)_rand.Next(0, 100);  //集热系统效率
            Global.Osystem.SolarAssuranceDay = (ushort)_rand.Next(0, 100);  //日太阳能保证率
            Global.Osystem.SolarAssuranceYear = (ushort)_rand.Next(0, 100);  //全年太阳能保证率
            Global.Osystem.EnergyAlternative = (ushort)_rand.Next(0, 100);  //常规能源替代量
            Global.Osystem.CarbonEmission = (ushort)_rand.Next(0, 100);  //二氧化碳减排量
            Global.Osystem.SulfurEmission = (ushort)_rand.Next(0, 100); //二氧化硫减排量
            Global.Osystem.DustEmission = (ushort)_rand.Next(0, 100);  //粉尘减排量
            Global.Osystem.FeeEffect = (ushort)_rand.Next(0, 100);   //项目费效比
            Global.Osystem.AuxiliaryHeat = (ushort)_rand.Next(0, 100);//辅助热源加热量
        }

        private void TimerStore2DbTick(object sender, EventArgs e)
        {
            // btnStore2Db_Click(null, null);
        }

        private void Button3Click1(object sender, EventArgs e)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            DateTime now = DateTime.Now;
            foreach (var item in Global.DtuList)
            {
                var record = new GprsDataRecord();
                _cacheLock.EnterWriteLock();
                try
                {
                    record.Initialize();
                    record.m_userid = item.Key;//消息数据包设置Gprs号码
                    
                    DateTimeFormatInfo format = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
                    format.DateSeparator = "-";
                    format.ShortDatePattern = @"yyyy/MM/dd/hh/mm/ss";
                    record.m_recv_date = now.ToString("d", format);//消息数据包设置上报时间
                    item.Value.RecvDate = now;//产生数据时时间

                    string allstring = "T1-" + ((ushort)_rand.Next(1, 100) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "T2-" + ((ushort)_rand.Next(1, 100) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "T3-" + ((ushort)_rand.Next(1, 100) * 0.8).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "T4-" + ((ushort)_rand.Next(1, 100) * 0.79).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "T5-" + ((ushort)_rand.Next(1, 100) * 0.99).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "T6-" + ((ushort)_rand.Next(1, 100) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "F1-" + ((ushort)_rand.Next(1000, 30000) * 6.7).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "F2-" + ((ushort)_rand.Next(2000, 7700) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "A1-" + ((ushort)_rand.Next(1, 100) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "A2-" + ((ushort)_rand.Next(1, 100) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "A3-" + ((ushort)_rand.Next(1, 100) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "P1-" + ((ushort)_rand.Next(1, 1000) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "W1-" + ((ushort)_rand.Next(1, 100) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    allstring += "v1-" + ((ushort)_rand.Next(1, 10000) * 0.89).ToString(CultureInfo.InvariantCulture) + " ";
                    record.m_data_len = (ushort)allstring.Length;
                    //byte[] byteArray =;
                    record.m_data_buf = System.Text.Encoding.Default.GetBytes(string.Copy(allstring));

                }

                finally
                {
                    _cacheLock.ExitWriteLock();
                }
                _dqueue.EnQueueItem(record);

                now = now.AddSeconds(1);
            }
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.

            //Format and display the TimeSpan value. 
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            //    ts.Hours, ts.Minutes, ts.Seconds,
            //    ts.Milliseconds / 10);
            //MessageBox.Show("produce time:"+elapsedTime.ToString());
            BtnBindingTestClick(null, null);
        }

       

    }
}
