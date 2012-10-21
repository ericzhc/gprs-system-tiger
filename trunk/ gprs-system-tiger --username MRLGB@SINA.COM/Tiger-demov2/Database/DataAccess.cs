using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Tiger.Database
{
    class DataAccess
    {
        //private string _confirString = "ConnectionString";
        private string _confirString = "ConnectionMysql";
        public DataAccess()
        {
        }

        /// <param name="configString">app.config 关键字</param>
        public DataAccess(string configString)
        {
            ConfigString = configString;
        }

        /**/
        /// <summary>
        /// 属性,设置数据库连接字符串
        /// </summary>
        public string ConfigString
        {
            get
            {
                return _confirString;
            }
            set
            {
                _confirString = value;
            }
        }

        //=====================================================获得连接字符串====================================
        #region 获得连接字符串
        /**/
        /// <summary>
        /// 获得连接字符串
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString(string ConfigString)
        {
            ConnectionStringSettingsCollection configStringCollention = ConfigurationManager.ConnectionStrings;
            if (configStringCollention == null || configStringCollention.Count <= 0)
            {
                throw new Exception("app.config 中无连接字符串!");
            }
            ConnectionStringSettings stringSettings = null;
            if (ConfigString == string.Empty)
            {
                stringSettings = ConfigurationManager.ConnectionStrings["ConnectionString"];
            }
            else
            {
                stringSettings = ConfigurationManager.ConnectionStrings[ConfigString];
            }
            return stringSettings.ConnectionString;

        }
        public string GetConnectionString()
        {
            return GetConnectionString(ConfigString);
        }

        #endregion
        //==============================================CreateConnection=============================

        #region 创建数据库连接 public DbConnection CreateConnection()
        /**/
        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <returns></returns>
        public MySqlConnection CreateConnection()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = GetConnectionString();
            return con;
        }
        #endregion

        //==============================================CreateCommand================================

        #region 创建执行命令对象
        /**/
        /// <summary>
        /// 创建执行命令对象
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>执行命令对象实例</returns>
        public MySqlCommand CreateCommand(string sql)
        {
            MySqlCommand _command = new MySqlCommand();
            _command.Connection = CreateConnection();
            _command.CommandText = sql;
            return _command;
        }

        #endregion
        //=========================================CreateAdapter()==============================================

        #region 创建数据适配器 CreateAdapter(string sql)
        /**/
        /// <summary>
        /// 创建数据适配器
        /// </summary>
        /// <param name="sql">SQL,语句</param>
        /// <returns>数据适配器实例</returns>
        public DbDataAdapter CreateAdapter(string sql)
        {
            var connection = CreateConnection();
            var command = new MySqlCommand {Connection = connection, CommandText = sql};
            var da = new MySqlDataAdapter {SelectCommand = command};

            return da;
        }
        #endregion

        //======================================================ExecuteCommand()============================================

        #region 执行非查询语句,并返回受影响的记录行数 ExecuteCommand(string sql)
        /**/
        /// <summary>
        /// 执行非查询语句,并返回受影响的记录行数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>受影响记录行数</returns>
        public int ExecuteCommand(string sql)
        {
            int result = 0;
            var command = CreateCommand(sql);
            try
            {
                command.Connection.Open();
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //  return 0;
            }
            finally
            {
                command.Connection.Close();
            }
            return result;
        }
        #endregion

        //=====================================================ExecuteScalar()=============================================

        #region 执行非查询语句,并返回首行首列的值 ExecuteScalar(string sql)

        /**/
        /// <summary>
        /// 执行非查询语句,并返回首行首列的值
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>Object</returns>
        public object ExecuteScalar(string sql)
        {
            object result = null;
            DbCommand command = CreateCommand(sql);
            try
            {
                command.Connection.Open();
                result = command.ExecuteScalar();
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Connection.Close();
            }
            return result;
        }
        #endregion

        //=====================================================ExecuteReader()=============================================

        #region 执行查询，并以DataReader返回结果集  ExecuteReader(string sql)
        /**/
        /// <summary>
        /// 执行查询，并以DataReader返回结果集
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>IDataReader</returns>
        public DbDataReader ExecuteReader(string sql)
        {
            DbDataReader result;
            DbCommand command = CreateCommand(sql);
            try
            {
                command.Connection.Open();
                result = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                throw;
            }
            finally
            {

            }
            return result;
        }
        #endregion

        //=====================================================GetDataSet()================================================

        #region 执行查询，并以DataSet返回结果集 GetDataSet(string sql)
        /**/
        /// <summary>
        /// 执行查询，并以DataSet返回结果集
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string sql)
        {
            var result = new DataSet();
            IDataAdapter dataAdapter = CreateAdapter(sql);
            try
            {
                dataAdapter.Fill(result);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
            return result;

        }
        #endregion

        //=====================================================GetDataView()===============================================

        #region 执行查询，并以DataView返回结果集   GetDataView(string sql)

        /**/
        /// <summary>
        /// 执行查询，并以DataView返回结果集
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>DataView</returns>
        public DataView GetDataView(string sql)
        {
            DataView dv = GetDataSet(sql).Tables[0].DefaultView;
            return dv;
        }
        #endregion

        //=====================================================GetDataTable()==============================================

        #region 执行查询，并以DataTable返回结果集   GetDataTable(string sql)

        /**/
        /// <summary>
        /// 执行查询，并以DataTable返回结果集
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string sql)
        {
            var dt = GetDataSet(sql).Tables[0];
            return dt;
        }
        #endregion

    }
}
