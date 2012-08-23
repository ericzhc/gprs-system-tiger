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
            this.unitList = new System.Windows.Forms.ComboBox();
            this.closeForm = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.unitGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.unitGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // unitList
            // 
            this.unitList.FormattingEnabled = true;
            this.unitList.Location = new System.Drawing.Point(12, 12);
            this.unitList.Name = "unitList";
            this.unitList.Size = new System.Drawing.Size(121, 20);
            this.unitList.TabIndex = 0;
            this.unitList.SelectedIndexChanged += new System.EventHandler(this.unitList_SelectedIndexChanged);
            // 
            // closeForm
            // 
            this.closeForm.Location = new System.Drawing.Point(142, 189);
            this.closeForm.Name = "closeForm";
            this.closeForm.Size = new System.Drawing.Size(75, 23);
            this.closeForm.TabIndex = 1;
            this.closeForm.Text = "关闭";
            this.closeForm.UseVisualStyleBackColor = true;
            this.closeForm.Click += new System.EventHandler(this.closeForm_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(407, 189);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "更新";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // unitGridView
            // 
            this.unitGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.unitGridView.Location = new System.Drawing.Point(12, 38);
            this.unitGridView.Name = "unitGridView";
            this.unitGridView.RowTemplate.Height = 23;
            this.unitGridView.Size = new System.Drawing.Size(608, 145);
            this.unitGridView.TabIndex = 3;
            // 
            // F_SystemSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 224);
            this.Controls.Add(this.unitGridView);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.closeForm);
            this.Controls.Add(this.unitList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "F_SystemSet";
            this.Text = "系统配置";
            this.Load += new System.EventHandler(this.F_SystemSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.unitGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox unitList;
        private System.Windows.Forms.Button closeForm;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView unitGridView;

    }
}