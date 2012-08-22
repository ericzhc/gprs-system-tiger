namespace Tiger
{
    partial class F_SystemSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_SystemSet));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.group = new System.Windows.Forms.GroupBox();
            this.Aperture_Area_txt = new System.Windows.Forms.TextBox();
            this.Flow_HeatUsing_txt = new System.Windows.Forms.TextBox();
            this.Flow_CollectorSys_txt = new System.Windows.Forms.TextBox();
            this.Auxiliary_power_txt = new System.Windows.Forms.TextBox();
            this.Aera_IrradiatedSum_txt = new System.Windows.Forms.TextBox();
            this.unit_alias_txt = new System.Windows.Forms.TextBox();
            this.unit_id_txt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.group.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.button3);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.group);
            this.splitContainer1.Size = new System.Drawing.Size(623, 398);
            this.splitContainer1.SplitterDistance = 215;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(215, 398);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(166, 365);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "label9";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(57, 365);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "label8";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(276, 288);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "Update";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(145, 288);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 288);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // group
            // 
            this.group.Controls.Add(this.Aperture_Area_txt);
            this.group.Controls.Add(this.Flow_HeatUsing_txt);
            this.group.Controls.Add(this.Flow_CollectorSys_txt);
            this.group.Controls.Add(this.Auxiliary_power_txt);
            this.group.Controls.Add(this.Aera_IrradiatedSum_txt);
            this.group.Controls.Add(this.unit_alias_txt);
            this.group.Controls.Add(this.unit_id_txt);
            this.group.Controls.Add(this.label7);
            this.group.Controls.Add(this.label6);
            this.group.Controls.Add(this.label5);
            this.group.Controls.Add(this.label4);
            this.group.Controls.Add(this.label3);
            this.group.Controls.Add(this.label2);
            this.group.Controls.Add(this.label1);
            this.group.Location = new System.Drawing.Point(16, 21);
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(292, 238);
            this.group.TabIndex = 0;
            this.group.TabStop = false;
            this.group.Text = "单元配置";
            // 
            // Aperture_Area_txt
            // 
            this.Aperture_Area_txt.Location = new System.Drawing.Point(166, 198);
            this.Aperture_Area_txt.Name = "Aperture_Area_txt";
            this.Aperture_Area_txt.Size = new System.Drawing.Size(110, 21);
            this.Aperture_Area_txt.TabIndex = 1;
            // 
            // Flow_HeatUsing_txt
            // 
            this.Flow_HeatUsing_txt.Location = new System.Drawing.Point(130, 168);
            this.Flow_HeatUsing_txt.Name = "Flow_HeatUsing_txt";
            this.Flow_HeatUsing_txt.Size = new System.Drawing.Size(110, 21);
            this.Flow_HeatUsing_txt.TabIndex = 1;
            // 
            // Flow_CollectorSys_txt
            // 
            this.Flow_CollectorSys_txt.Location = new System.Drawing.Point(152, 141);
            this.Flow_CollectorSys_txt.Name = "Flow_CollectorSys_txt";
            this.Flow_CollectorSys_txt.Size = new System.Drawing.Size(110, 21);
            this.Flow_CollectorSys_txt.TabIndex = 1;
            // 
            // Auxiliary_power_txt
            // 
            this.Auxiliary_power_txt.Location = new System.Drawing.Point(106, 110);
            this.Auxiliary_power_txt.Name = "Auxiliary_power_txt";
            this.Auxiliary_power_txt.Size = new System.Drawing.Size(110, 21);
            this.Auxiliary_power_txt.TabIndex = 1;
            // 
            // Aera_IrradiatedSum_txt
            // 
            this.Aera_IrradiatedSum_txt.Location = new System.Drawing.Point(94, 77);
            this.Aera_IrradiatedSum_txt.Name = "Aera_IrradiatedSum_txt";
            this.Aera_IrradiatedSum_txt.Size = new System.Drawing.Size(110, 21);
            this.Aera_IrradiatedSum_txt.TabIndex = 1;
            // 
            // unit_alias_txt
            // 
            this.unit_alias_txt.Location = new System.Drawing.Point(82, 49);
            this.unit_alias_txt.Name = "unit_alias_txt";
            this.unit_alias_txt.Size = new System.Drawing.Size(110, 21);
            this.unit_alias_txt.TabIndex = 1;
            // 
            // unit_id_txt
            // 
            this.unit_id_txt.Location = new System.Drawing.Point(76, 20);
            this.unit_id_txt.Name = "unit_id_txt";
            this.unit_id_txt.Size = new System.Drawing.Size(46, 21);
            this.unit_id_txt.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 201);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "太阳能集热器采光面积:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "热用户端出水流量:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "集热系统进出口流量:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "辅助热源功率:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "集热器面积:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "单元名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "GPIS_ID:";
            // 
            // F_SystemSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 398);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "F_SystemSet";
            this.Text = "系统配置";
            this.Load += new System.EventHandler(this.F_SystemSet_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.group.ResumeLayout(false);
            this.group.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox group;
        private System.Windows.Forms.TextBox unit_alias_txt;
        private System.Windows.Forms.TextBox unit_id_txt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Flow_HeatUsing_txt;
        private System.Windows.Forms.TextBox Flow_CollectorSys_txt;
        private System.Windows.Forms.TextBox Auxiliary_power_txt;
        private System.Windows.Forms.TextBox Aera_IrradiatedSum_txt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Aperture_Area_txt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}