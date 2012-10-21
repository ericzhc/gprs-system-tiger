using System.Windows.Forms;

namespace Tiger.Helper
{
    class Module
    {
        //DataAccess da = new DataAccess();
        //DataTable dt = new DataTable();

        #region 根据数据库生成TreeView节点
        public void TreeInit(TreeView tv)
        {
            tv.Nodes.Clear();
            using (var odbEntities = new DbTigerEntities())
            {
                // 1. Easy example but not very flexible
                //    Select all products without any constraints
                foreach (var union in odbEntities.unions)
                {
                    var nod = new TreeNode(union.alias) {Tag = union.UnitId};
                    //增加节点TAG属性
                    tv.Nodes.Add(nod);              
                }
             }
        }
        #endregion
    }
}
