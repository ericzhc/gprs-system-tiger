﻿using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Net;
using System.Threading;
using System.Linq;
using System.Globalization;
using System.Diagnostics;
using log4net;
using log4net.Config;

namespace Tiger
{
    public partial class F_Main : Form
    {
        #region 主窗口全局变量
        DataAccess da = new DataAccess();
        public F_ServerInfo serviceinfo = new F_ServerInfo();
        public F_CenterConfig config;
        public string serv_ip;
        public int connect_time;
        public int refresh_time;
        public int serv_port;
        public int serv_type;
        public int serv_mode;      
        public int serv_start = 0;
        public int sign;
        //private static int staticcount = 0;
        //public int Timer_store = 60*1000;
        //public int Timer_Statistic = 60*1000;
        //public int Timer_Sum = 60*1000;

        public bool recvdata;
        public bool threadrun;//控制线程
        public Thread thread_block;

        private Random rand = new Random();//Produce Data test
        private DoubleQueue Dqueue;
        //protected static readonly log4net.ILog m_Log =LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected log4net.ILog m_Log;
        private string logPath;

        BindingSource bsSystem = new BindingSource(); // System object
        //BindingSource bsP = new BindingSource(); // Passengers

        private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();//
        #endregion

        public F_Main()
        {
            InitializeComponent();
            LoadConfiguration();
            InitQueue();
            UpdateDTUListFromDB();
            //staticcount = 0;
            InitUIDataBinding();
        }

        private void LoadConfiguration()
        {
            logPath = Path.Combine(Directory.GetCurrentDirectory(), "logs");

            m_Log = log4net.LogManager.GetLogger(this.GetType().FullName);
            string path = Directory.GetCurrentDirectory();
            string filename = Path.Combine(path, "App.config");
            FileInfo file = new FileInfo(filename);

            //log4net.Config.XmlConfigurator.Configure(file);
            //m_Log.InfoFormat("Application Configuration Completed at {0}", DateTime.Now);
            //m_Log.InfoFormat("The logs will be placed in '{0}'.", logPath);
            m_Log.InfoFormat("________________________________");
            m_Log.InfoFormat("Application Star logging  at {0}", DateTime.Now);

            timerStore2Db.Interval = global.Timer_store; //记录实时状态到数据库的时间间隔

        }

        public void InitQueue()
        {
            Dqueue = new DoubleQueue();
            Dqueue.OnResponseData += new DoubleQueue.ResponseData(DoubleQueue_OnResponseData);
            global.attached = true;
            btn_Atach.Enabled = false;
            Dqueue.Run();
        }

        //add main ui control binding
        private void InitUIDataBinding()
        {
            //add data binding
            txt_System_heat.DataBindings.Add(new Binding("Text", global.osystem, "System_heat", false, DataSourceUpdateMode.Never));
            txt_System_efficiency.DataBindings.Add(new Binding("Text", global.osystem, "System_efficiency", false, DataSourceUpdateMode.Never));
            txt_Carbon_emission.DataBindings.Add(new Binding("Text", global.osystem, "Carbon_emission", false, DataSourceUpdateMode.Never));
            txt_Sulfur_emission.DataBindings.Add(new Binding("Text", global.osystem, "Sulfur_emission", false, DataSourceUpdateMode.Never));
            txt_Dust_emission.DataBindings.Add(new Binding("Text", global.osystem, "Dust_emission", false, DataSourceUpdateMode.Never));
            txt_Solar_assurance_year.DataBindings.Add(new Binding("Text", global.osystem, "Solar_assurance_year", false, DataSourceUpdateMode.Never));
            txt_Solar_assurance_day.DataBindings.Add(new Binding("Text", global.osystem, "Solar_assurance_day", false, DataSourceUpdateMode.Never));
            txt_Energy_alternative.DataBindings.Add(new Binding("Text", global.osystem, "Energy_alternative", false, DataSourceUpdateMode.Never));
            txt_Fee_effect.DataBindings.Add(new Binding("Text", global.osystem, "Fee_effect", false, DataSourceUpdateMode.Never));
        }

        void UpdateDTUListFromDB()
        {
            IList<union> Union = MyEntityFramework.GetAllUnits();

            ///遍历所有查询结果
            foreach (var Unit in Union)
            {
                if (!global.ParameterList.ContainsKey(Unit.UnitId))
                {
                    ParameterObject parao = new ParameterObject(Unit.UnitId, Unit.Aera_IrradiatedSum, Unit.Auxiliary_power, Unit.Flow_CollectorSys, Unit.Flow_HeatUsing, Unit.Volumn_HeatingBox);
                    parao.System_heat = Unit.System_heat;//赋给统计初值
                    global.ParameterList.Add(parao.Id, parao);
                }

                if (!global.DTUList.ContainsKey(Unit.UnitId))
                {
                    DTUObject dtu = new DTUObject(Unit.UnitId);
                      global.DTUList.Add(dtu.Id, dtu);
                }

                if (!global.SatisticList.ContainsKey(Unit.UnitId))
                {
                    StatisticObject dtustatistic = new StatisticObject(Unit.UnitId);
                    dtustatistic.System_heat = Unit.System_heat;
                    global.SatisticList.Add(dtustatistic.Id, dtustatistic);
                }
            
            }
   
        }

