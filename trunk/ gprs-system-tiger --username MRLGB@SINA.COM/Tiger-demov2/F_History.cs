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
    public partial class F_History : Form
    {
        public F_History()
        {
            InitializeComponent();
        }

        private void chartBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            chart1.DataBind();
        }
    }
}
