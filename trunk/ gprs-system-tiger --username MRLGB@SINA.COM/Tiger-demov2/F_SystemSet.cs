using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

namespace Tiger
{
    public partial class F_SystemSet : Form
    {
        protected db_tigerEntities unitContext;
        public F_SystemSet()
        {
            InitializeComponent();
            unitContext = new db_tigerEntities();
        }

        private void F_SystemSet_Load(object sender, EventArgs e)
        {
            try
            {
                ObjectQuery<tb_union_list> unitQuery = unitContext.tb_union_list;
                unitdateview.DataSource = unitQuery;
                unitdateview.Columns["time_online"].Visible = false;
                unitdateview.Columns["gprs_state"].Visible = false;
                unitdateview.Columns["Start_time"].Visible = false;
                unitdateview.Columns["Stop_time"].Visible = false;

                unitdateview.Columns["UnitId"].HeaderText = "ID";
                unitdateview.Columns["alias"].HeaderText = "采集点";
                unitdateview.Columns["Aera_IrradiatedSum"].HeaderText = "集热器面积";
                unitdateview.Columns["Volumn_HeatingBox"].HeaderText = "贮热水箱容量";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            unitContext.Dispose();
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            unitContext.SaveChanges();
        }
    }
}
