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
    public partial class F_Node_State : Form
    {
        Module module = new Module();
        public F_Node_State()
        {
            InitializeComponent();
        }

        private void F_Node_State_Load(object sender, EventArgs e)
        {
            module.TreeInit(treeView1);
        }        
    }
}
