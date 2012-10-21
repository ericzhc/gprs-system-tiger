
using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Tiger.Database;

namespace Tiger
{
    public partial class FBackup : Form
    {
        public bool ConstructSQLInOneLineFromSameTable = true;
        public bool DropAndRecreateTable = false;
        public bool DropAndRecreateDatabase = false;
        public bool AddDateToFilename = false;
        public bool EnableEncryption = false;
       
        public string FileExtension = "";

        //public F_Backup(string[] args)
        public FBackup()
        {
            InitializeComponent();
            textBox_filename.TextChanged += TextBoxTextChanged;
            textBox_FileExtension.TextChanged += TextBoxTextChanged;          
            checkBox_AddDate.CheckedChanged += CheckBoxCheckedChanged;          
        }

        private void FBackupLoad(object sender, EventArgs e)
        {
            LoadParameters();
            if (textBox_FileExtension.Text.Length == 0)
                textBox_FileExtension.Text = "sql";

        }

        void CheckBoxSaveCheckedChanged(object sender, EventArgs e)
        {
            //if (checkBox_Save.Checked) SaveParameters();
            //else
            {
                if (File.Exists((Application.StartupPath + "\\" + "parameters").Replace("\\\\", "\\")))
                    File.Delete((Application.StartupPath + "\\" + "parameters").Replace("\\\\", "\\"));
                LoadParameters();
            }
        }

        void CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        void TextBoxTextChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        void SaveParameters()
        {
            //if (!checkBox_Save.Checked) return;

            string a = textBox_FileExtension.Text + ";"
                + textBox_filename.Text + ";"               
                + checkBox_AddDate.Checked.ToString() + ";"
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
                //textBox_Server.Text = sa[0];
                //textBox_Username.Text = sa[1];
                //textBox_Password.Text = sa[2];
                //textBox_Database.Text = sa[3];
                //textBox_Port.Text = sa[4];
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
                textBox_FileExtension.Text = "";
                textBox_filename.Text = "";                
                checkBox_AddDate.Checked = true;               
            }
        }

        private void RefreshData()
        {
            lb_exampleFilename.Text = "";

            if (textBox_filename.Text.Length != 0)
            {
                lb_exampleFilename.Text = textBox_filename.Text.Trim();
            }
            else lb_exampleFilename.Text = "文件名";
                   
            if (checkBox_AddDate.Checked) lb_exampleFilename.Text = lb_exampleFilename.Text + DateTime.Now.ToString(" yyyy-MM-dd-HH-mm-ss");

            FileExtension = textBox_FileExtension.Text.Trim();

            if (FileExtension.Length != 0)
                lb_exampleFilename.Text = lb_exampleFilename.Text + "." + FileExtension;
          
            AddDateToFilename = checkBox_AddDate.Checked;        
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

                MySqlBackupRestore mb = new MySqlBackupRestore();
                mb.DropAndRecreateDatabase = DropAndRecreateDatabase;
                mb.DropAndRecreateTable = DropAndRecreateTable;
                mb.ConstructSQLInOneLineFromSameTable = ConstructSQLInOneLineFromSameTable;

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
                MySqlBackupRestore mb = new MySqlBackupRestore();
              
                mb.Restore(filename);
                
                MessageBox.Show("Restore successfull.", "Restore");
            }
            catch (Exception ex) // Log any error that occur during the backup process
            {
                string errorMessage = "Restore fail.\r\n\r\n" + ex.ToString();
                MessageBox.Show(errorMessage, "Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
