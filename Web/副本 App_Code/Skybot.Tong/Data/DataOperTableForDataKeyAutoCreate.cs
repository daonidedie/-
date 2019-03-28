/******************************************************
* 文件名：DataOperTableForDataKeyAutoCreate.cs
* 文件功能描述：数据库操作常用方法 表的实体操作方法 用于主键自动生成  命名空间 Skybot.Tong.Data
* 
* 创建标识：周渊 2007-8-18
* 
* 修改标识：周渊 2008-4-26
* 修改标识：周渊 2009-4-1 修改命名空间与目录
* 修改描述：按代码编写规范改写部分代码
* 修改标识: 
 *修改描述: 修改 p[0] = myt; 会返回一条空记录的问题
 *          加入代码 p = new System.Collections.Generic.List<T>().ToArray();
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
    /// 用于实现操作的方法
    /// 用于 自动生成
    /// </summary>
    /// <typeparam name="T">传入实现 IDataBaseTable 的数据表</typeparam>
    [System.ComponentModel.DataObject]
    public class DataOperTableForDataKeyAutoCreate<T> : IDataQueryTable<T> where T : IDataBaseTable, new()
    {

        /// <summary>
        /// 常用的数据库操作方法
        /// </summary>
        TongUse textData = new TongUse();

        /// <summary>
        /// 当前传入类型的实例
        /// </summary>
        T myt = new T();

        /// <summary>
        /// 数据库表映射类的公共属性
        /// </summary>
        private System.Reflection.PropertyInfo[] myDataTableMembers;

        /// <summary>
        /// 用於資料庫操作的類型 聯接
        /// </summary>
        System.Data.SqlClient.SqlConnection mySql = new SqlConnection();

        #region IDataQueryTable<T> 成员



        private string orderBy = "";
        /// <summary>
        /// 要初始化排序条件
        /// </summary>
        public string OrderBy
        {
            get
            {
                return orderBy;
            }
            set
            {
                orderBy = value;
            }
        }

        private string errorInfo = "";
        /// <summary>
        /// 出错的信息
        /// </summary>
        public string ErrorInfo
        {
            get
            {
                return errorInfo;
            }
            set
            {
                errorInfo = value;
            }
        }

        /// <summary>
        /// 用于实现插入的方法
        /// </summary>
        /// <param name="t">更新的对象</param>
        /// <returns></returns>
        public bool Operate_inser(T t)
        {
            System.Reflection.PropertyInfo[] Propertys = GetMembers(t.GetType());//得到成员

            mySql.ConnectionString = textData.MySql.ConnectionString;//建立新的实例
            if (mySql.State == System.Data.ConnectionState.Closed)
            {
                mySql.Open();
            }
            try
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.Text;
                Cmd.Connection = mySql;
                Cmd.CommandText = "INSERT INTO [" + t.TableName + "] (";
                foreach (System.Reflection.PropertyInfo Property in Propertys)
                {
                    if (Property.Name != t.TableDataKey)
                    {
                        Cmd.CommandText += "\t\t\t" + Property.Name + ",\r\n";
                    }
                }


                Cmd.CommandText = Cmd.CommandText.Substring(0, Cmd.CommandText.Length - 3);
                Cmd.CommandText += "			)\r\n";
                Cmd.CommandText += "values( \r\n";

                foreach (System.Reflection.PropertyInfo Property in Propertys)
                {
                    if (Property.Name != t.TableDataKey)
                    {
                        Cmd.CommandText += "\t\t\t@" + Property.Name + ",\r\n";
                    }
                }
                Cmd.CommandText = Cmd.CommandText.Substring(0, Cmd.CommandText.Length - 3);
                Cmd.CommandText += "\t\t\t ) \r\n";


                foreach (System.Reflection.PropertyInfo Property in Propertys)
                {
                    if (Property.Name != t.TableDataKey)
                    {
                        Cmd.Parameters.Add(new SqlParameter("@" + Property.Name, Property.GetValue(t, null)));
                    }
                }

                Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorInfo += "执行 " + t.TableName + "表的添加操作时出现错误" + ex.Message;
                return false;
            }
            finally
            {
                mySql.Close();
            }

            return true;

        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="t">更新的对象</param>
        /// <returns></returns>
        public bool Operate_update(T t)
        {

            System.Reflection.PropertyInfo[] Propertys = GetMembers(t.GetType());//得到成员

            mySql.ConnectionString = textData.MySql.ConnectionString;//建立新的实例
            if (mySql.State == System.Data.ConnectionState.Closed)
            {
                mySql.Open();
            }
            try
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.Text;
                Cmd.Connection = mySql;
                Cmd.CommandText = "UPDATE  [" + t.TableName + "] Set ";
                foreach (System.Reflection.PropertyInfo Property in Propertys)
                {
                    if (Property.Name != t.TableDataKey)
                    {
                        Cmd.CommandText += "\t\t\t" + Property.Name + " = @" + Property.Name + ",\r\n";
                    }
                }
                //删除最后一个 " , "
                Cmd.CommandText = Cmd.CommandText.Substring(0, Cmd.CommandText.Length - 3);
                //加入条件看看 以主键的值更新
                Cmd.CommandText += "\r\n where [" + t.TableDataKey + "] = @" + t.TableDataKey + " \r\n";


                //给参数加入数据值
                foreach (System.Reflection.PropertyInfo Property in Propertys)
                {

                    Cmd.Parameters.Add(new SqlParameter("@" + Property.Name, Property.GetValue(t, null)));

                }

                Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorInfo += "执行 " + t.TableName + "表的 更新 操作时出现错误" + ex.Message;
                return false;
            }
            finally
            {
                mySql.Close();
            }

            return true;
        }
        /// <summary>
        /// 传入 实例化的对象的主键值 此对象一定要指定主键
        /// </summary>
        /// <param name="dataTable_Obj">实例化的数据库表对象</param>
        /// <returns></returns>
        public System.Data.DataSet Oper_Select(T dataTable_Obj)
        {
            SqlDataAdapter MyDa = new SqlDataAdapter("", textData.SqlConnStr);
            MyDa.SelectCommand.Connection = (new TongUse()).MySql;
            MyDa.SelectCommand.CommandType = CommandType.Text;

            #region 查询方法

            MyDa.SelectCommand.CommandText = "select * from  [" + dataTable_Obj.TableName + "] where  [" + dataTable_Obj.TableDataKey + "] = @" + dataTable_Obj.TableDataKey + "  " + OrderBy;

            MyDa.SelectCommand.Parameters.Add("@" + dataTable_Obj.TableDataKey,
                //得到主键的值
                                                dataTable_Obj.GetType().GetProperty(dataTable_Obj.TableDataKey).GetValue(dataTable_Obj, null));
            #endregion


            DataSet MyDs = new DataSet();
            try
            {
                MyDa.Fill(MyDs);
            }
            catch (Exception ex)//資料填充時的異常處理
            {
                if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
                {
                    textData.MySql.Close();
                }
                MyDs = null;
                ErrorInfo += "执行查询操作ExecuteQuery(string Sql)时出现错误" + ex.Message;

            }

            if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
            {
                textData.MySql.Close();

            }
            return MyDs;
        }

        /// <summary>
        /// 传入一个 ID 值 用于得这个ID 值下的所有 数据
        /// </summary>
        /// <param name="_id">主键的ID值</param>
        /// <returns></returns>
        public System.Data.DataSet Oper_Select(string _id)
        {
            SqlDataAdapter MyDa = new SqlDataAdapter("", textData.SqlConnStr);
            MyDa.SelectCommand.Connection = (new TongUse()).MySql;
            MyDa.SelectCommand.CommandType = CommandType.Text;

            #region 查询方法
            MyDa.SelectCommand.CommandText = "select * from [" + myt.TableName + "] where  [" + myt.TableDataKey + "] =" + _id + "  " + OrderBy;
            #endregion


            DataSet MyDs = new DataSet();
            try
            {
                MyDa.Fill(MyDs);
            }
            catch (Exception ex)//資料填充時的異常處理
            {
                if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
                {
                    textData.MySql.Close();
                }
                MyDs = null;
                ErrorInfo += "执行查询操作ExecuteQuery(string Sql)时出现错误" + ex.Message;

            }

            if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
            {
                textData.MySql.Close();

            }
            return MyDs;
        }
        /// <summary>
        /// 传入ID 和反回名 返回一个ID 的一个字段的值
        /// </summary>
        /// <param name="_id">ID</param>
        /// <param name="CoulmnName">字段名</param>
        /// <returns></returns>
        public System.Data.DataSet Oper_Select(string _id, string CoulmnName)
        {
            SqlDataAdapter MyDa = new SqlDataAdapter("", textData.SqlConnStr);
            MyDa.SelectCommand.Connection = (new TongUse()).MySql;
            MyDa.SelectCommand.CommandType = CommandType.Text;

            #region 查询方法
            MyDa.SelectCommand.CommandText = "select " + CoulmnName + " from [" + myt.TableName + "] where  [" + myt.TableDataKey + "] =" + _id + "  " + OrderBy;
            #endregion


            DataSet MyDs = new DataSet();
            try
            {
                MyDa.Fill(MyDs);
            }
            catch (Exception ex)//資料填充時的異常處理
            {
                if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
                {
                    textData.MySql.Close();
                }
                MyDs = null;
                ErrorInfo += "执行查询操作ExecuteQuery(string Sql)时出现错误" + ex.Message;

            }

            if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
            {
                textData.MySql.Close();

            }
            return MyDs;
        }

        /// <summary>
        ///  传入字段名得到这个字段在表里的记录,只有一个字段 
        ///  返回 DataSet
        /// </summary>
        /// <param name="coulmnName">字段名  能在表里找到</param>
        /// <param name="recordNum">读出条数</param>
        /// <returns></returns>
        public System.Data.DataSet Oper_Select(string coulmnName, int recordNum)
        {
            SqlDataAdapter MyDa = new SqlDataAdapter("", textData.SqlConnStr);
            MyDa.SelectCommand.Connection = (new TongUse()).MySql;
            MyDa.SelectCommand.CommandType = CommandType.Text;

            #region 查询方法


            MyDa.SelectCommand.CommandText = "select Top (" + recordNum + ")  " + coulmnName + " from  [" + myt.TableName + "]     " + OrderBy;

            #endregion




            DataSet MyDs = new DataSet();
            try
            {
                MyDa.Fill(MyDs);
            }
            catch (Exception ex)//資料填充時的異常處理
            {
                if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
                {
                    textData.MySql.Close();
                }
                MyDs = null;
                ErrorInfo += "执行查询操作   Oper_Select(string CoulmnName, int RecordNum)(string Sql)时出现错误" + ex.Message;

            }

            if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
            {
                textData.MySql.Close();

            }
            return MyDs;
        }
        /// <summary>
        /// 传入字段集合 返回条数据
        /// </summary>
        /// <param name="coulmnName">字段的数组  数组长度为0 为 得到全部的字段  </param>
        /// <param name="recordNum">得到记录的条数</param>
        /// <returns></returns>
        public System.Data.DataSet OperSelectN(string[] coulmnName, int recordNum)
        {



            SqlDataAdapter MyDa = new SqlDataAdapter("", textData.SqlConnStr);
            MyDa.SelectCommand.Connection = (new TongUse()).MySql;
            MyDa.SelectCommand.CommandType = CommandType.Text;


            #region 查询方法

            string Cloumn = "";
            for (int i = 0; i < coulmnName.Length; i++)
            {
                Cloumn += coulmnName[i].ToString() + ",";
            }

            if (Cloumn != "")
            {
                Cloumn = Cloumn.Substring(0, Cloumn.Length - 1);
            }
            else
            {
                Cloumn = "*";
            }

            MyDa.SelectCommand.CommandText = "select top (" + recordNum + ") " + Cloumn + " from  [" + myt.TableName + "]    " + OrderBy;

            #endregion


            DataSet MyDs = new DataSet();
            try
            {
                MyDa.Fill(MyDs);
            }
            catch (Exception ex)//資料填充時的異常處理
            {
                if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
                {
                    textData.MySql.Close();
                }
                MyDs = null;
                ErrorInfo += "执行查询操作   Oper_Select(string CoulmnName, int RecordNum)(string Sql)时出现错误" + ex.Message;

            }

            if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
            {
                textData.MySql.Close();

            }
            return MyDs;
        }
        /// <summary>
        ///  传入多个字段名得到这个字段在表里的记录,只有一个字段 
        ///  返回 DataSet
        /// </summary>
        /// <param name="coulmnName">字段名  能在表里找到</param>
        /// <param name="recordNum">读出条数</param>
        /// <param name="_where">条件 如 newsid!="" and 0=0 </param>
        /// <returns></returns>
        public System.Data.DataSet Oper_Select(string coulmnName, int recordNum, string _where)
        {


            SqlDataAdapter MyDa = new SqlDataAdapter("", textData.SqlConnStr);
            MyDa.SelectCommand.Connection = (new TongUse()).MySql;
            MyDa.SelectCommand.CommandType = CommandType.Text;

            #region CX_Resume 的查询方法
            MyDa.SelectCommand.CommandText = "select Top (" + recordNum + ")  " + coulmnName + " from  [" + myt.TableName + "]   where " + _where + "  " + OrderBy;
            #endregion




            DataSet MyDs = new DataSet();
            try
            {
                MyDa.Fill(MyDs);
            }
            catch (Exception ex)//資料填充時的異常處理
            {
                if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
                {
                    textData.MySql.Close();
                }
                MyDs = null;
                ErrorInfo += "执行查询操作   Oper_Select(string CoulmnName, int RecordNum)(string Sql)时出现错误" + ex.Message;

            }

            if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
            {
                textData.MySql.Close();

            }
            return MyDs;



        }
        /// <summary>
        ///  传入字段集合 返回条数据
        ///  返回 DataSet
        /// </summary>
        /// <param name="coulmnName">字段名  能在表里找到 字段的数组  数组长度为0 为 得到全部的字段</param>
        /// <param name="recordNum">读出条数</param>
        /// <param name="_where">条件 如 newsid!="" and 0=0 </param>
        /// <returns></returns>
        public System.Data.DataSet OperSelectN(string[] coulmnName, int recordNum, string _where)
        {



            SqlDataAdapter MyDa = new SqlDataAdapter("", textData.SqlConnStr);
            MyDa.SelectCommand.Connection = (new TongUse()).MySql;
            MyDa.SelectCommand.CommandType = CommandType.Text;

            #region 查询方法
            string Cloumn = "";
            for (int i = 0; i < coulmnName.Length; i++)
            {
                Cloumn += coulmnName[i].ToString() + ",";
            }

            if (Cloumn != "")
            {
                Cloumn = Cloumn.Substring(0, Cloumn.Length - 1);
            }
            else
            {
                Cloumn = "*";
            }
            MyDa.SelectCommand.CommandText = "select top (" + recordNum + ") " + Cloumn + " from  [" + myt.TableName + "]   where " + _where + "   " + OrderBy;
            #endregion

            DataSet MyDs = new DataSet();
            try
            {
                MyDa.Fill(MyDs);
            }
            catch (Exception ex)//資料填充時的異常處理
            {
                if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
                {
                    textData.MySql.Close();
                }
                MyDs = null;
                ErrorInfo += "执行查询操作   Oper_Select(string CoulmnName, int RecordNum)(string Sql)时出现错误" + ex.Message;

            }

            if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
            {
                textData.MySql.Close();

            }
            return MyDs;

        }
        /// <summary>
        /// 执行SQL语句 返回dataSet
        /// </summary>
        /// <param name="sql">执行的SQL</param>
        /// <returns></returns>
        public System.Data.DataSet ExecuteQuery(string sql)
        {
            SqlDataAdapter MyDa = new SqlDataAdapter("", textData.SqlConnStr);
            MyDa.SelectCommand.Connection = textData.MySql;
            MyDa.SelectCommand.CommandType = CommandType.Text;

            #region CX_Resume 的查询方法
            MyDa.SelectCommand.CommandText = sql;
            #endregion



            DataSet MyDs = new DataSet();
            try
            {
                MyDa.Fill(MyDs);
            }
            catch (Exception ex)//資料填充時的異常處理
            {
                if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
                {
                    textData.MySql.Close();
                }
                MyDs = null;
                ErrorInfo += "执行查询操作ExecuteQuery(string Sql)时出现错误" + ex.Message;

            }

            if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
            {
                textData.MySql.Close();

            }
            return MyDs;
        }

        /// <summary>
        /// 得到执行的SQL语句
        /// </summary>
        /// <param name="recordNum">得到记录条数 (int)</param>
        /// <returns></returns>
        public string GetSql(int recordNum)
        {
            string tempsql = "select Top (" + recordNum + ") *  from  [" + myt.TableName + "]     " + OrderBy; //当前SQL

            return tempsql;

        }

        /// <summary>
        /// 得到执行的SQL语句
        /// </summary>
        /// <param name="recordNum">得到记录条数 (int)</param>
        /// <param name="_where">查询条件</param>
        /// <returns></returns>
        public string GetSql(int recordNum, string _where)
        {

            string tempsql = "select Top (" + recordNum + ") *  from  [" + myt.TableName + "]   where  " + _where + "   " + OrderBy; //当前SQL

            return tempsql;
        }

        /// <summary>
        /// 得到执行的SQL语句
        /// </summary>
        /// <param name="recordNum">得到记录条数 (string) * 为所有</param>
        /// <returns></returns>
        public string GetSql(string recordNum)
        {
            string tempsql = "select *  from  [" + myt.TableName + "]     " + OrderBy;
            if (recordNum.IndexOf("*") == -1)
            {
                tempsql = "select Top (" + recordNum + ") *  from  [" + myt.TableName + "]     " + OrderBy; //当前SQL
            }

            return tempsql;
        }

        /// <summary>
        /// 得到执行的SQL语句
        /// </summary>
        /// <param name="recordNum">得到记录条数 (string) * 为所有</param>
        /// <param name="_where">查询条件</param>
        /// <returns></returns>
        public string GetSql(string recordNum, string _where)
        {

            string tempsql = "select *  from  [" + myt.TableName + "]   where  " + _where + "   " + OrderBy;
            if (recordNum.IndexOf("*") == -1)
            {
                tempsql = "select Top (" + recordNum + ") *  from  [" + myt.TableName + "]   where  " + _where + "   " + OrderBy; //当前SQL
            }

            return tempsql;
        }






        /// <summary>
        /// 传入删除的对象
        /// </summary>
        /// <param name="dataKeyValue">要删除的对象</param>
        public bool Oper_Del(string dataKeyValue)
        {
            bool tempvalue = false;

            //执行删除
            ExecuteQuery("delete  [" + myt.TableName + "] where  [" + myt.TableDataKey + "] ='" + dataKeyValue + "'  ");

            if (errorInfo == "")
            {
                tempvalue = true;
            }

            return tempvalue;

        }
        /// <summary>
        /// 传入删除的主键值删除对象
        /// </summary>
        /// <param name="dataKeyValue">主键的值</param>
        public bool Oper_DelByDataKey(string dataKeyValue)
        {
            bool tempvalue = false;

            //执行删除
            try
            {
                int.Parse(dataKeyValue);
                ExecuteQuery("delete   [" + myt.TableName + "] where  [" + myt.TableDataKey + "] =" + dataKeyValue + "  ");

            }
            catch
            {
                errorInfo = "";
                ExecuteQuery("delete   [" + myt.TableName + "] where  [" + myt.TableDataKey + "] = '" + dataKeyValue + "'  ");
            }



            if (errorInfo == "")
            {
                tempvalue = true;
            }

            return tempvalue;
        }
        /// <summary>
        /// 传入要删除的主键的数组将一起删除
        /// </summary>
        /// <param name="dataKeyValue">主键的值</param>
        public bool Oper_DelByDataKey(int dataKeyValue)
        {
            bool tempvalue = false;

            //执行删除
            ExecuteQuery("delete  [" + myt.TableName + "] where  [" + myt.TableDataKey + "] =" + dataKeyValue + "  ");

            if (errorInfo == "")
            {
                tempvalue = true;
            }

            return tempvalue;
        }
        /// <summary>
        /// 传入删除的主键值删除对象
        /// </summary>
        /// <param name="dataKeyValue">传入主键的值</param>
        public bool Oper_DelByDataKey(DateTime dataKeyValue)
        {
            bool tempvalue = false;

            //执行删除
            ExecuteQuery("delete  [" + myt.TableName + "] where  [" + myt.TableDataKey + "] = '" + dataKeyValue + "'  ");

            if (errorInfo == "")
            {
                tempvalue = true;
            }

            return tempvalue;
        }
        /// <summary>
        /// 传入删除主键的数组值 string类型的数组
        /// </summary>
        /// <param name="dataKeyValue">传入删除的主键值 请使用 string[]数组</param>
        public bool Oper_DelByDataKey(string[] dataKeyValue)
        {
            bool tempvalue = false;

            string Cloumn = "";
            string Sqlstring = "";
            for (int i = 0; i < dataKeyValue.Length; i++)
            {
                Cloumn += dataKeyValue[i].ToString() + ",";
                Sqlstring += "'" + dataKeyValue[i].ToString() + "'" + ",";
            }

            if (Cloumn != "")
            {
                Cloumn = Cloumn.Substring(0, Cloumn.Length - 1);
                Sqlstring = Sqlstring.Substring(0, Sqlstring.Length - 1);
            }
            else
            {
                Cloumn = "000";
            }

            //执行删除
            ExecuteQuery("delete  [" + myt.TableName + "] where  [" + myt.TableDataKey + "] in (" + Cloumn + ")  ");

            if (errorInfo == "")
            {
                tempvalue = true;
            }
            else
            {
                //执行删除
                ExecuteQuery("delete  [" + myt.TableName + "] where  [" + myt.TableDataKey + "] in (" + Sqlstring + ")  ");
                if (errorInfo == "")
                {
                    tempvalue = true;
                }
            }

            return tempvalue;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="dataKeyValue">传入主键的值</param>
        public bool Oper_Exists(int dataKeyValue)
        {
            ///看看是不是大于0
            try
            {
                if (ExecuteQuery("select [" + myt.TableDataKey + "] from  [" + myt.TableName + "]   where [" + myt.TableDataKey + "] =" + dataKeyValue).Tables[0].Rows.Count > 0)
                {
                    return true;

                }
            }
            catch
            {

                return false;
            }
            return false;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="dataKeyValue">传入主键的值</param>
        public bool Oper_Exists(DateTime dataKeyValue)
        {
            ///看看是不是大于0
            try
            {
                if (ExecuteQuery("select  [" + myt.TableDataKey + "]  from  [" + myt.TableName + "]   where [" + myt.TableDataKey + "] = '" + dataKeyValue + "'").Tables[0].Rows.Count > 0)
                {
                    return true;

                }
            }
            catch
            {

                return false;
            }
            return false;

        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="dataKeyValue">传入主键的值</param>
        public bool Oper_Exists(string dataKeyValue)
        {
            ///看看是不是大于0
            try
            {
                if (ExecuteQuery("select  [" + myt.TableDataKey + "]  from  [" + myt.TableName + "]   where [" + myt.TableDataKey + "] = '" + dataKeyValue + "'").Tables[0].Rows.Count > 0)
                {
                    return true;

                }
            }
            catch
            {

                return false;
            }
            return false;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="dataObject">传入的数据对像</param>
        public bool Oper_Exists(T dataObject)
        {
            ///看看是不是大于0
            try
            {
                if (ExecuteQuery("select [" + myt.TableDataKey + "]   from  [" + myt.TableName + "]   where [" + myt.TableDataKey + "] = '" + dataObject.GetType().GetProperty(myt.TableDataKey).GetValue(dataObject, null) + "'").Tables[0].Rows.Count > 0)
                {
                    return true;

                }
            }
            catch
            {

                return false;
            }
            return false;
        }

        /// <summary>
        /// 是否存在该记录 查找指定的栏目的指定的值
        /// </summary>
        /// <param name="columnValue">栏目值</param>
        /// <param name="column">栏目名</param>
        /// <returns></returns>
        public bool Oper_Exists(string columnValue, string column)
        {
            SqlDataAdapter MyDa = new SqlDataAdapter("", textData.SqlConnStr);
            MyDa.SelectCommand.Connection = textData.MySql;
            MyDa.SelectCommand.CommandType = CommandType.Text;

            #region CX_Resume 的查询方法
            MyDa.SelectCommand.CommandText = "select [" + column + "]   from  [" + myt.TableName + "]   where [" + column + "] = @" + column;
            MyDa.SelectCommand.Parameters.Add(new SqlParameter("@" + column, columnValue));
            #endregion
            DataSet MyDs = new DataSet();
            try
            {
                MyDa.Fill(MyDs);
            }
            catch (Exception ex)//資料填充時的異常處理
            {
                if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
                {
                    textData.MySql.Close();
                }
                MyDs = null;
                ErrorInfo += "执行  是否存在该记录 查找指定的栏目的指定的值 操作 Oper_Exists(string ColumnValue, string Column) 时出现错误" + ex.Message;
            }

            if (textData.MySql.State == ConnectionState.Open)///ConnectionState 得到當前資料源聯接的狀態
            {
                textData.MySql.Close();

            }

            ///看看是不是大于0
            try
            {
                if (MyDs.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
            }
            catch
            {

                return false;
            }
            return false;

        }


        #region 数据转对象
        /// <summary>
        ///  传入读出数据的条数 条件
        ///  返回  类型的数组
        /// </summary>
        /// <param name="recordNum">读出条数</param>
        /// <param name="_where">条件 如 newsid!="" and 0=0 </param>
        /// <returns></returns>
        public T[] Oper_SelectToObject(int recordNum, string _where)
        {
            return (T[])ConvertToObject(OperSelectN(new String[] { }, recordNum, _where));
        }
        /// <summary>
        ///  传入读出数据的条数 
        ///  返回  类型的数组
        /// </summary>
        /// <param name="recordNum">读出条数</param>
        /// <returns></returns>
        public T[] Oper_SelectToObject(int recordNum)
        {
            return (T[])ConvertToObject(OperSelectN(new String[] { }, recordNum));
        }
        #endregion



        #endregion

        /// <summary>
        /// 把对应表的数据转成对象
        /// </summary>
        /// <param name="myDs">传入的dataset</param>
        /// <returns></returns>
        private T[] ConvertToObject(DataSet myDs)
        {
            DataTable Mydt = myDs.Tables[0];

            //定义一个方法用于显示数据 类型的集合
            T[] p = new T[1];
            if (Mydt.Rows.Count == 0)
            {
                //p[0] = myt;
                p = new System.Collections.Generic.List<T>().ToArray();

            }
            else
            {
                p = new T[Mydt.Rows.Count];
            }

            try
            {
                for (int i = 0; i < Mydt.Rows.Count; i++)
                {

                    object Mydato = Activator.CreateInstance(myt.GetType());//实例化新对像

                    #region 实现转换的方法

                    ///开始使用方法传入属性值
                    foreach (System.Reflection.PropertyInfo Proper in myt.GetType().GetProperties())
                    {
                        if (Proper.Name != "TableName" && Proper.Name != "TableDataKey")//删除接口的两个默认属性
                        {
                            Mydato.GetType().GetProperty(Proper.Name).SetValue(Mydato,
                                //传值
                                //  Mydt.Rows[i][Proper.Name].GetType().ToString() 得到当前的类型
                                // Myt.GetType().GetProperty(Proper.Name).GetValue(Myt, null);//返回默认值
                                // Mydt.Rows[i][Proper.Name] 得到当前的值
                                Mydt.Rows[i][Proper.Name].GetType().ToString() == System.DBNull.Value.GetType().ToString() ? myt.GetType().GetProperty(Proper.Name).GetValue(myt, null) : Mydt.Rows[i][Proper.Name]
                                , null);
                        }
                        else
                        {
                            //设定值
                            Mydato.GetType().GetProperty(Proper.Name).SetValue(Mydato,
                                myt.GetType().GetProperty(Proper.Name).GetValue(myt, null),
                                null);
                        }
                    }

                    #endregion
                    p[i] = (T)Mydato;


                }
            }
            catch (Exception ex)
            {
                errorInfo += "执行  把对应表的数据转成对象时出现错误" + ex.Message + ex.Source;
            }

            return p;
        }

        /// <summary>
        /// 得到传入对像的成员 返回下面所有的属性
        /// </summary>
        /// <param name="type">传入 实现 IDataBaseTable接口的数据库操作对像 </param>
        /// <returns></returns>
        private System.Reflection.PropertyInfo[] GetMembers(Type type)
        {

            myDataTableMembers = new System.Reflection.PropertyInfo[type.GetProperties().Length - 2];//设定初始长度
            int i = 0;
            foreach (System.Reflection.PropertyInfo Proper in type.GetProperties())
            {
                if (Proper.Name != "TableName" && Proper.Name != "TableDataKey")//删除接口的两个默认属性
                {
                    myDataTableMembers[i] = Proper;
                }
                i++;
            }

            return myDataTableMembers;
        }

    }
}
