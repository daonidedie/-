/******************************************************
* 文件名：IDataBaseTable.cs
* 文件功能描述：数据库操作常用方法 表接口的定义 命名空间 Skybot.Tong.Data
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
    #region 定意表结构
    /// <summary>
    /// 用于实现表操作的映射接口
    /// </summary>
    public interface IDataBaseTable
    {
        /// <summary>
        /// 表的主键名
        /// </summary>
        string TableDataKey { get; set; }

        /// <summary>
        /// 数据库表名
        /// </summary>
        string TableName { get; set; }
    }
    #endregion
}
