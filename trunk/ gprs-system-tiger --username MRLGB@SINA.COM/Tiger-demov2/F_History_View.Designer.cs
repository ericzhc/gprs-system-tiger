namespace Tiger
{
    partial class F_History_View
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.viewchart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.viewbindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.viewentityDataSource = new EFWinforms.EntityDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.viewchart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewbindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // viewchart
            // 
            chartArea1.Name = "ChartArea1";
            this.viewchart.ChartAreas.Add(chartArea1);
            this.viewchart.DataSource = this.viewbindingSource;
            this.viewchart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.viewchart.Legends.Add(legend1);
            this.viewchart.Location = new System.Drawing.Point(0, 0);
            this.viewchart.Name = "viewchart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Temp_HeatingBox";
            series1.XValueMember = "DateTime_RecvDate";
            series1.YValueMembers = "Temp_HeatingBox";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Temp_CollectorBox";
            series2.XValueMember = "DateTime_RecvDate";
            series2.YValueMembers = "Temp_CollectorBox";
            this.viewchart.Series.Add(series1);
            this.viewchart.Series.Add(series2);
            this.viewchart.Size = new System.Drawing.Size(714, 277);
            this.viewchart.TabIndex = 0;
            this.viewchart.Text = "chart1";
            title1.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title1.DockedToChartArea = "ChartArea1";
            title1.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.ForeColor = System.Drawing.Color.Red;
            title1.IsDockedInsideChartArea = false;
            title1.Name = "Title1";
            title1.Text = "状态数据曲线图";
            this.viewchart.Titles.Add(title1);
            // 
            // viewbindingSource
            // 
            this.viewbindingSource.DataMember = "tb_unit_state";
            this.viewbindingSource.DataSource = this.viewentityDataSource;
            this.viewbindingSource.Position = 0;
            this.viewbindingSource.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.viewbindingSource_ListChanged);
            // 
            // viewentityDataSource
            // 
            this.viewentityDataSource.ObjectContextType = typeof(Tiger.DbTigerEntities);
            // 
            // F_History_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 277);
            this.Controls.Add(this.viewchart);
            this.Name = "F_History_View";
            this.Text = "F_History_View";
            ((System.ComponentModel.ISupportInitialize)(this.viewchart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewbindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart viewchart;
        private System.Windows.Forms.BindingSource viewbindingSource;
        private EFWinforms.EntityDataSource viewentityDataSource;
    }
}