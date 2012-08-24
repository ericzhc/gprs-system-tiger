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
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
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
        Temp_HeatingBox = T1 ,
        T2,
        Temp_CollectorBox = T2,
        T3,
        Temp_CollectorIn = T3,
        T4,
        Temp_CollectorOut=T4,
        T5,
        Temp_Ambient=T5,
        T6,
        Humidity_Ambient=T6,
        F1,
        Flow_CollectorSys=F1,
        F2,
        Flow_HeatUsing=F2,
        A1,
        Amount_Irradiated=A1,
        A2,
        Amount_IrradiatedSum=A2,   
        A3,
        Aera_IrradiatedSum=A3,
        P1,
        Auxiliary_power=P1,
        W1,
        Speed_Wind=W1,
        V1,
        Volumn_HeatingBox=V1,
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

    public class DTUObject
    {
        private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        public float[] Field1;
        public ushort[] Field2;
        public DateTime[] FieldTime;
        //public ushort Temp_HeatingBox;//供热水箱温度1-T1
        //public ushort Temp_CollectorBox;//集热水箱温度2-T2
        //public ushort Temp_CollectorIn;//集热系统进口温度3-T3
        //public ushort Temp_CollectorOut;//集热系统出口温度4-T4
        //public ushort Temp_Ambient;//环境温度-T5
        //public ushort Humidity_Ambient; //环境湿度6-T6
        //public ushort Flow_CollectorSys;//集热系统流量7-F1
        //public ushort Flow_HeatUsing;//热用户端出水流量8-F2
        //public ushort Amount_Irradiated;//太阳能辐照量9-A1
        //public ushort Amount_IrradiatedSum;//总辐照量10-A2
        //public ushort Speed_Wind;// 风速11-W1  
        //public ushort Auxiliary_power;//功率12-P1

        //public ushort SystemState;//系统状态13-S1
        //public ushort ErrorState;//系统故障状态14 -E1   
        //用户输入参数
        //public ushort Aera_IrradiatedSum;// 集热器面积1-A3
        //public ushort Volumn_HeatingBox;//贮热水箱容量（供热水箱）-V1
        //public DateTime starttest;//降温时间起始时间3
        //public DateTime stoptest;//降温时间停止时间4

        //public DateTime RecvDate;//接收时间

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

        public DTUObject() 
        {
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

        public bool  UpdateAll(float[] InputArr1 = null, ushort[] InputArr2 = null, DateTime[] InputArr3 = null)
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

        public void UpdateDTUObject(GPRS_DATA_RECORD RecvMessage)
        {
            if (!RecvMessage.Equals(null))
            {
                //解析消息，构建DTUObject
                // 
                    string HexString = StrToHex(RecvMessage.m_data_buf, RecvMessage.m_data_len);

                    foreach (Field1NO s in Enum.GetValues(typeof(Field1NO)))//枚举所有字段-有冗余!!!
                    {
						 if (!s.Equals(Field1NO.MAX))
						 {
							Regex rx = new Regex(global.patternstr[(ushort)s], RegexOptions.Compiled | RegexOptions.IgnoreCase);
							Match matchetemp = rx.Match(HexString);

							cacheLock.EnterWriteLock();
							try
							{
								Field1[(ushort)s] = float.Parse(matchetemp.Groups[s.ToString()].Value);
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

                    //foreach (Field2NO s in Enum.GetValues(typeof(Field2NO)))
                    //{
                    //    Regex rx = new Regex(global.patternstr[(ushort)s], RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    //    Match matchetemp = rx.Match(HexString);
                    //    DTUObject.Field2[(ushort)s] = ushort.Parse(matchetemp.Groups[0].Value);
                    //}

                    //foreach (FieldTimeNO s in Enum.GetValues(typeof(FieldTimeNO)))
                    //{
                    //    Regex rx = new Regex(global.patternstr[(ushort)s], RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    //    Match matchetemp = rx.Match(HexString);
                    //    DTUObject.FieldTime[(ushort)s] = DateTime.Parse(matchetemp.Groups[0].Value);
                    //}     
            }    
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
            DTUList.Add("new id1",new DTUObject());
            DTUList.Add("new id2", new DTUObject());
            DTUList.Add("new id3", new DTUObject());
              
        }

        public void UpdateSystemObject(SystemObject Inobject)
        {
            cacheLock.EnterWriteLock();
            try
            {
                Id = Inobject.Id;
                System_heat = Inobject.System_heat;//供热水箱温度
                Conventional_energy = Inobject.Conventional_energy;  //系统常规热源耗能量
                Storage_tank = Inobject.Storage_tank; //贮热水箱热损系数
                System_efficiency = Inobject.System_efficiency;  //集热系统效率
                Solar_assurance_day = Inobject.Solar_assurance_day;  //日太阳能保证率
                Solar_assurance_year = Inobject.Solar_assurance_year;  //全年太阳能保证率
                Energy_alternative = Inobject.Energy_alternative;  //常规能源替代量
                Carbon_emission = Inobject.Carbon_emission;  //二氧化碳减排量
                Sulfur_emission = Inobject.Sulfur_emission; //二氧化硫减排量
                Dust_emission = Inobject.Dust_emission;  //粉尘减排量
                Fee_effect = Inobject.Fee_effect;   //项目费效比
                Auxiliary_heat = Inobject.Auxiliary_heat;//辅助热源加热量
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }

        }

        private static SortedList<string, DTUObject> _DTUList = new SortedList<string, DTUObject>();

        public static SortedList<string, DTUObject> DTUList
        {
            get { return SystemObject._DTUList; }
            set { SystemObject._DTUList = value; }
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

       public static SortedList<string, DTUObject> DTUList = new SortedList<string, DTUObject>();

       //public static SortedList<string, SystemObject> SatisticList = new SortedList<string, SystemObject>();

       public static SystemObject osystem = new SystemObject();

       public static void checkTemp(string instr)
       {
           if (Convert.ToInt16(instr) < 0 || Convert.ToInt16(instr) > 100)
           {
               MessageBox.Show("温度值需在范围（0-100）！");
           }
       }

       public static void checkHour(string instr)
       {
           if (Convert.ToInt16(instr) < 0 || Convert.ToInt16(instr) > 23)
           {
               MessageBox.Show("时间小时值需在范围（0-23）！");
           }
       }

       public static void checkMinute(string instr)
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

       public static string[]  patternstr=
       {
            @"[T][1][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",
            @"[T][2][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",
            @"[T][3][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",
            @"[T][4][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",
            @"[T][5][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",
            @"[T][6][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",
            @"[F][1][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",
            @"[F][2][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",
			@"[A][1][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",
            @"[A][2][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",
			@"[S][1][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",
            @"[E][1][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",
			@"[P][1][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",
            @"[W][1][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))"
       };
           

    }

    public static class MyEntityFramework
    {
        public static IList<tb_union_list> GetAllUnits()
        {
            ///BlogDBEntities是继承于ObjectContext类，自动生成
            ///可以打开Blog.Desgner.cs文件找到
            ///原型： public partial class BlogDBEntities : ObjectContext
            ///可以理解为 他代表了当前数据库环境对象
            ///同时，在Blog.Desgner.cs里还可以找到两个实体BlogUser及Post
            db_tigerEntities unitDB = new db_tigerEntities();
            ///采用Linq语法读取数据
            IList<tb_union_list> units = unitDB.tb_union_list.ToList<tb_union_list>();
            return units;
        }
    }
}
