using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;
using System.Windows.Forms;
using Tiger.Properties;

//using XCrypt;

namespace Tiger.Database
{
    class MySqlBackupRestore
    {
        /// <summary>
        /// Get or Set the Full Path + the Filename to Backup or Restore
        /// </summary>
        public string Filename = "";
        public string MySQLConnectionString = "";
        public string EncryptionKey = "";
        public bool DropAndRecreateDatabase = false;
        public bool DropAndRecreateTable = false;
        public bool ConstructSQLInOneLineFromSameTable = true;
        public bool EncryptBackupFile = false;

        readonly DataAccess _da = new DataAccess();
        Form _f;
        ProgressBar _progressBar1;

        public MySqlBackupRestore()
        {
            MySQLConnectionString = _da.GetConnectionString();
        }       

        public void Backup(string filename, bool dropRecreateDatabase, bool dropRecreateTable, bool constructSQLInOneLineOfSameTable, bool encryptBackupFILE)
        {
            Filename = filename;
            DropAndRecreateDatabase = dropRecreateDatabase;
            DropAndRecreateTable = dropRecreateTable;
            ConstructSQLInOneLineFromSameTable = constructSQLInOneLineOfSameTable;
            EncryptBackupFile = encryptBackupFILE;
            Backup();
        }

        public void Backup(string filename)
        {
            Filename = filename;
            Backup();
        }

        public void Backup()
        {
            var sqLs = new List<string>();

            string myCon = "";

            if (MySQLConnectionString.Length != 0)
                myCon = MySQLConnectionString;
         
            var conn = new MySqlConnection(myCon);

            #region Get all tables' name in database
            string sqlcmd = "show tables;";
            var dtTable = new DataTable();
            var da = new MySqlDataAdapter(sqlcmd, conn);
            da.Fill(dtTable);
            #endregion

            #region Construct: Create Database SQL command
            
            var dtDatabase = new DataTable();
            sqlcmd = "select database();";
            da = new MySqlDataAdapter(sqlcmd, conn);
            da.Fill(dtDatabase);
            string databaseName = dtDatabase.Rows[0][0] + "";
            
            if (DropAndRecreateDatabase)
            {
                sqLs.Add("drop database if exists `" + databaseName + "`;");
            }

            dtDatabase = new DataTable();
            sqlcmd = "show create database `" + databaseName + "`;";
            da = new MySqlDataAdapter(sqlcmd, conn);
            da.Fill(dtDatabase);
            sqLs.Add((dtDatabase.Rows[0][1] + "").Replace("CREATE DATABASE", "create database if not exists") + ";");
            sqLs.Add("use `" + databaseName + "`;");
            #endregion

            foreach (string tablename in from DataRow dr in dtTable.Rows select dr[0] + "")
            {
                #region Backup Each Table's Structure
                if (DropAndRecreateTable)
                {
                    sqLs.Add("drop table if exists `" + tablename + "`;");
                }
                string sql2 = "show create table `" + tablename + "`;";
                var dtCreateTable = new DataTable();
                var da2 = new MySqlDataAdapter(sql2, conn);
                da2.Fill(dtCreateTable);
                string createTable = (dtCreateTable.Rows[0][1] + "").Replace("\n", string.Empty);
                sqLs.Add(createTable.Replace("CREATE TABLE", "create table if not exists") + ";");
                #endregion

                // Delete all rows in table

                sqLs.Add("delete from `" + tablename + "`;");

                #region Get all column's name in table
                var dtColumn = new DataTable();
                string sql3 = "show columns from `" + tablename + "`";
                var da3 = new MySqlDataAdapter(sql3, conn);
                da3.Fill(dtColumn);
                #endregion

                #region Get all rows in table
                var dtRows = new DataTable();
                string sql4 = "select * from `" + tablename + "`";
                var da4 = new MySqlDataAdapter(sql4, conn);
                da4.Fill(dtRows);
                #endregion

                if (dtRows.Rows.Count != 0)
                {
                    var sb = new StringBuilder();

                    if (ConstructSQLInOneLineFromSameTable)
                        sb.AppendFormat("insert into `" + tablename + "` value (");

                    for (var i = 0; i < dtRows.Rows.Count; i++)
                    {
                        if (!ConstructSQLInOneLineFromSameTable)
                            sb.AppendFormat("insert into `" + tablename + "` value (");

                        for (var j = 0; j < dtRows.Columns.Count; j++)
                        {
                            var datatype = dtRows.Columns[j].DataType.ToString();

                            string text;

                            if (datatype == "System.DateTime")
                            {
                                DateTime dtime;
                                if (DateTime.TryParse(dtRows.Rows[i][j] + "", out dtime))
                                {
                                    text = "'" + Convert.ToDateTime(dtRows.Rows[i][j]).ToString("yyyy-MM-dd HH:mm:ss") + "'";
                                }
                                else
                                    text = "null";
                            }
                            else if (dtRows.Rows[i][j] + "" == "")
                                text = "null";
                            else
                            {
                                text = dtRows.Rows[i][j] + "";

                                // Escape special character for MySQL commands

                                text = text.Replace("\\", "\\\\");
                                text = text.Replace("'", "\\'");
                                text = "'" + text + "'";
                            }

                            sb.AppendFormat(text);
                            if (j + 1 != dtRows.Columns.Count)
                                sb.AppendFormat(",");
                        }

                        if (ConstructSQLInOneLineFromSameTable)
                        {
                            sb.AppendFormat(i + 1 != dtRows.Rows.Count ? "),(" : ");");
                        }
                        else
                        {
                            sb.AppendFormat(");");
                            sqLs.Add(sb.ToString());
                            sb = new StringBuilder();
                        }
                    }
                    if (ConstructSQLInOneLineFromSameTable)
                    {
                        sqLs.Add(sb.ToString());
/*
                        sb = new StringBuilder();
*/
                    }
                }
            }

            var output = new string[sqLs.Count];

            #region Encryption | Encrypt the output SQL text

            //if (EncryptBackupFile)
            //{
            //    XCryptEngine xe = new XCryptEngine();
            //    xe.InitializeEngine(XCryptEngine.AlgorithmType.Rijndael);
            //    xe.Key = EncryptionKey;

            //    for (int i = 0; i < SQLs.Count; i++)
            //    {
            //        output[i] = xe.Encrypt(SQLs[i]);
            //    }
            //}
            //else
            //{
                for (int i = 0; i < sqLs.Count; i++)
                {
                    output[i] = sqLs[i];
                }
            //}

            #endregion

            File.WriteAllLines(Filename, output, Encoding.UTF8);
        }

