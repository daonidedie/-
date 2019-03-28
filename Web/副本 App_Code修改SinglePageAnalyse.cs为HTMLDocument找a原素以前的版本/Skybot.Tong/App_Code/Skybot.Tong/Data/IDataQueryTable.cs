/******************************************************
* 文件名：IDataQueryTable.cs
* 文件功能描述：数据库操作常用方法 接口定义 命名空间 Skybot.Tong.Data
* 
* 创建标识：周渊 2007-8-18
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skybot.Tong.Data
{
    /// <summary>
    /// 用于数据表操作的接口
    /// </summary>
    /// <typeparam name="T">当前操作数据库表在 项目中的 映射 类型</typeparam>
    public interface IDataQueryTable<T>
    {

        /// <summary>
        /// 要初始化排序条件
        /// </summary>
        string OrderBy { get; set; }

        /// <summary>
        /// 出错的信息
        /// </summary>
        string ErrorInfo { get; set; }


        /// <summary>
        /// 用于实现插入的方法
        /// </summary>
        /// <param name="t">更新的对象</param>
        /// <returns></returns>
        bool Operate_inser(T t);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="t">更新的对象</param>
        /// <returns></returns>
        bool Operate_update(T t);

        /// <summary>
        /// 传入 实例化的对象的主键值 此对象一定要指定主键
        /// </summary>
        /// <param name="dataTable_Obj">实例化的数据库表对象</param>
        /// <returns></returns>
        DataSet Oper_Select(T dataTable_Obj);

        /// <summary>
        /// 传入一个 ID 值 用于得这个ID 值下的所有 数据
        /// </summary>
        /// <param name="_id">主键的ID值</param>
        /// <returns></returns>
        DataSet Oper_Select(string _id);

        /// <summary>
        /// 传入ID 和反回名 返回一个ID 的一个字段的值
        /// </summary>
        /// <param name="_id">ID</param>
        /// <param name="coulmnName">字段名</param>
        /// <returns></returns>
        DataSet Oper_Select(string _id, string coulmnName);

        /// <summary>
        ///  传入字段名得到这个字段在表里的记录,只有一个字段 
        ///  返回 DataSet
        /// </summary>
        /// <param name="coulmnName">字段名  能在表里找到</param>
        /// <param name="recordNum">读出条数</param>
        /// <returns></returns>
        DataSet Oper_Select(string coulmnName, int recordNum);


        /// <summary>
        /// 传入字段集合 返回条数据
        /// </summary>
        /// <param name="coulmnName">字段的数据组 多个用</param>
        /// <param name="recordNum">得到记录的条数</param>
        /// <returns></returns>
        DataSet OperSelectN(string[] coulmnName, int recordNum);

        /// <summary>
        ///  传入字段名得到这个字段在表里的记录,只有一个字段 
        ///  返回 DataSet
        /// </summary>
        /// <param name="coulmnName">字段名  能在表里找到</param>
        /// <param name="recordNum">读出条数</param>
        /// <param name="_where">条件 如 newsid!="" and 0=0 </param>
        /// <returns></returns>
        DataSet Oper_Select(string coulmnName, int recordNum, string _where);


        /// <summary>
        /// 传入字段集合 返回条数据
        /// </summary>
        /// <param name="coulmnName">字段的数据组 多个用</param>
        /// <param name="recordNum">得到记录的条数</param>
        /// <param name="_where">条件 如 newsid!="" and 0=0 </param>
        /// <returns></returns>
        DataSet OperSelectN(string[] coulmnName, int recordNum, string _where);

        /// <summary>
        /// 执行SQL语句 返回dataSet
        /// </summary>
        /// <param name="Sql">执行的SQL</param>
        /// <returns></returns>
        DataSet ExecuteQuery(string Sql);

        /// <summary>
        /// 得到执行的SQL语句
        /// </summary>
        /// <param name="recordNum">得到记录条数 (int)</param>
        /// <returns></returns>
        string GetSql(int recordNum);

        /// <summary>
        /// 得到执行的SQL语句
        /// </summary>
        /// <param name="recordNum">得到记录条数 (int)</param>
        /// <param name="_where">查询条件</param>
        /// <returns></returns>
        string GetSql(int recordNum, string _where);

        /// <summary>
        /// 得到执行的SQL语句
        /// </summary>
        /// <param name="recordNum">得到记录条数 (string) * 为所有</param>
        /// <returns></returns>
        string GetSql(string recordNum);

        /// <summary>
        /// 得到执行的SQL语句
        /// </summary>
        /// <param name="recordNum">得到记录条数 (string) * 为所有</param>
        /// <param name="_where">查询条件</param>
        /// <returns></returns>
        string GetSql(string recordNum, string _where);


        /// <summary>
        /// 传入删除的对象
        /// </summary>
        /// <param name="dataKeyValue">要删除的对象</param>
        bool Oper_Del(string dataKeyValue);

        /// <summary>
        /// 传入删除的主键值删除对象
        /// </summary>
        /// <param name="dataKeyValue">主键的值</param>
        bool Oper_DelByDataKey(string dataKeyValue);

        /// <summary>
        /// 传入要删除的主键的数组将一起删除
        /// </summary>
        /// <param name="dataKeyValue">主键的值</param>
        bool Oper_DelByDataKey(int dataKeyValue);

        /// <summary>
        /// 传入删除的主键值删除对象
        /// </summary>
        /// <param name="dataKeyValue">传入主键的值</param>
        bool Oper_DelByDataKey(DateTime dataKeyValue);

        /// <summary>
        /// 传入删除主键的数组值 string类型的数组
        /// </summary>
        /// <param name="dataKeyValue">传入删除的主键值 请使用 string[]数组</param>
        bool Oper_DelByDataKey(string[] dataKeyValue);

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="dataKeyValue">传入主键的值</param>
        bool Oper_Exists(int dataKeyValue);

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="dataKeyValue">传入主键的值</param>
        bool Oper_Exists(DateTime dataKeyValue);


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="dataKeyValue">传入主键的值</param>
        bool Oper_Exists(string dataKeyValue);

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="dataObject">传入的数据对像</param>
        bool Oper_Exists(T dataObject);

        /// <summary>
        /// 是否存在该记录 查找指定的栏目的指定的值
        /// </summary>
        /// <param name="columnValue">栏目值</param>
        /// <param name="column">栏目名</param>
        /// <returns></returns>
        bool Oper_Exists(string columnValue, string column);

        #region 数据转对象

        /// <summary>
        ///  传入读出数据的条数 条件
        ///  返回 Data_CX_NEWS 类型的数组
        /// </summary>
        /// <param name="recordNum">读出条数</param>
        /// <param name="_where">条件 如 newsid!="" and 0=0 </param>
        /// <returns></returns>
        T[] Oper_SelectToObject(int recordNum, string _where);

        /// <summary>
        ///  传入读出数据的条数 
        ///  返回 Data_CX_NEWS 类型的数组
        /// </summary>
        /// <param name="recordNum">读出条数</param>
        /// <returns></returns>
        T[] Oper_SelectToObject(int recordNum);


        #endregion






    }
}
