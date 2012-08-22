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
    public partial class F_SystemSet : Form
    {
        //DataAccess da = new DataAccess();
        //DataTable dt = new DataTable();

        Module module = new Module();
        public F_SystemSet()
        {
            InitializeComponent();
        }

        private void F_SystemSet_Load(object sender, EventArgs e)
        {
            module.TreeInit(treeView1);
            foreach (TreeNode tn in treeView1.Nodes)
            {
                label8.Text = label8.Text + tn.Tag;
            }
        }

           //根据ID检索tb_union_list和tb_unit_para表。
        //string selectstr = "SELECT tb_union_list.UnitId,tb_union_list.alias,tb_unit_para.Aera_IrradiatedSum,tb_unit_para.Auxiliary_power,tb_unit_para.Flow_CollectorSys, tb_unit_para.Flow_HeatUsing,tb_unit_para.Aperture_Area from tb_union_list,tb_unit_para where tb_union_list.UnitId = tb_unit_para.UnitId";
        

        //#region  刷新配置界面GPRS状态 
        ///// <summary>
        ///// 根据gprs的ID号，此ID已经被写入treeview节点的TAG中
        ///// </summary>
        ///// <param name="unit_id"></param>
        //private void gprs_stat_refresh(string unit_id)
        //{
        //    DataAccess da = new DataAccess();
        //    DataTable dt = new DataTable();

        //    //根据ID检索tb_union_list和tb_unit_para表。
        //    string selectstr = "SELECT tb_union_list.UnitId,tb_union_list.alias,tb_unit_para.Aera_IrradiatedSum,tb_unit_para.Auxiliary_power,tb_unit_para.Flow_CollectorSys, tb_unit_para.Flow_HeatUsing,tb_unit_para.Aperture_Area from tb_union_list,tb_unit_para where tb_union_list.UnitId = tb_unit_para.UnitId";

        //    dt = da.GetDataTable(selectstr);

        //    int index = 0;
        //    while (String.Compare(dt.Rows[index]["UnitId"].ToString(), unit_id) == 0)
        //    {
        //        unit_id_txt.Text = dt.Rows[index]["UnitId"].ToString();
        //        unit_alias_txt.Text = dt.Rows[index]["alias"].ToString();
        //        Aera_IrradiatedSum_txt.Text = dt.Rows[index]["Aera_IrradiatedSum"].ToString();
        //        Auxiliary_power_txt.Text = dt.Rows[index]["Auxiliary_power"].ToString();
        //        Flow_CollectorSys_txt.Text = dt.Rows[index]["Flow_CollectorSys"].ToString();
        //        Flow_HeatUsing_txt.Text = dt.Rows[index]["Flow_HeatUsing"].ToString();
        //        Aperture_Area_txt.Text = dt.Rows[index]["Aperture_Area"].ToString();

        //        index++;

        //    }
           
        //}
        //#endregion

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
           // gprs_stat_refresh(treeView1.SelectedNode.Tag.ToString());
            label9.Text = treeView1.SelectedNode.Tag.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var context = new db_tigerEntities())
            {       
                addCustomer(context);           
            }
        }

        //private void treeView1_Click(object sender, EventArgs e)
        //{
        //    gprs_stat_refresh(treeView1.SelectedNode.Tag.ToString());
        //}


        //private void treeView1_Click(object sender, EventArgs e)
        //{
        //    gprs_stat_refresh(treeView1.SelectedNode.Tag.ToString());
        //}

        private static void addCustomer(db_tigerEntities context)
        {
            try
            {
                tb_union_list unit = new tb_union_list
                {
                    UnitId = "13695655652",
                    alias = "HFUU",
                    Aera_IrradiatedSum="101",
                    Volumn_HeatingBox="2000",

                };
                context.tb_union_list.AddObject(unit);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        // Update a Customer Record
        private static void modifyCustomer(db_tigerEntities context)
        {
            try
            {
                tb_union_list c = context.tb_union_list
                                    .First(i => i.UnitId == "13695655652");
                c.UnitId = "13695655651";
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }
        }

        // Delete a Customer Record
        private static void deleteCustomer(db_tigerEntities context)
        {
            try
            {
                tb_union_list cust = context.tb_union_list
                                       .First(i => i.UnitId == "13695655652");
                context.tb_union_list.DeleteObject(cust);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }
        }
        
    }
}
