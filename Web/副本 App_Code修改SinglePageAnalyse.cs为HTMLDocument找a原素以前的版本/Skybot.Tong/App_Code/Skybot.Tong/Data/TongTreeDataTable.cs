/******************************************************
* 文件名：TongTreeDataTable.cs
* 文件功能描述：web数据操作 通用 类型 命名空间 Skybot.Tong.Data
* 
* 创建标识：周渊 2008-1-15 
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
    /// 把传入的一个 dataSet 格式化成 和理的Tree型DataTable
    /// 要调用请使用 动态方法实例化
    /// 2008-1-15 加入
    /// </summary>
    [System.ComponentModel.DataObject]
    public class TongTreeDataTable
    {

        /// <summary>
        /// 数据操作的常用方法
        /// </summary>
        private TongUse textData = new TongUse();

        /// <summary>
        /// 用户存放数据的临时表
        /// </summary>
        DataTable showTreeMyDt = new DataTable();

        /// <summary>
        /// 用于保存数据的DataTable
        /// </summary>
        DataTable create_HTML_ColumnNewDt = new DataTable();
        int ii = 0;



        string errorInfo = "";
        /// <summary>
        /// 出错信息
        /// </summary>
        public string ErrorInfo
        {
            get { return errorInfo; }
            set { errorInfo = value; }
        }
        /// <summary>
        /// 记录总数
        /// </summary>
        private int dataCount = 0;

        /// <summary>
        /// 当前的总记录
        /// </summary>
        public int DataCount
        {
            get { return dataCount; }
            set { dataCount = value; }
        }
        /// <summary>
        /// 返回菜单显示的HTML代码
        /// </summary>
        /// <param name="select">显示的展次</param>
        /// <param name="valueColumn">得到的值字段名</param>
        /// <param name="nameCoulmn">得到名称的字段名</param>
        /// <param name="parentID">父ID的字段名</param>
        /// <param name="tableName">要得到的表名</param>
        /// <param name="mainIDColumn">主要ID用于循环 ID</param>
        protected void ShowTree(string select, string valueColumn, string nameCoulmn, string parentID, string tableName, string mainIDColumn)
        {

            string sql;
            int i = 0;
            DataTable ShowTreeMyDt = new DataTable();
            sql = "Select * From [" + tableName + "] where  [" + parentID + "] =" + select + " order by " + valueColumn + " desc";
            try
            {

                ShowTreeMyDt = SqlGetDataSet(sql).Tables[0];//创建DataTable MyDt
            }
            catch (Exception ex)
            {
                errorInfo += ex.Message + "<br/>";
            }
            for (i = 0; i < ShowTreeMyDt.Rows.Count; i++)//遁环记录集
            {
                int o = 0;
                try
                {
                    o = int.Parse(select);
                }
                catch
                {
                    o = 999;
                }
                if (o == 0)//如果只一有层的话
                {

                    DataRow Row = create_HTML_ColumnNewDt.NewRow();
                    Row[0] = ShowTreeMyDt.Rows[i][valueColumn].ToString();
                    Row[1] = textData.EStr(ii, "…") + ShowTreeMyDt.Rows[i][nameCoulmn].ToString();
                    create_HTML_ColumnNewDt.Rows.Add(Row);

                }
                else
                {
                    DataRow Row = create_HTML_ColumnNewDt.NewRow();
                    Row[0] = ShowTreeMyDt.Rows[i][valueColumn].ToString();
                    Row[1] = textData.EStr(ii, "…") + ShowTreeMyDt.Rows[i][nameCoulmn].ToString();
                    create_HTML_ColumnNewDt.Rows.Add(Row);

                }
                ii = ii + 1;
                ShowTree(ShowTreeMyDt.Rows[i][mainIDColumn].ToString(), valueColumn, nameCoulmn, parentID, tableName, mainIDColumn);//调用自己显示菜单ＨＴＭＬ
                ii -= 1;

            }
        }

        #region 处理不同的数据库

        /// <summary>
        /// 传入 sql 返回 DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private DataSet SqlGetDataSet(string sql)
        {

            return textData.SqlataSet(sql);

        }
        #endregion




        /// <summary>
        /// 前台调用的方法
        /// </summary>
        /// <param name="tableName">用于操作的数据库表名称</param>
        /// <param name="mainIDColumn">主要ID用于循环 ID</param>
        /// <param name="valueColumn">得到的值字段 ID or GUid or more</param>
        /// <param name="_name">得到名称的字段名 Name</param>
        /// <param name="parentID">父ID的字段名 ParendID</param>
        /// <returns>返回DataTable</returns>
        public DataTable TreeTable(string tableName, string mainIDColumn, string valueColumn, string _name, string parentID)
        {
            //清除数据
            create_HTML_ColumnNewDt.Clear();
            create_HTML_ColumnNewDt.Dispose();
            DataTable MyDt = new DataTable();

            //添加新列头用于生成列
            create_HTML_ColumnNewDt.Columns.Add("Value");
            create_HTML_ColumnNewDt.Columns.Add("Name");
            ShowTree("0", valueColumn, _name, parentID, tableName, mainIDColumn);
            MyDt = create_HTML_ColumnNewDt;
            DataCount = MyDt.Rows.Count;
            return MyDt;
        }



    }
}
