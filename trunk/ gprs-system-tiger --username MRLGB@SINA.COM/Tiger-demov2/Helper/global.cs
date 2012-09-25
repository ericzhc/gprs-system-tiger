using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Tiger
{
    /// <summary>
    /// IniFile 的摘要说明。
    /// </summary>

    //*********************************************
    //ini文件操作类，调用该类中函数操作配置文件，
    //用来保存和读取服务参数
    //*********************************************
    public class IniFile
    {
        public string inipath;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string Section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string Section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary> 
        /// 构造方法 
        /// </summary> 
        /// <param name="INIPath">文件路径</param> 
        public IniFile(string INIPath)
        {
            inipath = INIPath;
        }
        /// <summary> 
        /// 写入INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        /// <param name="Value">值</param> 
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.inipath);
        }
        /// <summary> 
        /// 读出INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        public string IniReadValue(string Section, string Key, string Value)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, this.inipath);
            if (i == 0)
                return Value;
            else
                return temp.ToString();
        }
        /// <summary> 
        /// 验证文件是否存在 
        /// </summary> 
        /// <returns>布尔值</returns> 
        public bool ExistINIFile()
        {
            return File.Exists(inipath);
        }
    }

    public enum Field1NO
    {
        T1=0,
        Temp_HeatingBox = T1,
        T2,
        Temp_CollectorBox = T2,
        T3,
        Temp_CollectorIn = T3,
        T4,
        Temp_CollectorOut = T4,
        T5,
        Temp_Ambient = T5,
        T6,
        Humidity_Ambient = T6,
        F1,
        Flow_CollectorSys = F1,
        F2,
        Flow_HeatUsing = F2,
        A1,
        Amount_Irradiated = A1,
        A2,
        Amount_IrradiatedSum = A2,
        A3,
        Aera_IrradiatedSum = A3,
        P1,
        Auxiliary_power = P1,
        W1,
        Speed_Wind = W1,
        V1,
        Volumn_HeatingBox = V1,
        MAX
    }

    public enum Field2NO
    {
        S1 = 0,
        SystemState = S1,
        E1,
        ErrorState = E1,
        MAX
    }

    public enum FieldTimeNO
    {
        Recv_Time=0,
        D0 = Recv_Time,
        Start_Time,
        D1=Start_Time,
        Stop_Time, 
        D2=Stop_Time,
        MAX
    }

    public enum UserRole
    {
        Administrator = 0,
        RegionAdmin,
        RegionReader
    }

    public class DTUObject
    {
        private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        public float[] Field1;
        public ushort[] Field2;
        public DateTime[] FieldTime;

        private string _Id;//ID
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private bool online;
        public bool Online
        {
            get { return online; }
            set { online = value; }
        }

        private float _Temp_HeatingBox;//供热水箱温度1-T1
        public float Temp_HeatingBox
        {
            get { return _Temp_HeatingBox; }
            set { _Temp_HeatingBox = value; }
        }
        private float _Temp_CollectorBox;//集热水箱温度2-T2
        public float Temp_CollectorBox
        {
            get { return _Temp_CollectorBox; }
            set { _Temp_CollectorBox = value; }
        }
        private float _Temp_CollectorIn;//集热系统进口温度3-T3
        public float Temp_CollectorIn
        {
            get { return _Temp_CollectorIn; }
            set { _Temp_CollectorIn = value; }
        }
        private float _Temp_CollectorOut;//集热系统出口温度4-T4
        public float Temp_CollectorOut
        {
            get { return _Temp_CollectorOut; }
            set { _Temp_CollectorOut = value; }
        }
        private float _Temp_Ambient;//环境温度-T5
        public float Temp_Ambient
        {
            get { return _Temp_Ambient; }
            set { _Temp_Ambient = value; }
        }
        private float _Humidity_Ambient; //环境湿度6-T6
        public float Humidity_Ambient
        {
            get { return _Humidity_Ambient; }
            set { _Humidity_Ambient = value; }
        }
        private float _Flow_CollectorSys;//集热系统流量7-F1
        public float Flow_CollectorSys
        {
            get { return _Flow_CollectorSys; }
            set { _Flow_CollectorSys = value; }
        }
        private float _Flow_HeatUsing;//热用户端出水流量8-F2
        public float Flow_HeatUsing
        {
            get { return _Flow_HeatUsing; }
            set { _Flow_HeatUsing = value; }
        }
        private float _Amount_Irradiated;//太阳能辐照量9-A1
        public float Amount_Irradiated
        {
            get { return _Amount_Irradiated; }
            set { _Amount_Irradiated = value; }
        }
        private float _Amount_IrradiatedSum;//总辐照量10-A2
        public float Amount_IrradiatedSum
        {
            get { return _Amount_IrradiatedSum; }
            set { _Amount_IrradiatedSum = value; }
        }
        private float _Speed_Wind;// 风速11-W1  
        public float Speed_Wind
        {
            get { return _Speed_Wind; }
            set { _Speed_Wind = value; }
        }
        private float _Auxiliary_power;//功率12-P1
        public float Auxiliary_power
        {
            get { return _Auxiliary_power; }
            set { _Auxiliary_power = value; }
        }

        public ushort  _SystemState;//系统状态13-S1
        public ushort _ErrorState;//系统故障状态14 -E1   
        //用户输入参数
        private float _Aera_IrradiatedSum;// 集热器面积1-A3
        public float Aera_IrradiatedSum
        {
            get { return _Aera_IrradiatedSum; }
            set { _Aera_IrradiatedSum = value; }
        }
        private float _Volumn_HeatingBox;//贮热水箱容量（供热水箱）-V1
        public float Volumn_HeatingBox
        {
            get { return _Volumn_HeatingBox; }
            set { _Volumn_HeatingBox = value; }
        }
        private DateTime _starttest;//降温时间起始时间3
        public DateTime Starttest
        {
            get { return _starttest; }
            set { _starttest = value; }
        }
        private DateTime _stoptest;//降温时间停止时间4
        public DateTime Stoptest
        {
            get { return _stoptest; }
            set { _stoptest = value; }
        }

        private DateTime _RecvDate;//接收时间
        public DateTime RecvDate
        {
            get { return _RecvDate; }
            set { _RecvDate = value; }
        }

        public DTUObject(string useid) 
        {
            Id = useid;
            Field1 = new float[(ushort)Field1NO.MAX];
            Field2 = new ushort[(ushort)Field2NO.MAX];
            FieldTime = new DateTime[(ushort)FieldTimeNO.MAX];
        }
        public void UpdateDTUOnline(bool on)
        {
            cacheLock.EnterWriteLock();
            try
            {
                Online = on;
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
           
        }

        public bool UpdateAll(float[] InputArr1 = null, ushort[] InputArr2 = null, DateTime[] InputArr3 = null)
        {
            if ((InputArr1 == null) || (InputArr2 == null) || (InputArr3 == null))
            {
                cacheLock.EnterWriteLock();
                try
                {
                    Array.Clear(Field1, 0, Field1.Length);
                    Array.Clear(Field2, 0, Field2.Length);
                    Array.Clear(FieldTime, 0, FieldTime.Length);
                }
                finally
                {
                    cacheLock.ExitWriteLock();
                }
                return false;
            }

            cacheLock.EnterWriteLock();
            try
            {
                Array.Copy(InputArr1, Field1, InputArr1.Length);
                Array.Copy(InputArr2, Field2, InputArr2.Length);
                Array.Copy(InputArr3, FieldTime, InputArr3.Length);
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
            return true;

        }

        public bool UpdateField()
        {
            //not null
            Aera_IrradiatedSum = Field1[(ushort)Field1NO.Aera_IrradiatedSum];
            Amount_Irradiated = Field1[(ushort)Field1NO.Amount_Irradiated];
            Auxiliary_power = Field1[(ushort)Field1NO.Auxiliary_power];
            Flow_CollectorSys = Field1[(ushort)Field1NO.Flow_CollectorSys];
            Flow_HeatUsing = Field1[(ushort)Field1NO.Flow_HeatUsing];
            Humidity_Ambient = Field1[(ushort)Field1NO.Humidity_Ambient];
            Speed_Wind = Field1[(ushort)Field1NO.Speed_Wind];
            Temp_Ambient = Field1[(ushort)Field1NO.Temp_Ambient];
            Temp_CollectorBox = Field1[(ushort)Field1NO.Temp_CollectorBox];
            Temp_CollectorIn = Field1[(ushort)Field1NO.Temp_CollectorIn];
            Temp_CollectorOut = Field1[(ushort)Field1NO.Temp_CollectorOut];
            Temp_HeatingBox = Field1[(ushort)Field1NO.Temp_HeatingBox];
            Volumn_HeatingBox = Field1[(ushort)Field1NO.Volumn_HeatingBox];
            return true;

        }

        public void UpdateDTUObject(GPRS_DATA_RECORD RecvMessage)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
          
            if (!RecvMessage.Equals(null))
            {
                //解析消息，构建DTUObject
                // 
                    //string HexString = StrToHex(RecvMessage.m_data_buf, RecvMessage.m_data_len);
                    string HexString = System.Text.Encoding.Default.GetString(RecvMessage.m_data_buf);
                    foreach (Field1NO s in Enum.GetValues(typeof(Field1NO)))//枚举所有字段-有冗余!!!
                    {
						 if (!s.Equals(Field1NO.MAX))
						 {		
                            string strpa = global.patternstrfloat[(ushort)s];
                            Regex rx = new Regex(strpa, RegexOptions.Compiled | RegexOptions.IgnoreCase);
							Match matchetemp = rx.Match(HexString);

							cacheLock.EnterWriteLock();
							try
							{
                                float x= float.Parse(matchetemp.Groups[s.ToString()].Value);
                                ushort y = (ushort)s;
                                Field1[y] = x;
							}
							catch 
							{
                            //
							}
							finally
							{
								cacheLock.ExitWriteLock();
							}
						 }                      
                    }

                    foreach (Field2NO s in Enum.GetValues(typeof(Field2NO)))//枚举所有字段-有冗余!!!
                    {
                        if (!s.Equals(Field2NO.MAX))
                        {
                            string strpa = global.patternstrint[(ushort)s];
                            Regex rx = new Regex(strpa, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                            Match matchetemp = rx.Match(HexString);

                            cacheLock.EnterWriteLock();
                            try
                            {
                                ushort x = ushort.Parse(matchetemp.Groups[s.ToString()].Value);
                                ushort y = (ushort)s;
                                Field2[y] = x;
                            }
                            catch
                            {
                                //
                            }
                            finally
                            {
                                cacheLock.ExitWriteLock();
                            }
                        }
                    }

                    foreach (FieldTimeNO s in Enum.GetValues(typeof(FieldTimeNO)))//枚举所有字段-有冗余!!!
                    {
                        if (!s.Equals(FieldTimeNO.MAX))
                        {
                            string strpa = global.patternstrdatatime[(ushort)s];
                            Regex rx = new Regex(strpa, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                            Match matchetemp = rx.Match(HexString);

                            cacheLock.EnterWriteLock();
                            try
                            {
                                DateTime x = DateTime.Parse(matchetemp.Groups[s.ToString()].Value);
                                ushort y = (ushort)s;
                                FieldTime[y] = x;
                            }
                            catch
                            {
                                //
                            }
                            finally
                            {
                                cacheLock.ExitWriteLock();
                            }
                        }
                    }
            }
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value. 
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            //    ts.Hours, ts.Minutes, ts.Seconds,
            //    ts.Milliseconds / 10);
            //MessageBox.Show(elapsedTime.ToString());
        }

        private string StrToHex(byte[] str, int len)//将BYTE数组里的数据转换为16进制，参数是BYTE数组，和数组里的数据长度
        {
            string hex = "";
            string s;
            int asc;
            for (int i = 0; i < len; i++)
            {
                s = "";
                asc = str[i];
                //hex = hex + System.Convert.ToString(asc,16);
                s = System.Convert.ToString(asc, 16);
                for (int j = 0; j < s.Length; j++)
                {
                    if (s.Length == 1)
                        hex = hex + '0';
                    if (s[j] == 'a')
                        hex = hex + 'A';
                    else if (s[j] == 'b')
                        hex = hex + 'B';
                    else if (s[j] == 'c')
                        hex = hex + 'C';
                    else if (s[j] == 'd')
                        hex = hex + 'D';
                    else if (s[j] == 'e')
                        hex = hex + 'E';
                    else if (s[j] == 'f')
                        hex = hex + 'F';
                    else
                        hex = hex + s[j];
                }
                if (i < (len - 1))
                    hex = hex + " ";
            }
            return hex;
        }
    }

    public class DTUStatisticObject : INotifyPropertyChanged
    {
        private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        public event PropertyChangedEventHandler PropertyChanged;

        private string _Id;//ID
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private ushort _System_heat;  //集热系统得热量1
        public ushort System_heat
        {
            get { return _System_heat; }
            set
            {
                if (_System_heat != value)
                {
                    _System_heat = value;
                    OnPropertyChanged("System_heat");
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private ushort _Conventional_energy;  //系统常规热源耗能量2
        public ushort Conventional_energy
        {
            get { return _Conventional_energy; }
            set
            {
                _Conventional_energy = value;
                OnPropertyChanged("Conventional_energy");
            }
        }
        private ushort _Storage_tank; //贮热水箱热损系数3
        public ushort Storage_tank
        {
            get { return _Storage_tank; }
            set
            {
                _Storage_tank = value;
                OnPropertyChanged("Storage_tank");
            }
        }
        private ushort _System_efficiency;  //集热系统效率4

        public ushort System_efficiency
        {
            get { return _System_efficiency; }
            set
            {
                _System_efficiency = value;
                OnPropertyChanged("System_efficiency");
            }
        }
        private ushort _Solar_assurance_day;  //日太阳能保证率5

        public ushort Solar_assurance_day
        {
            get { return _Solar_assurance_day; }
            set { _Solar_assurance_day = value; OnPropertyChanged("Solar_assurance_day"); }
        }
        private ushort _Solar_assurance_year;  //全年太阳能保证率6

        public ushort Solar_assurance_year
        {
            get { return _Solar_assurance_year; }
            set { _Solar_assurance_year = value; OnPropertyChanged("Solar_assurance_year"); }
        }
        private ushort _Energy_alternative;  //常规能源替代量7

        public ushort Energy_alternative
        {
            get { return _Energy_alternative; }
            set { _Energy_alternative = value; OnPropertyChanged("Energy_alternative"); }
        }
        private ushort _Carbon_emission; //二氧化碳减排量8

        public ushort Carbon_emission
        {
            get { return _Carbon_emission; }
            set { _Carbon_emission = value; OnPropertyChanged("Carbon_emission"); }
        }
        private ushort _Sulfur_emission; //二氧化硫减排量9

        public ushort Sulfur_emission
        {
            get { return _Sulfur_emission; }
            set { _Sulfur_emission = value; OnPropertyChanged("Sulfur_emission"); }
        }
        private ushort _Dust_emission;  //粉尘减排量10

        public ushort Dust_emission
        {
            get { return _Dust_emission; }
            set { _Dust_emission = value; OnPropertyChanged("Dust_emission"); }
        }
        private ushort _Fee_effect; //项目费效比11

        public ushort Fee_effect
        {
            get { return _Fee_effect; }
            set { _Fee_effect = value; OnPropertyChanged("Fee_effect"); }
        }
        private ushort _Auxiliary_heat;//辅助热源加热量12

        public ushort Auxiliary_heat
        {
            get { return _Auxiliary_heat; }
            set { _Auxiliary_heat = value; OnPropertyChanged("Auxiliary_heat"); }
        }


        public DTUStatisticObject()
        {
            ;
        }

        public DTUStatisticObject(string unitid)
        {
            Id=unitid;
        }

        public void UpdateSystemObject(SystemObject Inobject)
        {
            cacheLock.EnterWriteLock();
            try
            {
                
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }

        }
       
    }

    public class SystemObject : INotifyPropertyChanged
    { 
        private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        public event PropertyChangedEventHandler PropertyChanged;

        private ushort _System_heat;  //集热系统得热量1
        public ushort System_heat
        {
            get { return _System_heat; }
            set
            {
                if (_System_heat != value)
                {
                    _System_heat = value;
                    OnPropertyChanged("System_heat");
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private ushort _Conventional_energy;  //系统常规热源耗能量2
        public ushort Conventional_energy
        {
            get { return _Conventional_energy; }
            set { 
                    _Conventional_energy = value;
                    OnPropertyChanged("Conventional_energy");
                }
        }
        private ushort _Storage_tank; //贮热水箱热损系数3
        public ushort Storage_tank
        {
            get { return _Storage_tank; }
            set { 
                    _Storage_tank = value;
                    OnPropertyChanged("Storage_tank");
                }
        }
        private ushort _System_efficiency;  //集热系统效率4

        public ushort System_efficiency
        {
            get { return _System_efficiency; }
            set { 
                    _System_efficiency = value;
                    OnPropertyChanged("System_efficiency");
                }
        }
        private ushort _Solar_assurance_day;  //日太阳能保证率5

        public ushort Solar_assurance_day
        {
            get { return _Solar_assurance_day; }
            set { _Solar_assurance_day = value; OnPropertyChanged("Solar_assurance_day"); }
        }
        private ushort _Solar_assurance_year;  //全年太阳能保证率6

        public ushort Solar_assurance_year
        {
            get { return _Solar_assurance_year; }
            set { _Solar_assurance_year = value; OnPropertyChanged("Solar_assurance_year"); }
        }
        private ushort _Energy_alternative;  //常规能源替代量7

        public ushort Energy_alternative
        {
            get { return _Energy_alternative; }
            set { _Energy_alternative = value; OnPropertyChanged("Energy_alternative"); }
        }
        private ushort _Carbon_emission; //二氧化碳减排量8

        public ushort Carbon_emission
        {
            get { return _Carbon_emission; }
            set { _Carbon_emission = value; OnPropertyChanged("Carbon_emission"); }
        }
        private ushort _Sulfur_emission; //二氧化硫减排量9

        public ushort Sulfur_emission
        {
            get { return _Sulfur_emission; }
            set { _Sulfur_emission = value; OnPropertyChanged("Sulfur_emission"); }
        }
        private ushort _Dust_emission;  //粉尘减排量10

        public ushort Dust_emission
        {
            get { return _Dust_emission; }
            set { _Dust_emission = value; OnPropertyChanged("Dust_emission"); }
        }
        private ushort _Fee_effect; //项目费效比11

        public ushort Fee_effect
        {
            get { return _Fee_effect; }
            set { _Fee_effect = value; OnPropertyChanged("Fee_effect"); }
        }
        private ushort _Auxiliary_heat;//辅助热源加热量12

        public ushort Auxiliary_heat
        {
            get { return _Auxiliary_heat; }
            set { _Auxiliary_heat = value; OnPropertyChanged("Auxiliary_heat"); }
        }

        public ushort Id { get; set; } //ID

        public SystemObject() 
        {
            
        }

    }

    public class ThreadIdentity
    {
        public string threadName;

        public ThreadIdentity(string threadName)
        {
            this.threadName = threadName;
        }
    }

    public static class global
    {
       public static bool attached=true;
       public static string currentuser;
       public static ushort Timer_store = 60;
       public static ushort Timer_Statistic = 60;
       public static ushort Timer_Sum = 60;

       public static SortedList<string, DTUObject> DTUList = new SortedList<string, DTUObject>();//实时状态LIST

       public static SortedList<string, DTUStatisticObject> SatisticList = new SortedList<string, DTUStatisticObject>();//实时统计数据LIST

       public static SystemObject osystem = new SystemObject();//实时汇总统计对象

       public static void checkTemp(string instr)//验证温度值
       {
           if (Convert.ToInt16(instr) < 0 || Convert.ToInt16(instr) > 100)
           {
               MessageBox.Show("温度值需在范围（0-100）！");
           }
       }

       public static void checkHour(string instr)//验证时间小时值
       {
           if (Convert.ToInt16(instr) < 0 || Convert.ToInt16(instr) > 23)
           {
               MessageBox.Show("时间小时值需在范围（0-23）！");
           }
       }

       public static void checkMinute(string instr)//验证时间分钟值
       {
           if (Convert.ToInt16(instr) < 0 || Convert.ToInt16(instr) > 59)
           {
               MessageBox.Show("时间分钟值需在范围（0-59）！");
           }
       }

       public static string StrToHex(byte[] str, int len)//将BYTE数组里的数据转换为16进制，参数是BYTE数组，和数组里的数据长度
       {
           string hex = "";
           string s;
           int asc;
           for (int i = 0; i < len; i++)
           {
               s = "";
               asc = str[i];
               //hex = hex + System.Convert.ToString(asc,16);
               s = System.Convert.ToString(asc, 16);
               for (int j = 0; j < s.Length; j++)
               {
                   if (s.Length == 1)
                       hex = hex + '0';
                   if (s[j] == 'a')
                       hex = hex + 'A';
                   else if (s[j] == 'b')
                       hex = hex + 'B';
                   else if (s[j] == 'c')
                       hex = hex + 'C';
                   else if (s[j] == 'd')
                       hex = hex + 'D';
                   else if (s[j] == 'e')
                       hex = hex + 'E';
                   else if (s[j] == 'f')
                       hex = hex + 'F';
                   else
                       hex = hex + s[j];
               }
               if (i < (len - 1))
                   hex = hex + " ";
           }
           return hex;
       }

       public static string[]  patternstrfloat=
       {
            @"[T][1][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//T1
            @"[T][2][-]+(?<T2>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//T2
            @"[T][3][-]+(?<T3>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//T3
            @"[T][4][-]+(?<T4>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//T4
            @"[T][5][-]+(?<T5>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//T5
            @"[T][6][-]+(?<T6>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//T6
            @"[F][1][-]+(?<F1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//F1
            @"[F][2][-]+(?<F1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//F2
			@"[A][1][-]+(?<A1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//A1
            @"[A][2][-]+(?<A2>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//A2
            @"[A][3][-]+(?<A3>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//A3		
			@"[P][1][-]+(?<P1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//P1
            @"[W][1][-]+(?<W1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//W1
            @"[V][1][-]+(?<V1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))"//V1
       };

       public static string[] patternstrint =
       {
            @"[S][1][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//S1
            @"[E][1][-]+(?<T2>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))"//E1
       };

       public static string[] patternstrdatatime =
       {
            @"[D][0][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//D0
            @"[D][1][-]+(?<T2>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//D1
            @"[D][2][-]+(?<T3>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))"//D2
       };

       public static string[] userrolestring =
       {
            "超级管理员",//D0
            "区域管理员",//D1
            "区域查看员"//D2
       };
           

    }

    public static class MyEntityFramework
    {
        public static IList<union> GetAllUnits()
        {
            ///BlogDBEntities是继承于ObjectContext类，自动生成
            ///可以打开Blog.Desgner.cs文件找到
            ///原型： public partial class BlogDBEntities : ObjectContext
            ///可以理解为 他代表了当前数据库环境对象
            ///同时，在Blog.Desgner.cs里还可以找到两个实体BlogUser及Post
            DbTigerEntities unitDB = new DbTigerEntities();
            ///采用Linq语法读取数据
            IList<union> units = unitDB.unions.ToList<union>();
            return units;
        }

        //public static IList<tb_unit_state> GetAllUnitStates()
        //{
            /////BlogDBEntities是继承于ObjectContext类，自动生成
            /////可以打开Blog.Desgner.cs文件找到
            /////原型： public partial class BlogDBEntities : ObjectContext
            /////可以理解为 他代表了当前数据库环境对象
            /////同时，在Blog.Desgner.cs里还可以找到两个实体BlogUser及Post
            //db_tigerEntities unitDB = new db_tigerEntities();
            /////采用Linq语法读取数据
            //IList<tb_unit_state> unitstates = unitDB.union.ToList<tb_unit_state>();
            //return unitstates;
        //}
    }
}
