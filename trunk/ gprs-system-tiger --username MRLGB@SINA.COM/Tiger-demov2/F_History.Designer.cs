namespace Tiger
{
    partial class F_History
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
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.entityDataSource1 = new EFWinforms.EntityDataSource(this.components);
            this._txtMin = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.DataSource = this.chartBindingSource;
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "state";
            series1.XValueMember = "DateTime_RecvDate";
            series1.YValueMembers = "Temp_HeatingBox";
            series1.YValuesPerPoint = 2;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(914, 463);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.ForeColor = System.Drawing.Color.Maroon;
            title1.Name = "Title1";
            title1.Text = "采集单元在线时间统计图表";
            this.chart1.Titles.Add(title1);
            // 
            // chartBindingSource
            // 
            this.chartBindingSource.DataMember = "tb_unit_state";
            this.chartBindingSource.DataSource = this.entityDataSource1;
            this.chartBindingSource.Filter = "";
            this.chartBindingSource.Position = 0;
            this.chartBindingSource.Sort = "";
            this.chartBindingSource.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.chartBindingSource_ListChanged);
            // 
            // entityDataSource1
            // 
            this.entityDataSource1.ObjectContextType = typeof(Tiger.db_tigerEntities);
            // 
            // _txtMin
            // 
            this._txtMin.Location = new System.Drawing.Point(202, 13);
            this._txtMin.Name = "_txtMin";
            this._txtMin.Size = new System.Drawing.Size(100, 21);
            this._txtMin.TabIndex = 1;
            this._txtMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._txtMin_KeyPress);
            this._txtMin.Validated += new System.EventHandler(this._txtMin_Validated);
            // 
            // F_History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 463);
            this.Controls.Add(this._txtMin);
            this.Controls.Add(this.chart1);
            this.Name = "F_History";
            this.Text = "F_history";
            this.Load += new System.EventHandler(this.F_History_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.BindingSource chartBindingSource;
        private EFWinforms.EntityDataSource entityDataSource1;
        private System.Windows.Forms.TextBox _txtMin;
    }
}