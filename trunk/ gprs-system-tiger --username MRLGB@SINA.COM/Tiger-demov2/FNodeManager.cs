using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Tiger.Helper;
using Tiger.Properties;

namespace Tiger
{
    public partial class FNodeManager : Form
    {
        private bool _validForm;
        public FNodeManager()
        {
            InitializeComponent();
            textBox_Unitid.Validating += ValidateTextBox;
            textBox_Alias.Validating += ValidateTextBox;
            textBox_AreaIrr.Validating += ValidateTextBox;
            textBox_Flow_CollectorSys.Validating += ValidateTextBox;
            textBox_Flow_HeatUsing.Validating += ValidateTextBox;
            textBox_Auxiliary_power.Validating += ValidateTextBox;
            textBox_VolumHeat.Validating += ValidateTextBox;
        }

        private void FSystemConfigLoad(object sender, EventArgs e)
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

        private void ButtonOkClick(object sender, EventArgs e)
        {
            Global.TimerStore = (ushort)((numericUpDown1.Value > 0) ? numericUpDown1.Value * 1000 : 60 * 1000);
            Global.TimerStatistic=(ushort)((numericUpDown2.Value > 0) ? numericUpDown2.Value * 1000 : 60 * 1000);
            this.Close();
        }

        private void ButtonAddClick(object sender, EventArgs e)
        {
            if (_validForm) 
            {
                if ((textBox_Alias.Text != "") && (textBox_Unitid.Text != ""))
                {
                    using (var context = new DbTigerEntities())
                    {
                        try
                        {
                            var now = DateTime.Now;
                            //DateTimeFormatInfo format = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat;
                            //format.DateSeparator = "-";
                            //format.ShortDatePattern = @"yyyy/MM/dd/hh/mm/ss";
                            var unit = new union
                            {
                                UnitId = textBox_Unitid.Text,
                                alias = textBox_Alias.Text,
                                Aera_IrradiatedSum = float.Parse(textBox_AreaIrr.Text),
                                Flow_CollectorSys = float.Parse(textBox_Flow_CollectorSys.Text),
                                Flow_HeatUsing = float.Parse(textBox_Flow_HeatUsing.Text),
                                Auxiliary_power = float.Parse(textBox_Auxiliary_power.Text),
                                Volumn_HeatingBox = float.Parse(textBox_VolumHeat.Text),
                                System_heat = float.Parse(textBox_SystemHeat.Text),
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

                MessageBox.Show(Resources.FNodeManager_button_ADD_Click_);
                LoadData();
            }
            
        }

        private void ButtonModifyClick(object sender, EventArgs e)
        {
            if (_validForm) 
            {
                using (var context = new DbTigerEntities())
                {
                    try
                    {
                        var c = context.unions.First(i => i.UnitId == textBox_Unitid.Text);
                        c.alias = textBox_Alias.Text;
                        c.Aera_IrradiatedSum = float.Parse(textBox_AreaIrr.Text);
                        c.Flow_CollectorSys = float.Parse(textBox_Flow_CollectorSys.Text);
                        c.Flow_HeatUsing = float.Parse(textBox_Flow_HeatUsing.Text);
                        c.Auxiliary_power = float.Parse(textBox_Auxiliary_power.Text);
                        c.Volumn_HeatingBox = float.Parse(textBox_VolumHeat.Text);
                        context.SaveChanges();
                       
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        Global.ParameterList[(textBox_Unitid.Text)].UpateParameterObject(textBox_Unitid.Text,
                                                                                         float.Parse(textBox_AreaIrr.Text),
                                                                                         float.Parse(textBox_Auxiliary_power.Text),
                                                                                         float.Parse(textBox_Flow_CollectorSys.Text),
                                                                                         float.Parse(textBox_Flow_HeatUsing.Text),
                                                                                         float.Parse(textBox_VolumHeat.Text));

                    }

                }
                MessageBox.Show(Resources.FNodeManager_button_Modify_Click_);
                LoadData();     
            }
           
        }

        private void ButtonDelClick(object sender, EventArgs e)
        {
            if (!_validForm) return;
            var currenuser = textBox_Unitid.Text;
            if (currenuser != "")
            {
                try
                {
                    using (var context = new DbTigerEntities())
                    {
                        try
                        {
                            var unit = context.unions
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
                catch (Exception)
                {
                    //MessageBox.Show(ex.InnerException.ToString());
                }
            }
            MessageBox.Show(Resources.FNodeManager_button_Del_Click_);
            LoadData();
        }

        private void DataGridView1SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                textBox_Unitid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox_Alias.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox_AreaIrr.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox_VolumHeat.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox_Flow_CollectorSys.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox_Flow_HeatUsing.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                textBox_Auxiliary_power.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                textBox_SystemHeat.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            }
            catch (Exception)
            {

            }
        }

        #region Validating

        private void ValidateTextBox(object sender, CancelEventArgs e)
        {
           bool nameValid = true, nikNameValid = true, passwordValid = true;
           if (String.IsNullOrEmpty(((TextBox)sender).Text))
           {
               switch (Convert.ToByte(((TextBox)sender).Tag))
               {
                   case 0:
                       errorProvider1.SetError(textBox_Unitid, "Please, enter your name");
                       nameValid = false;
                       break;
                   case 1:
                       errorProvider1.SetError(textBox_Alias, "Please, enter your nikname");
                       nikNameValid = false;
                       break;
                   case 2:
                       errorProvider1.SetError(textBox_AreaIrr, "Please, enter your password");
                       passwordValid = false;
                       break;
               }
           }
           else
           {
               switch (Convert.ToByte(((TextBox)sender).Tag))
               {
                   case 0: errorProvider1.SetError(textBox_Unitid, "");
                       break;
                   case 1: errorProvider1.SetError(textBox_Alias, "");
                       break;
                   case 2: errorProvider1.SetError(textBox_AreaIrr, "");
                       break;
               }
           }
           _validForm = nameValid && nikNameValid && passwordValid;
        }

        #endregion
    }
}
