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

        //add main ui control binding
        private void InitUIDataBinding(string unitid)
        {
            //add data binding
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string uinitid = (string)(treeView1.SelectedNode.Tag);
            InitUIDataBinding(uinitid);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (!treeView1.SelectedNode.FirstNode.Equals(null))
            //{
            //    string uinitid = (string)(treeView1.SelectedNode.FirstNode.Tag);
            //    MessageBox.Show("模拟刷新UI");
            //}
            //else
            //{
            //    MessageBox.Show("请选择左边列表");
            //}
            
        }
    }
}
