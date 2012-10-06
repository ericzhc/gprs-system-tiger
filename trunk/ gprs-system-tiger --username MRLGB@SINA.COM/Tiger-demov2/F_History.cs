using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.Objects;

namespace Tiger
{
    public partial class F_History : Form
    {
        private DbTigerEntities context;//数据库的上下文。
        private bool[] selectflag;
        private ushort seriesmax =5;
        private enum NO 
        {
            T_Heating=0,
            T_CollectBox,
            T_CollectIn,
            T_CollectOut,
            T_Ambient
        }
        private decimal[] T;

        public F_History()
        {
            InitializeComponent();
            selectflag = new bool[5];
            T = new decimal[5];
            context = new DbTigerEntities();
        }

        private void F_History_Load(object sender, EventArgs e)
        {
            // Enable range selection and zooming UI by default
            chart1.ChartAreas["Default"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["Default"].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas["Default"].AxisX.ScaleView.Zoomable = true;

            // Set scrollbar size
            chart1.ChartAreas["Default"].AxisX.ScrollBar.Size = 16;

            // Show small scroll buttons only
            chart1.ChartAreas["Default"].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;

            // Scrollbars position
            chart1.ChartAreas["Default"].AxisX.ScrollBar.IsPositionedInside = false;


            chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;//FixedCount VariableCount
            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:g}";

            foreach (var union in context.unions)
            {
                comboBox_ID.Items.Add(union.UnitId);
            }

            dateTimePicker_from.Format = DateTimePickerFormat.Custom;
            dateTimePicker_from.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateTimePicker_to.Format = DateTimePickerFormat.Custom;
            dateTimePicker_to.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            

        }

        private void checkBox_T_HeatingBox_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Series[(ushort)NO.T_Heating].Enabled = checkBox_T_HeatingBox.Checked;
        }

