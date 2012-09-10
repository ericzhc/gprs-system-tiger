﻿using System;
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
            //chart1.DataBind();
        }

        private void F_History_Load(object sender, EventArgs e)
        {
            // show an X label every 3 Minute

            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:g}";
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();

            //chartArea1.Area3DStyle.Inclination = 15;
            //chartArea1.Area3DStyle.IsClustered = true;
            //chartArea1.Area3DStyle.IsRightAngleAxes = false;
            //chartArea1.Area3DStyle.Perspective = 10;
            //chartArea1.Area3DStyle.Rotation = 10;
            //chartArea1.Area3DStyle.WallWidth = 0;
            //chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            //chartArea1.AxisX.LabelStyle.Format = "MMM dd";
            //chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            //chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            //chartArea1.AxisY.IsStartedFromZero = false;
            //chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            //chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            //chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            //chartArea1.BackColor = System.Drawing.Color.OldLace;
            //chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            //chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            //chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            //chartArea1.Name = "Default";
            //chartArea1.ShadowColor = System.Drawing.Color.Transparent;
            //this.chart1.ChartAreas.Add(chartArea1);

            context = new db_tigerEntities();

            //try
            //{
            //    // Create a query for orders and related items.
            //    var orderQuery = context.tb_unit_state
            //        .Where("it.UnitID = '13695655652'");

            //    // Set the data source of the binding source to the ObjectResult 
            //    // returned when the query is executed.
            //    chartBindingSource.DataSource =
            //        orderQuery.Execute(MergeOption.AppendOnly);
            //}
            //catch (EntitySqlException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            dateTimePicker_from.Format = DateTimePickerFormat.Long;
            dateTimePicker_from.CustomFormat = "MM/dd/yyyy HHH:mm";

            dateTimePicker_to.Format = DateTimePickerFormat.Long;
            dateTimePicker_to.CustomFormat = "MM/dd/yyyy HHH:mm";

        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            //StringBuilder conditionstring = new StringBuilder();

            //if ((checkBox1.Checked) &&(!comboBox1.SelectedItem.Equals(null)))
            //{
            //    conditionstring.Append("it.UnitID = '" + comboBox1.SelectedItem.ToString() + "'");
            //}
            //if ((checkBox2.Checked) && (dateTimePicker_from.Value < dateTimePicker_to.Value))
            //{
            //    conditionstring.Append("and it.DateTime_RecvDate > " + dateTimePicker_from.Value + "'");
            //    conditionstring.Append("and it.DateTime_RecvDate < " + dateTimePicker_to.Value + "'");
            //}

            //{

            //    context = new db_tigerEntities();
            //    //CompareTo( p.StartDate)>=0
            //    try
            //    {
            //        // Create a query for orders and related items.
            //        var orderQuery = context.tb_unit_state
            //            .Where("it.UnitID = '" + comboBox1.SelectedItem.ToString() + "'");

            //        // Set the data source of the binding source to the ObjectResult 
            //        // returned when the query is executed.
            //        chartBindingSource.DataSource =
            //            orderQuery.Execute(MergeOption.AppendOnly);
            //    }
            //    catch (EntitySqlException ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //    chart1.Invalidate();
            //}

        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            chart1.Series[4].Points.Clear();

            if ((checkBox1.Checked) && (!comboBox1.SelectedItem.Equals(null)) && (checkBox2.Checked) && (dateTimePicker_from.Value < dateTimePicker_to.Value))
            {
                string id = comboBox1.SelectedItem.ToString();
                var states =
                   from state in context.tb_unit_state
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
                        chart1.Series[0].Points.AddXY(state.DateTime_RecvDate, state.Temp_HeatingBox);
                        chart1.Series[1].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorBox);
                        chart1.Series[2].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorIn);
                        chart1.Series[3].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorOut);
                        chart1.Series[4].Points.AddXY(state.DateTime_RecvDate, state.Temp_Ambient);
                    }
                
            }
            else 
            {
                if (((checkBox1.Checked) && (!comboBox1.SelectedItem.Equals(null))))
                {
                    string id = comboBox1.SelectedItem.ToString();
                    var states =
                        from state in context.tb_unit_state
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
                        chart1.Series[0].Points.AddXY(state.DateTime_RecvDate, state.Temp_HeatingBox);
                        chart1.Series[1].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorBox);
                        chart1.Series[2].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorIn);
                        chart1.Series[3].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorOut);
                        chart1.Series[4].Points.AddXY(state.DateTime_RecvDate, state.Temp_Ambient);
                    }
                }

                if ((checkBox2.Checked) && (dateTimePicker_from.Value < dateTimePicker_to.Value))
                {
                    var states =
                        from state in context.tb_unit_state
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
                        chart1.Series[0].Points.AddXY(state.DateTime_RecvDate, state.Temp_HeatingBox);
                        chart1.Series[1].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorBox);
                        chart1.Series[2].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorIn);
                        chart1.Series[3].Points.AddXY(state.DateTime_RecvDate, state.Temp_CollectorOut);
                        chart1.Series[4].Points.AddXY(state.DateTime_RecvDate, state.Temp_Ambient);
                    }
                    
                }
            
            }

            chart1.Invalidate();

               
            }
        }
    }
   