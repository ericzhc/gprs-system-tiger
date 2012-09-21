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
    public partial class F_SystemConfig : Form
    {
        public F_SystemConfig()
        {
            InitializeComponent();
        }

        private void F_SystemConfig_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (var ctx = new DbTigerEntities())
            {
                dataGridView1.DataSource = ctx.unions.ToList();
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            global.Timer_store = (ushort)((numericUpDown1.Value > 0) ? numericUpDown1.Value * 1000 : 60 * 1000);
        }

        private void button_ADD_Click(object sender, EventArgs e)
        {
            if ((textBox_name.Text != "") && (textBox_unitid.Text != ""))
            {
                using (var context = new DbTigerEntities())
                {
                    try
                    {
                        DateTime now = DateTime.Now;
                        //DateTimeFormatInfo format = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
                        //format.DateSeparator = "-";
                        //format.ShortDatePattern = @"yyyy/MM/dd/hh/mm/ss";
                        union unit = new union
                        {
                            UnitId= textBox_unitid.Text,
                            alias = textBox_name.Text,
                            
                        };
                        context.unions.Add(unit);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }

                }
            }

            MessageBox.Show("该采集点添加成功！");
            LoadData();
        }

        private void button_Modify_Click(object sender, EventArgs e)
        {
            using (var context = new DbTigerEntities())
            {
                try
                {
                    union c = context.unions
                                   .First(i => i.UnitId == textBox_unitid.Text);
                    c.alias = textBox_name.Text;
                    context.SaveChanges();
                    Console.WriteLine("Customer Record Modified");
                }
                catch (Exception ex)
                {

                }

            }
            MessageBox.Show("该采集点名称修改成功！");
            LoadData();
        }

        private void button_Del_Click(object sender, EventArgs e)
        {
            string currenuser = textBox_unitid.Text;
            if (currenuser != "")
            {
                try
                {
                    using (var context = new DbTigerEntities())
                    {
                        try
                        {
                            union unit = context.unions
                                                   .First(i => i.UnitId == currenuser);
                            context.unions.Remove(unit);
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.InnerException.ToString());
                        }

                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.InnerException.ToString());
                }
            }
            MessageBox.Show("该用户删除成功！");
            LoadData();
        }

        
    }
}
