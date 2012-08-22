// MySQL Backup & Restore V1.1
// Release Date: 20 Sep 2011
// Project Site: http://www.codeproject.com/KB/database/mysqlbackuprestore.aspx
// License: This article, along with any associated source code and files, 
//          is licensed under The Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
//using XCrypt;

namespace MySQL_Backup_and_Restore
{
    public partial class Form1 : Form
    {
        public bool Construct_SQL_In_One_Line_From_Same_Table = true;
        public bool DropAndRecreateTable = false;
        public bool DropAndRecreateDatabase = false;
        public bool AddDateToFilename = false;
        public bool EnableEncryption = false;
                        //MySqlBackupRestore mb = new MySqlBackupRestore(textBox_Server.Text, textBox_Username.Text,
                            //textBox_Password.Text, textBox_Database.Text, textBox_Port.Text, textBox_otherDetails.Text);
        public string SeverStr = "localhost";
        public string Username = "root";
        public string Pass = "hfuu";
        public string Database = "db_energy";
        public string Port = "3306";
        public string otherDetails = "";

        public string FileExtension = "";

        public Form1(string[] args)
        {
            InitializeComponent();

            textBox_Server.TextChanged += new EventHandler(textBox_TextChanged);
            textBox_Username.TextChanged += new EventHandler(textBox_TextChanged);
            textBox_Password.TextChanged += new EventHandler(textBox_TextChanged);
            textBox_Database.TextChanged += new EventHandler(textBox_TextChanged);
            textBox_Port.TextChanged += new EventHandler(textBox_TextChanged);
            textBox_filename.TextChanged += new EventHandler(textBox_TextChanged);
            textBox_FileExtension.TextChanged += new EventHandler(textBox_TextChanged);

            //checkBox_ConstructSQLIn1Line.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
            //checkBox_dontDisplay.CheckedChanged += new EventHandler(checkBox_dontDisplay_CheckedChanged);
            //checkBox_Save.CheckedChanged += new EventHandler(checkBox_Save_CheckedChanged);
            checkBox_AddDate.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
            //checkBox_DropDatabase.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
            //checkBox_encryption.CheckedChanged += new EventHandler(checkBox_encryption_CheckedChanged);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadParameters();
            if (textBox_FileExtension.Text.Length == 0)
                textBox_FileExtension.Text = "sql";

        }

