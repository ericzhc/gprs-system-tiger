namespace Tiger
{
    partial class F_NodeManager
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button_ok = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_Auxiliary_power = new System.Windows.Forms.TextBox();
            this.textBox_Flow_CollectorSys = new System.Windows.Forms.TextBox();
            this.textBox_Flow_HeatUsing = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button_Del = new System.Windows.Forms.Button();
            this.button_Modify = new System.Windows.Forms.Button();
            this.button_ADD = new System.Windows.Forms.Button();
            this.textBox_VolumHeat = new System.Windows.Forms.TextBox();
            this.textBox_Alias = new System.Windows.Forms.TextBox();
            this.textBox_AreaIrr = new System.Windows.Forms.TextBox();
            this.textBox_Unitid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.textBox_SystemHeat = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(732, 150);
            this.dataGridView1.TabIndex = 12;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(372, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "定时记录时间间隔（秒）：";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(552, 25);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(85, 21);
            this.numericUpDown1.TabIndex = 14;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(497, 520);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 15;
            this.button_ok.Text = "ok";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_SystemHeat);
            this.groupBox1.Controls.Add(this.textBox_Auxiliary_power);
            this.groupBox1.Controls.Add(this.textBox_Flow_CollectorSys);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBox_Flow_HeatUsing);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.button_Del);
            this.groupBox1.Controls.Add(this.button_Modify);
            this.groupBox1.Controls.Add(this.button_ADD);
            this.groupBox1.Controls.Add(this.textBox_VolumHeat);
            this.groupBox1.Controls.Add(this.textBox_Alias);
            this.groupBox1.Controls.Add(this.textBox_AreaIrr);
            this.groupBox1.Controls.Add(this.textBox_Unitid);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 169);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(727, 345);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "详细设置：";
            // 
            // textBox_Auxiliary_power
            // 
            this.textBox_Auxiliary_power.Location = new System.Drawing.Point(182, 269);
            this.textBox_Auxiliary_power.Name = "textBox_Auxiliary_power";
            this.textBox_Auxiliary_power.Size = new System.Drawing.Size(100, 21);
            this.textBox_Auxiliary_power.TabIndex = 43;
            this.textBox_Auxiliary_power.Tag = "6";
            // 
            // textBox_Flow_CollectorSys
            // 
            this.textBox_Flow_CollectorSys.Location = new System.Drawing.Point(182, 188);
            this.textBox_Flow_CollectorSys.Name = "textBox_Flow_CollectorSys";
            this.textBox_Flow_CollectorSys.Size = new System.Drawing.Size(100, 21);
            this.textBox_Flow_CollectorSys.TabIndex = 44;
            this.textBox_Flow_CollectorSys.Tag = "4";
            // 
            // textBox_Flow_HeatUsing
            // 
            this.textBox_Flow_HeatUsing.Location = new System.Drawing.Point(182, 229);
            this.textBox_Flow_HeatUsing.Name = "textBox_Flow_HeatUsing";
            this.textBox_Flow_HeatUsing.Size = new System.Drawing.Size(100, 21);
            this.textBox_Flow_HeatUsing.TabIndex = 45;
            this.textBox_Flow_HeatUsing.Tag = "5";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 235);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 12);
            this.label7.TabIndex = 40;
            this.label7.Text = "热用户端出水流量(㎏/s)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(75, 275);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 12);
            this.label8.TabIndex = 41;
            this.label8.Text = "辅助热源功率(kw)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 12);
            this.label5.TabIndex = 42;
            this.label5.Text = "集热系统进出口流量(㎏/s)";
            // 
            // button_Del
            // 
            this.button_Del.Location = new System.Drawing.Point(484, 310);
            this.button_Del.Name = "button_Del";
            this.button_Del.Size = new System.Drawing.Size(75, 23);
            this.button_Del.TabIndex = 39;
            this.button_Del.Text = "删除";
            this.button_Del.UseVisualStyleBackColor = true;
            this.button_Del.Click += new System.EventHandler(this.button_Del_Click);
            // 
            // button_Modify
            // 
            this.button_Modify.Location = new System.Drawing.Point(324, 310);
            this.button_Modify.Name = "button_Modify";
            this.button_Modify.Size = new System.Drawing.Size(75, 23);
            this.button_Modify.TabIndex = 38;
            this.button_Modify.Text = "修改信息";
            this.button_Modify.UseVisualStyleBackColor = true;
            this.button_Modify.Click += new System.EventHandler(this.button_Modify_Click);
            // 
            // button_ADD
            // 
            this.button_ADD.Location = new System.Drawing.Point(182, 310);
            this.button_ADD.Name = "button_ADD";
            this.button_ADD.Size = new System.Drawing.Size(75, 23);
            this.button_ADD.TabIndex = 37;
            this.button_ADD.Text = "添加";
            this.button_ADD.UseVisualStyleBackColor = true;
            this.button_ADD.Click += new System.EventHandler(this.button_ADD_Click);
            // 
            // textBox_VolumHeat
            // 
            this.textBox_VolumHeat.Location = new System.Drawing.Point(182, 142);
            this.textBox_VolumHeat.Name = "textBox_VolumHeat";
            this.textBox_VolumHeat.Size = new System.Drawing.Size(100, 21);
            this.textBox_VolumHeat.TabIndex = 32;
            this.textBox_VolumHeat.Tag = "3";
            // 
            // textBox_Alias
            // 
            this.textBox_Alias.Location = new System.Drawing.Point(182, 62);
            this.textBox_Alias.Name = "textBox_Alias";
            this.textBox_Alias.Size = new System.Drawing.Size(100, 21);
            this.textBox_Alias.TabIndex = 33;
            this.textBox_Alias.Tag = "1";
            // 
            // textBox_AreaIrr
            // 
            this.textBox_AreaIrr.Location = new System.Drawing.Point(182, 102);
            this.textBox_AreaIrr.Name = "textBox_AreaIrr";
            this.textBox_AreaIrr.Size = new System.Drawing.Size(100, 21);
            this.textBox_AreaIrr.TabIndex = 34;
            this.textBox_AreaIrr.Tag = "2";
            // 
            // textBox_Unitid
            // 
            this.textBox_Unitid.Location = new System.Drawing.Point(182, 29);
            this.textBox_Unitid.Name = "textBox_Unitid";
            this.textBox_Unitid.Size = new System.Drawing.Size(100, 21);
            this.textBox_Unitid.TabIndex = 35;
            this.textBox_Unitid.Tag = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(69, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 29;
            this.label4.Text = "加热水箱容积(m³)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(135, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 28;
            this.label3.Text = "别名:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "集热器面积(㎡):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 31;
            this.label1.Text = "上报号码:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(396, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "统计时间间隔（秒）：";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(552, 60);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(85, 21);
            this.numericUpDown2.TabIndex = 14;
            // 
            // textBox_SystemHeat
            // 
            this.textBox_SystemHeat.Location = new System.Drawing.Point(537, 138);
            this.textBox_SystemHeat.Name = "textBox_SystemHeat";
            this.textBox_SystemHeat.Size = new System.Drawing.Size(100, 21);
            this.textBox_SystemHeat.TabIndex = 43;
            this.textBox_SystemHeat.Tag = "6";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(396, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "统计时间间隔（秒）：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(396, 145);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(149, 12);
            this.label11.TabIndex = 13;
            this.label11.Text = "统计系统得热量初值（）：";
            // 
            // F_NodeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 544);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.dataGridView1);
            this.Name = "F_NodeManager";
            this.Text = "F_SystemConfig";
            this.Load += new System.EventHandler(this.F_SystemConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_Del;
        private System.Windows.Forms.Button button_Modify;
        private System.Windows.Forms.Button button_ADD;
        private System.Windows.Forms.TextBox textBox_VolumHeat;
        private System.Windows.Forms.TextBox textBox_Alias;
        private System.Windows.Forms.TextBox textBox_AreaIrr;
        private System.Windows.Forms.TextBox textBox_Unitid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox textBox_Auxiliary_power;
        private System.Windows.Forms.TextBox textBox_Flow_CollectorSys;
        private System.Windows.Forms.TextBox textBox_Flow_HeatUsing;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.TextBox textBox_SystemHeat;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;

    }
}