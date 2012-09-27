using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Tiger
{
    public partial class F_HistoryUpdate : Form
    {
        private DbTigerEntities context;//数据库的上下文
        public F_HistoryUpdate()
        {
            InitializeComponent();
            context = new DbTigerEntities();
            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:g}";       

            {
                foreach (var union in context.unions)
                {
                    comboBox_ID.Items.Add(union.UnitId);
                }
            }
            PlotData();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            chart1.EnableZoomAndPanControls(ChartCursorSelected, ChartCursorMoved);
        }
        private void PlotData()
        {
            //int DataSizeBase = 1000; //Increase this number to plot more points

            Series Ser1 = chart1.Series[0];
            Series Ser2 = chart1.Series[1];
            Series Ser3 = chart1.Series[2];
            Series Ser4 = chart1.Series[3];
            Series Ser5 = chart1.Series[4];

            string id = "13695655652";
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
                Ser1.Points.AddXY(state.DateTime_RecvDate, state.Temp_HeatingBox);
                Ser2.Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorBox);
                Ser3.Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorIn);
                Ser4.Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorOut);
                Ser5.Points.AddXY(state.DateTime_RecvDate, state.Temp_Ambient);      
            }


        //    for (int x = 0; x < (10 * DataSizeBase); x++)
        //        Ser1.Points.AddXY(Math.PI * 0.1 * x, Math.Sin(Math.PI * 0.1 * x));

        //    Series Ser2 = chart1.Series[1];
        //    for (int x = 0; x < (5 * DataSizeBase); x++)
        //        Ser2.Points.AddXY(Math.PI * 0.2 * x, Math.Cos(Math.PI * 0.2 * x));
        }

        private void ClearData()
        {
            foreach (Series ptrSeries in chart1.Series)
                ptrSeries.ClearPoints();
        }

        private void btnPlot_Click(object sender, EventArgs e)
        {
            ClearData();
            StartStopWatch();
            PlotData();
            Application.DoEvents();
            CheckStopWatch("Plot datas");
        }

        private void btnClearDataFast_Click(object sender, EventArgs e)
        {
            StartStopWatch();
            ClearData();
            Application.DoEvents();
            CheckStopWatch("Clear datas");
        }

        private void btnClearDataSlow_Click(object sender, EventArgs e)
        {
            StartStopWatch();
            foreach (Series ptrSeries in chart1.Series)
                ptrSeries.Points.Clear();
            Application.DoEvents();
            CheckStopWatch("Clear datas");
        }

        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        private void StartStopWatch() { watch.Restart(); }
        private void CheckStopWatch(string message)
        {
            watch.Stop();
            MessageBox.Show(message + " took " + watch.ElapsedMilliseconds.ToString() + "ms");
        }

        private void ChartCursorSelected(double x, double y)
        {
            txtChartSelect.Text = x.ToString("F4") + ", " + y.ToString("F4");
        }
        private void ChartCursorMoved(double x, double y)
        {
            txtChartValue.Text = x.ToString("F4") + ", " + y.ToString("F4");
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text.StartsWith("Item"))
            {
                ToolStripMenuItem ptrMenu = (ToolStripMenuItem) e.ClickedItem;
                if (ptrMenu.HasDropDownItems) return;
                MessageBox.Show(ptrMenu.Text);
            }
        }

        private void item11ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }

        private void item12ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test2");
        }

        private void item13ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test3");
        }

        private void item14ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test4");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            chart1.DrawHorizontalLine(0.5, Color.Green, lineWidth: 3, lineStyle: ChartDashStyle.DashDot);
            chart1.DrawVerticalLine(750, Color.Orange, lineWidth: 3, lineStyle: ChartDashStyle.Dot);
            chart1.DrawRectangle(1000, -0.3, 500, 0.6, Color.Lime, lineWidth: 2);
            chart1.DrawLine(1500, 2000, -1, 1, Color.Pink, lineWidth: 2);
            chart1.AddText("Test chart message", 1000, 0.3, Color.White, textStyle: TextStyle.Shadow);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            chart1.Annotations.Clear();
        }

        private void button_Refrsh_Click(object sender, EventArgs e)
        {

        }
    }
}
