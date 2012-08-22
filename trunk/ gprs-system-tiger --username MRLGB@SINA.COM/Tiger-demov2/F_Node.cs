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
    public partial class F_Node : Form
    {
        Module module = new Module();
        public F_Node()
        {
            InitializeComponent();
        }

        private void FNode_Load(object sender, EventArgs e)
        {
            module.TreeInit(treeView1);
        }
       
    }
}