        void checkBox_Save_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBox_Save.Checked) SaveParameters();
            //else
            {
                if (File.Exists((Application.StartupPath + "\\" + "parameters").Replace("\\\\", "\\")))
                    File.Delete((Application.StartupPath + "\\" + "parameters").Replace("\\\\", "\\"));
                LoadParameters();
            }
        }

        void checkBox_dontDisplay_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBox_dontDisplay.Checked)
            //{
            //    textBox_Server.PasswordChar = '*';
            //    textBox_Username.PasswordChar = '*';
            //    textBox_Password.PasswordChar = '*';
            //    textBox_Database.PasswordChar = '*';
            //    textBox_Port.PasswordChar = '*';
            //    textBox_otherDetails.PasswordChar = '*'; 
            //}
            //else
            //{
                textBox_Server.PasswordChar = '\0';
                textBox_Username.PasswordChar = '\0';
                textBox_Password.PasswordChar = '\0';
                textBox_Database.PasswordChar = '\0';
                textBox_Port.PasswordChar = '\0';
                textBox_otherDetails.PasswordChar = '\0';
            //}
            //if (checkBox_Save.Checked) SaveParameters();
        }

        void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        //void checkBox_encryption_CheckedChanged(object sender, EventArgs e)
        //{
        //    textBox_encryptKey.ReadOnly = !checkBox_encryption.Checked;
        //    EnableEncryption = checkBox_encryption.Checked;
        //}

        void textBox_TextChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        void SaveParameters()
        {
            //if (!checkBox_Save.Checked) return;

            string a = textBox_Server.Text + ";"
                + textBox_Username.Text + ";"
                + textBox_Password.Text + ";"
                + textBox_Database.Text + ";"
                + textBox_Port.Text + ";"
                + textBox_FileExtension.Text + ";"
                + textBox_filename.Text + ";"
                //+ checkBox_dontDisplay.Checked.ToString() + ";"
                //+ checkBox_ConstructSQLIn1Line.Checked.ToString() + ";"
                //+ checkBox_DeleteTable.Checked.ToString() + ";"
                + checkBox_AddDate.Checked.ToString() + ";"
                //+ checkBox_DropDatabase.Checked.ToString() + ";"
                + textBox_folder.Text.Trim();
            string b = Convert.ToBase64String(Encoding.UTF8.GetBytes(a));
            //XCryptEngine xe = new XCryptEngine(); xe.InitializeEngine(XCryptEngine.AlgorithmType.Rijndael);
            //string c = xe.Encrypt(b);
            File.WriteAllText((Application.StartupPath + "\\" + "parameters").Replace("\\\\", "\\"), b, Encoding.UTF8);
        }

        void LoadParameters()
        {
            try
            {
                //checkBox_Save.Checked = false;
                string c = File.ReadAllText((Application.StartupPath + "\\" + "parameters").Replace("\\\\", "\\"), Encoding.UTF8);
                //XCryptEngine xe = new XCryptEngine(); xe.InitializeEngine(XCryptEngine.AlgorithmType.Rijndael);
                //string b = xe.Decrypt(c);
                string a = Encoding.UTF8.GetString(Convert.FromBase64String(c));
                string[] sa = a.Split(';');
                textBox_Server.Text = sa[0];
                textBox_Username.Text = sa[1];
                textBox_Password.Text = sa[2];
                textBox_Database.Text = sa[3];
                textBox_Port.Text = sa[4];
                textBox_FileExtension.Text = sa[5];
                textBox_filename.Text = sa[6];
                //if (sa[7] == "True") checkBox_dontDisplay.Checked = true; else checkBox_dontDisplay.Checked = false;
                //if (sa[8] == "True") checkBox_ConstructSQLIn1Line.Checked = true; else checkBox_ConstructSQLIn1Line.Checked = false;
                //if (sa[9] == "True") checkBox_DeleteTable.Checked = true; else checkBox_DeleteTable.Checked = false;
                if (sa[10] == "True") checkBox_AddDate.Checked = true; else checkBox_AddDate.Checked = false;
                //if (sa[11] == "True") checkBox_DropDatabase.Checked = true; else checkBox_DropDatabase.Checked = false;
                textBox_folder.Text = sa[12];
                //checkBox_Save.Checked = true;
            }
            catch
            {
                //checkBox_Save.Checked = false;
                textBox_Server.Text = "";
                textBox_Username.Text = "";
                textBox_Password.Text = "";
                textBox_Database.Text = "";
                textBox_Port.Text = "";
                textBox_FileExtension.Text = "";
                textBox_filename.Text = "";
                //checkBox_dontDisplay.Checked = false;
                //checkBox_ConstructSQLIn1Line.Checked = true;
                //checkBox_DeleteTable.Checked = false;
                checkBox_AddDate.Checked = true;
                //checkBox_DropDatabase.Checked = false;
            }
        }

        private void RefreshData()
        {
            if (!textBox_Port.Focused && textBox_Port.Text.Trim().Length == 0)
                textBox_Port.Text = "3306";

            lb_exampleFilename.Text = "";

            if (textBox_filename.Text.Length != 0)
            {
                lb_exampleFilename.Text = textBox_filename.Text.Trim();
            }
            else lb_exampleFilename.Text = "ChoseYourOwnName";
                   
            if (checkBox_AddDate.Checked) lb_exampleFilename.Text = lb_exampleFilename.Text + DateTime.Now.ToString(" yyyy-MM-dd-HH-mm-ss");

            FileExtension = textBox_FileExtension.Text.Trim();

            if (FileExtension.Length != 0)
                lb_exampleFilename.Text = lb_exampleFilename.Text + "." + FileExtension;

            //DropAndRecreateDatabase = checkBox_DropDatabase.Checked;
            //DropAndRecreateTable = checkBox_DeleteTable.Checked;
            //Construct_SQL_In_One_Line_From_Same_Table = checkBox_ConstructSQLIn1Line.Checked;
            AddDateToFilename = checkBox_AddDate.Checked;

            //if (checkBox_Save.Checked) SaveParameters();

            //EnableEncryption = checkBox_encryption.Checked;
        }

        private void button_choseFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select the location of backup file.";
            if (DialogResult.OK != fbd.ShowDialog())
                return;
            textBox_folder.Text = fbd.SelectedPath;
        }

        private void button_Backup_Click(object sender, EventArgs e)
        {
            RefreshData();
            Backup();
        }

        private void button_Restore_Click(object sender, EventArgs e)
        {
            RefreshData();
            Restore();
        }

        public void Backup()
        {
            #region Check Required Connection Parameters

            bool okConnect = true;
            string errorMsg = "";
            if (textBox_Server.Text.Length == 0)
            { errorMsg += "Database server not define.\r\n"; okConnect = false; }
            if (textBox_Username.Text.Length == 0)
            { errorMsg += "Username must not empty.\r\n"; okConnect = false; }
            if (textBox_Password.Text.Length == 0)
            { errorMsg += "Password is not entered.\r\n"; okConnect = false; }
            if (textBox_Database.Text.Length == 0)
            { errorMsg += "No database is defined.\r\n"; okConnect = false; }
            if (!okConnect)
            {
                ToolTip tt = new ToolTip();
                tt.ToolTipTitle = "Some entry is blank.";
                tt.Show(errorMsg, this, textBox_Server.Location.X + textBox_Server.Width, textBox_Server.Location.Y + textBox_Server.Height, 7000);
                ToolTip tt2 = new ToolTip();
                tt2.ToolTipTitle = "Cannot Backup";
                tt2.Show("Some parameters is missing.", this, button_Backup.Location, 2000);
                return;
            }
            #endregion

            string backupFile = "";
            string filter = "";
            try
            {
                #region Get the Backup Filename and Path

                if (FileExtension != "") filter = "*." + FileExtension + "|*." + FileExtension;

                if (textBox_filename.Text.Trim().Length == 0)
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Title = "Save Database Backup File";
                    saveFileDialog1.Filter = filter;
                    if (Directory.Exists(textBox_folder.Text.Trim()))
                        saveFileDialog1.InitialDirectory = textBox_folder.Text.Trim();
                    if (DialogResult.OK != saveFileDialog1.ShowDialog())
                        return;
                    backupFile = saveFileDialog1.FileName;
                    if (AddDateToFilename)
                    {
                        if (FileExtension != "")
                            backupFile = backupFile.Replace("." + FileExtension, DateTime.Now.ToString(" yyyy-MM-dd-HH-mm-ss") + "." + FileExtension);
                        else
                            backupFile += DateTime.Now.ToString(" yyyy-MM-dd-HH-mm-ss");
                    }
                }
                else
                {
                    backupFile = textBox_filename.Text.Trim();
                    if (checkBox_AddDate.Checked) backupFile += DateTime.Now.ToString(" yyyy-MM-dd-HH-mm-ss");
                    if (FileExtension.Length != 0) backupFile = backupFile + "." + FileExtension;
                    string folderpath = "";
                    if (Directory.Exists(textBox_folder.Text.Trim()))
                        folderpath = textBox_folder.Text.Trim();
                    else
                    {
                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        fbd.Description = "Choose where to save the backup file.\r\n" + backupFile;
                        if (DialogResult.OK != fbd.ShowDialog())
                            return;
                        folderpath = fbd.SelectedPath;
                    }
                    backupFile = (folderpath + "\\").Replace("\\\\", "\\") + backupFile;
                }
                #endregion

                // Start Backup Process

                MySqlBackupRestore mb = new MySqlBackupRestore(SeverStr, Username, Pass, Database, Port, otherDetails);
               // MySqlBackupRestore mb = new MySqlBackupRestore(textBox_Server.Text, textBox_Username.Text, textBox_Password.Text, textBox_Database.Text, textBox_Port.Text, textBox_otherDetails.Text);
                mb.DropAndRecreateDatabase = DropAndRecreateDatabase;
                mb.DropAndRecreateTable = DropAndRecreateTable;
                mb.Construct_SQL_In_One_Line_From_Same_Table = Construct_SQL_In_One_Line_From_Same_Table;

                //if (EnableEncryption)
                //{
                //    mb.EncryptBackupFile = EnableEncryption;
                //    mb.EncryptionKey = textBox_encryptKey.Text;
                //}

                mb.Backup(backupFile);

                // End of Backup Process

                MessageBox.Show("Backup Successfully.\n\nYour backup file is created at:\r\n" + backupFile, "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) // Log any error that occur during the backup process
            {
                string errorMessage = "Backup fail.\r\n\r\n" + ex.ToString();
                MessageBox.Show(errorMessage, "Error");
            }
        }

        public void Restore()
        {
            #region Check Required Connection Parameters

            bool okConnect = true;
            string errorMsg = "";
            if (textBox_Server.Text.Length == 0)
            { errorMsg += "Database server not define.\r\n"; okConnect = false; }
            if (textBox_Username.Text.Length == 0)
            { errorMsg += "Username must not empty.\r\n"; okConnect = false; }
            if (textBox_Password.Text.Length == 0)
            { errorMsg += "Password is not entered.\r\n"; okConnect = false; }
            if (!okConnect)
            {
                ToolTip tt = new ToolTip();
                tt.ToolTipTitle = "Some entry is blank.";
                tt.Show(errorMsg, this, textBox_Server.Location.X + textBox_Server.Width, textBox_Server.Location.Y + textBox_Server.Height, 7000);
                ToolTip tt2 = new ToolTip();
                tt2.ToolTipTitle = "Cannot Restore";
                tt2.Show("Some parameters is missing.", this, button_Restore.Location, 2000);
                return;
            }
            #endregion
            // Locate backup file -----------------------------

            string filename = "";
            OpenFileDialog f2 = new OpenFileDialog();
            f2.Title = "Open Database Backup File";
            f2.Filter = "*." + FileExtension + "|*." + FileExtension + "|All Files|*.*";
            if (Directory.Exists(textBox_folder.Text.Trim()))
                f2.InitialDirectory = textBox_folder.Text.Trim();
            if (DialogResult.OK != f2.ShowDialog())
                return;

            filename = f2.FileName;

            // End of Locate backup file ----------------------

            try
            {
                MySqlBackupRestore mb = new MySqlBackupRestore(SeverStr, Username, Pass, Database, Port, otherDetails);
                //MySqlBackupRestore mb = new MySqlBackupRestore(textBox_Server.Text, textBox_Username.Text, textBox_Password.Text, "", textBox_Port.Text, textBox_otherDetails.Text);

                //if (EnableEncryption)
                //{
                //    mb.Restore(filename, true, textBox_encryptKey.Text);
                //}
                //else
                //{
                    mb.Restore(filename);
                //}

                MessageBox.Show("Restore successfull.", "Restore");
            }
            catch (Exception ex) // Log any error that occur during the backup process
            {
                string errorMessage = "Restore fail.\r\n\r\n" + ex.ToString();
                MessageBox.Show(errorMessage, "Error");
            }
        }
    }
}
