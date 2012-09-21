using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace Tiger
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
                    TreeNode nod = new TreeNode(union.alias);
                    //增加节点TAG属性
                    nod.Tag = union.UnitId;
                    tv.Nodes.Add(nod);              
                }
             }
        }
        #endregion
    }
}
