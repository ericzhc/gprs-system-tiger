namespace Tiger
{
    partial class FNodeState
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("采集器节点0");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("采集器节点01");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("采集器节点02");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("采集器节点03");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("采集器节点04");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FNodeState));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txt_Fee_effect1 = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.txt_Energy_alternative1 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txt_Solar_assurance_year1 = new System.Windows.Forms.TextBox();
            this.txt_Solar_assurance_day1 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txt_Dust_emission1 = new System.Windows.Forms.TextBox();
            this.txt_Sulfur_emission1 = new System.Windows.Forms.TextBox();
            this.txt_Carbon_emission1 = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txt_System_efficiency1 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txt_System_heat1 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1016, 600);
            this.splitContainer1.SplitterDistance = 274;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点0";
            treeNode1.Text = "采集器节点0";
            treeNode2.Name = "节点1";
            treeNode2.Text = "采集器节点01";
            treeNode3.Name = "节点2";
            treeNode3.Text = "采集器节点02";
            treeNode4.Name = "节点3";
            treeNode4.Text = "采集器节点03";
            treeNode5.Name = "节点4";
            treeNode5.Text = "采集器节点04";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(274, 600);
            this.treeView1.TabIndex = 3;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Gprs_32.ico");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox8);
            this.groupBox1.Controls.Add(this.groupBox9);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(738, 280);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox8.AutoSize = true;
            this.groupBox8.Controls.Add(this.txt_Fee_effect1);
            this.groupBox8.Controls.Add(this.label30);
            this.groupBox8.Location = new System.Drawing.Point(385, 207);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(282, 68);
            this.groupBox8.TabIndex = 52;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "项目费效比：";
            // 
            // txt_Fee_effect1
            // 
            this.txt_Fee_effect1.ForeColor = System.Drawing.Color.Red;
            this.txt_Fee_effect1.Location = new System.Drawing.Point(185, 19);
            this.txt_Fee_effect1.Name = "txt_Fee_effect1";
            this.txt_Fee_effect1.Size = new System.Drawing.Size(83, 21);
            this.txt_Fee_effect1.TabIndex = 24;
            this.txt_Fee_effect1.Text = "0";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(12, 23);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(77, 12);
            this.label30.TabIndex = 0;
            this.label30.Text = "项目费效比：";
            // 
            // groupBox9
            // 
            this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox9.AutoSize = true;
            this.groupBox9.Controls.Add(this.txt_Energy_alternative1);
            this.groupBox9.Controls.Add(this.label20);
            this.groupBox9.Location = new System.Drawing.Point(51, 207);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(282, 69);
            this.groupBox9.TabIndex = 51;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "常规能源替代量：";
            // 
            // txt_Energy_alternative1
            // 
            this.txt_Energy_alternative1.ForeColor = System.Drawing.Color.Red;
            this.txt_Energy_alternative1.Location = new System.Drawing.Point(185, 20);
            this.txt_Energy_alternative1.Name = "txt_Energy_alternative1";
            this.txt_Energy_alternative1.Size = new System.Drawing.Size(80, 21);
            this.txt_Energy_alternative1.TabIndex = 24;
            this.txt_Energy_alternative1.Text = "0";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 23);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(101, 12);
            this.label20.TabIndex = 0;
            this.label20.Text = "常规能源替代量：";
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.AutoSize = true;
            this.groupBox7.Controls.Add(this.txt_Solar_assurance_year1);
            this.groupBox7.Controls.Add(this.txt_Solar_assurance_day1);
            this.groupBox7.Controls.Add(this.label18);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Location = new System.Drawing.Point(385, 92);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(282, 91);
            this.groupBox7.TabIndex = 50;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "系统太阳能保证率:";
            // 
            // txt_Solar_assurance_year1
            // 
            this.txt_Solar_assurance_year1.ForeColor = System.Drawing.Color.Red;
            this.txt_Solar_assurance_year1.Location = new System.Drawing.Point(185, 42);
            this.txt_Solar_assurance_year1.Name = "txt_Solar_assurance_year1";
            this.txt_Solar_assurance_year1.Size = new System.Drawing.Size(83, 21);
            this.txt_Solar_assurance_year1.TabIndex = 24;
            this.txt_Solar_assurance_year1.Text = "0";
            // 
            // txt_Solar_assurance_day1
            // 
            this.txt_Solar_assurance_day1.ForeColor = System.Drawing.Color.Red;
            this.txt_Solar_assurance_day1.Location = new System.Drawing.Point(185, 16);
            this.txt_Solar_assurance_day1.Name = "txt_Solar_assurance_day1";
            this.txt_Solar_assurance_day1.Size = new System.Drawing.Size(83, 21);
            this.txt_Solar_assurance_day1.TabIndex = 24;
            this.txt_Solar_assurance_day1.Text = "0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 42);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(107, 12);
            this.label18.TabIndex = 0;
            this.label18.Text = "太阳能全年保证率:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "太阳能日保证率:";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.AutoSize = true;
            this.groupBox5.Controls.Add(this.txt_Dust_emission1);
            this.groupBox5.Controls.Add(this.txt_Sulfur_emission1);
            this.groupBox5.Controls.Add(this.txt_Carbon_emission1);
            this.groupBox5.Controls.Add(this.label29);
            this.groupBox5.Controls.Add(this.label27);
            this.groupBox5.Controls.Add(this.label28);
            this.groupBox5.Location = new System.Drawing.Point(51, 92);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(282, 110);
            this.groupBox5.TabIndex = 49;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "环境效益";
            // 
            // txt_Dust_emission1
            // 
            this.txt_Dust_emission1.ForeColor = System.Drawing.Color.Red;
            this.txt_Dust_emission1.Location = new System.Drawing.Point(185, 61);
            this.txt_Dust_emission1.Name = "txt_Dust_emission1";
            this.txt_Dust_emission1.Size = new System.Drawing.Size(80, 21);
            this.txt_Dust_emission1.TabIndex = 24;
            this.txt_Dust_emission1.Text = "0";
            // 
            // txt_Sulfur_emission1
            // 
            this.txt_Sulfur_emission1.ForeColor = System.Drawing.Color.Red;
            this.txt_Sulfur_emission1.Location = new System.Drawing.Point(185, 37);
            this.txt_Sulfur_emission1.Name = "txt_Sulfur_emission1";
            this.txt_Sulfur_emission1.Size = new System.Drawing.Size(80, 21);
            this.txt_Sulfur_emission1.TabIndex = 24;
            this.txt_Sulfur_emission1.Text = "0";
            // 
            // txt_Carbon_emission1
            // 
            this.txt_Carbon_emission1.ForeColor = System.Drawing.Color.Red;
            this.txt_Carbon_emission1.Location = new System.Drawing.Point(185, 14);
            this.txt_Carbon_emission1.Name = "txt_Carbon_emission1";
            this.txt_Carbon_emission1.Size = new System.Drawing.Size(80, 21);
            this.txt_Carbon_emission1.TabIndex = 24;
            this.txt_Carbon_emission1.Text = "0";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(12, 61);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(125, 12);
            this.label29.TabIndex = 0;
            this.label29.Text = "粉尘减排量（吨/年）:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(12, 40);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(149, 12);
            this.label27.TabIndex = 0;
            this.label27.Text = "二氧化硫减排量（吨/年）:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(12, 17);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(149, 12);
            this.label28.TabIndex = 0;
            this.label28.Text = "二氧化碳减排量（吨/年）:";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.AutoSize = true;
            this.groupBox4.Controls.Add(this.txt_System_efficiency1);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Location = new System.Drawing.Point(385, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(282, 63);
            this.groupBox4.TabIndex = 47;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "太阳能集热系统效率:";
            // 
            // txt_System_efficiency1
            // 
            this.txt_System_efficiency1.ForeColor = System.Drawing.Color.Red;
            this.txt_System_efficiency1.Location = new System.Drawing.Point(185, 14);
            this.txt_System_efficiency1.Name = "txt_System_efficiency1";
            this.txt_System_efficiency1.Size = new System.Drawing.Size(80, 21);
            this.txt_System_efficiency1.TabIndex = 24;
            this.txt_System_efficiency1.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(12, 23);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(113, 12);
            this.label19.TabIndex = 0;
            this.label19.Text = "太阳能集热系统效率";
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.AutoSize = true;
            this.groupBox6.Controls.Add(this.txt_System_heat1);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Location = new System.Drawing.Point(51, 20);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(282, 63);
            this.groupBox6.TabIndex = 48;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "系统有用得热量:";
            // 
            // txt_System_heat1
            // 
            this.txt_System_heat1.ForeColor = System.Drawing.Color.Red;
            this.txt_System_heat1.Location = new System.Drawing.Point(185, 14);
            this.txt_System_heat1.Name = "txt_System_heat1";
            this.txt_System_heat1.Size = new System.Drawing.Size(80, 21);
            this.txt_System_heat1.TabIndex = 24;
            this.txt_System_heat1.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(155, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "太阳能集热系统有用得热量:";
            // 
            // FNodeState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 600);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FNodeState";
            this.Text = "节点状态";
            this.Load += new System.EventHandler(this.FNodeStateLoad);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txt_System_efficiency1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txt_System_heat1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txt_Solar_assurance_year1;
        private System.Windows.Forms.TextBox txt_Solar_assurance_day1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txt_Dust_emission1;
        private System.Windows.Forms.TextBox txt_Sulfur_emission1;
        private System.Windows.Forms.TextBox txt_Carbon_emission1;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox txt_Fee_effect1;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox txt_Energy_alternative1;
        private System.Windows.Forms.Label label20;
    }
}