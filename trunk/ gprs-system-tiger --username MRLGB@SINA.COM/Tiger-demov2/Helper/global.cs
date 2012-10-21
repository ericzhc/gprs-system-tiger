using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Globalization;
using Tiger.Gprs;
using Tiger.Properties;

namespace Tiger.Helper
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
        public string Inipath;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary> 
        /// 构造方法 
        /// </summary> 
        /// <param name="iniPath">文件路径</param> 
        public IniFile(string iniPath)
        {
            Inipath = iniPath;
        }
        /// <summary> 
        /// 写入INI文件 
        /// </summary> 
        /// <param name="section">项目名称(如 [TypeName] )</param> 
        /// <param name="key">键</param> 
        /// <param name="value">值</param> 
        public void IniWriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, Inipath);
        }

        /// <summary> 
        /// 读出INI文件 
        /// </summary> 
        /// <param name="section">项目名称(如 [TypeName] )</param> 
        /// <param name="key">键</param>
        /// <param name="value"> </param> 
        public string IniReadValue(string section, string key, string value)
        {
            var temp = new StringBuilder(500);
            int i = GetPrivateProfileString(section, key, "", temp, 500, Inipath);
            if (i == 0)
                return value;
            return temp.ToString();
        }
        /// <summary> 
        /// 验证文件是否存在 
        /// </summary> 
        /// <returns>布尔值</returns> 
        public bool ExistINIFile()
        {
            return File.Exists(Inipath);
        }
    }

    public enum Field1No
    {
        T1=0,
        TempHeatingBox = T1,
        T2,
        TempCollectorBox = T2,
        T3,
        TempCollectorIn = T3,
        T4,
        TempCollectorOut = T4,
        T5,
        TempAmbient = T5,
        T6,
        HumidityAmbient = T6,
        F1,
        FlowCollectorSys = F1,
        F2,
        FlowHeatUsing = F2,
        A1,
        AmountIrradiated = A1,
        A2,
        AmountIrradiatedSum = A2,
        A3,
        AeraIrradiatedSum = A3,
        P1,
        AuxiliaryPower = P1,
        W1,
        SpeedWind = W1,
        V1,
        VolumnHeatingBox = V1,
        MAX
    }

    public enum Field2No
    {
        S1 = 0,
        SystemState = S1,
        E1,
        ErrorState = E1,
        MAX
    }

    public enum FieldTimeNo
    {
        RecvTime=0,
        D0 = RecvTime,
        StartTime,
        D1=StartTime,
        StopTime, 
        D2=StopTime,
        MAX
    }

    public enum UserRole
    {
        Administrator = 0,
        RegionAdmin,
        RegionReader
    }

    public class DtuObject
    {
        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
        public float[] Field1;
        public ushort[] Field2;
        public DateTime[] FieldTime;

        public string Id { get; set; }

        public bool Online { get; set; }

        public float TempHeatingBox { get; set; }

        public float TempCollectorBox { get; set; }

        public float TempCollectorIn { get; set; }

        public float TempCollectorOut { get; set; }

        public float TempAmbient { get; set; }

        public float HumidityAmbient { get; set; }

        public float FlowCollectorSys { get; set; }

        public float FlowHeatUsing { get; set; }

        public float AmountIrradiated { get; set; }

        public float AmountIrradiatedSum { get; set; }

        public float SpeedWind { get; set; }

        public float AuxiliaryPower { get; set; }

        public ushort  SystemState;//系统状态13-S1
        public ushort ErrorState;//系统故障状态14 -E1   
        //用户输入参数
        public float AeraIrradiatedSum { get; set; }

        public float VolumnHeatingBox { get; set; }

        public DateTime Starttest { get; set; }

        public DateTime Stoptest { get; set; }

        public DateTime RecvDate { get; set; }

        public DtuObject(string useid) 
        {
            Id = useid;
            Field1 = new float[(ushort)Field1No.MAX];
            Field2 = new ushort[(ushort)Field2No.MAX];
            FieldTime = new DateTime[(ushort)FieldTimeNo.MAX];
        }
        public void UpdateDtuOnline(bool on)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                Online = on;
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
           
        }

        public bool UpdateAll(float[] inputArr1 = null, ushort[] inputArr2 = null, DateTime[] inputArr3 = null)
        {
            if ((inputArr1 == null) || (inputArr2 == null) || (inputArr3 == null))
            {
                _cacheLock.EnterWriteLock();
                try
                {
                    Array.Clear(Field1, 0, Field1.Length);
                    Array.Clear(Field2, 0, Field2.Length);
                    Array.Clear(FieldTime, 0, FieldTime.Length);
                }
                finally
                {
                    _cacheLock.ExitWriteLock();
                }
                return false;
            }

            _cacheLock.EnterWriteLock();
            try
            {
                Array.Copy(inputArr1, Field1, inputArr1.Length);
                Array.Copy(inputArr2, Field2, inputArr2.Length);
                Array.Copy(inputArr3, FieldTime, inputArr3.Length);
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
            return true;

        }

        public bool UpdateField()
        {
            //not null
            AeraIrradiatedSum = Field1[(ushort)Field1No.AeraIrradiatedSum];
            AmountIrradiated = Field1[(ushort)Field1No.AmountIrradiated];
            AuxiliaryPower = Field1[(ushort)Field1No.AuxiliaryPower];
            FlowCollectorSys = Field1[(ushort)Field1No.FlowCollectorSys];
            FlowHeatUsing = Field1[(ushort)Field1No.FlowHeatUsing];
            HumidityAmbient = Field1[(ushort)Field1No.HumidityAmbient];
            SpeedWind = Field1[(ushort)Field1No.SpeedWind];
            TempAmbient = Field1[(ushort)Field1No.TempAmbient];
            TempCollectorBox = Field1[(ushort)Field1No.TempCollectorBox];
            TempCollectorIn = Field1[(ushort)Field1No.TempCollectorIn];
            TempCollectorOut = Field1[(ushort)Field1No.TempCollectorOut];
            TempHeatingBox = Field1[(ushort)Field1No.TempHeatingBox];
            VolumnHeatingBox = Field1[(ushort)Field1No.VolumnHeatingBox];
            return true;

        }

        public void UpdateDtuObject(GprsDataRecord recvMessage)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
          
            if (!recvMessage.Equals(null))
            {
                //解析消息，构建DTUObject
                // 
                    //string HexString = StrToHex(RecvMessage.m_data_buf, RecvMessage.m_data_len);
                    string hexString = Encoding.Default.GetString(recvMessage.m_data_buf);
                    foreach (Field1No s in Enum.GetValues(typeof(Field1No)))//枚举所有字段-有冗余!!!
                    {
						 if (!s.Equals(Field1No.MAX))
						 {		
                            string strpa = Global.Patternstrfloat[(ushort)s];
                            var rx = new Regex(strpa, RegexOptions.Compiled | RegexOptions.IgnoreCase);
							Match matchetemp = rx.Match(hexString);

							_cacheLock.EnterWriteLock();

							try
							{
                                float x= float.Parse(matchetemp.Groups[s.ToString()].Value);
                                var y = (ushort)s;
                                Field1[y] = x;
							}
							catch (Exception)
							{
							    //
							}
							finally
							{
								_cacheLock.ExitWriteLock();
							}
						 }                      
                    }

                    foreach (Field2No s in Enum.GetValues(typeof(Field2No)))//枚举所有字段-有冗余!!!
                    {
                        if (!s.Equals(Field2No.MAX))
                        {
                            string strpa = Global.Patternstrint[(ushort)s];
                            var rx = new Regex(strpa, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                            Match matchetemp = rx.Match(hexString);

                            _cacheLock.EnterWriteLock();
                            try
                            {
                                ushort x = ushort.Parse(matchetemp.Groups[s.ToString()].Value);
                                var y = (ushort)s;
                                Field2[y] = x;
                            }
                            catch (Exception)
                            {
                                //
                            }
                            finally
                            {
                                _cacheLock.ExitWriteLock();
                            }
                        }
                    }

                    foreach (FieldTimeNo s in Enum.GetValues(typeof(FieldTimeNo)))//枚举所有字段-有冗余!!!
                    {
                        if (!s.Equals(FieldTimeNo.MAX))
                        {
                            string strpa = Global.Patternstrdatatime[(ushort)s];
                            var rx = new Regex(strpa, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                            var matchetemp = rx.Match(hexString);

                            _cacheLock.EnterWriteLock();
                            try
                            {
                                DateTime x = DateTime.Parse(matchetemp.Groups[s.ToString()].Value);
                                var y = (ushort)s;
                                FieldTime[y] = x;
                            }
                            catch (Exception)
                            {
                                //
                            }
                            finally
                            {
                                _cacheLock.ExitWriteLock();
                            }
                        }
                    }
            }
            //赋值消息接收时间
            _cacheLock.EnterWriteLock();
            try
            {
                DateTime x = DateTime.ParseExact(recvMessage.m_recv_date, "yyyy/MM/dd/hh/mm/ss", new CultureInfo("en-US"));
                FieldTime[(ushort)FieldTimeNo.RecvTime] = x;
                RecvDate = x;
                Global.ParameterList[recvMessage.m_userid].DeltaTime = (x - Global.ParameterList[recvMessage.m_userid].LastUpdatTime).Seconds;
                Global.ParameterList[recvMessage.m_userid].LastUpdatTime = x;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.

            // Format and display the TimeSpan value. 
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            //    ts.Hours, ts.Minutes, ts.Seconds,
            //    ts.Milliseconds / 10);
            //MessageBox.Show(elapsedTime.ToString());
        }
    }

    public class ParameterObject 
    {
        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
        public string Id { get; set; }

        public float FlowCollectorSys { get; set; }

        public float FlowHeatUsing { get; set; }

        public float AuxiliaryPower { get; set; }

        public float AeraIrradiatedSum { get; set; }

        public float VolumnHeatingBox { get; set; }

        public DateTime LastUpdatTime { get; set; }

        public int DeltaTime { get; set; }

        public float SystemHeat { get; set; }

        public ParameterObject(string useid,float a3,float p1,float f1,float f2,float v1) 
        {
            Id = useid;
            FlowCollectorSys = f1;
            FlowHeatUsing = f2;
            AuxiliaryPower = p1;
            AeraIrradiatedSum = a3;
            VolumnHeatingBox = v1;
        }
        public void UpateParameterObject(string useid, float a3, float p1, float f1, float f2, float v1)
        {
            _cacheLock.EnterWriteLock();
            Id = useid;
            FlowCollectorSys = f1;
            FlowHeatUsing = f2;
            AuxiliaryPower = p1;
            AeraIrradiatedSum = a3;
            VolumnHeatingBox = v1;
            _cacheLock.ExitWriteLock();
        }
    }

    public class StatisticObject : INotifyPropertyChanged
    {
        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
        public event PropertyChangedEventHandler PropertyChanged;

        public string Id { get; set; }

        private float _systemHeat;  //集热系统得热量1
        public float SystemHeat
        {
            get { return _systemHeat; }
            set
            {
                //if (!(Math.Abs(_systemHeat - value) > EPSILON)) return;
                _systemHeat = value;
                OnPropertyChanged("System_heat");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private float _conventionalEnergy;  //系统常规热源耗能量2
        public float ConventionalEnergy
        {
            get { return _conventionalEnergy; }
            set
            {
                _conventionalEnergy = value;
                OnPropertyChanged("Conventional_energy");
            }
        }
        private float _storageTank; //贮热水箱热损系数3
        public float StorageTank
        {
            get { return _storageTank; }
            set
            {
                _storageTank = value;
                OnPropertyChanged("Storage_tank");
            }
        }
        private float _systemEfficiency;  //集热系统效率4

        public float SystemEfficiency
        {
            get { return _systemEfficiency; }
            set
            {
                _systemEfficiency = value;
                OnPropertyChanged("System_efficiency");
            }
        }
        private float _solarAssuranceDay;  //日太阳能保证率5

        public float SolarAssuranceDay
        {
            get { return _solarAssuranceDay; }
            set { _solarAssuranceDay = value; OnPropertyChanged("Solar_assurance_day"); }
        }
        private float _solarAssuranceYear;  //全年太阳能保证率6

        public float SolarAssuranceYear
        {
            get { return _solarAssuranceYear; }
            set { _solarAssuranceYear = value; OnPropertyChanged("Solar_assurance_year"); }
        }
        private float _energyAlternative;  //常规能源替代量7

        public float EnergyAlternative
        {
            get { return _energyAlternative; }
            set { _energyAlternative = value; OnPropertyChanged("Energy_alternative"); }
        }
        private float _carbonEmission; //二氧化碳减排量8

        public float CarbonEmission
        {
            get { return _carbonEmission; }
            set { _carbonEmission = value; OnPropertyChanged("Carbon_emission"); }
        }
        private float _sulfurEmission; //二氧化硫减排量9

        public float SulfurEmission
        {
            get { return _sulfurEmission; }
            set { _sulfurEmission = value; OnPropertyChanged("Sulfur_emission"); }
        }
        private float _dustEmission;  //粉尘减排量10

        public float DustEmission
        {
            get { return _dustEmission; }
            set { _dustEmission = value; OnPropertyChanged("Dust_emission"); }
        }
        private float _feeEffect; //项目费效比11

        public float FeeEffect
        {
            get { return _feeEffect; }
            set { _feeEffect = value; OnPropertyChanged("Fee_effect"); }
        }
        private float _auxiliaryHeat;//辅助热源加热量12

        public float AuxiliaryHeat
        {
            get { return _auxiliaryHeat; }
            set { _auxiliaryHeat = value; OnPropertyChanged("Auxiliary_heat"); }
        }


        public StatisticObject()
        {
        }

        public StatisticObject(string unitid)
        {
            Id=unitid;
        }

        public void UpdateSystemObject(SystemObject inobject)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }

        }
       
    }

    public class SystemObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private float _systemHeat;  //集热系统得热量1
        public float SystemHeat
        {
            get { return _systemHeat; }
            set
            {
                //if (Math.Abs(_systemHeat - value) > EPSILON)
                {
                    _systemHeat = value;
                    //OnPropertyChanged("System_heat");
                }
            }
        } 

        private void OnPropertyChanged(string propertyName)
        {

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
            //if (PropertyChanged != null)
            //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private float _conventionalEnergy;  //系统常规热源耗能量2
        public float ConventionalEnergy
        {
            get { return _conventionalEnergy; }
            set { 
                    _conventionalEnergy = value;
                    OnPropertyChanged("Conventional_energy");
                }
        }
        private float _storageTank; //贮热水箱热损系数3
        public float StorageTank
        {
            get { return _storageTank; }
            set { 
                    _storageTank = value;
                    OnPropertyChanged("Storage_tank");
                }
        }
        private float _systemEfficiency;  //集热系统效率4

        public float SystemEfficiency
        {
            get { return _systemEfficiency; }
            set { 
                    _systemEfficiency = value;
                    OnPropertyChanged("System_efficiency");
                }
        }
        private float _solarAssuranceDay;  //日太阳能保证率5

        public float SolarAssuranceDay
        {
            get { return _solarAssuranceDay; }
            set { _solarAssuranceDay = value; OnPropertyChanged("Solar_assurance_day"); }
        }
        private float _solarAssuranceYear;  //全年太阳能保证率6

        public float SolarAssuranceYear
        {
            get { return _solarAssuranceYear; }
            set { _solarAssuranceYear = value; OnPropertyChanged("Solar_assurance_year"); }
        }
        private float _energyAlternative;  //常规能源替代量7

        public float EnergyAlternative
        {
            get { return _energyAlternative; }
            set { _energyAlternative = value; OnPropertyChanged("Energy_alternative"); }
        }
        private float _carbonEmission; //二氧化碳减排量8

        public float CarbonEmission
        {
            get { return _carbonEmission; }
            set { _carbonEmission = value; OnPropertyChanged("Carbon_emission"); }
        }
        private float _sulfurEmission; //二氧化硫减排量9

        public float SulfurEmission
        {
            get { return _sulfurEmission; }
            set { _sulfurEmission = value; OnPropertyChanged("Sulfur_emission"); }
        }
        private float _dustEmission;  //粉尘减排量10

        public float DustEmission
        {
            get { return _dustEmission; }
            set { _dustEmission = value; OnPropertyChanged("Dust_emission"); }
        }
        private float _feeEffect; //项目费效比11

        public float FeeEffect
        {
            get { return _feeEffect; }
            set { _feeEffect = value; OnPropertyChanged("Fee_effect"); }
        }
        private float _auxiliaryHeat;//辅助热源加热量12

        public float AuxiliaryHeat
        {
            get { return _auxiliaryHeat; }
            set { _auxiliaryHeat = value; OnPropertyChanged("Auxiliary_heat"); }
        }

        public ushort Id { get; set; } //ID
    }

    public class ThreadIdentity
    {
        public string ThreadName;

        public ThreadIdentity(string threadName)
        {
            ThreadName = threadName;
        }
    }



    public static class Global
    {
       public static bool Attached=true;
       public static string Currentuser;
       public static ushort TimerStore = 60;
       public static ushort TimerStatistic = 60;
       public static ushort TimerSum = 60;

       public static SortedList<string, ParameterObject> ParameterList = new SortedList<string, ParameterObject>();//实时输入参数LIST
       public static SortedList<string, DtuObject> DtuList = new SortedList<string, DtuObject>();//实时状态LIST

       public static SortedList<string, StatisticObject> SatisticList = new SortedList<string, StatisticObject>();//实时统计数据LIST

       public static SystemObject Osystem = new SystemObject();//实时汇总统计对象

       public static void CheckTemp(string instr)//验证温度值
       {
           if (Convert.ToInt16(instr) < 0 || Convert.ToInt16(instr) > 100)
           {
               MessageBox.Show(Resources.Global_CheckTemp_);
           }
       }

       public static void CheckHour(string instr)//验证时间小时值
       {
           if (Convert.ToInt16(instr) < 0 || Convert.ToInt16(instr) > 23)
           {
               MessageBox.Show(Resources.Global_checkHour_);
           }
       }

       public static void CheckMinute(string instr)//验证时间分钟值
       {
           if (Convert.ToInt16(instr) < 0 || Convert.ToInt16(instr) > 59)
           {
               MessageBox.Show(Resources.Global_CheckMinute_);
           }
       }

       public static string StrToHex(byte[] str, int len)//将BYTE数组里的数据转换为16进制，参数是BYTE数组，和数组里的数据长度
       {
           string hex = "";
           for (int i = 0; i < len; i++)
           {
               int asc = str[i];
               //hex = hex + System.Convert.ToString(asc,16);
               string s = Convert.ToString(asc, 16);
               foreach (char t in s)
               {
                   if (s.Length == 1)
                       hex = hex + '0';
                   if (t == 'a')
                       hex = hex + 'A';
                   else if (t == 'b')
                       hex = hex + 'B';
                   else if (t == 'c')
                       hex = hex + 'C';
                   else if (t == 'd')
                       hex = hex + 'D';
                   else if (t == 'e')
                       hex = hex + 'E';
                   else if (t == 'f')
                       hex = hex + 'F';
                   else
                       hex = hex + t;
               }
               if (i < (len - 1))
                   hex = hex + " ";
           }
           return hex;
       }

       public static string[]  Patternstrfloat=
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

       public static string[] Patternstrint =
       {
            @"[S][1][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//S1
            @"[E][1][-]+(?<T2>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))"//E1
       };

       public static string[] Patternstrdatatime =
       {
            @"[D][0][-]+(?<T1>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//D0
            @"[D][1][-]+(?<T2>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))",//D1
            @"[D][2][-]+(?<T3>([1-9]\d*\.\d*|0\.\d*[1-9]\d*\s))"//D2
       };

       public static string[] Userrolestring =
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
            var unitDB = new DbTigerEntities();
            IList<union> units = unitDB.unions.ToList();
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
