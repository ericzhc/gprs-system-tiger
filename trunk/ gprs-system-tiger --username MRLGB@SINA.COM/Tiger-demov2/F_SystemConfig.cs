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
        //private db_tigerEntities tigerContext;
        Module module = new Module();
        public F_SystemSet()
        {
            InitializeComponent();
            //tigerContext = new db_tigerEntities();
        }

        private void F_SystemSet_Load(object sender, EventArgs e)
        {
            #region 更新GRID 老的方法
            //try
            //{
            //    // Bind the ComboBox control to the query, 
            //    // which is executed during data binding.                      
            //    ObjectQuery<tb_union_list> unitQuery = tigerContext.tb_union_list;
            //    unitGridView.DataSource = unitQuery;
            //    unitGridView.Columns["time_online"].Visible = false;
            //    unitGridView.Columns["Start_time"].Visible = false;
            //    unitGridView.Columns["Stop_time"].Visible = false;
            //    unitGridView.Columns["gprs_state"].Visible = false;
            //   // unitGridView.Columns["time_online"].Visible = true;

            //    unitGridView.Columns["UnitId"].HeaderText = "ID";
            //    unitGridView.Columns["alias"].HeaderText = "名称";
            //    unitGridView.Columns["Aera_IrradiatedSum"].HeaderText = "集热器面积";
            //    unitGridView.Columns["Volumn_HeatingBox"].HeaderText = "贮热水箱容量";
               

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            #endregion
        }

        private void closeForm_Click(object sender, EventArgs e)
        {
            //tigerContext.Dispose();
            this.Close();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            config_entityDataSource.SaveChanges();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            config_entityDataSource.Refresh();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            config_entityDataSource.CancelChanges();
        }

        private void config_entityDataSource_DataError(object sender, EFWinforms.DataErrorEventArgs e)
        {
            MessageBox.Show("Error Detected:\r\n" + e.Exception.Message);
            config_entityDataSource.CancelChanges();
            e.Handled = true;

        }  
        
    }
}
