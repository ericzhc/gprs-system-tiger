using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;

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

    public class DTUObject
    {
        private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        public ushort Temp_HeatingBox;//供热水箱温度
        public ushort Temp_CollectorBox;//集热水箱温度
        public ushort Temp_CollectorIn;//集热系统进口温度
        public ushort Temp_CollectorOut;//集热系统出口温度
        public ushort Temp_Ambient;//环境温度
        public ushort Humidity_Ambient; //环境湿度
        public ushort Flow_CollectorSys;//集热系统流量
        public ushort Flow_HeatUsing;//热用户端出水流量
        public ushort Amount_Irradiated;//太阳能辐照量
        public ushort Amount_IrradiatedSum;//总辐照量
        public ushort Speed_Wind;// 风速
        public ushort Aera_IrradiatedSum;// 集热器面积
        public ushort Amount_HeatingSum;// 辅助热源加热量
        public ushort SystemState;//系统状态
        public ushort ErrorState;//系统故障状态
        public DateTime RecvDate;//接收时间
        public ushort Id;//ID

        private bool online;
        public bool Online
        {
            get { return online; }
            set { online = value; }
        }

        public DTUObject() 
        {
            ;
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
        public void UpdateDTUObject(DTUObject Inobject )
        {
            cacheLock.EnterWriteLock();
            try
            {
               Id = Inobject.Id;
               Online = Inobject.Online;
               Temp_HeatingBox= Inobject.Temp_HeatingBox;//供热水箱温度
               Temp_CollectorBox= Inobject.Temp_CollectorBox;//集热水箱温度
               Temp_CollectorIn= Inobject.Temp_CollectorIn;//集热系统进口温度
               Temp_CollectorOut= Inobject.Temp_CollectorOut;//集热系统出口温度
               Temp_Ambient= Inobject.Temp_Ambient;//环境温度
               Humidity_Ambient= Inobject.Humidity_Ambient; //环境湿度
               Flow_CollectorSys= Inobject.Flow_CollectorSys;//集热系统流量
               Flow_HeatUsing= Inobject.Flow_HeatUsing;//热用户端出水流量
               Amount_Irradiated= Inobject.Amount_Irradiated;//太阳能辐照量
               Amount_IrradiatedSum= Inobject.Amount_IrradiatedSum;//总辐照量
               Speed_Wind= Inobject.Speed_Wind;// 风速
               Aera_IrradiatedSum= Inobject.Aera_IrradiatedSum;// 集热器面积
               Amount_HeatingSum= Inobject.Amount_HeatingSum;// 辅助热源加热量
               SystemState= Inobject.SystemState;//系统状态
               ErrorState= Inobject.ErrorState;//系统故障状态
               RecvDate= Inobject.RecvDate;//接收时间
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
         
        }
        public void UpdateDTUObject(GPRS_DATA_RECORD RecvMessage, ref DTUObject DTUObject)
        {
            if (DTUObject!=null)
            {
                string HexString=StrToHex(RecvMessage.m_data_buf, RecvMessage.m_data_len);
                //解析消息，构建DTUObject
                // 
                cacheLock.EnterWriteLock();
                try
                {
                    //DTUObject.Temp_HeatingBox = 1;
                    //DTUObject.Temp_CollectorBox = 2;
                }
                finally
                {
                    cacheLock.ExitWriteLock();
                }
            
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

    public class StatisticObject
    { 
        private ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        public ushort  System_heat;  //集热系统得热量
        public ushort  Conventional_energy;  //系统常规热源耗能量
        public ushort  Storage_tank; //贮热水箱热损系数
        public ushort  System_efficiency;  //集热系统效率
        public ushort  Solar_assurance_day;  //日太阳能保证率
        public ushort  Solar_assurance_year;  //全年太阳能保证率
        public ushort  Energy_alternative;  //常规能源替代量
        public ushort  Carbon_emission;  //二氧化碳减排量
        public ushort  Sulfur_emission; //二氧化硫减排量
        public ushort  Dust_emission;  //粉尘减排量
        public ushort  Fee_effect;   //项目费效比
        public ushort  Auxiliary_heat;//辅助热源加热量
        public ushort Id;//ID

        public StatisticObject() 
        {
            ;
        }

        public void UpdateStatisticObject(StatisticObject Inobject)
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

       public static SortedList<string, StatisticObject> DTUList = new SortedList<string, StatisticObject>();

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
    } 
}