        private void checkBox_T_CollectorBox_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Series[(ushort)NO.T_CollectBox].Enabled = checkBox_T_CollectorBox.Checked;
        }

        private void checkBox_T_CollectorIn_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Series[(ushort)NO.T_CollectIn].Enabled = checkBox_T_CollectorIn.Checked;
        }

        private void checkBoxT_CollectorOut_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Series[(ushort)NO.T_CollectOut].Enabled = checkBoxT_CollectorOut.Checked;
        }

        private void checkBox_T_Ambient_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Series[(ushort)NO.T_Ambient].Enabled = checkBox_T_Ambient.Checked;
        }

        private void button_Refrsh_Click(object sender, EventArgs e)
        {
            ZoomingExtents_Load(null,null);
            linkLabelAllYes_LinkClicked(null,null);
        }

        private void linkLabelAllYes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            selectAll(true);
        }

        private void linkLabelAllNo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            selectAll(false);
        }

        private void selectAll(bool check) 
        {
            foreach (System.Windows.Forms.Control control in this.groupBox2.Controls)
            {
                if (control is System.Windows.Forms.CheckBox)
                {
                    System.Windows.Forms.CheckBox pb = (System.Windows.Forms.CheckBox)control;
                    pb.Checked = check;
                }
            }

        }

        private void ZoomingExtents_Load(object sender, System.EventArgs e)
        {
            // Fill chart with random data
            string id = "";
            //if ((checkBoxID.Checked) && (!comboBox_ID.SelectedItem.Equals(null)))
            if ((checkBoxID.Checked) && (!comboBox_ID.SelectedItem.Equals(null)) && (checkBox2.Checked) && (dateTimePicker_from.Value < dateTimePicker_to.Value))

            {
                id = comboBox_ID.SelectedItem.ToString();
            }

            var states =
            from state in context.unitstates
            where (state.UnitId == id && state.DateTime_RecvDate > dateTimePicker_from.Value && state.DateTime_RecvDate < dateTimePicker_to.Value)
            select new
            {
                Temp_HeatingBox = state.Temp_HeatingBox,
                Temp_CollectorBox = state.Temp_CollectorBox,
                Temp_CollectorIn = state.Temp_CollectorIn,
                Temp_CollectorOut = state.Temp_CollectorOut,
                Temp_Ambient = state.Temp_Ambient,
                DateTime_RecvDate = state.DateTime_RecvDate,
            };

            // Set original view
            chart1.ChartAreas["Default"].AxisX.ScaleView.Position = dateTimePicker_from.Value.AddHours(12).ToOADate();//时间转double
            chart1.ChartAreas["Default"].AxisX.ScaleView.MinSizeType = DateTimeIntervalType.Hours;
            chart1.ChartAreas["Default"].AxisX.ScaleView.Size = 1;
            chart1.ChartAreas["Default"].CursorX.Interval = 1;
            chart1.ChartAreas["Default"].CursorX.IntervalType = DateTimeIntervalType.Hours;

            if (chart1.Series.Count == seriesmax)
            {
                for (int index = 0; index < seriesmax; index++)
                {
                    chart1.Series[index].Points.Clear();
                }
            }

            if (chart1.Series.Count == 0)
                for (int index = 0; index < seriesmax; index++)
                {
                    Series series = chart1.Series.Add("Series " + (chart1.Series.Count).ToString());
                    series.ChartType = SeriesChartType.FastLine;
                    series.ChartArea = "Default";
                    series.BorderWidth = 2;
                    series.BorderColor = Color.FromArgb(120, 147, 190);
                    series.ShadowColor = Color.FromArgb(64, 0, 0, 0);
                    series.ShadowOffset = 2;

                    // Set marker attributes for the whole series            
                    series.MarkerStyle = MarkerStyle.Diamond;
                    series.MarkerSize = 3;
                    series.MarkerColor = Color.Magenta;
                    series.MarkerBorderColor = Color.Red;
                    series.MarkerBorderWidth = 1;

                }


            foreach (var state in states)
            {
                chart1.Series[0].Points.AddXY(state.DateTime_RecvDate, state.Temp_HeatingBox);
                chart1.Series[1].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorBox);
                chart1.Series[2].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorIn);
                chart1.Series[3].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorOut);
                chart1.Series[4].Points.AddXY(state.DateTime_RecvDate, state.Temp_Ambient);
            }
 
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxBackColor_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateChartSettings();
        }

        private void checkBoxResetButton_CheckedChanged(object sender, System.EventArgs e)
        {
            UpdateChartSettings();
        }

        private void comboBoxSize_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateChartSettings();
        }

        private void UpdateChartSettings()
        {
            // Set scrollbar size
            //if (comboBoxSize.Text != String.Empty)
            //{
            //    chart1.ChartAreas["Default"].AxisX.ScrollBar.Size = int.Parse(comboBoxSize.Text);
            //}

            //// Do not show buttons with size 5
            //checkBoxResetButton.Enabled = true;
            //checkBoxSmallScrollButton.Enabled = true;
            //if (chart1.ChartAreas["Default"].AxisX.ScrollBar.Size == 5)
            //{
            //    checkBoxResetButton.Checked = false;
            //    checkBoxSmallScrollButton.Checked = false;
            //    checkBoxResetButton.Enabled = false;
            //    checkBoxSmallScrollButton.Enabled = false;
            //}

            //// Position of Scrollbars
            //chart1.ChartAreas["Default"].AxisX.ScrollBar.IsPositionedInside = checkBoxInside.Checked;

            //// Set scrollbar buttons
            //chart1.ChartAreas["Default"].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.None;
            //if (checkBoxResetButton.Checked && checkBoxSmallScrollButton.Checked)
            //{
            //    chart1.ChartAreas["Default"].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;
            //}
            //else if (checkBoxResetButton.Checked)
            //{
            //    chart1.ChartAreas["Default"].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.ResetZoom;
            //}
            //else if (checkBoxSmallScrollButton.Checked)
            //{
            //    chart1.ChartAreas["Default"].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            //}

            //// Set scrollbar colors
            //chart1.ChartAreas["Default"].AxisX.ScrollBar.BackColor = Color.White;
            //chart1.ChartAreas["Default"].AxisX.ScrollBar.ButtonColor = Color.FromArgb(224, 224, 224);
            //chart1.ChartAreas["Default"].AxisX.ScrollBar.LineColor = Color.Black;
            //if (comboBoxBackColor.Text != String.Empty && comboBoxBackColor.Text != "Default")
            //{
            //    chart1.ChartAreas["Default"].AxisX.ScrollBar.BackColor = Color.FromName(comboBoxBackColor.Text);
            //}
            //if (comboBoxLineColor.Text != String.Empty && comboBoxLineColor.Text != "Default")
            //{
            //    chart1.ChartAreas["Default"].AxisX.ScrollBar.LineColor = Color.FromName(comboBoxLineColor.Text);
            //}
            //if (comboBoxButtonsColor.Text != String.Empty && comboBoxButtonsColor.Text != "Default")
            //{
            //    chart1.ChartAreas["Default"].AxisX.ScrollBar.ButtonColor = Color.FromName(comboBoxButtonsColor.Text);
            //}
        }


        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            UpdateChartSettings();
        }


    }
}
   
