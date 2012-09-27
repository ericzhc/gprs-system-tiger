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
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_History));
            this.chartBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_T_Ambient = new System.Windows.Forms.CheckBox();
            this.checkBoxT_CollectorOut = new System.Windows.Forms.CheckBox();
            this.checkBox_T_CollectorIn = new System.Windows.Forms.CheckBox();
            this.checkBox_T_CollectorBox = new System.Windows.Forms.CheckBox();
            this.checkBox_T_HeatingBox = new System.Windows.Forms.CheckBox();
            this.button_Refrsh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_to = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_from = new System.Windows.Forms.DateTimePicker();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.comboBox_ID = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.entityDataSource1 = new EFWinforms.EntityDataSource(this.components);
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chartBindingSource
            // 
            this.chartBindingSource.DataSource = this.entityDataSource1;
            this.chartBindingSource.Filter = "";
            this.chartBindingSource.Position = 0;
            this.chartBindingSource.Sort = "";
            this.chartBindingSource.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.chartBindingSource_ListChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chart1);
            this.splitContainer1.Size = new System.Drawing.Size(1004, 601);
            this.splitContainer1.SplitterDistance = 130;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_T_Ambient);
            this.groupBox1.Controls.Add(this.checkBoxT_CollectorOut);
            this.groupBox1.Controls.Add(this.checkBox_T_CollectorIn);
            this.groupBox1.Controls.Add(this.checkBox_T_CollectorBox);
            this.groupBox1.Controls.Add(this.checkBox_T_HeatingBox);
            this.groupBox1.Controls.Add(this.button_Refrsh);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dateTimePicker_to);
            this.groupBox1.Controls.Add(this.dateTimePicker_from);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.comboBox_ID);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1004, 130);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据筛选：";
            // 
            // checkBox_T_Ambient
            // 
            this.checkBox_T_Ambient.AutoSize = true;
            this.checkBox_T_Ambient.Location = new System.Drawing.Point(571, 98);
            this.checkBox_T_Ambient.Name = "checkBox_T_Ambient";
            this.checkBox_T_Ambient.Size = new System.Drawing.Size(78, 16);
            this.checkBox_T_Ambient.TabIndex = 7;
            this.checkBox_T_Ambient.Text = "T_Ambient";
            this.checkBox_T_Ambient.UseVisualStyleBackColor = true;
            this.checkBox_T_Ambient.CheckedChanged += new System.EventHandler(this.checkBox_T_Ambient_CheckedChanged);
            // 
            // checkBoxT_CollectorOut
            // 
            this.checkBoxT_CollectorOut.AutoSize = true;
            this.checkBoxT_CollectorOut.Location = new System.Drawing.Point(438, 98);
            this.checkBoxT_CollectorOut.Name = "checkBoxT_CollectorOut";
            this.checkBoxT_CollectorOut.Size = new System.Drawing.Size(108, 16);
            this.checkBoxT_CollectorOut.TabIndex = 7;
            this.checkBoxT_CollectorOut.Text = "T_CollectorOut";
            this.checkBoxT_CollectorOut.UseVisualStyleBackColor = true;
            this.checkBoxT_CollectorOut.CheckedChanged += new System.EventHandler(this.checkBoxT_CollectorOut_CheckedChanged);
            // 
            // checkBox_T_CollectorIn
            // 
            this.checkBox_T_CollectorIn.AutoSize = true;
            this.checkBox_T_CollectorIn.Location = new System.Drawing.Point(299, 98);
            this.checkBox_T_CollectorIn.Name = "checkBox_T_CollectorIn";
            this.checkBox_T_CollectorIn.Size = new System.Drawing.Size(102, 16);
            this.checkBox_T_CollectorIn.TabIndex = 7;
            this.checkBox_T_CollectorIn.Text = "T_CollectorIn";
            this.checkBox_T_CollectorIn.UseVisualStyleBackColor = true;
            this.checkBox_T_CollectorIn.CheckedChanged += new System.EventHandler(this.checkBox_T_CollectorIn_CheckedChanged);
            // 
            // checkBox_T_CollectorBox
            // 
            this.checkBox_T_CollectorBox.AutoSize = true;
            this.checkBox_T_CollectorBox.Location = new System.Drawing.Point(160, 98);
            this.checkBox_T_CollectorBox.Name = "checkBox_T_CollectorBox";
            this.checkBox_T_CollectorBox.Size = new System.Drawing.Size(108, 16);
            this.checkBox_T_CollectorBox.TabIndex = 7;
            this.checkBox_T_CollectorBox.Text = "T_CollectorBox";
            this.checkBox_T_CollectorBox.UseVisualStyleBackColor = true;
            this.checkBox_T_CollectorBox.CheckedChanged += new System.EventHandler(this.checkBox_T_CollectorBox_CheckedChanged);
            // 
            // checkBox_T_HeatingBox
            // 
            this.checkBox_T_HeatingBox.AutoSize = true;
            this.checkBox_T_HeatingBox.Location = new System.Drawing.Point(43, 98);
            this.checkBox_T_HeatingBox.Name = "checkBox_T_HeatingBox";
            this.checkBox_T_HeatingBox.Size = new System.Drawing.Size(96, 16);
            this.checkBox_T_HeatingBox.TabIndex = 7;
            this.checkBox_T_HeatingBox.Text = "T_HeatingBox";
            this.checkBox_T_HeatingBox.UseVisualStyleBackColor = true;
            this.checkBox_T_HeatingBox.CheckedChanged += new System.EventHandler(this.checkBox_T_HeatingBox_CheckedChanged);
            // 
            // button_Refrsh
            // 
            this.button_Refrsh.Location = new System.Drawing.Point(670, 63);
            this.button_Refrsh.Name = "button_Refrsh";
            this.button_Refrsh.Size = new System.Drawing.Size(142, 23);
            this.button_Refrsh.TabIndex = 6;
            this.button_Refrsh.Text = "自定义查询";
            this.button_Refrsh.UseVisualStyleBackColor = true;
            this.button_Refrsh.Click += new System.EventHandler(this.button_Refrsh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(341, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "——";
            // 
            // dateTimePicker_to
            // 
            this.dateTimePicker_to.Location = new System.Drawing.Point(400, 57);
            this.dateTimePicker_to.Name = "dateTimePicker_to";
            this.dateTimePicker_to.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker_to.TabIndex = 4;
            // 
            // dateTimePicker_from
            // 
            this.dateTimePicker_from.Location = new System.Drawing.Point(135, 58);
            this.dateTimePicker_from.Name = "dateTimePicker_from";
            this.dateTimePicker_from.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker_from.TabIndex = 3;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(43, 58);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(60, 16);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "时间：";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // comboBox_ID
            // 
            this.comboBox_ID.FormattingEnabled = true;
            this.comboBox_ID.Location = new System.Drawing.Point(135, 21);
            this.comboBox_ID.Name = "comboBox_ID";
            this.comboBox_ID.Size = new System.Drawing.Size(121, 20);
            this.comboBox_ID.TabIndex = 1;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(43, 21);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "unitid =";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // entityDataSource1
            // 
            this.entityDataSource1.ObjectContextType = typeof(Tiger.DbTigerEntities);
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.DataSource = this.entityDataSource1;
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "T_HeatingBox";
            series1.XValueMember = "DateTime_RecvDate";
            series1.YValueMembers = "Temp_HeatingBox";
            series1.YValuesPerPoint = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "T_CollectorBox";
            series2.XValueMember = "DateTime_RecvDate";
            series2.YValueMembers = "Temp_CollectorBox";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Legend1";
            series3.Name = "T_CollectorIn";
            series3.XValueMember = "DateTime_RecvDate";
            series3.YValueMembers = "Temp_CollectorIn";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Legend = "Legend1";
            series4.Name = "T_CollectorOut";
            series4.XValueMember = "DateTime_RecvDate";
            series4.YValueMembers = "Temp_CollectorOut";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Legend = "Legend1";
            series5.Name = "T_Ambient";
            series5.XValueMember = "DateTime_RecvDate";
            series5.YValueMembers = "Temp_Ambient";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Series.Add(series5);
            this.chart1.Size = new System.Drawing.Size(1004, 467);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.ForeColor = System.Drawing.Color.Maroon;
            title1.Name = "Title1";
            title1.Text = "采集单元状态统计图表";
            this.chart1.Titles.Add(title1);
            // 
            // F_History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 601);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "F_History";
            this.Text = "F_history";
            this.Load += new System.EventHandler(this.F_History_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartBindingSource)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource chartBindingSource;
        private EFWinforms.EntityDataSource entityDataSource1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox_ID;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker_to;
        private System.Windows.Forms.DateTimePicker dateTimePicker_from;
        private System.Windows.Forms.Button button_Refrsh;
        private System.Windows.Forms.CheckBox checkBox_T_HeatingBox;
        private System.Windows.Forms.CheckBox checkBox_T_CollectorIn;
        private System.Windows.Forms.CheckBox checkBox_T_CollectorBox;
        private System.Windows.Forms.CheckBox checkBoxT_CollectorOut;
        private System.Windows.Forms.CheckBox checkBox_T_Ambient;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}