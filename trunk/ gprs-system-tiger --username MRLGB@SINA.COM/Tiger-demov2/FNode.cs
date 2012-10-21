using System;
using System.Windows.Forms;
using Tiger.Helper;

namespace Tiger
{
    public partial class FNode : Form
    {
        readonly Module _module = new Module();
        private string _unitid;
        private BindingSource _bs;

        public FNode()
        {
            InitializeComponent();
        }

        private void FNode_Load(object sender, EventArgs e)
        {
            _module.TreeInit(treeView1);
            _bs = new BindingSource {DataSource = typeof (DtuObject)};
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (!treeView1.SelectedNode.Tag.Equals(null))
                {
                    _unitid=(string)treeView1.SelectedNode.Tag;
                }
                else
                {
                    MessageBox.Show("选择项不是！");
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "System error.\r\n\r\n" + ex.ToString();
                MessageBox.Show(errorMessage, "Error");
            }

            //dataGridView1.Columns.Add("111");
            Global.DtuList[_unitid].UpdateField();//从状态数组（归类后的数据）赋值给具体成员变量
            _bs.Clear();
            _bs.Add(Global.DtuList[_unitid]);
            dataGridView1.DataSource = _bs;
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
