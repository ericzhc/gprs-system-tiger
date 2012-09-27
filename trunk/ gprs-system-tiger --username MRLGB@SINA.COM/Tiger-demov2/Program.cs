using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Tiger
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。http://code.google.com/p/gprs-system-tiger/source/checkout
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new F_HistoryUpdate());
            //Application.Run(new LoginContext());
        }
    }
}
