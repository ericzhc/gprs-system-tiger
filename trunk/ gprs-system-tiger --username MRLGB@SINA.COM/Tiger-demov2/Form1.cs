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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            //string id = "";
            //if ((checkBoxID.Checked) && (!comboBox_ID.SelectedItem.Equals(null)))
            //{
            //    id = comboBox_ID.SelectedItem.ToString();
            //}

            //var states =
            //from state in context.unitstates
            //where (state.UnitId == id)
            //select new
            //{
            //    Temp_HeatingBox = state.Temp_HeatingBox,
            //    Temp_CollectorBox = state.Temp_CollectorBox,
            //    Temp_CollectorIn = state.Temp_CollectorIn,
            //    Temp_CollectorOut = state.Temp_CollectorOut,
            //    Temp_Ambient = state.Temp_Ambient,
            //    DateTime_RecvDate = state.DateTime_RecvDate,
            //};


            //if (chart1.Series.Count == seriesmax)
            //{
            //    for (int index = 0; index < seriesmax; index++)
            //    {
            //        chart1.Series[index].Points.Clear();
            //    }
            //}

            //if (chart1.Series.Count == 0)
            //    for (int index = 0; index < seriesmax; index++)
            //    {
            //        Series series = chart1.Series.Add("Series " + (chart1.Series.Count).ToString());
            //        series.ChartType = SeriesChartType.FastLine;
            //        series.ChartArea = "Default";
            //        series.BorderWidth = 2;
            //        series.BorderColor = Color.FromArgb(120, 147, 190);
            //        series.ShadowColor = Color.FromArgb(64, 0, 0, 0);
            //        series.ShadowOffset = 2;
            //    }


            //foreach (var state in states)
            //{
            //    chart1.Series[0].Points.AddXY(state.DateTime_RecvDate, state.Temp_HeatingBox);
            //    chart1.Series[1].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorBox);
            //    chart1.Series[2].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorIn);
            //    chart1.Series[3].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorOut);
            //    chart1.Series[4].Points.AddXY(state.DateTime_RecvDate, state.Temp_Ambient);
            //}
        }
    }
}
