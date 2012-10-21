using System;
using System.Windows.Forms;
using Tiger.Helper;

namespace Tiger
{
    public partial class FNodeState : Form
    {
        readonly Module _module = new Module();
        public FNodeState()
        {
            InitializeComponent();
        }

        private void FNodeStateLoad(object sender, EventArgs e)
        {
            _module.TreeInit(treeView1);
        }

        //add main ui control binding
        private void InitUIDataBinding()
        {
            //add data binding
        }

        private void TreeView1AfterSelect(object sender, TreeViewEventArgs e)
        {
            //var uinitid = (string)(treeView1.SelectedNode.Tag);
            InitUIDataBinding();
        }

        private void Button1Click(object sender, EventArgs e)
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
