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
            legend1.Name = "Legend1";
            this.viewchart.Legends.Add(legend1);
            this.viewchart.Location = new System.Drawing.Point(40, 26);
            this.viewchart.Name = "viewchart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.viewchart.Series.Add(series1);
            this.viewchart.Size = new System.Drawing.Size(460, 208);
            this.viewchart.TabIndex = 0;
            this.viewchart.Text = "chart1";
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
            this.ClientSize = new System.Drawing.Size(564, 277);
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