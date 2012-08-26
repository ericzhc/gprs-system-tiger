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
        private db_tigerEntities context;

        public F_History()
        {
            InitializeComponent();
        }

        private void chartBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            ApplyFilter();
            chart1.DataBind();
        }

        private void F_History_Load(object sender, EventArgs e)
        {
            // show an X label every 3 Minute

            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:g}";
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();

            chartArea1.Area3DStyle.Inclination = 15;
            chartArea1.Area3DStyle.IsClustered = true;
            chartArea1.Area3DStyle.IsRightAngleAxes = false;
            chartArea1.Area3DStyle.Perspective = 10;
            chartArea1.Area3DStyle.Rotation = 10;
            chartArea1.Area3DStyle.WallWidth = 0;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisX.LabelStyle.Format = "MMM dd";
            chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.BackColor = System.Drawing.Color.OldLace;
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.Name = "Default";
            chartArea1.ShadowColor = System.Drawing.Color.Transparent;
            this.chart1.ChartAreas.Add(chartArea1);

            context = new db_tigerEntities();

            try
            {
                // Create a query for orders and related items.
                var orderQuery = context.tb_unit_state
                    .Where("it.UnitID = '13695655652'");

                // Set the data source of the binding source to the ObjectResult 
                // returned when the query is executed.
                chartBindingSource.DataSource =
                    orderQuery.Execute(MergeOption.AppendOnly);
            }
            catch (EntitySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // apply the filter
        private void ApplyFilter()
        {
            //// never show discontinued products
            //var filter = " ";

            //// apply minimum price condition
            //var minPrice = _txtMin.Text.Trim();
            //if (!string.IsNullOrEmpty(minPrice))
            //{
            //    double d;
            //    if (!double.TryParse(minPrice, out d))
            //    {
            //        MessageBox.Show("Invalid Minimum Unit Price, please try again.");
            //    }
            //    else
            //    {
            //        filter += string.Format("(Temp_HeatingBox >= {0})", minPrice);
            //    }
            //}

            //// set the filter
            //chartBindingSource.Filter = filter;
        }

        private void _txtMin_Validated(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void _txtMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ApplyFilter();
                e.Handled = true;
            }
        }
    }
}