        private void DoubleQueue_OnResponseData(ushort ID, GPRS_DATA_RECORD values)
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


            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadPoolTask), values);
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++

        }

        // This thread procedure performs the task specified by the 
        // ThreadPool.QueueUserWorkItem
        public void ThreadPoolTask(object message)
        {
            GPRS_DATA_RECORD gprsrecord = (GPRS_DATA_RECORD)message;
            global.DTUList[gprsrecord.m_userid].UpdateDTUObject(gprsrecord);//更新DTUList实时状态
            ComputeStatistic();
            btnStore2Db_Click(null, null);//Testing+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //Thread.Sleep(2000);


            //DateTime now = DateTime.Now;
            //DateTimeFormatInfo format = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
            //format.DateSeparator = "-";
            //format.ShortDatePattern = @"yyyy/MM/dd";
            //string timestring = now.ToString("d", format);
            //MessageBox.Show((staticcount++).ToString());

        }
        private void button2_Click(object sender, EventArgs e)
        {
            F_Node fnode = new F_Node();
            fnode.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            F_Node_State fnode = new F_Node_State();
            fnode.ShowDialog();
        }

        private void 数据备份ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_Backup f_backup = new F_Backup();
            f_backup.ShowDialog();
        }

        private void 数据清除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(this, "确认要清除数据库记录么？", "清除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void 系统退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void 系统配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //F_SystemSet f_systemset = new F_SystemSet();
            F_NodeManager f_systemconfig = new F_NodeManager();
            f_systemconfig.ShowDialog();
        }

        //**********************************************************
        //获得本机所有IP，并添加到IP列表框		
        //**********************************************************
        public void AddIP(ComboBox comboBox)
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
        private void 启动数据中心ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sign = 9999;
            GPRS.SetCustomIP(GPRS.inet_addr(serv_ip));
            GPRS.SetWorkMode(serv_mode);//开发包函数，设置服务模式：2-消息，0-阻塞，1-非阻塞
            GPRS.SelectProtocol(serv_type);//开发包函数，设置服务类型：0-UDP，1-TCP
            StringBuilder mess = new StringBuilder(1000);
            if (serv_mode == 2)
                sign = GPRS.start_net_service(this.Handle, GPRS.WM_DTU, serv_port, mess);//开发包函数，消息模式启动服务
            else
            {
                sign = GPRS.start_net_service(this.Handle, 0, serv_port, mess);//开发包函数，非消息模式启动服务
                timer3.Interval = 100;
                timer3.Enabled = true;//启动非消息模式下数据读取和处理定时器
            }
            if (serv_mode == 0)
            {
                timer3.Enabled = false;
                threadrun = true;
                recvdata = false;
                thread_block = new Thread(new ThreadStart(pressdata));//创建新的阻塞模式下读取数据线程
                thread_block.Start(); //启动阻塞模式下读取数据线程
            }
            if (sign == 0)
            {
                //服务启动后，主窗口设置
                timer2.Interval = refresh_time * 1000;
                timer2.Enabled = true;
                serv_start = 1;
                启动数据中心ToolStripMenuItem.Enabled = false;
                停止服务ToolStripMenuItem.Enabled = true;
                分离DTUToolStripMenuItem.Enabled = true;
                //toolBarButton1.Enabled = false;
                //toolBarButton2.Enabled = true;
                //toolBarButton3.Enabled = true;
                //toolBarButton8.Enabled = true;
                statusBar1.Panels[1].Text = "服务启动";
                if (serv_type == 0)
                {
                    if (serv_mode == 0) AddText("UDP:阻塞模式" + "\r\n");
                    else if (serv_mode == 1) AddText("UDP:非阻塞模式" + "\r\n");
                    else if (serv_mode == 2) AddText("UDP:消息模式" + "\r\n");
                }
                else if (serv_type == 1)
                {
                    if (serv_mode == 0) AddText("TCP:阻塞模式" + "\r\n");
                    else if (serv_mode == 1) AddText("TCP:非阻塞模式" + "\r\n");
                    else if (serv_mode == 2) AddText("TCP:消息模式" + "\r\n");
                }
            }
            else
                serv_start = 0;
            AddText(mess.ToString() + "\n");
        }

        //*********************************
        //停止服务
        //*********************************
        private void 停止服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder mess = new StringBuilder(1000);
            //停止服务
            GPRS.do_close_all_user2(mess);//开发包函数，使所有DTU下线
            timer2.Enabled = false;
            if (serv_mode != 2)
                timer3.Enabled = false;
            if (serv_mode == 0)
            {
                GPRS.cancel_read_block();//阻塞模式下取消阻塞读取
                threadrun = false;//退出线程处理函数
                thread_block.Abort();//终止线程
            }
            if (GPRS.stop_net_service(mess) == 0)//开发包函数，停止服务
            {
                //界面处理
                serv_start = 0;
                //RefreshList();
                启动数据中心ToolStripMenuItem.Enabled = true;
                停止服务ToolStripMenuItem.Enabled = false;
                分离DTUToolStripMenuItem.Enabled = false;
                //toolBarButton1.Enabled = true;
                //toolBarButton2.Enabled = false;
                //toolBarButton3.Enabled = false;
                //toolBarButton8.Enabled = false;
                statusBar1.Panels[1].Text = "服务停止";
            }
            AddText("\n" + mess.ToString());
        }

        //*****************************************
        //分离终端菜单响应函数
        //*****************************************
        private void 分离DTUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //StringBuilder mess = new StringBuilder(500);
            //if (textBox2.Text.Length == 11)
            //{
            //    if (MessageBox.Show("确定使该DTU下线？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            //        if (GPRS.do_close_one_user2(textBox2.Text, mess) == 0)//开发包函数，使某个DTU下线并发下线指令
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
        private void 中心参数设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serv_start == 0)
            {
                //AddIP(serviceinfo.comboBox1);
               // serviceinfo.comboBox1.SelectedIndex = 0;
                serviceinfo.comboBox2.Text = connect_time.ToString();
                serviceinfo.comboBox3.Text = refresh_time.ToString();
                serviceinfo.textBox1.Text = serv_port.ToString();
                if (serv_type == 0)
                    serviceinfo.radioButton2.Checked = true;
                else if (serv_type == 1)
                    serviceinfo.radioButton1.Checked = true;
                if (serv_mode == 0)
                    serviceinfo.radioButton4.Checked = true;
                else if (serv_mode == 1)
                    serviceinfo.radioButton5.Checked = true;
                else if (serv_type == 2)
                    serviceinfo.radioButton3.Checked = true;
                if (serviceinfo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    serv_ip = serviceinfo.comboBox1.Text;
                    connect_time = int.Parse(serviceinfo.comboBox2.Text);
                    refresh_time = int.Parse(serviceinfo.comboBox3.Text);
                    serv_port = int.Parse(serviceinfo.textBox1.Text);
                    if (serviceinfo.radioButton2.Checked)
                        serv_type = 0;
                    else if (serviceinfo.radioButton1.Checked)
                        serv_type = 1;
                    if (serviceinfo.radioButton4.Checked)
                        serv_mode = 0;
                    else if (serviceinfo.radioButton5.Checked)
                        serv_mode = 1;
                    else if (serviceinfo.radioButton3.Checked)
                        serv_mode = 2;
                }
            }
            if (serv_start == 1)
            {
                MessageBox.Show("请先关闭服务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //****************************************
        //远程参数配置菜单响应函数
        //****************************************
        private void 服务参数设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uint i, iDtuAmount;
            GPRS_USER_INFO user_info = new GPRS_USER_INFO();
            config = new F_CenterConfig();
            iDtuAmount = GPRS.get_max_user_amount();
            for (i = 0; i < iDtuAmount; i++)
            {
                GPRS.get_user_at(i, ref user_info);//开发包函数，通过DTU顺序号获得DTU信息
                if (user_info.m_status == 1)//在线
                {
                    //向列表框添加ID号
                    config.comboBox8.Items.Add(user_info.m_userid);
                }
            }
            config.ShowDialog();
        }

        //***************************************************
        //加载窗体时读取服务参数配置文件到各服务参数变量
        //***************************************************
        private void F_Main_Load(object sender, System.EventArgs e)
        {
            //从配置文件Sysconfig.ini读取各项参数到服务参数变量
            //thread = new Thread(new ThreadStart( pressdata ));
            serv_start = 0;
            string path = System.IO.Directory.GetCurrentDirectory();
            path = path + "\\Sysconfig.ini";
            IniFile ini = new IniFile(path);
            serv_ip = ini.IniReadValue("ServerConfig", "serv_ip", "127.0.0.1");
            connect_time = int.Parse(ini.IniReadValue("ServerConfig", "connect_time", "30"));
            refresh_time = int.Parse(ini.IniReadValue("ServerConfig", "refresh_time", "3"));
            serv_port = int.Parse(ini.IniReadValue("ServerConfig", "serv_port", "5002"));
            serv_type = int.Parse(ini.IniReadValue("ServerConfig", "serv_type", "0"));
            serv_mode = int.Parse(ini.IniReadValue("ServerConfig", "serv_mode", "2"));
            
            //add data binding
            //txt_System_heat.DataBindings.Add(new Binding("Text", global.osystem, "System_heat", false, DataSourceUpdateMode.Never));
            //this.StartPosition = FormStartPosition.CenterScreen;
        }

        //********************************************
        //退出
        //********************************************
        private void F_Main_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StringBuilder mess = new StringBuilder(500);
            if (MessageBox.Show("确定退出吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                m_Log.InfoFormat("Application Stop logging  at {0}", DateTime.Now);
                m_Log.InfoFormat("________________________________");
                //如果服务正在启动，调用开发包函数使所有DTU下线并关闭服务
                if (serv_start == 1)
                {
                   停止服务ToolStripMenuItem.PerformClick(); 
                }
                ///遍历所有list元素
                foreach (KeyValuePair<string, DTUObject> item in global.DTUList)
                {
                    using (var context = new DbTigerEntities())
                    {
                        try
                        {
                            union c = context.unions.First(i => i.UnitId == item.Key);
                            c.System_heat = global.SatisticList[item.Key].System_heat;//记录上次保存数据
                            context.SaveChanges();

                        }
                        catch (Exception ex)
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
        private void F_Main_Closed(object sender, System.EventArgs e)
        {
            //保存各项参数到配置文件Sysconfig.ini
            string path = System.IO.Directory.GetCurrentDirectory();
            path = path + "\\Sysconfig.ini";
            IniFile ini = new IniFile(path);
            ini.IniWriteValue("ServerConfig", "serv_ip", serv_ip);
            ini.IniWriteValue("ServerConfig", "connect_time", connect_time.ToString());
            ini.IniWriteValue("ServerConfig", "refresh_time", refresh_time.ToString());
            ini.IniWriteValue("ServerConfig", "serv_port", serv_port.ToString());
            ini.IniWriteValue("ServerConfig", "serv_type", serv_type.ToString());
            ini.IniWriteValue("ServerConfig", "serv_mode", serv_mode.ToString());

           
           
        }

        //
        private void timerProduce_Tick(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (KeyValuePair<string, DTUObject> item in global.DTUList)
            {
                GPRS_DATA_RECORD record = new GPRS_DATA_RECORD();
                cacheLock.EnterWriteLock();
                try
                {
                    record.Initialize();
                    record.m_userid = item.Key;//消息数据包设置GPRS号码
                    DateTime now = DateTime.Now;
                    DateTimeFormatInfo format = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
                    format.DateSeparator = "/";
                    format.ShortDatePattern = @"yyyy/MM/dd/hh/mm/ss";
                    record.m_recv_date = now.ToString("d", format);//消息数据包设置上报时间
                    item.Value.RecvDate = now;//产生数据时时间

                    string allstring = "T1-" + ((ushort)rand.Next(1, 100) * 0.89).ToString() + " ";
                    allstring += "T2-" + ((ushort)rand.Next(1, 100) * 0.89).ToString() + " ";
                    allstring += "T3-" + ((ushort)rand.Next(1, 100) * 0.8).ToString() + " ";
                    allstring += "T4-" + ((ushort)rand.Next(1, 100) * 0.79).ToString() + " ";
                    allstring += "T5-" + ((ushort)rand.Next(1, 100) * 0.99).ToString() + " ";
                    allstring += "T6-" + ((ushort)rand.Next(1, 100) * 0.89).ToString() + " ";
                    allstring += "F1-" + ((ushort)rand.Next(1000, 30000) * 6.7).ToString() + " ";
                    allstring += "F2-" + ((ushort)rand.Next(2000, 7700) * 0.89).ToString() + " ";
                    allstring += "A1-" + ((ushort)rand.Next(1, 100) * 0.89).ToString() + " ";
                    allstring += "A2-" + ((ushort)rand.Next(1, 100) * 0.89).ToString() + " ";
                    allstring += "A3-" + ((ushort)rand.Next(1, 100) * 0.89).ToString() + " ";
                    allstring += "P1-" + ((ushort)rand.Next(1, 1000) * 0.89).ToString() + " ";
                    allstring += "W1-" + ((ushort)rand.Next(1, 100) * 0.89).ToString() + " ";
                    allstring += "v1-" + ((ushort)rand.Next(1, 10000) * 0.89).ToString() + " ";
                    record.m_data_len = (ushort)allstring.Length;
                    //byte[] byteArray =;
                    record.m_data_buf = System.Text.Encoding.Default.GetBytes(string.Copy(allstring));
                }

                finally
                {
                    cacheLock.ExitWriteLock();
                }
                Dqueue.EnQueueItem(record);
            
            }
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

             //Format and display the TimeSpan value. 
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            //    ts.Hours, ts.Minutes, ts.Seconds,
            //    ts.Milliseconds / 10);
            //MessageBox.Show("produce time:"+elapsedTime.ToString());
            btnBindingTest_Click(null, null);
        }

        //**********************************
        //定时发送数据定时器
        //**********************************
        private void timer1_Tick(object sender, EventArgs e)
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
            //            if (GPRS.do_send_user_data(textBox2.Text, bc, len, mess) == 0)//开发包函数，发送数据到DTU
            //            {
            //                msg = "\n向 ";
            //                msg = msg + textBox2.Text + " 16进制发送数据:" + textBox3.Text + "\r\n";
            //                AddText(msg);
            //            }
            //    }
            //    //else if (GPRS.do_send_user_data(textBox2.Text, bc, textBox3.TextLength, mess) == 0)
            //    //{
            //    //    msg = "\n向 ";
            //    //    msg = msg + textBox2.Text + " 发送数据:" + textBox3.Text + "\r\n";
            //    //    AddText(msg);
            //    //}
            //}
            //else
            //    MessageBox.Show("没有选择DTU或发送内容为空！", "发送信息错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        //************************************************
        //非阻塞模式下数据处理定时器
        //************************************************
        private void timer3_Tick(object sender, EventArgs e)
        {
            //int i;
            GPRS_DATA_RECORD recdPtr = new GPRS_DATA_RECORD();
            StringBuilder mess = new StringBuilder(100);

            //读取DTU数据		
            if (serv_mode == 1)
            {
                if (GPRS.do_read_proc(ref recdPtr, mess, true/*checkBox2.Checked*/) >= 0)
                {
                    byte a = recdPtr.m_data_type;
                    //RefreshList();
                    switch (recdPtr.m_data_type)
                    {
                        case 0x01:	//注册包												
                            GPRS_USER_INFO user_info = new GPRS_USER_INFO();
                            //ushort usPort;
                            if (GPRS.get_user_info(recdPtr.m_userid, ref user_info) == 0)
                            {
                                //已经注册过	
                                AddText("\n" + recdPtr.m_userid + "---注册" + "\r\n");
                                //for (i = 0; i <	listView1.Items.Count; i++)
                                //    if (listView1.Items[i].Text == user_info.m_userid)
                                //    {		
                                //        listView1.Items[i].SubItems.Add(user_info.m_logon_date);
                                //        listView1.Items[i].SubItems.Add(user_info.m_update_time.ToString());
                                //        listView1.Items[i].SubItems.Add(GPRS.inet_ntoa(GPRS.ntohl(user_info.m_sin_addr)));
                                //        usPort = user_info.m_sin_port;
                                //        listView1.Items[i].SubItems.Add(usPort.ToString());
                                //        listView1.Items[i].SubItems.Add(GPRS.inet_ntoa(GPRS.ntohl(user_info.m_local_addr)));
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
                            MessageBox.Show("参数设置成功", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 0x0b:
                            //AddText("\n"+recdPtr.m_userid+"---参数查询成功"+"\r\n");
                            //config.readconf();++++++++++++++
                            //config.timer2.Enabled = false;++++++++++++++
                            //config.button1.Enabled = true;++++++++++++++
                            MessageBox.Show("参数查询成功", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        default:
                            break;
                    }
                }
            }
        }

        //********************************************************
        //阻塞模式下线程处理函数
        //********************************************************
        protected void pressdata()
        {
            //int i;
            GPRS_DATA_RECORD recdPtr = new GPRS_DATA_RECORD();
            StringBuilder mess = new StringBuilder(100);
            GPRS_USER_INFO user_info = new GPRS_USER_INFO();
            //ushort usPort;

            //读取DTU数据				
            while (threadrun)
            {
                if (recvdata)
                    continue;
                if (threadrun == false)
                    break;
                if (GPRS.do_read_proc(ref recdPtr, mess, true/*checkBox2.Checked*/) >= 0)
                {
                    //byte a = recdPtr.m_data_type;
                    //RefreshList();
                    recvdata = true;
                    switch (recdPtr.m_data_type)
                    {
                        case 0x01:	//注册包												
                            if (GPRS.get_user_info(recdPtr.m_userid, ref user_info) == 0)
                            {
                                //已经注册过	
                                //AddText("\n"+recdPtr.m_userid + "---注册"+"\r\n");					
                                //for (i = 0; i <	listView1.Items.Count; i++)
                                //    if (listView1.Items[i].Text == user_info.m_userid)
                                //    {		
                                //        listView1.Items[i].SubItems.Add(user_info.m_logon_date);
                                //        listView1.Items[i].SubItems.Add(user_info.m_update_time.ToString());
                                //        listView1.Items[i].SubItems.Add(GPRS.inet_ntoa(GPRS.ntohl(user_info.m_sin_addr)));
                                //        usPort = user_info.m_sin_port;
                                //        listView1.Items[i].SubItems.Add(usPort.ToString());
                                //        listView1.Items[i].SubItems.Add(GPRS.inet_ntoa(GPRS.ntohl(user_info.m_local_addr)));
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
                            MessageBox.Show("参数设置成功", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 0x0b:
                            //AddText("\n"+recdPtr.m_userid+"---参数查询成功"+"\r\n");
                            //config.readconf();+++++++
                            //config.timer2.Enabled = false;+++++++
                            //config.button1.Enabled = true;+++++++
                            MessageBox.Show("参数查询成功", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        default:
                            break;
                    }
                    recvdata = false;
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
            if (m.Msg == GPRS.WM_DTU)
            {
                GPRS_DATA_RECORD recdPtr = new GPRS_DATA_RECORD();
                StringBuilder mess = new StringBuilder(100);

                //开发包函数，读取DTU数据				
                if (GPRS.do_read_proc(ref recdPtr, mess, true/*checkBox2.Checked*/) >= 0)
                {
                    byte a = recdPtr.m_data_type;
                    //RefreshList();
                    switch (recdPtr.m_data_type)
                    {
                        case 0x01:	//注册包												
                            GPRS_USER_INFO user_info = new GPRS_USER_INFO();
                            //ushort usPort;
                            if (GPRS.get_user_info(recdPtr.m_userid, ref user_info) == 0)//开发包函数，通过ID获取DTU信息
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
                                //logger.Info("Receive a message from GPRS Modules!.");
                                //AddText("\r\n" + recdPtr.m_userid + "---" + recdPtr.m_recv_date + "---" + recdPtr.m_data_len.ToString() + "\r\n"
                                //    + StrToHex(recdPtr.m_data_buf, recdPtr.m_data_len) + "\r\n");
                                //    

                                Dqueue.EnQueueItem(recdPtr);
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
                            MessageBox.Show("参数设置成功", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 0x0b:
                            //收到查询参数数据包，取消参数查询超时定时器，并读取各项参数到DEMO
                            //AddText("\n"+recdPtr.m_userid+"---参数查询成功"+"\r\n");
                            //config.readconf();//读取各项参数+++++++
                            //config.timer2.Enabled = false;+++++++
                            //config.button1.Enabled = true;+++++++
                            MessageBox.Show("参数查询成功", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        default:
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

        private void btnProduceData_Click(object sender, EventArgs e)
        {
            //timerProduce.Enabled = !timerProduce.Enabled;
            timerProduce_Tick(null,null);
        }

        private void btn_Atach_Click(object sender, EventArgs e)
        {
            Dqueue.OnResponseData += new DoubleQueue.ResponseData(DoubleQueue_OnResponseData);
            global.attached = true;
            btn_Atach.Enabled = false;
            btn_Detach.Enabled = true;
        }

        private void btn_Detach_Click(object sender, EventArgs e)
        {
            Dqueue.OnResponseData -= new DoubleQueue.ResponseData(DoubleQueue_OnResponseData);
            global.attached = false;
            btn_Atach.Enabled = true;
            btn_Detach.Enabled = false;
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
            //GPRS_USER_INFO user_info = new GPRS_USER_INFO();
            ////listView1.Items.Clear();//清空终端登陆列表
            //str = str + 0x00 + 0x00 + 0x00;
            //iDtuAmount = GPRS.get_max_user_amount();//开发包函数，返回中心最大连接DTU数量
            //for (i = 0; i < iDtuAmount; i++)
            //{
            //    GPRS.get_user_at(i, ref user_info);//开发包函数，通过DTU顺序号获取DTU信息
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
            //            GPRS.do_close_one_user2(user_info.m_userid, mess);//开发包函数，使某个DTU下线并发下线指令
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
            //if (txt_gprscontent.Text.Length >= textBox1.MaxLength)
            //    txt_gprscontent.Text = "";
            //txt_gprscontent.AppendText(str);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_Log.Info("click once!.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_add_binding_Click(object sender, EventArgs e)
        {
            InitUIDataBinding();
            //this.btn_add_binding.Enabled = false;
        }

        private void 统计要素历史数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_History f_history_view = new F_History();
            f_history_view.ShowDialog();
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 显示要素历史数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_HistoryUpdate f_history_view = new F_HistoryUpdate();
            f_history_view.ShowDialog();
        }

        private void ledBulb1_Click(object sender, EventArgs e)
        {
            //((LedBulb)sender).On = !((LedBulb)sender).On;
        }

        private void checkBox_store_CheckedChanged(object sender, EventArgs e)
        { 
            timerStore2Db.Enabled = checkBox_store.Checked;
        }

        private void checkBox_Produce_CheckedChanged(object sender, EventArgs e)
        {
            timerProduce.Enabled = checkBox_Produce.Checked;
        }

        private void 用户添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_UserManager fuser = new F_UserManager();
            fuser.ShowDialog();
        }

        private void StoreDtuState2Db()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            using (var context = new DbTigerEntities())
            {
                ///遍历所有list元素
                foreach (KeyValuePair<string, DTUObject> item in global.DTUList)
                {
                    try
                    {
                        //DateTime now = DateTime.Now;
                        //DateTimeFormatInfo format = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
                        //format.DateSeparator = "-";
                        //format.ShortDatePattern = @"yyyy/MM/dd/hh/mm/ss";

                        unitstate unitstate = new unitstate
                        {
                            UnitId = item.Key,
                            DateTime_RecvDate = item.Value.RecvDate,//产生数据时时间
                            Temp_HeatingBox = (float)(item.Value.Field1[(ushort)(Field1NO.Temp_HeatingBox)]),
                            Temp_CollectorBox = (float)(item.Value.Field1[(ushort)(Field1NO.Temp_CollectorBox)]),
                            Temp_CollectorIn = (float)(item.Value.Field1[(ushort)(Field1NO.Temp_CollectorIn)]),
                            Temp_CollectorOut = (float)(item.Value.Field1[(ushort)(Field1NO.Temp_CollectorOut)]),
                            Temp_Ambient = (float)(item.Value.Field1[(ushort)(Field1NO.Temp_Ambient)]),
                            Humidity_Ambient = (float)(item.Value.Field1[(ushort)(Field1NO.Humidity_Ambient)]),
                            //Flow_CollectorSys = (decimal)(item.Value.Field1[(ushort)(Field1NO.Flow_CollectorSys)]),
                            //Flow_HeatUsing = (decimal)(item.Value.Field1[(ushort)(Field1NO.Flow_HeatUsing)]),
                            Amount_Irradiated = (float)(item.Value.Field1[(ushort)(Field1NO.Amount_Irradiated)]),
                            Amount_IrradiatedSum = (float)(item.Value.Field1[(ushort)(Field1NO.Amount_IrradiatedSum)]),
                            //Aera_IrradiatedSum = (decimal)(item.Value.Field1[(ushort)(Field1NO.Aera_IrradiatedSum)]),
                            Speed_Wind = (float)(item.Value.Field1[(ushort)(Field1NO.Speed_Wind)]),
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
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            //Format and display the TimeSpan value. 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            //MessageBox.Show("store in db time:" + elapsedTime.ToString());
        }

        private void ComputeStatistic() 
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            using (var context = new DbTigerEntities())
            {
                ///遍历所有list元素
                foreach (KeyValuePair<string, DTUObject> item in global.DTUList)
                {
                    try
                    {
                        singleunitstatistic single = new singleunitstatistic
                        {
                            UnitId = item.Key,
                            DateTime_Statics = item.Value.RecvDate,//产生数据时时间
                            System_heat = (global.ParameterList[item.Key].System_heat)+(global.ParameterList[item.Key].Delta_time)*(global.DTUList[item.Key].Field1[(ushort)Field1NO.Flow_CollectorSys]),

                        };
                        global.SatisticList[item.Key].System_heat = single.System_heat;//保存统计数据到全局状态表
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

        private void btnStore2Db_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            using (var context = new DbTigerEntities())
            {
                ///遍历所有list元素
                foreach (KeyValuePair<string, DTUObject> item in global.DTUList)
                {
                    try
                    {
                        //DateTime now = DateTime.Now;
                        //DateTimeFormatInfo format = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
                        //format.DateSeparator = "-";
                        //format.ShortDatePattern = @"yyyy/MM/dd/hh/mm/ss";

                        unitstate unitstate = new unitstate
                        {
                            UnitId = item.Key,
                            DateTime_RecvDate = item.Value.RecvDate,//产生数据时时间
                            Temp_HeatingBox = (float)(item.Value.Field1[(ushort)(Field1NO.Temp_HeatingBox)]),
                            Temp_CollectorBox = (float)(item.Value.Field1[(ushort)(Field1NO.Temp_CollectorBox)]),
                            Temp_CollectorIn = (float)(item.Value.Field1[(ushort)(Field1NO.Temp_CollectorIn)]),
                            Temp_CollectorOut = (float)(item.Value.Field1[(ushort)(Field1NO.Temp_CollectorOut)]),
                            Temp_Ambient = (float)(item.Value.Field1[(ushort)(Field1NO.Temp_Ambient)]),
                            Humidity_Ambient = (float)(item.Value.Field1[(ushort)(Field1NO.Humidity_Ambient)]),
                            //Flow_CollectorSys = (decimal)(item.Value.Field1[(ushort)(Field1NO.Flow_CollectorSys)]),
                            //Flow_HeatUsing = (decimal)(item.Value.Field1[(ushort)(Field1NO.Flow_HeatUsing)]),
                            Amount_Irradiated = (float)(item.Value.Field1[(ushort)(Field1NO.Amount_Irradiated)]),
                            Amount_IrradiatedSum = (float)(item.Value.Field1[(ushort)(Field1NO.Amount_IrradiatedSum)]),
                            //Aera_IrradiatedSum = (decimal)(item.Value.Field1[(ushort)(Field1NO.Aera_IrradiatedSum)]),
                            Speed_Wind = (float)(item.Value.Field1[(ushort)(Field1NO.Speed_Wind)]),
                            Delta_Time = global.ParameterList[item.Key].Delta_time,

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
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            //Format and display the TimeSpan value. 
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            //MessageBox.Show("store in db time:" + elapsedTime.ToString());
        }

        private void btnBindingTest_Click(object sender, EventArgs e)
        {
            global.osystem.System_heat = (ushort)rand.Next(0, 100);
            global.osystem.System_heat = (ushort)rand.Next(0, 100);//供热水箱温度
            global.osystem.Conventional_energy = (ushort)rand.Next(0, 100);  //系统常规热源耗能量
            global.osystem.Storage_tank = (ushort)rand.Next(0, 100); //贮热水箱热损系数
            global.osystem.System_efficiency = (ushort)rand.Next(0, 100);  //集热系统效率
            global.osystem.Solar_assurance_day = (ushort)rand.Next(0, 100);  //日太阳能保证率
            global.osystem.Solar_assurance_year = (ushort)rand.Next(0, 100);  //全年太阳能保证率
            global.osystem.Energy_alternative = (ushort)rand.Next(0, 100);  //常规能源替代量
            global.osystem.Carbon_emission = (ushort)rand.Next(0, 100);  //二氧化碳减排量
            global.osystem.Sulfur_emission = (ushort)rand.Next(0, 100); //二氧化硫减排量
            global.osystem.Dust_emission = (ushort)rand.Next(0, 100);  //粉尘减排量
            global.osystem.Fee_effect = (ushort)rand.Next(0, 100);   //项目费效比
            global.osystem.Auxiliary_heat = (ushort)rand.Next(0, 100);//辅助热源加热量
        }

        private void timerStore2Db_Tick(object sender, EventArgs e)
        {
            // btnStore2Db_Click(null, null);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            DateTime now;
            now = DateTime.Now;
            foreach (KeyValuePair<string, DTUObject> item in global.DTUList)
            {
                GPRS_DATA_RECORD record = new GPRS_DATA_RECORD();
                cacheLock.EnterWriteLock();
                try
                {
                    record.Initialize();
                    record.m_userid = item.Key;//消息数据包设置GPRS号码
                    
                    DateTimeFormatInfo format = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
                    format.DateSeparator = "-";
                    format.ShortDatePattern = @"yyyy/MM/dd/hh/mm/ss";
                    record.m_recv_date = now.ToString("d", format);//消息数据包设置上报时间
                    item.Value.RecvDate = now;//产生数据时时间

                    string allstring = "T1-" + ((ushort)rand.Next(1, 100) * 0.89).ToString() + " ";
                    allstring += "T2-" + ((ushort)rand.Next(1, 100) * 0.89).ToString() + " ";
                    allstring += "T3-" + ((ushort)rand.Next(1, 100) * 0.8).ToString() + " ";
                    allstring += "T4-" + ((ushort)rand.Next(1, 100) * 0.79).ToString() + " ";
                    allstring += "T5-" + ((ushort)rand.Next(1, 100) * 0.99).ToString() + " ";
                    allstring += "T6-" + ((ushort)rand.Next(1, 100) * 0.89).ToString() + " ";
                    allstring += "F1-" + ((ushort)rand.Next(1000, 30000) * 6.7).ToString() + " ";
                    allstring += "F2-" + ((ushort)rand.Next(2000, 7700) * 0.89).ToString() + " ";
                    allstring += "A1-" + ((ushort)rand.Next(1, 100) * 0.89).ToString() + " ";
                    allstring += "A2-" + ((ushort)rand.Next(1, 100) * 0.89).ToString() + " ";
                    allstring += "A3-" + ((ushort)rand.Next(1, 100) * 0.89).ToString() + " ";
                    allstring += "P1-" + ((ushort)rand.Next(1, 1000) * 0.89).ToString() + " ";
                    allstring += "W1-" + ((ushort)rand.Next(1, 100) * 0.89).ToString() + " ";
                    allstring += "v1-" + ((ushort)rand.Next(1, 10000) * 0.89).ToString() + " ";
                    record.m_data_len = (ushort)allstring.Length;
                    //byte[] byteArray =;
                    record.m_data_buf = System.Text.Encoding.Default.GetBytes(string.Copy(allstring));

                }

                finally
                {
                    cacheLock.ExitWriteLock();
                }
                Dqueue.EnQueueItem(record);

                now.AddSeconds(1);
            }
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            //Format and display the TimeSpan value. 
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            //    ts.Hours, ts.Minutes, ts.Seconds,
            //    ts.Milliseconds / 10);
            //MessageBox.Show("produce time:"+elapsedTime.ToString());
            btnBindingTest_Click(null, null);
        }

       

    }
}
