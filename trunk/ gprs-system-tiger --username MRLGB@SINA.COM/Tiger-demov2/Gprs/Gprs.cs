using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Tiger.Gprs
{
    //结构定义
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct GprsDataRecord
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
        public string m_userid;							//终端模块号码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string m_recv_date;                     //接收到数据包的时间
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]	//这里做了修改，转换时由ByValTStr变为ByValArray类型，
        public byte[] m_data_buf;					//存储接收到的数据
        public ushort m_data_len;					//接收到的数据包长度
        public byte m_data_type;				//接收到的数据包类型
        public void Initialize()					//初始化byte[]的字段
        {
            m_data_buf = new byte[1024];
        }
        //UnmanagedType.LPStr
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct GprsUserInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
        public string m_userid;				//终端模块号码	
        public uint m_sin_addr;				//终端模块进入Internet的代理主机IP地址
        public ushort m_sin_port;				//终端模块进入Internet的代理主机IP端口
        public uint m_local_addr;			//终端模块在移动网内IP地址
        public ushort m_local_port;			//终端模块在移动网内IP端口
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string m_logon_date;			//终端模块登录时间
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]	//这里做了修改，转换时由ByValTStr变为ByValArray类型，
        public byte[] m_update_time;			//终端用户更新时间
        //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        //	public string	m_update_time;
        public byte m_status;				//终端模块状态, 1 在线 0 不在线
        public void Initialize()					//初始化byte[]的字段
        {
            m_update_time = new byte[20];
        }
    }

    public static class Gprs
    {      
        //定义接口函数		

        //启动服务
        [DllImport(".\\wcomm_dll.dll")]
        public static extern int start_Gprs_server(
            IntPtr hWnd,
            int wMsg,
            int nServerPort,
            [MarshalAs(UnmanagedType.LPStr)]
			StringBuilder mess);
        //启动服务
        [DllImport(".\\wcomm_dll.dll")]
        public static extern int start_net_service(
            IntPtr hWnd,
            int wMsg,
            int nServerPort,
            [MarshalAs(UnmanagedType.LPStr)]
			StringBuilder mess);

        //停止服务
        [DllImport(".\\wcomm_dll.dll")]
        public static extern int stop_Gprs_server(
            [MarshalAs(UnmanagedType.LPStr)]
			StringBuilder mess);
        //停止服务
        [DllImport(".\\wcomm_dll.dll")]
        public static extern int stop_net_service(
            [MarshalAs(UnmanagedType.LPStr)]
			StringBuilder mess);

        //读取数据
        [DllImport(".\\wcomm_dll.dll")]
        public static extern int do_read_proc(
            ref GprsDataRecord recdPtr,
            [MarshalAs(UnmanagedType.LPStr)]
			StringBuilder mess,
            bool reply);

        //发送数据
        [DllImport(".\\wcomm_dll.dll")]
        public static extern int do_send_user_data(
            [MarshalAs(UnmanagedType.LPStr)]
			string userid,
            //[MarshalAs(UnmanagedType.LPStr)]
            //string data,
            byte[] data,
            int len,
            [MarshalAs(UnmanagedType.LPStr)]
			StringBuilder mess);

        //获取终端信息
        [DllImport(".\\wcomm_dll.dll")]
        public static extern int get_user_info(
            [MarshalAs(UnmanagedType.LPStr)]
			string userid,
            ref GprsUserInfo infoPtr);

        //设置服务模式
        [DllImport(".\\wcomm_dll.dll")]
        public static extern int SetWorkMode(int nWorkMode);

        //取消阻塞读取
        [DllImport(".\\wcomm_dll.dll")]
        public static extern void cancel_read_block();

        //使某个DTU下线
        [DllImport(".\\wcomm_dll.dll")]
        public static extern int do_close_one_user(
            [MarshalAs(UnmanagedType.LPStr)]
			string userid,
            [MarshalAs(UnmanagedType.LPStr)]
			StringBuilder mess);

        //使所有DTU下线
        [DllImport(".\\wcomm_dll.dll")]
        public static extern int do_close_all_user(
            [MarshalAs(UnmanagedType.LPStr)]
			StringBuilder mess);

        //使某个DTU下线
        [DllImport(".\\wcomm_dll.dll")]
        public static extern int do_close_one_user2(
            [MarshalAs(UnmanagedType.LPStr)]
			string userid,
            [MarshalAs(UnmanagedType.LPStr)]
			StringBuilder mess);

        //使所有DTU下线
        [DllImport(".\\wcomm_dll.dll")]
        public static extern int do_close_all_user2(
            [MarshalAs(UnmanagedType.LPStr)]
			StringBuilder mess);

        //设置服务类型
        [DllImport(".\\wcomm_dll.dll")]
        public static extern int SelectProtocol(int nProtocol);

        //指定服务IP
        [DllImport(".\\wcomm_dll.dll")]
        public static extern void SetCustomIP(int ip);

        //获得最大DTU连接数量
        [DllImport(".\\wcomm_dll.dll")]
        public static extern uint get_max_user_amount();

        //获取终端信息
        [DllImport(".\\wcomm_dll.dll")]
        public static extern int get_user_at(uint index, ref GprsUserInfo infoPtr);

        //定义一些SOCKET API函数
        [DllImport("Ws2_32.dll")]
        public static extern Int32 inet_addr(string ip);
        [DllImport("Ws2_32.dll")]
        public static extern string inet_ntoa(uint ip);
        [DllImport("Ws2_32.dll")]
        public static extern uint htonl(uint ip);
        [DllImport("Ws2_32.dll")]
        public static extern uint ntohl(uint ip);
        [DllImport("Ws2_32.dll")]
        public static extern ushort htons(ushort ip);
        [DllImport("Ws2_32.dll")]
        public static extern ushort ntohs(ushort ip);

        public const int WmDtu = 0x400 + 100;
    }
}
