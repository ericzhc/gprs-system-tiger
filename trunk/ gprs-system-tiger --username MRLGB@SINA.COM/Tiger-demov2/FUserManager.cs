using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tiger.Helper;

namespace Tiger
{
    public partial class FUserManager : Form
    {
        //private BindingSource bs;
        public FUserManager()
        {
            InitializeComponent();
        }

        private void F_User_Load(object sender, EventArgs e)
        {
            LoadData();
            foreach (UserRole role in Enum.GetValues(typeof(UserRole))) 
            {
                string temp = Global.Userrolestring[(ushort)role];
                comboBox_role.Items.Add(temp);
            }
            
        }

        private void LoadData() 
        {
            using (var ctx = new DbTigerEntities())
            {
                dataGridView1.DataSource = ctx.logininfors.ToList();
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {

        }

        private void button_cancel_Click(object sender, EventArgs e)
        {

        }

        private void button_ADD_Click(object sender, EventArgs e)
        {
            if ((textBox_name.Text != "") && (textBox_password.Text != ""))
            {
                using (var context = new DbTigerEntities())
                {
                    try
                    {
                        DateTime now = DateTime.Now;
                        //DateTimeFormatInfo format = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
                        //format.DateSeparator = "-";
                        //format.ShortDatePattern = @"yyyy/MM/dd/hh/mm/ss";
                        logininfor logininforo = new logininfor
                        {
                            username = textBox_name.Text,
                            password = textBox_password.Text,
                            role = (short)comboBox_role.SelectedIndex
                        };
                        context.logininfors.Add(logininforo);
                        context.SaveChanges();
                        MessageBox.Show("该用户添加成功！");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }

                }
            }

            
            LoadData();
        }

        private void button_Modify_Click(object sender, EventArgs e)
        {
            using (var context = new DbTigerEntities())
             {
                  try
                  {
                     logininfor c = context.logininfors
                                    .First(i => i.username == textBox_name.Text);
                        c.password = textBox_password.Text;
                        context.SaveChanges();
                    }
                     catch (Exception)
                    {
                        
                    }
           
             }
             MessageBox.Show("该用户密码修改成功！");
             LoadData();
           
        }

        private void button_Del_Click(object sender, EventArgs e)
        {
             string currenuser =textBox_name.Text ;
             if (currenuser != "")
             {
                 try
                 {
                     using (var context = new DbTigerEntities())
                     {
                         try
                         {
                             logininfor logino = context.logininfors
                                                    .First(i => i.username == currenuser);
                             context.logininfors.Remove(logino);
                             context.SaveChanges();
                         }
                         catch (Exception ex)
                         {
                             MessageBox.Show(ex.InnerException.ToString());
                         }

                     }
                 }
                 catch (Exception)
                 {
                     //MessageBox.Show(ex.InnerException.ToString());
                 }
             }
             MessageBox.Show("该用户删除成功！");
             LoadData();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                textBox_name.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox_password.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                //MessageBox.Show(global.userrolestring[int.Parse(dataGridView1.SelectedRows[0].Cells[2].Value.ToString())]);
                comboBox_role.Text = Global.Userrolestring[int.Parse(dataGridView1.SelectedRows[0].Cells[2].Value.ToString())];
            }
            catch (System.Exception)
            {
            	
            }
           
        }

        private void button_ModifyRole_Click(object sender, EventArgs e)
        {
            using (var context = new DbTigerEntities())
            {
                try
                {
                    logininfor c = context.logininfors
                                   .First(i => i.username == textBox_name.Text);
                    c.role =(short)comboBox_role.SelectedIndex;
                    context.SaveChanges();
                }
                catch (Exception)
                {

                }

            }
            MessageBox.Show("该用户权限修改成功！");
            LoadData();
           
        }
    }
}
