namespace Tiger
{
    partial class F_SystemConfig
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
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.entityDataSource1 = new EFWinforms.EntityDataSource(this.components);
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.unitIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeonlineDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aliasDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aeraIrradiatedSumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.volumnHeatingBoxDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.starttimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stoptimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gprsstateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entityBindingNavigator1 = new EFWinforms.EntityBindingNavigator();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button_ok = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox4
            // 
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.entityDataSource1, "tb_union_list.Volumn_HeatingBox", true));
            this.textBox4.Location = new System.Drawing.Point(398, 87);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 21);
            this.textBox4.TabIndex = 7;
            // 
            // entityDataSource1
            // 
            this.entityDataSource1.ObjectContextType = typeof(Tiger.db_tigerEntities);
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.entityDataSource1, "tb_union_list.alias", true));
            this.textBox3.Location = new System.Drawing.Point(134, 93);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 21);
            this.textBox3.TabIndex = 8;
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.entityDataSource1, "tb_union_list.Aera_IrradiatedSum", true));
            this.textBox2.Location = new System.Drawing.Point(398, 53);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 9;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.entityDataSource1, "tb_union_list.UnitId", true));
            this.textBox1.Location = new System.Drawing.Point(134, 53);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(268, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "Volumn_HeatingBox";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Aera_IrradiatedSum";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "UnitID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "Details:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.unitIdDataGridViewTextBoxColumn,
            this.timeonlineDataGridViewTextBoxColumn,
            this.aliasDataGridViewTextBoxColumn,
            this.aeraIrradiatedSumDataGridViewTextBoxColumn,
            this.volumnHeatingBoxDataGridViewTextBoxColumn,
            this.starttimeDataGridViewTextBoxColumn,
            this.stoptimeDataGridViewTextBoxColumn,
            this.gprsstateDataGridViewTextBoxColumn});
            this.dataGridView1.DataMember = "tb_union_list";
            this.dataGridView1.DataSource = this.entityDataSource1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 168);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(567, 150);
            this.dataGridView1.TabIndex = 12;
            // 
            // unitIdDataGridViewTextBoxColumn
            // 
            this.unitIdDataGridViewTextBoxColumn.DataPropertyName = "UnitId";
            this.unitIdDataGridViewTextBoxColumn.HeaderText = "UnitId";
            this.unitIdDataGridViewTextBoxColumn.Name = "unitIdDataGridViewTextBoxColumn";
            // 
            // timeonlineDataGridViewTextBoxColumn
            // 
            this.timeonlineDataGridViewTextBoxColumn.DataPropertyName = "time_online";
            this.timeonlineDataGridViewTextBoxColumn.HeaderText = "time_online";
            this.timeonlineDataGridViewTextBoxColumn.Name = "timeonlineDataGridViewTextBoxColumn";
            this.timeonlineDataGridViewTextBoxColumn.Visible = false;
            // 
            // aliasDataGridViewTextBoxColumn
            // 
            this.aliasDataGridViewTextBoxColumn.DataPropertyName = "alias";
            this.aliasDataGridViewTextBoxColumn.HeaderText = "Name";
            this.aliasDataGridViewTextBoxColumn.Name = "aliasDataGridViewTextBoxColumn";
            // 
            // aeraIrradiatedSumDataGridViewTextBoxColumn
            // 
            this.aeraIrradiatedSumDataGridViewTextBoxColumn.DataPropertyName = "Aera_IrradiatedSum";
            this.aeraIrradiatedSumDataGridViewTextBoxColumn.HeaderText = "Aera_IrradiatedSum";
            this.aeraIrradiatedSumDataGridViewTextBoxColumn.Name = "aeraIrradiatedSumDataGridViewTextBoxColumn";
            // 
            // volumnHeatingBoxDataGridViewTextBoxColumn
            // 
            this.volumnHeatingBoxDataGridViewTextBoxColumn.DataPropertyName = "Volumn_HeatingBox";
            this.volumnHeatingBoxDataGridViewTextBoxColumn.HeaderText = "Volumn_HeatingBox";
            this.volumnHeatingBoxDataGridViewTextBoxColumn.Name = "volumnHeatingBoxDataGridViewTextBoxColumn";
            // 
            // starttimeDataGridViewTextBoxColumn
            // 
            this.starttimeDataGridViewTextBoxColumn.DataPropertyName = "Start_time";
            this.starttimeDataGridViewTextBoxColumn.HeaderText = "Start_time";
            this.starttimeDataGridViewTextBoxColumn.Name = "starttimeDataGridViewTextBoxColumn";
            this.starttimeDataGridViewTextBoxColumn.Visible = false;
            // 
            // stoptimeDataGridViewTextBoxColumn
            // 
            this.stoptimeDataGridViewTextBoxColumn.DataPropertyName = "Stop_time";
            this.stoptimeDataGridViewTextBoxColumn.HeaderText = "Stop_time";
            this.stoptimeDataGridViewTextBoxColumn.Name = "stoptimeDataGridViewTextBoxColumn";
            this.stoptimeDataGridViewTextBoxColumn.Visible = false;
            // 
            // gprsstateDataGridViewTextBoxColumn
            // 
            this.gprsstateDataGridViewTextBoxColumn.DataPropertyName = "gprs_state";
            this.gprsstateDataGridViewTextBoxColumn.HeaderText = "gprs_state";
            this.gprsstateDataGridViewTextBoxColumn.Name = "gprsstateDataGridViewTextBoxColumn";
            this.gprsstateDataGridViewTextBoxColumn.Visible = false;
            // 
            // entityBindingNavigator1
            // 
            this.entityBindingNavigator1.DataMember = "tb_union_list";
            this.entityBindingNavigator1.DataSource = this.entityDataSource1;
            this.entityBindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.entityBindingNavigator1.Name = "entityBindingNavigator1";
            this.entityBindingNavigator1.Size = new System.Drawing.Size(612, 25);
            this.entityBindingNavigator1.TabIndex = 0;
            this.entityBindingNavigator1.Text = "entityBindingNavigator1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 353);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "定时记录时间间隔（秒）：";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(182, 351);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(85, 21);
            this.numericUpDown1.TabIndex = 14;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(134, 386);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 15;
            this.button_ok.Text = "ok";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // F_SystemConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 430);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.entityBindingNavigator1);
            this.Name = "F_SystemConfig";
            this.Text = "F_SystemConfig";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private EFWinforms.EntityBindingNavigator entityBindingNavigator1;
        private EFWinforms.EntityDataSource entityDataSource1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeonlineDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aliasDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aeraIrradiatedSumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn volumnHeatingBoxDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn starttimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stoptimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gprsstateDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button_ok;

    }
}