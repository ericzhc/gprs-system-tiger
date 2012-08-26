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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.viewchart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.viewbindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.viewentityDataSource = new EFWinforms.EntityDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.viewchart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewbindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // viewchart
            // 
            chartArea2.Name = "ChartArea1";
            this.viewchart.ChartAreas.Add(chartArea2);
            this.viewchart.DataSource = this.viewbindingSource;
            legend2.Name = "Legend1";
            this.viewchart.Legends.Add(legend2);
            this.viewchart.Location = new System.Drawing.Point(12, 26);
            this.viewchart.Name = "viewchart";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Legend1";
            series3.Name = "Temp_HeatingBox";
            series3.XValueMember = "DateTime_RecvDate";
            series3.YValueMembers = "Temp_HeatingBox";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Legend = "Legend1";
            series4.Name = "Temp_CollectorBox";
            series4.XValueMember = "DateTime_RecvDate";
            series4.YValueMembers = "Temp_CollectorBox";
            this.viewchart.Series.Add(series3);
            this.viewchart.Series.Add(series4);
            this.viewchart.Size = new System.Drawing.Size(690, 208);
            this.viewchart.TabIndex = 0;
            this.viewchart.Text = "chart1";
            title2.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title2.DockedToChartArea = "ChartArea1";
            title2.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.ForeColor = System.Drawing.Color.Red;
            title2.IsDockedInsideChartArea = false;
            title2.Name = "Title1";
            title2.Text = "状态数据曲线图";
            this.viewchart.Titles.Add(title2);
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
            this.viewentityDataSource.ObjectContextType = typeof(Tiger.db_tigerEntities);
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