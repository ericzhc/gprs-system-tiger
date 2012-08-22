
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
//using XCrypt;

namespace Tiger
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
        public bool Construct_SQL_In_One_Line_From_Same_Table = true;
        public bool EncryptBackupFile = false;

        DataAccess da = new DataAccess();
        Form f = null;
        ProgressBar progressBar1 = null;

        public MySqlBackupRestore()
        {
            MySQLConnectionString = da.GetConnectionString();
        }       

        public void Backup(string filename, bool DropRecreateDatabase, bool DropRecreateTable, bool Construct_SQL_In_One_Line_Of_Same_Table, bool EncryptBackupFILE)
        {
            Filename = filename;
            DropAndRecreateDatabase = DropRecreateDatabase;
            DropAndRecreateTable = DropRecreateTable;
            Construct_SQL_In_One_Line_From_Same_Table = Construct_SQL_In_One_Line_Of_Same_Table;
            EncryptBackupFile = EncryptBackupFILE;
            Backup();
        }

        public void Backup(string filename)
        {
            Filename = filename;
            Backup();
        }

        public void Backup()
        {
            List<string> SQLs = new List<string>();

            string myCon = "";

            if (MySQLConnectionString.Length != 0)
                myCon = MySQLConnectionString;
         
            MySqlConnection conn = new MySqlConnection(myCon);

            #region Get all tables' name in database
            string sqlcmd = "show tables;";
            DataTable dtTable = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(sqlcmd, conn);
            da.Fill(dtTable);
            #endregion

            #region Construct: Create Database SQL command
            
            DataTable dtDatabase = new DataTable();
            sqlcmd = "select database();";
            da = new MySqlDataAdapter(sqlcmd, conn);
            da.Fill(dtDatabase);
            string DatabaseName = dtDatabase.Rows[0][0] + "";
            
            if (DropAndRecreateDatabase)
            {
                SQLs.Add("drop database if exists `" + DatabaseName + "`;");
            }

            dtDatabase = new DataTable();
            sqlcmd = "show create database `" + DatabaseName + "`;";
            da = new MySqlDataAdapter(sqlcmd, conn);
            da.Fill(dtDatabase);
            SQLs.Add((dtDatabase.Rows[0][1] + "").Replace("CREATE DATABASE", "create database if not exists") + ";");
            SQLs.Add("use `" + DatabaseName + "`;");
            #endregion

            foreach (DataRow dr in dtTable.Rows)
            {
                string tablename = dr[0] + "";

                #region Backup Each Table's Structure
                if (DropAndRecreateTable)
                {
                    SQLs.Add("drop table if exists `" + tablename + "`;");
                }
                string sql2 = "show create table `" + tablename + "`;";
                DataTable dtCreateTable = new DataTable();
                MySqlDataAdapter da2 = new MySqlDataAdapter(sql2, conn);
                da2.Fill(dtCreateTable);
                string createTable = (dtCreateTable.Rows[0][1] + "").Replace("\n", string.Empty);
                SQLs.Add(createTable.Replace("CREATE TABLE", "create table if not exists") + ";");
                #endregion

                // Delete all rows in table

                SQLs.Add("delete from `" + tablename + "`;");

                #region Get all column's name in table
                DataTable dtColumn = new DataTable();
                string sql3 = "show columns from `" + tablename + "`";
                MySqlDataAdapter da3 = new MySqlDataAdapter(sql3, conn);
                da3.Fill(dtColumn);
                #endregion

                #region Get all rows in table
                DataTable dtRows = new DataTable();
                string sql4 = "select * from `" + tablename + "`";
                MySqlDataAdapter da4 = new MySqlDataAdapter(sql4, conn);
                da4.Fill(dtRows);
                #endregion

                #region Loop each row and construct sql INSERT command

                if (dtRows.Rows.Count != 0)
                {
                    StringBuilder sb = new StringBuilder();

                    if (Construct_SQL_In_One_Line_From_Same_Table)
                        sb.AppendFormat("insert into `" + tablename + "` value (");

                    for (int i = 0; i < dtRows.Rows.Count; i++)
                    {
                        if (!Construct_SQL_In_One_Line_From_Same_Table)
                            sb.AppendFormat("insert into `" + tablename + "` value (");

                        for (int j = 0; j < dtRows.Columns.Count; j++)
                        {
                            string datatype = dtRows.Columns[j].DataType.ToString();

                            string text = "";

                            DateTime dtime = DateTime.Now;
                            if (datatype == "System.DateTime")
                            {
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

                        if (Construct_SQL_In_One_Line_From_Same_Table)
                        {
                            if (i + 1 != dtRows.Rows.Count)
                            {
                                sb.AppendFormat("),(");
                            }
                            else
                            {
                                sb.AppendFormat(");");
                            }
                        }
                        else
                        {
                            sb.AppendFormat(");");
                            SQLs.Add(sb.ToString());
                            sb = new StringBuilder();
                        }
                    }
                    if (Construct_SQL_In_One_Line_From_Same_Table)
                    {
                        SQLs.Add(sb.ToString());
                        sb = new StringBuilder();
                    }
                }
                #endregion
            }

            string[] output = null;
            output = new string[SQLs.Count];

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
                for (int i = 0; i < SQLs.Count; i++)
                {
                    output[i] = SQLs[i];
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

        public void Restore(string filename, bool Decryption, string EncryptionKEY)
        {
            Filename = filename;
            EncryptBackupFile = Decryption;
            EncryptionKey = EncryptionKEY;
            Restore();
        }

        public void Restore()
        {
            string myCon = "";

            if (MySQLConnectionString.Length != 0)
                myCon = MySQLConnectionString;
           
            MySqlConnection conn = new MySqlConnection(myCon);

            conn.Open(); // Test connection
            conn.Close();

            string[] sqls = File.ReadAllLines(Filename, Encoding.UTF8);

          
            NewProgressForm();
            progressBar1.Maximum = sqls.Length;
            progressBar1.Value = 0;

            f.Show();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            conn.Open();

            // Start Restoring the Database
            foreach (string s in sqls)
            {
                progressBar1.Value += 1;
                cmd.CommandText = s;
                cmd.ExecuteNonQuery();
            }

            f.Close();
        }

        void NewProgressForm()
        {
            progressBar1 = new ProgressBar();
            progressBar1.Location = new System.Drawing.Point(25, 25);
            progressBar1.Size = new System.Drawing.Size(150, 20);
            f = new Form();
            f.Size = new System.Drawing.Size(220, 120);
            f.Text = "Progress...";
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Controls.Add(progressBar1);
            f.ShowIcon = false;
        }
    }
}
