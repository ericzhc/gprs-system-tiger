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
        private string unitid;
        private BindingSource bs;

        public F_Node()
        {
            InitializeComponent();
        }

        private void FNode_Load(object sender, EventArgs e)
        {
            module.TreeInit(treeView1);
            bs = new BindingSource();
            bs.DataSource=typeof(DTUObject);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (!treeView1.SelectedNode.Tag.Equals(null))
                {
                    unitid=(string)treeView1.SelectedNode.Tag;
                }
                else
                {
                    MessageBox.Show("选择项不是！");
                }
            }
            catch (System.Exception ex)
            {
                string errorMessage = "System error.\r\n\r\n" + ex.ToString();
                MessageBox.Show(errorMessage, "Error");
            }

            //dataGridView1.Columns.Add("111");
            global.DTUList[unitid].UpdateField();//从状态数组（归类后的数据）赋值给具体成员变量
            bs.Clear();
            bs.Add(global.DTUList[unitid]);
            dataGridView1.DataSource = bs;
            //dataGridView1.DataSource = global.DTUList[unitid];
        }

        private void btn_send_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
    }
}
