/******************************************************
* 文件名：WorkXml.cs
* 文件功能描述： 操作XML 类型
* 
* 创建标识：周渊 2006-8-22
* 
* 修改标识：周渊 2008-4-26
* 修改描述：按代码编写规范改写部分代码
* 
******************************************************/


using System;
using System.Data;
using System.IO;

namespace Skybot.Tong
{
    #region 操作XML 类型 函数和方法 命名空间 Skybot.Tong

    /// <summary>
    /// 通过DataSet操作XML的类
    /// </summary>
    public class OperateXmlByDataSet
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public OperateXmlByDataSet()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region GetDataSetByXml
        /// <summary>
        /// 读取xml直接返回DataSet
        /// </summary>
        /// <param name="strXmlPath">xml文件相对路径</param>
        /// <returns></returns>
        public static DataSet GetDataSetByXml(string strXmlPath)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                if (ds.Tables.Count > 0)
                {
                    return ds;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region GetDataViewByXml
        /// <summary>
        /// 读取Xml返回一个经排序或筛选后的DataView
        /// </summary>
        /// <param name="strXmlPath">XML文件相对路径</param>
        /// <param name="strWhere">筛选条件，如："name = 'kgdiwss'"</param>
        /// <param name="strSort">排序条件，如："Id desc"</param>
        /// <returns></returns>
        public static DataView GetDataViewByXml(string strXmlPath, string strWhere, string strSort)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                DataView dv = new DataView(ds.Tables[0]);
                if (strSort != null)
                {
                    dv.Sort = strSort;
                }
                if (strWhere != null)
                {
                    dv.RowFilter = strWhere;
                }
                return dv;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region 写入XML来自DataSet WriteXmlByDataSet
        /// <summary>
        /// 向Xml文件插入一行数据
        /// </summary>
        /// <param name="strXmlPath">xml文件相对路径</param>
        /// <param name="_columns">要插入行的列名数组，如：string[] Columns = {"name","IsMarried"};</param>
        /// <param name="_columnValue">要插入行每列的值数组，如：string[] ColumnValue={"明天去要饭","false"};</param>
        /// <returns>成功返回true,否则返回false</returns>
        public static bool WriteXmlByDataSet(string strXmlPath, string[] _columns, string[] _columnValue)
        {
            try
            {
                //根据传入的XML路径得到.XSD的路径，两个文件放在同一个目录下
                string strXsdPath = strXmlPath.Substring(0, strXmlPath.IndexOf(".")) + ".xsd";

                DataSet ds = new DataSet();
                //读xml架构，关系到列的数据类型
                ds.ReadXmlSchema(GetXmlFullPath(strXsdPath));
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                DataTable dt = ds.Tables[0];
                //在原来的表格基础上创建新行
                DataRow newRow = dt.NewRow();

                //循环给一行中的各个列赋值
                for (int i = 0; i < _columns.Length; i++)
                {
                    newRow[_columns[i]] = _columnValue[i];
                }
                dt.Rows.Add(newRow);
                dt.AcceptChanges();
                ds.AcceptChanges();

                ds.WriteXml(GetXmlFullPath(strXmlPath));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region UpdateXmlRow
        /// <summary>
        /// 更行符合条件的一条Xml记录
        /// </summary>
        /// <param name="strXmlPath">XML文件路径</param>
        /// <param name="_columns">列名数组</param>
        /// <param name="_columnValue">列值数组</param>
        /// <param name="strWhereColumnName">条件列名</param>
        /// <param name="strWhereColumnValue">条件列值</param>
        /// <returns></returns>
        public static bool UpdateXmlRow(string strXmlPath, string[] _columns, string[] _columnValue, string strWhereColumnName, string strWhereColumnValue)
        {
            try
            {
                string strXsdPath = strXmlPath.Substring(0, strXmlPath.IndexOf(".")) + ".xsd";

                DataSet ds = new DataSet();
                //读xml架构，关系到列的数据类型
                ds.ReadXmlSchema(GetXmlFullPath(strXsdPath));
                ds.ReadXml(GetXmlFullPath(strXmlPath));

                //先判断行数
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //如果当前记录为符合Where条件的记录
                        if (ds.Tables[0].Rows[i][strWhereColumnName].ToString().Trim().Equals(strWhereColumnValue))
                        {
                            //循环给找到行的各列赋新值
                            for (int j = 0; j < _columns.Length; j++)
                            {
                                ds.Tables[0].Rows[i][_columns[j]] = _columnValue[j];
                            }
                            //更新DataSet
                            ds.AcceptChanges();
                            //重新写入XML文件
                            ds.WriteXml(GetXmlFullPath(strXmlPath));
                            return true;
                        }
                    }

                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region DeleteXmlRowByIndex
        /// <summary>
        /// 通过删除DataSet中iDeleteRow这一行，然后重写Xml以实现删除指定行
        /// </summary>
        /// <param name="strXmlPath"></param>
        /// <param name="iDeleteRow">要删除的行在DataSet中的Index值</param>
        public static bool DeleteXmlRowByIndex(string strXmlPath, int iDeleteRow)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //删除符号条件的行
                    ds.Tables[0].Rows[iDeleteRow].Delete();
                }
                ds.WriteXml(GetXmlFullPath(strXmlPath));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region DeleteXmlRows
        /// <summary>
        /// 删除strColumn列中值为ColumnValue的行
        /// </summary>
        /// <param name="strXmlPath">xml相对路径</param>
        /// <param name="strColumn">列名</param>
        /// <param name="ColumnValue">strColumn列中值为ColumnValue的行均会被删除</param>
        /// <returns></returns>
        public static bool DeleteXmlRows(string strXmlPath, string strColumn, string[] ColumnValue)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));

                //先判断行数
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //判断行多还是删除的值多，多的for循环放在里面
                    if (ColumnValue.Length > ds.Tables[0].Rows.Count)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < ColumnValue.Length; j++)
                            {
                                if (ds.Tables[0].Rows[i][strColumn].ToString().Trim().Equals(ColumnValue[j]))
                                {
                                    ds.Tables[0].Rows[i].Delete();
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < ColumnValue.Length; j++)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (ds.Tables[0].Rows[i][strColumn].ToString().Trim().Equals(ColumnValue[j]))
                                {
                                    ds.Tables[0].Rows[i].Delete();
                                }
                            }
                        }
                    }
                    ds.WriteXml(GetXmlFullPath(strXmlPath));
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion




        #region 写入或替换XML文件 方法名: WriteXml

        /// <summary>
        /// 写入或替换XML文件 方法名: WriteXml 返回 bool值
        /// </summary>
        /// <param name="XmlString">要写入的XML文件代码 已格式化好的</param>
        /// <param name="Path">要写入的新文件名 要加Server.Path</param>
        /// <returns>返回 bool值</returns>
        public static bool WriteXml(string XmlString, string Path)
        {
            try
            {
                FileStream XmlFle = new FileStream(Path, FileMode.Create, FileAccess.ReadWrite);//对文件的引用后


                StreamWriter WrXml = new StreamWriter(XmlFle);//StreamWriter 可以向文件写入字符串，而 FileStream 则不能。这将使您的工作变得容易许多。 
                WrXml.Write(XmlString);//写入文件 cc 为写入文件的内容
                WrXml.Close();//关闭
                XmlFle.Close();//关闭
                return true;
            }
            catch (Exception ex)
            {
                TongUse TextData = new TongUse();

                TextData.Err(ex.Message);
                return false;
            }
        }

        #endregion


        #region 显示树的XML文件 调用方法 ShowTree()

        ///// <summary>
        ///// SelectXML 方法要输出的XML文本文件 使用请
        ///// </summary>
        //string TreeXmlText = "<?xml version='1.0' encoding='utf-8'?>\r<系统菜单>";

        TongUse TextData = new TongUse();

        ///// <summary>
        ///// 写XML层次函数
        ///// </summary>
        ///// <param name="parentId">参数parentId 递归从属 ID</param>
        ///// <param name="layer">参数parentId 递归 层次ID 相当于XML文件的层次结构 </param>
        //protected void SelectXML(int parentId, int layer)
        //{
        //    int j, cId = 0;
        //    string sNbsp = "";//输出的空格 \t 为TAB 

        //    //建立记录集  相当于ASP 中的rs 

        //    DataTable Myds = TextData.SqlParameStor("ShowTree", "@parentId=" + parentId).Tables[0];//调用存贮过程创建DataTalbe对象

        //    j = Myds.Rows.Count;// Myds.Rows.Count记录集中的记录数

        //    for (int i = 0; i <= (layer - 1); i++)//输入TAB 显示层次
        //    {
        //        sNbsp += "\t \t";
        //    }

        //    //TreeXmlText += "<一级分类菜单 ";//输出XML



        //    for (int i = 0; i < j; i++)
        //    {

        //        cId = Int32.Parse(Myds.Rows[i]["id"].ToString());//得到现在行的索引值;相当于ASP rs("id")

        //        DataTable MyDt = TextData.SqlParameStor("ShowTreeId", "@idX=" + cId).Tables[0];//建立从属记录集

        //        if (MyDt.Rows.Count > 0)//看看有是不是不只一条记录
        //        {


        //            TreeXmlText += sNbsp + cId + "<一级分类菜单 分类菜单名='" + MyDt.Rows[0]["ClassName"].ToString() + "' 显示url='~/" + TextData.CodeUrlText(MyDt.Rows[0]["URL"].ToString()) + "' Trang='main'>\n";



        //        }
        //        else//如果只有一条记录
        //        {

        //            TreeXmlText += sNbsp + cId + "<一级分类菜单 分类菜单名='" + Myds.Rows[0]["ClassName"].ToString() + "' 显示url='~/" + TextData.CodeUrlText(Myds.Rows[0]["URL"].ToString()) + "' Trang='main'>\n";
        //        }


        //        SelectXML(cId, (layer + 1));//调用函数本身执行递归

        //        TreeXmlText += sNbsp + "</一级分类菜单>\n";


        //    }






        //    // TreeXmlText += "</一级分类菜单>\n"; 一定要删掉　不然ＸＭＬ文件无法显示
        //}

        ///// <summary>
        ///// 返回XML字符
        ///// </summary>
        //public String ShowTree()
        //{
        //    SelectXML(0, 0);
        //    TreeXmlText += "</系统菜单>";
        //    return TreeXmlText;
        //}

        #endregion

        #region DeleteXmlAllRows
        /// <summary>
        /// 删除所有行
        /// </summary>
        /// <param name="strXmlPath">XML路径</param>
        /// <returns></returns>
        public static bool DeleteXmlAllRows(string strXmlPath)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                //如果记录条数大于0
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //移除所有记录
                    ds.Tables[0].Rows.Clear();
                }
                //重新写入，这时XML文件中就只剩根节点了
                ds.WriteXml(GetXmlFullPath(strXmlPath));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region GetXmlFullPath
        /// <summary>
        /// 返回完整路径
        /// </summary>
        /// <param name="strPath">Xml的路径</param>
        /// <returns></returns>
        public static string GetXmlFullPath(string strPath)
        {
            if (strPath.IndexOf(":") > 0)
            {
                return strPath;
            }
            else
            {
                return System.Web.HttpContext.Current.Server.MapPath(strPath);
            }
        }
        #endregion

        #region 遍历XML文件 调用方法 调用方法 XmlFileAllNodes(Server.MapPath("treeout.xml"), "")
        /// <summary>
        /// 添加栏目的临时级别
        /// </summary>
        int XmlFileAllNodesCloumnLvTemp = 0;

        /// <summary>
        /// 添加栏目的临时ID
        /// </summary>
        int XmlFileAllNodesColumnsIDTemp = 0;

        /// <summary>
        /// 添加栏目函数
        /// </summary>
        /// <param name="xmlpath">XML栏目文件</param>
        /// <param name="XmlData">为空 "" </param>
        /// <![CDATA[调用方法 XmlFileAllNodes(Server.MapPath("treeout.xml"), "")]]>
        /// <returns></returns>
        public string XmlFileAllNodes(string xmlpath, String XmlData)
        {
            System.Xml.XmlDataDocument MyXml = new System.Xml.XmlDataDocument();
            if (XmlData == "")
            {
                MyXml.Load(xmlpath);//得到XML数据
            }
            else
            {
                MyXml.LoadXml(XmlData);
            }

            for (int i = 0; i < MyXml.DocumentElement.ChildNodes.Count; i++)
            {

                try
                {   //得到树状结构
                    if (XmlFileAllNodesCloumnLvTemp == 0)//一级节点
                    {
                        System.Web.HttpContext.Current.Response.Write(TextData.EStr(XmlFileAllNodesCloumnLvTemp, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") + XmlFileAllNodesColumnsIDTemp + MyXml.DocumentElement.ChildNodes[i].Attributes.GetNamedItem("Cont").Value + XmlFileAllNodesCloumnLvTemp + "\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n<br/>");
                    }
                    else
                    {

                        System.Web.HttpContext.Current.Response.Write(TextData.EStr(XmlFileAllNodesCloumnLvTemp, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") + XmlFileAllNodesColumnsIDTemp + MyXml.DocumentElement.ChildNodes[i].Attributes.GetNamedItem("Cont").Value + XmlFileAllNodesCloumnLvTemp + "\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n<br/>");
                        //如果是子节点

                    }
                    XmlFileAllNodesColumnsIDTemp += 1;
                    XmlFileAllNodesCloumnLvTemp++;
                    XmlFileAllNodes(xmlpath, MyXml.DocumentElement.ChildNodes[i].OuterXml);//得到这个XML文件下的所有子节点
                    XmlFileAllNodesCloumnLvTemp--;
                }
                catch (System.Xml.XmlException)//这里如是最下层就不进行下一次的调用自己了
                {
                }
            }
            return "";
        }
        #endregion
    }

    /// <summary>
    /// 格式化HTML 类
    /// </summary>
    public class HtmlToXML
    {


        /// <summary>
        /// Html To XMl  返回格式化好的XML文件
        /// </summary>
        /// <param name="html">传入要格式化的HTML文件</param>
        /// <returns>返回格式化好的XML文件</returns>
        public static string HTMLConvert(string html)
        {
            if (string.IsNullOrEmpty(html.Trim()))
            {
                return string.Empty;
            }
            //solve ]]> 
            //处理节点
            html = System.Text.RegularExpressions.Regex.Replace(html, @"<!\s{0,}\[\s{0,}CDATA\s{0,}\[\s{0,}|\s{0,}\]\s{0,}\]\s{0,}>", "");
            using (Sgml.SgmlReader reader = new Sgml.SgmlReader())
            {
                reader.DocType = "HTML";
                reader.InputStream = new System.IO.StringReader(html);
                using (System.IO.StringWriter stringWriter = new System.IO.StringWriter())
                {
                    //实例化对象
                    using (System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(stringWriter))
                    {
                        reader.WhitespaceHandling = System.Xml.WhitespaceHandling.None;
                        writer.Formatting = System.Xml.Formatting.Indented;
                        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                        doc.Load(reader);
                        if (doc.DocumentElement == null)
                        {
                            return "Html to XML Error this programe can not Convert";
                        }
                        else
                        {
                            doc.DocumentElement.WriteContentTo(writer);
                        }
                        writer.Close();
                        string xhtml = stringWriter.ToString();
                        if (xhtml == null)
                        {
                            return stringWriter.ToString();
                        }
                        return xhtml;
                    }
                }
            }
        }
    }
    #endregion
}


