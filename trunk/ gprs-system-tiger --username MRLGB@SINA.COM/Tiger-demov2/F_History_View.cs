using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tiger
{
    public partial class F_History_View : Form
    {
        public F_History_View()
        {
            InitializeComponent();
        }

        private void viewbindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            viewchart.DataBind();
        }
    }
}
