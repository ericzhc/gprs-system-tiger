namespace Tiger
{
    partial class FBackup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FBackup));
            this.button_Backup = new System.Windows.Forms.Button();
            this.button_Restore = new System.Windows.Forms.Button();
            this.textBox_FileExtension = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.button_choseFolder = new System.Windows.Forms.Button();
            this.textBox_folder = new System.Windows.Forms.TextBox();
            this.textBox_filename = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox_AddDate = new System.Windows.Forms.CheckBox();
            this.lb_exampleFilename = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Backup
            // 
            this.button_Backup.Location = new System.Drawing.Point(397, 82);
            this.button_Backup.Name = "button_Backup";
            this.button_Backup.Size = new System.Drawing.Size(75, 22);
            this.button_Backup.TabIndex = 0;
            this.button_Backup.Text = "备份";
            this.button_Backup.UseVisualStyleBackColor = true;
            this.button_Backup.Click += new System.EventHandler(this.button_Backup_Click);
            // 
            // button_Restore
            // 
            this.button_Restore.Location = new System.Drawing.Point(397, 117);
            this.button_Restore.Name = "button_Restore";
            this.button_Restore.Size = new System.Drawing.Size(75, 22);
            this.button_Restore.TabIndex = 1;
            this.button_Restore.Text = "恢复";
            this.button_Restore.UseVisualStyleBackColor = true;
            this.button_Restore.Click += new System.EventHandler(this.button_Restore_Click);
            // 
            // textBox_FileExtension
            // 
            this.textBox_FileExtension.Location = new System.Drawing.Point(126, 44);
            this.textBox_FileExtension.Name = "textBox_FileExtension";
            this.textBox_FileExtension.Size = new System.Drawing.Size(100, 21);
            this.textBox_FileExtension.TabIndex = 16;
            this.textBox_FileExtension.Text = "backup";
            this.textBox_FileExtension.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.button_choseFolder);
            this.groupBox2.Controls.Add(this.textBox_folder);
            this.groupBox2.Controls.Add(this.textBox_filename);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.checkBox_AddDate);
            this.groupBox2.Controls.Add(this.lb_exampleFilename);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox_FileExtension);
            this.groupBox2.Location = new System.Drawing.Point(12, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(356, 156);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "备份恢复文件属性";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 71);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 26;
            this.label12.Text = "存放路径：";
            // 
            // button_choseFolder
            // 
            this.button_choseFolder.Location = new System.Drawing.Point(310, 65);
            this.button_choseFolder.Name = "button_choseFolder";
            this.button_choseFolder.Size = new System.Drawing.Size(28, 26);
            this.button_choseFolder.TabIndex = 25;
            this.button_choseFolder.UseVisualStyleBackColor = true;
            this.button_choseFolder.Click += new System.EventHandler(this.button_choseFolder_Click);
            // 
            // textBox_folder
            // 
            this.textBox_folder.Location = new System.Drawing.Point(96, 68);
            this.textBox_folder.Name = "textBox_folder";
            this.textBox_folder.Size = new System.Drawing.Size(208, 21);
            this.textBox_folder.TabIndex = 24;
            // 
            // textBox_filename
            // 
            this.textBox_filename.Location = new System.Drawing.Point(126, 20);
            this.textBox_filename.Name = "textBox_filename";
            this.textBox_filename.Size = new System.Drawing.Size(100, 21);
            this.textBox_filename.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "文件名：";
            // 
            // checkBox_AddDate
            // 
            this.checkBox_AddDate.AutoSize = true;
            this.checkBox_AddDate.Location = new System.Drawing.Point(17, 94);
            this.checkBox_AddDate.Name = "checkBox_AddDate";
            this.checkBox_AddDate.Size = new System.Drawing.Size(288, 16);
            this.checkBox_AddDate.TabIndex = 20;
            this.checkBox_AddDate.Text = "在文件名后增加当前时间 (yyyy-MM-dd-HH-mm-ss)";
            this.checkBox_AddDate.UseVisualStyleBackColor = true;
            // 
            // lb_exampleFilename
            // 
            this.lb_exampleFilename.AutoSize = true;
            this.lb_exampleFilename.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_exampleFilename.Location = new System.Drawing.Point(17, 136);
            this.lb_exampleFilename.Name = "lb_exampleFilename";
            this.lb_exampleFilename.Size = new System.Drawing.Size(47, 11);
            this.lb_exampleFilename.TabIndex = 19;
            this.lb_exampleFilename.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "文件名示例：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "文件扩展：";
            this.label3.Visible = false;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(397, 151);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "退出";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // F_Backup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(483, 191);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button_Restore);
            this.Controls.Add(this.button_Backup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "F_Backup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库备份恢复";
            this.Load += new System.EventHandler(this.FBackupLoad);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Backup;
        private System.Windows.Forms.Button button_Restore;
        private System.Windows.Forms.TextBox textBox_FileExtension;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox_AddDate;
        private System.Windows.Forms.Label lb_exampleFilename;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_filename;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_choseFolder;
        private System.Windows.Forms.TextBox textBox_folder;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button1;
    }
}