namespace Tiger
{
    partial class FHistory
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
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FHistory));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_Refrsh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_to = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_from = new System.Windows.Forms.DateTimePicker();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.comboBox_ID = new System.Windows.Forms.ComboBox();
            this.checkBoxID = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.linkLabelAllNo = new System.Windows.Forms.LinkLabel();
            this.linkLabelAllYes = new System.Windows.Forms.LinkLabel();
            this.checkBox_T_Ambient = new System.Windows.Forms.CheckBox();
            this.checkBoxT_CollectorOut = new System.Windows.Forms.CheckBox();
            this.checkBox_T_CollectorIn = new System.Windows.Forms.CheckBox();
            this.checkBox_T_CollectorBox = new System.Windows.Forms.CheckBox();
            this.checkBox_T_HeatingBox = new System.Windows.Forms.CheckBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer_Refesh = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chart1);
            this.splitContainer1.Size = new System.Drawing.Size(1004, 601);
            this.splitContainer1.SplitterDistance = 130;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(1004, 130);
            this.splitContainer2.SplitterDistance = 568;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_Refrsh);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dateTimePicker_to);
            this.groupBox1.Controls.Add(this.dateTimePicker_from);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.comboBox_ID);
            this.groupBox1.Controls.Add(this.checkBoxID);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(568, 130);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据筛选：";
            // 
            // button_Refrsh
            // 
            this.button_Refrsh.Location = new System.Drawing.Point(213, 91);
            this.button_Refrsh.Name = "button_Refrsh";
            this.button_Refrsh.Size = new System.Drawing.Size(142, 23);
            this.button_Refrsh.TabIndex = 6;
            this.button_Refrsh.Text = "查询结果";
            this.button_Refrsh.UseVisualStyleBackColor = true;
            this.button_Refrsh.Click += new System.EventHandler(this.button_Refrsh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(315, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "——";
            // 
            // dateTimePicker_to
            // 
            this.dateTimePicker_to.Location = new System.Drawing.Point(350, 53);
            this.dateTimePicker_to.Name = "dateTimePicker_to";
            this.dateTimePicker_to.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker_to.TabIndex = 4;
            // 
            // dateTimePicker_from
            // 
            this.dateTimePicker_from.Location = new System.Drawing.Point(109, 54);
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
            this.comboBox_ID.Location = new System.Drawing.Point(135, 20);
            this.comboBox_ID.Name = "comboBox_ID";
            this.comboBox_ID.Size = new System.Drawing.Size(121, 20);
            this.comboBox_ID.TabIndex = 1;
            // 
            // checkBoxID
            // 
            this.checkBoxID.AutoSize = true;
            this.checkBoxID.Location = new System.Drawing.Point(43, 21);
            this.checkBoxID.Name = "checkBoxID";
            this.checkBoxID.Size = new System.Drawing.Size(90, 16);
            this.checkBoxID.TabIndex = 0;
            this.checkBoxID.Text = "Gprs 号码：";
            this.checkBoxID.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.linkLabelAllNo);
            this.groupBox2.Controls.Add(this.linkLabelAllYes);
            this.groupBox2.Controls.Add(this.checkBox_T_Ambient);
            this.groupBox2.Controls.Add(this.checkBoxT_CollectorOut);
            this.groupBox2.Controls.Add(this.checkBox_T_CollectorIn);
            this.groupBox2.Controls.Add(this.checkBox_T_CollectorBox);
            this.groupBox2.Controls.Add(this.checkBox_T_HeatingBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(432, 130);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据序列显示：";
            // 
            // linkLabelAllNo
            // 
            this.linkLabelAllNo.AutoSize = true;
            this.linkLabelAllNo.Location = new System.Drawing.Point(221, 96);
            this.linkLabelAllNo.Name = "linkLabelAllNo";
            this.linkLabelAllNo.Size = new System.Drawing.Size(41, 12);
            this.linkLabelAllNo.TabIndex = 13;
            this.linkLabelAllNo.TabStop = true;
            this.linkLabelAllNo.Text = "全不选";
            this.linkLabelAllNo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAllNo_LinkClicked);
            // 
            // linkLabelAllYes
            // 
            this.linkLabelAllYes.AutoSize = true;
            this.linkLabelAllYes.Location = new System.Drawing.Point(128, 96);
            this.linkLabelAllYes.Name = "linkLabelAllYes";
            this.linkLabelAllYes.Size = new System.Drawing.Size(29, 12);
            this.linkLabelAllYes.TabIndex = 13;
            this.linkLabelAllYes.TabStop = true;
            this.linkLabelAllYes.Text = "全选";
            this.linkLabelAllYes.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAllYes_LinkClicked);
            // 
            // checkBox_T_Ambient
            // 
            this.checkBox_T_Ambient.AutoSize = true;
            this.checkBox_T_Ambient.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox_T_Ambient.Location = new System.Drawing.Point(192, 59);
            this.checkBox_T_Ambient.Name = "checkBox_T_Ambient";
            this.checkBox_T_Ambient.Size = new System.Drawing.Size(78, 16);
            this.checkBox_T_Ambient.TabIndex = 8;
            this.checkBox_T_Ambient.Text = "T_Ambient";
            this.checkBox_T_Ambient.UseVisualStyleBackColor = true;
            this.checkBox_T_Ambient.CheckedChanged += new System.EventHandler(this.checkBox_T_Ambient_CheckedChanged);
            // 
            // checkBoxT_CollectorOut
            // 
            this.checkBoxT_CollectorOut.AutoSize = true;
            this.checkBoxT_CollectorOut.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxT_CollectorOut.Location = new System.Drawing.Point(39, 59);
            this.checkBoxT_CollectorOut.Name = "checkBoxT_CollectorOut";
            this.checkBoxT_CollectorOut.Size = new System.Drawing.Size(108, 16);
            this.checkBoxT_CollectorOut.TabIndex = 9;
            this.checkBoxT_CollectorOut.Text = "T_CollectorOut";
            this.checkBoxT_CollectorOut.UseVisualStyleBackColor = true;
            this.checkBoxT_CollectorOut.CheckedChanged += new System.EventHandler(this.checkBoxT_CollectorOut_CheckedChanged);
            // 
            // checkBox_T_CollectorIn
            // 
            this.checkBox_T_CollectorIn.AutoSize = true;
            this.checkBox_T_CollectorIn.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox_T_CollectorIn.Location = new System.Drawing.Point(297, 32);
            this.checkBox_T_CollectorIn.Name = "checkBox_T_CollectorIn";
            this.checkBox_T_CollectorIn.Size = new System.Drawing.Size(102, 16);
            this.checkBox_T_CollectorIn.TabIndex = 10;
            this.checkBox_T_CollectorIn.Text = "T_CollectorIn";
            this.checkBox_T_CollectorIn.UseVisualStyleBackColor = true;
            this.checkBox_T_CollectorIn.CheckedChanged += new System.EventHandler(this.checkBox_T_CollectorIn_CheckedChanged);
            // 
            // checkBox_T_CollectorBox
            // 
            this.checkBox_T_CollectorBox.AutoSize = true;
            this.checkBox_T_CollectorBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox_T_CollectorBox.Location = new System.Drawing.Point(162, 32);
            this.checkBox_T_CollectorBox.Name = "checkBox_T_CollectorBox";
            this.checkBox_T_CollectorBox.Size = new System.Drawing.Size(108, 16);
            this.checkBox_T_CollectorBox.TabIndex = 11;
            this.checkBox_T_CollectorBox.Text = "T_CollectorBox";
            this.checkBox_T_CollectorBox.UseVisualStyleBackColor = true;
            this.checkBox_T_CollectorBox.CheckedChanged += new System.EventHandler(this.checkBox_T_CollectorBox_CheckedChanged);
            // 
            // checkBox_T_HeatingBox
            // 
            this.checkBox_T_HeatingBox.AutoSize = true;
            this.checkBox_T_HeatingBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox_T_HeatingBox.Location = new System.Drawing.Point(51, 32);
            this.checkBox_T_HeatingBox.Name = "checkBox_T_HeatingBox";
            this.checkBox_T_HeatingBox.Size = new System.Drawing.Size(96, 16);
            this.checkBox_T_HeatingBox.TabIndex = 12;
            this.checkBox_T_HeatingBox.Text = "T_HeatingBox";
            this.checkBox_T_HeatingBox.UseVisualStyleBackColor = true;
            this.checkBox_T_HeatingBox.CheckedChanged += new System.EventHandler(this.checkBox_T_HeatingBox_CheckedChanged);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.chart1.BackSecondaryColor = System.Drawing.Color.White;
            this.chart1.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chart1.BorderlineWidth = 2;
            this.chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
            chartArea1.Area3DStyle.Inclination = 15;
            chartArea1.Area3DStyle.IsClustered = true;
            chartArea1.Area3DStyle.IsRightAngleAxes = false;
            chartArea1.Area3DStyle.Perspective = 10;
            chartArea1.Area3DStyle.Rotation = 10;
            chartArea1.Area3DStyle.WallWidth = 0;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.ScrollBar.BackColor = System.Drawing.Color.White;
            chartArea1.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            chartArea1.AxisX.ScrollBar.LineColor = System.Drawing.Color.Black;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.BackColor = System.Drawing.Color.Gainsboro;
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorX.SelectionColor = System.Drawing.Color.LightBlue;
            chartArea1.Name = "Default";
            chartArea1.ShadowColor = System.Drawing.Color.Transparent;
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            legend1.IsTextAutoFit = false;
            legend1.Name = "Default";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(1004, 467);
            this.chart1.TabIndex = 3;
            title1.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold);
            title1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            title1.Name = "Title1";
            title1.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            title1.ShadowOffset = 3;
            title1.Text = "采集单元状态统计图表";
            this.chart1.Titles.Add(title1);
            // 
            // timer_Refesh
            // 
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_Refrsh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker_to;
        private System.Windows.Forms.DateTimePicker dateTimePicker_from;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ComboBox comboBox_ID;
        private System.Windows.Forms.CheckBox checkBoxID;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel linkLabelAllNo;
        private System.Windows.Forms.LinkLabel linkLabelAllYes;
        private System.Windows.Forms.CheckBox checkBox_T_Ambient;
        private System.Windows.Forms.CheckBox checkBoxT_CollectorOut;
        private System.Windows.Forms.CheckBox checkBox_T_CollectorIn;
        private System.Windows.Forms.CheckBox checkBox_T_CollectorBox;
        private System.Windows.Forms.CheckBox checkBox_T_HeatingBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Timer timer_Refesh;
    }
}