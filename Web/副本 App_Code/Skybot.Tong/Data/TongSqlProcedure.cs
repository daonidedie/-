/******************************************************
* 文件名：TongSqlProcedure.cs
* 文件功能描述：web数据操作 通用 类型 命名空间 Skybot.Tong.Data
* 
* 创建标识：周渊 2005-12-6
* 
* 修改标识：周渊 2008-4-26 
* 修改标识：周渊 2009-4-1 修改命名空间与目录
* 修改描述：按代码编写规范改写部分代码
* 
******************************************************/

using System;
using System.Data;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Security.Cryptography;//下面　SHA-1加密用的類
using System.Collections;


namespace Skybot.Tong.Data
{
    /// <summary>
    /// SQL 存储过程调用类
    /// 2008-1-12 加入
    /// </summary>
    ///<![CDATA[
    ///
    ///Response.Write(HttpUtility.UrlDecode(HttpUtility.UrlEncodeUnicode("实现添加修改删除")));
    ///DataSet Myds = new DataSet();
    /// 设置连接对象
    ///Bocom.BaseInfo.Tong.TongSqlProcedure SqlProcedure = new Bocom.BaseInfo.Tong.TongSqlProcedure() { Procedure = "ShowTree" };
    ///SqlProcedure.SetInputValue("@parentId", 0);
    ///SqlProcedure.ExecuteAdapter().Fill(Myds);
    ///Response.Write(Myds.Tables[0].Rows.Count);
    /// ]]>
    /// 
    [System.ComponentModel.DataObject]
    public class TongSqlProcedure
    {

        #region 数据成员
        /// <summary>
        /// sql 所用的 联接
        /// </summary>
        private SqlConnection _sqlConnection = null;
        /// <summary>
        /// 空白数据
        /// </summary>
        private String _srocedure = String.Empty;
        /// <summary>
        /// 用于SQL处理命令
        /// </summary>
        private SqlCommand _sqlCmd = new SqlCommand();
        private Hashtable _inputTable = null; // 保存input参数和值
        private String _lastError = String.Empty;
        /// <summary>
        /// 数据操作常用方法
        /// </summary>
        private TongUse textData = new TongUse();
        #endregion
        #region 构造函数