        public void Restore(string filename)
        {
            Filename = filename;
            Restore();
        }

        public void Restore(string filename, bool decryption, string encryptionKEY)
        {
            Filename = filename;
            EncryptBackupFile = decryption;
            EncryptionKey = encryptionKEY;
            Restore();
        }

        public void Restore()
        {
            string myCon = "";

            if (MySQLConnectionString.Length != 0)
                myCon = MySQLConnectionString;
           
            var conn = new MySqlConnection(myCon);

            conn.Open(); // Test connection
            conn.Close();

            string[] sqls = File.ReadAllLines(Filename, Encoding.UTF8);

          
            NewProgressForm();
            _progressBar1.Maximum = sqls.Length;
            _progressBar1.Value = 0;

            _f.Show();

            var cmd = new MySqlCommand {Connection = conn};
            conn.Open();

            // Start Restoring the Database
            foreach (string s in sqls)
            {
                _progressBar1.Value += 1;
                cmd.CommandText = s;
                cmd.ExecuteNonQuery();
            }

            _f.Close();
        }

        void NewProgressForm()
        {
            _progressBar1 = new ProgressBar
                                {Location = new System.Drawing.Point(25, 25), Size = new System.Drawing.Size(150, 20)};
            _f = new Form
                    {
                        Size = new System.Drawing.Size(220, 120),
                        Text = Resources.MySqlBackupRestore_NewProgressForm_Progress___,
                        StartPosition = FormStartPosition.CenterScreen
                    };
            _f.Controls.Add(_progressBar1);
            _f.ShowIcon = false;
        }
    }
}
