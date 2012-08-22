using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Tiger
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
            ConnectionStringSettingsCollection ConfigStringCollention = ConfigurationManager.ConnectionStrings;
            if (ConfigStringCollention == null || ConfigStringCollention.Count <= 0)
            {
                throw new Exception("app.config 中无连接字符串!");
            }
            ConnectionStringSettings StringSettings = null;
            if (ConfigString == string.Empty)
            {
                StringSettings = ConfigurationManager.ConnectionStrings["ConnectionString"];
            }
            else
            {
                StringSettings = ConfigurationManager.ConnectionStrings[ConfigString];
            }
            return StringSettings.ConnectionString;

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
            MySqlConnection _connection = CreateConnection();
            MySqlCommand _command = new MySqlCommand();
            _command.Connection = _connection;
            _command.CommandText = sql;
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = _command;

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
            int _result = 0;
            MySqlCommand _command = CreateCommand(sql);
            try
            {
                _command.Connection.Open();
                _result = _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //  return 0;
            }
            finally
            {
                _command.Connection.Close();
            }
            return _result;
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
            object _result = null;
            DbCommand _command = CreateCommand(sql);
            try
            {
                _command.Connection.Open();
                _result = _command.ExecuteScalar();
            }
            catch
            {
                throw;
            }
            finally
            {
                _command.Connection.Close();
            }
            return _result;
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
            DbDataReader _result;
            DbCommand _command = CreateCommand(sql);
            try
            {
                _command.Connection.Open();
                _result = _command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                throw;
            }
            finally
            {

            }
            return _result;
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
            DataSet _result = new DataSet();
            IDataAdapter _dataAdapter = CreateAdapter(sql);
            try
            {
                _dataAdapter.Fill(_result);
            }
            catch
            {
                throw;
            }
            finally
            {
            }
            return _result;

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
            DataTable dt = GetDataSet(sql).Tables[0];
            return dt;
        }
        #endregion

    }
}
