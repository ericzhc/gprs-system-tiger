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
        private enum NO 
        {
            T_Heating,
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
        }

        private void chartBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            //chart1.DataBind();
        }

        private void F_History_Load(object sender, EventArgs e)
        {
            //chart1.EnableZoomAndPanControls(ChartCursorSelected, ChartCursorMoved);
            // show an X label every 3 Minute

            chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;//FixedCount VariableCount
            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:g}";

            //chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:g}";

            //读取手机电话号码列表
            context = new DbTigerEntities();
            //using (var odbEntities = new D())
            {
                foreach (var union in context.unions)
                {
                    comboBox_ID.Items.Add(union.UnitId);
                }
            }
            
            dateTimePicker_from.Format = DateTimePickerFormat.Long;
            dateTimePicker_from.CustomFormat = "MM/dd/yyyy HHH:mm";

            dateTimePicker_to.Format = DateTimePickerFormat.Long;
            dateTimePicker_to.CustomFormat = "MM/dd/yyyy HHH:mm";

        }

        private void checkBox_T_HeatingBox_CheckedChanged(object sender, EventArgs e)
        {
            selectflag[(ushort)NO.T_Heating] = checkBox_T_HeatingBox.Checked;
        }

        private void checkBox_T_CollectorBox_CheckedChanged(object sender, EventArgs e)
        {
            selectflag[(ushort)NO.T_CollectBox] = checkBox_T_CollectorBox.Checked;
        }

        private void checkBox_T_CollectorIn_CheckedChanged(object sender, EventArgs e)
        {
            selectflag[(ushort)NO.T_CollectIn] = checkBox_T_CollectorIn.Checked;
        }

        private void checkBoxT_CollectorOut_CheckedChanged(object sender, EventArgs e)
        {
            selectflag[(ushort)NO.T_CollectOut] = checkBoxT_CollectorOut.Checked;
        }

        private void checkBox_T_Ambient_CheckedChanged(object sender, EventArgs e)
        {
            selectflag[(ushort)NO.T_Ambient] = checkBox_T_Ambient.Checked;
        }

        private void showselcetview(object x, object y0, object y1, object y2, object y3, object y4) 
        {
            foreach (NO s in Enum.GetValues(typeof(NO)))//枚举所有字段
            {
                if (selectflag[(ushort)s] == true)
                {
                    switch (s)
                    {
                        case NO.T_Heating:
                            chart1.Series[(ushort)s].Points.AddXY((System.DateTime)x, (decimal)y0);
                            break;
                        case NO.T_CollectBox:
                            chart1.Series[(ushort)s].Points.AddXY((System.DateTime)x, (decimal)y1);
                            break;
                        case NO.T_CollectIn:
                            chart1.Series[(ushort)s].Points.AddXY((System.DateTime)x, (decimal)y2);
                            break;
                        case NO.T_CollectOut:
                            chart1.Series[(ushort)s].Points.AddXY((System.DateTime)x, (decimal)y3);
                            break;
                        case NO.T_Ambient:
                            chart1.Series[(ushort)s].Points.AddXY((System.DateTime)x, (decimal)y4);
                            break;
                        default:              
                            break;
                    }
                   
                }
            }
     
        }

        private void button_Refrsh_Click(object sender, EventArgs e)
        {
            ////清楚现有图标数据（5个温度值）
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            chart1.Series[4].Points.Clear();

            ////选择了某个手机号码。同时选择其实时间。
            if ((checkBox1.Checked) && (!comboBox_ID.SelectedItem.Equals(null)) && (checkBox2.Checked) && (dateTimePicker_from.Value < dateTimePicker_to.Value))
            {
                string id = comboBox_ID.SelectedItem.ToString();//手机号
                //LINQ 
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
                foreach (var state in states)
                {
                    showselcetview(state.DateTime_RecvDate, state.Temp_HeatingBox, state.Temp_CollectorBox, state.Temp_CollectorIn, state.Temp_CollectorOut, state.Temp_Ambient);

                    //chart1.Series[0].Points.AddXY(state.DateTime_RecvDate, state.Temp_HeatingBox);
                    //chart1.Series[1].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorBox);
                    //chart1.Series[2].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorIn);
                    //chart1.Series[3].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorOut);
                    //chart1.Series[4].Points.AddXY(state.DateTime_RecvDate, state.Temp_Ambient);
                }

            }
            else
            {//选择了某个手机号码
                if (((checkBox1.Checked) && (!comboBox_ID.SelectedItem.Equals(null))))
                {
                    string id = comboBox_ID.SelectedItem.ToString();
                    var states =
                        from state in context.unitstates
                        where (state.UnitId == id)
                        select new
                        {
                            Temp_HeatingBox = state.Temp_HeatingBox,
                            Temp_CollectorBox = state.Temp_CollectorBox,
                            Temp_CollectorIn = state.Temp_CollectorIn,
                            Temp_CollectorOut = state.Temp_CollectorOut,
                            Temp_Ambient = state.Temp_Ambient,
                            DateTime_RecvDate = state.DateTime_RecvDate,
                        };
                    foreach (var state in states)
                    {
                        showselcetview(state.DateTime_RecvDate, state.Temp_HeatingBox, state.Temp_CollectorBox, state.Temp_CollectorIn, state.Temp_CollectorOut, state.Temp_Ambient);
                    }
                }
                //。同时选择其实时间。
                if ((checkBox2.Checked) && (dateTimePicker_from.Value < dateTimePicker_to.Value))
                {
                    var states =
                        from state in context.unitstates
                        where (state.DateTime_RecvDate > dateTimePicker_from.Value && state.DateTime_RecvDate < dateTimePicker_to.Value)
                        select new
                        {
                            Temp_HeatingBox = state.Temp_HeatingBox,
                            Temp_CollectorBox = state.Temp_CollectorBox,
                            Temp_CollectorIn = state.Temp_CollectorIn,
                            Temp_CollectorOut = state.Temp_CollectorOut,
                            Temp_Ambient = state.Temp_Ambient,
                            DateTime_RecvDate = state.DateTime_RecvDate,
                        };
                    foreach (var state in states)
                    {
                        showselcetview(state.DateTime_RecvDate, state.Temp_HeatingBox, state.Temp_CollectorBox, state.Temp_CollectorIn, state.Temp_CollectorOut, state.Temp_Ambient);

                        //chart1.Series[0].Points.AddXY(state.DateTime_RecvDate, state.Temp_HeatingBox);
                        //chart1.Series[1].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorBox);
                        //chart1.Series[2].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorIn);
                        //chart1.Series[3].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorOut);
                        //chart1.Series[4].Points.AddXY(state.DateTime_RecvDate, state.Temp_Ambient);
                    }

                }

            }

            chart1.Invalidate();

               
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        //private void ChartCursorSelected(double x, double y)
        //{
        //    txtChartSelect.Text = x.ToString("F4") + ", " + y.ToString("F4");
        //}
        //private void ChartCursorMoved(double x, double y)
        //{
        //    txtChartValue.Text = x.ToString("F4") + ", " + y.ToString("F4");
        //}
        
    }
    }
   