        /// <summary>
        /// 默认
        /// </summary>
        public TongSqlProcedure()
        {
            _inputTable = new Hashtable();
            _sqlCmd.CommandType = CommandType.StoredProcedure;
            this.SqlConnection = textData.MySql;
        }
        /// <summary>
        /// 传入联接
        /// </summary>
        /// <param name="_sqlConnection">一个SQL联接</param>
        public TongSqlProcedure(SqlConnection _sqlConnection)
            : this()
        {
            this.SqlConnection = _sqlConnection;
        }
        /// <summary>
        /// 传入联接和存储过程名
        /// </summary>
        /// <param name="procedure">储过程名</param>
        /// <param name="_sqlConnection">一个SQL联接</param>
        public TongSqlProcedure(String procedure, SqlConnection _sqlConnection)
            : this()
        {
            this.SqlConnection = _sqlConnection;
            this.Procedure = procedure;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 错误的信息
        /// </summary>
        public String LastError
        {
            get
            {
                return this._lastError;
            }
        }
        /// <summary>
        /// 返回值
        /// </summary>
        public Object ReturnValue
        {
            get
            {
                return _sqlCmd.Parameters["RetVal"].Value;
            }
        }

        /// <summary>
        /// 设定SQLconn
        /// </summary>
        public SqlConnection SqlConnection
        {
            set
            {
                this._sqlConnection = value;
                _sqlCmd.Connection = this._sqlConnection;
            }
        }

        /// <summary>
        ///存储过程名
        /// </summary>
        public String Procedure
        {
            set
            {
                this._srocedure = value;
                _sqlCmd.CommandText = this._srocedure;
            }

            get
            {
                return this._srocedure;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 执行存储过程，仅返回是否成功标志
        /// </summary>
        /// <param name="procedure">存储过程名</param>
        /// <returns>是否成功标志</returns>
        public Boolean ExecuteNonQuery(String procedure)
        {
            this.Procedure = procedure;
            return ExecuteNonQuery();
        }

        /// <summary>
        /// 执行存储过程，仅返回是否成功标志
        /// </summary>
        /// <returns>是否成功标志</returns>
        public Boolean ExecuteNonQuery()
        {
            Boolean RetValue = true;
            // 绑定参数
            if (Bindings() == true)
            {
                try
                {
                    // 执行
                    _sqlCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    _lastError = "execute command error: " + ex.Message;
                    RetValue = false;
                }
            }
            else
            {
                RetValue = false;
            }

            _inputTable.Clear();

            return RetValue;
        }

        /// <summary>
        /// 执行存储过程，返回SqlDataReader
        /// </summary>
        /// <param name="procedure">存储过程名</param>
        /// <returns>数据库读取行的只进流SqlDataReader</returns>
        public SqlDataReader ExecuteReader(String procedure)
        {
            this.Procedure = procedure;
            return ExecuteReader();
        }

        /// <summary>
        /// 执行存储过程，返回SqlDataReader
        /// </summary>
        /// <returns>数据库读取行的只进流SqlDataReader</returns>
        public SqlDataReader ExecuteReader()
        {
            SqlDataReader sqlReader = null;
            // 绑定参数
            if (Bindings() == true)
            {
                try
                {
                    // 执行
                    sqlReader = _sqlCmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    _lastError = "execute command error: " + ex.Message;
                }
            }

            _inputTable.Clear();

            return sqlReader;
        }

        /// <summary>
        /// 执行存储过程，返回SqlDataAdapter
        /// </summary>
        /// <param name="procedure">存储过程名</param>
        /// <returns>SqlDataAdapter</returns>
        public SqlDataAdapter ExecuteAdapter(String procedure)
        {
            this.Procedure = procedure;
            return ExecuteAdapter();
        }

        /// <summary>
        /// 执行存储过程，返回SqlDataAdapter
        /// </summary>
        /// <returns>SqlDataAdapter</returns>
        public SqlDataAdapter ExecuteAdapter()
        {
            SqlDataAdapter sqlAdapter = null;

            // 绑定参数
            if (Bindings() == true)
            {
                try
                {
                    // 执行
                    sqlAdapter = new SqlDataAdapter(_sqlCmd);
                }
                catch (Exception ex)
                {
                    _lastError = "execute command error: " + ex.Message;
                }
            }

            _inputTable.Clear();

            return sqlAdapter;
        }

        /// <summary>
        /// 获取output的键值
        /// </summary>
        /// <param name="outPut">output键名称</param>
        /// <returns>output键值</returns>
        public Object GetOutputValue(String outPut)
        {
            return _sqlCmd.Parameters[outPut].Value;
        }

        /// <summary>
        /// 设置Input参数值
        /// </summary>
        /// <param name="key">参数名</param>
        /// <param name="_value">参数值</param>
        public void SetInputValue(String key, Object _value)
        {
            if (key == null)
            {
                return;
            }
            if (!key.StartsWith("@"))
            {
                key = "@" + key;
            }

            if (_inputTable.ContainsKey(key))
            {
                _inputTable[key] = _value;
            }
            else
            {
                _inputTable.Add(key, _value);
            }
        }

        /// <summary>
        /// 获取已设置的Input参数值
        /// 注：存储过程被成功执行后, Input参数被清空
        /// </summary>
        /// <param name="key">参数名</param>
        /// <returns>参数值</returns>
        public Object GetInputValue(String key)
        {
            if (key == null)
            {
                return null;
            }
            if (!key.StartsWith("@"))
            {
                key = "@" + key;
            }

            if (_inputTable.ContainsKey(key))
            {
                return _inputTable[key];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 给SqlCommand对象绑定参数
        /// </summary>
        /// <returns>是否成功标志</returns>
        private Boolean Bindings()
        {
            _sqlCmd.Parameters.Clear();
            XmlReader sqlXmlReader = GetParameters();
            try
            {
                while (sqlXmlReader.Read())
                {
                    try
                    {
                        if (Byte.Parse(sqlXmlReader["isoutparam"]) == 1)
                        {
                            // 绑定output参数
                            _sqlCmd.Parameters.Add(sqlXmlReader["name"],
                             GetSqlDbType(sqlXmlReader["type"]),
                             Int32.Parse(sqlXmlReader["length"])).Direction = ParameterDirection.Output;
                        }
                        else
                        {
                            // 绑定input参数，并赋值
                            _sqlCmd.Parameters.Add(sqlXmlReader["name"],
                             GetSqlDbType(sqlXmlReader["type"]),
                             Int32.Parse(sqlXmlReader["length"])).Value = this.GetInputValue(sqlXmlReader["name"]);
                            /*
                             * 不必担心赋值的ParametersValue类型问题，SqlParameter.Value是object类型，自动转换
                             */
                        }
                    }
                    catch (Exception ex)
                    {
                        _lastError = sqlXmlReader["name"] + " parameter error: " + ex.Message;
                        return false;
                    }
                }

                // 绑定返回值
                _sqlCmd.Parameters.Add("RetVal", SqlDbType.Variant).Direction = ParameterDirection.ReturnValue;
            }
            catch (Exception ex)
            {
                _lastError = "binding parameter error: " + ex.Message;
                return false;
            }

            return true;
        }

        /// <summary>
        /// 由存储过程名, 取包含参数的XmlReader
        /// </summary>
        /// <returns>包含参数的XmlReader</returns>
        private XmlReader GetParameters()
        {
            String sqlStr = "Select B.[name], C.[name] AS [type], B.length, B.isoutparam, B.isnullable";
            sqlStr += " FROM sysobjects AS A INNER JOIN";
            sqlStr += " syscolumns AS B ON A.id = B.id AND A.xtype = 'P' AND A.name = '" + _srocedure + "' INNER JOIN";
            sqlStr += " systypes C ON B.xtype = C.xtype AND C.[name] <> 'sysname'";
            sqlStr += " orDER BY ROW_NUMBER() OVER (ORDER BY B.id), B.isoutparam";
            sqlStr += " FOR XML RAW";

            SqlCommand sqlCmd;
            System.Data.SqlClient.SqlConnection MyConn = new SqlConnection(_sqlConnection.ConnectionString); ;


            MyConn.Open();
            sqlCmd = new SqlCommand(sqlStr, MyConn);
            // <row name="Action" type="varchar" length="50" isoutparam="0" isnullable="1" />
            XmlReader sqlXmlReader = null;
            try
            {
                sqlXmlReader = sqlCmd.ExecuteXmlReader();
            }
            catch (Exception ex)
            {
                if (sqlXmlReader != null) sqlXmlReader.Close();
                sqlXmlReader = null;
                _lastError = "get parameters error: " + ex.Message;
            }
            finally
            {
                sqlCmd.Dispose();
                sqlCmd = null;
                MyConn.Close();
                MyConn.Dispose();
                MyConn = null;
            }
            return sqlXmlReader;
        }

        /// <summary>
        /// 传入一个类型返回对应的SQL类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        protected internal static SqlDbType GetSqlDbType(String typeName)
        {
            switch (typeName)
            {
                case "image":
                    return SqlDbType.Image;
                case "text":
                    return SqlDbType.Text;
                case "uniqueidentifier":
                    return SqlDbType.UniqueIdentifier;
                case "tinyint":
                    return SqlDbType.TinyInt;
                case "smallint":
                    return SqlDbType.SmallInt;
                case "int":
                    return SqlDbType.Int;
                case "smalldatetime":
                    return SqlDbType.SmallDateTime;
                case "real":
                    return SqlDbType.Real;
                case "money":
                    return SqlDbType.Money;
                case "datetime":
                    return SqlDbType.DateTime;
                case "float":
                    return SqlDbType.Float;
                case "sql_variant":
                    return SqlDbType.Variant;
                case "ntext":
                    return SqlDbType.NText;
                case "bit":
                    return SqlDbType.Bit;
                case "decimal":
                    return SqlDbType.Decimal;
                case "numeric":
                    return SqlDbType.Decimal;
                case "smallmoney":
                    return SqlDbType.SmallMoney;
                case "bigint":
                    return SqlDbType.BigInt;
                case "varbinary":
                    return SqlDbType.VarBinary;
                case "varchar":
                    return SqlDbType.VarChar;
                case "binary":
                    return SqlDbType.Binary;
                case "char":
                    return SqlDbType.Char;
                case "timestamp":
                    return SqlDbType.Timestamp;
                case "nvarchar":
                    return SqlDbType.NVarChar;
                case "nchar":
                    return SqlDbType.NChar;
                case "xml":
                    return SqlDbType.Xml;
                default:
                    return SqlDbType.Variant;
            }
        }
        #endregion
    }
}
