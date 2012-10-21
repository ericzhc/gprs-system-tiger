using System;
using System.Windows.Forms;

namespace Tiger
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。http://code.google.com/p/Gprs-system-tiger/source/checkout
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FMain());
            Application.Run(new LoginContext());
        }
    }
}
