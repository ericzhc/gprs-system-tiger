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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_SystemSet));
            this.closeForm = new System.Windows.Forms.Button();
            this.Refresh = new System.Windows.Forms.Button();
            this.unitGridView = new System.Windows.Forms.DataGridView();
            this.unitIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeonlineDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aliasDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aeraIrradiatedSumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.volumnHeatingBoxDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.starttimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stoptimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gprsstateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.config_entityDataSource = new EFWinforms.EntityDataSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Save = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.unitGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // closeForm
            // 
            this.closeForm.Location = new System.Drawing.Point(38, 284);
            this.closeForm.Name = "closeForm";
            this.closeForm.Size = new System.Drawing.Size(75, 23);
            this.closeForm.TabIndex = 1;
            this.closeForm.Text = "Close";
            this.closeForm.UseVisualStyleBackColor = true;
            this.closeForm.Click += new System.EventHandler(this.closeForm_Click);
            // 
            // Refresh
            // 
            this.Refresh.Location = new System.Drawing.Point(249, 284);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(75, 23);
            this.Refresh.TabIndex = 2;
            this.Refresh.Text = "Refresh";
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // unitGridView
            // 
            this.unitGridView.AutoGenerateColumns = false;
            this.unitGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.unitGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.unitIdDataGridViewTextBoxColumn,
            this.timeonlineDataGridViewTextBoxColumn,
            this.aliasDataGridViewTextBoxColumn,
            this.aeraIrradiatedSumDataGridViewTextBoxColumn,
            this.volumnHeatingBoxDataGridViewTextBoxColumn,
            this.starttimeDataGridViewTextBoxColumn,
            this.stoptimeDataGridViewTextBoxColumn,
            this.gprsstateDataGridViewTextBoxColumn});
            this.unitGridView.DataMember = "tb_union_list";
            this.unitGridView.DataSource = this.config_entityDataSource;
            this.unitGridView.Location = new System.Drawing.Point(12, 38);
            this.unitGridView.Name = "unitGridView";
            this.unitGridView.RowTemplate.Height = 23;
            this.unitGridView.Size = new System.Drawing.Size(447, 228);
            this.unitGridView.TabIndex = 3;
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
            this.aliasDataGridViewTextBoxColumn.HeaderText = "alias";
            this.aliasDataGridViewTextBoxColumn.Name = "aliasDataGridViewTextBoxColumn";
            // 
            // aeraIrradiatedSumDataGridViewTextBoxColumn
            // 
            this.aeraIrradiatedSumDataGridViewTextBoxColumn.DataPropertyName = "Aera_IrradiatedSum";
            this.aeraIrradiatedSumDataGridViewTextBoxColumn.HeaderText = "集热器面积";
            this.aeraIrradiatedSumDataGridViewTextBoxColumn.Name = "aeraIrradiatedSumDataGridViewTextBoxColumn";
            // 
            // volumnHeatingBoxDataGridViewTextBoxColumn
            // 
            this.volumnHeatingBoxDataGridViewTextBoxColumn.DataPropertyName = "Volumn_HeatingBox";
            this.volumnHeatingBoxDataGridViewTextBoxColumn.HeaderText = "贮热水箱容量";
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
            // config_entityDataSource
            // 
            this.config_entityDataSource.ObjectContextType = typeof(Tiger.db_tigerEntities);
            this.config_entityDataSource.DataError += new System.EventHandler<EFWinforms.DataErrorEventArgs>(this.config_entityDataSource_DataError);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(521, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "数据存储时间间隔：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(521, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "数据统计时间间隔：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(521, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据统计汇总时间间隔：";
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(150, 284);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 5;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(363, 284);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 6;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // F_SystemSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 319);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.unitGridView);
            this.Controls.Add(this.Refresh);
            this.Controls.Add(this.closeForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "F_SystemSet";
            this.Text = "系统配置";
            this.Load += new System.EventHandler(this.F_SystemSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.unitGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeForm;
        private System.Windows.Forms.Button Refresh;
        private System.Windows.Forms.DataGridView unitGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private EFWinforms.EntityDataSource config_entityDataSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeonlineDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aliasDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aeraIrradiatedSumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn volumnHeatingBoxDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn starttimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stoptimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gprsstateDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Cancel;

    }
}