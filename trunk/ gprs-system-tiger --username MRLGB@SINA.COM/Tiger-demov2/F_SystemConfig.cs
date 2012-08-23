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
        private db_tigerEntities tigerContext;
        Module module = new Module();
        public F_SystemSet()
        {
            InitializeComponent();
        }

        private void F_SystemSet_Load(object sender, EventArgs e)
        {
            //Initialize the ObjectContext
            tigerContext = new db_tigerEntities();

            // Define a query that returns all Department  
            // objects and course objects, ordered by name.
            var departmentQuery = from d in tigerContext.tb_union_list
                                  orderby d.alias
                                  select d;
            try
            {
                // Bind the ComboBox control to the query, 
                // which is executed during data binding.
                // To prevent the query from being executed multiple times during binding, 
                // it is recommended to bind controls to the result of the Execute method. 
                this.unitList.DisplayMember = "alias";
                this.unitList.DataSource = ((ObjectQuery)departmentQuery).Execute(MergeOption.AppendOnly);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void closeForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void unitList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Get the object for the selected alias.
                tb_union_list unit = (tb_union_list)this.unitList.SelectedItem;
              //  Department department = (Department)this.departmentList.SelectedItem;

                //Bind the grid view to the collection of Course objects
                // that are related to the selected Department object.
             //   courseGridView.DataSource = department.Courses;

                unitGridView.DataSource = tigerContext.tb_union_list;

                //// Hide the columns that are bound to the navigation properties on Course.
                unitGridView.Columns["time_online"].Visible = false;
                unitGridView.Columns["Start_time"].Visible = false;
                unitGridView.Columns["Stop_time"].Visible = false;
                unitGridView.Columns["time_online"].Visible = false;

                unitGridView.AllowUserToDeleteRows = false;
                unitGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }  
        
    }
}
