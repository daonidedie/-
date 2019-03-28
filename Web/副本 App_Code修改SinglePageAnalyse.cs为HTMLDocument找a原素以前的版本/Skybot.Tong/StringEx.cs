/******************************************************
* 文件名：Exceloper.cs
* 文件功能描述：Excel 输出打印模块  命名空间 Skybot.Tong
* 创建标识：周渊 2007-8-18
* 修改标识：周渊 2008-5-8
* 修改标识：周渊 2009-4-1 修改命名空间与目录
* 修改描述：按代码编写规范改写部分代码
* 
******************************************************/

using System;
using System.IO;
using System.Text;
using System.Data;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using System.Data.OleDb;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Skybot.Tong
{
    /// <summary>
    /// 说    明：Excel输出打印模块
    ///			  暂时不提供操作Excel对象样式方法，样式可以在Excel模板中设置好
    /// </summary>
    public class ExcelOperate
    {
        /// <summary> 
        /// 将DataTable中的数据导出到指定的Excel文件中 
        /// </summary> 
        /// <param name="page">Web页面对象</param> 
        /// <param name="tab">包含被导出数据的DataTable对象</param> 
        /// <param name="fileName">Excel文件的名称</param> 
        public static bool Export(System.Web.UI.Page page, System.Data.DataTable tab, string fileName)
        {
            bool returnvalue = false;

            System.Web.HttpResponse httpResponse = page.Response;
            System.Web.UI.WebControls.DataGrid dataGrid = new System.Web.UI.WebControls.DataGrid();
            dataGrid.DataSource = tab.DefaultView;
            dataGrid.AllowPaging = false;
            dataGrid.HeaderStyle.BackColor = System.Drawing.Color.BurlyWood;
            dataGrid.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            dataGrid.HeaderStyle.Font.Bold = true;
            dataGrid.DataBind();
            httpResponse.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8)); //filename="*.xls"; 
            httpResponse.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            httpResponse.ContentType = "application/ms-excel";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            dataGrid.RenderControl(hw);

            string filePath = System.Web.HttpContext.Current.Server.MapPath("/BasicInfo/temp/") + fileName;
            System.IO.StreamWriter sw = System.IO.File.CreateText(filePath);
            sw.Write(tw.ToString());
            sw.Close();
            sw.Dispose();
            returnvalue = DownFile(httpResponse, fileName, filePath);
            httpResponse.End();

            return returnvalue;
        }

        /// <summary>
        /// 输入下载的文件
        /// </summary>
        /// <param name="Response">用于输出的对像</param>
        /// <param name="fileName">输出文件名</param>
        /// <param name="fullPath">输出路径</param>
        private static bool DownFile(System.Web.HttpResponse Response, string fileName, string fullPath)
        {
            try
            {
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" +
                HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8) + ";charset=UTF-8");
                System.IO.FileStream fs = System.IO.File.OpenRead(fullPath);
                long fLen = fs.Length;
                int size = 102400;//每100K同时下载数据 
                byte[] readData = new byte[size];//指定缓冲区的大小 
                if (size > fLen) size = Convert.ToInt32(fLen);
                long fPos = 0;
                bool isEnd = false;
                while (!isEnd)
                {
                    if ((fPos + size) > fLen)
                    {
                        size = Convert.ToInt32(fLen - fPos);
                        readData = new byte[size];
                        isEnd = true;
                    }
                    fs.Read(readData, 0, size);//读入一个压缩块 
                    Response.BinaryWrite(readData);
                    fPos += size;
                }
                fs.Close();
                fs.Dispose();

                System.IO.File.Delete(fullPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary> 
        /// 将指定Excel文件中的数据转换成DataTable对象，供应用程序进一步处理 
        /// </summary> 
        /// <param name="filePath">文件路径</param> 
        public static System.Data.DataTable Import(string filePath)
        {
            System.Data.DataTable rs = new System.Data.DataTable();
            bool canOpen = false;

            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;" +
            "Data Source=" + filePath + ";" +
            "Extended Properties=\"Excel 8.0;\"");

            try//尝试数据连接是否可用 
            {
                conn.Open();
                conn.Close();
                canOpen = true;
            }
            catch { }

            if (canOpen)
            {
                try//如果数据连接可以打开则尝试读入数据 
                {
                    OleDbCommand myOleDbCommand = new OleDbCommand("SELECT * FROM [Sheet1$]", conn);
                    OleDbDataAdapter myData = new OleDbDataAdapter(myOleDbCommand);
                    myData.Fill(rs);
                    conn.Close();
                }
                catch//如果数据连接可以打开但是读入数据失败，则从文件中提取出工作表的名称，再读入数据 
                {
                    string sheetName = GetSheetName(filePath);
                    if (sheetName.Length > 0)
                    {
                        OleDbCommand myOleDbCommand = new OleDbCommand("SELECT * FROM [" + sheetName + "$]", conn);
                        OleDbDataAdapter myData = new OleDbDataAdapter(myOleDbCommand);
                        myData.Fill(rs);
                        conn.Close();
                    }
                }
            }
            else
            {
                System.IO.StreamReader tmpStream = File.OpenText(filePath);
                string tmpStr = tmpStream.ReadToEnd();
                tmpStream.Close();
                rs = GetDataTableFromString(tmpStr);
                tmpStr = "";
            }
            return rs;
        }

        /// <summary> 
        /// 将指定Html字符串的数据转换成DataTable对象 --根据tr,td等特殊字符进行处理 
        /// </summary> 
        /// <param name="tmpHtml">Html字符串</param> 
        /// <returns>返回DataTable</returns> 
        private static DataTable GetDataTableFromString(string tmpHtml)
        {
            string tmpStr = tmpHtml;
            DataTable TB = new DataTable();
            try
            {
                //先处理一下这个字符串，删除第一个<tr>之前合最后一个</tr>之后的部分 
                int index = tmpStr.IndexOf("<tr");
                if (index > -1)
                    tmpStr = tmpStr.Substring(index);
                else
                    return TB;

                index = tmpStr.LastIndexOf("</tr>");
                if (index > -1)
                    tmpStr = tmpStr.Substring(0, index + 5);
                else
                    return TB;

                bool existsSparator = false;
                char Separator = Convert.ToChar("^");

                //如果原字符串中包含分隔符“^”则先把它替换掉 
                if (tmpStr.IndexOf(Separator.ToString()) > -1)
                {
                    existsSparator = true;
                    tmpStr = tmpStr.Replace("^", "^$&^");
                }

                //先根据“</tr>”分拆 
                string[] tmpRow = tmpStr.Replace("</tr>", "^").Split(Separator);

                for (int i = 0; i < tmpRow.Length - 1; i++)
                {
                    DataRow newRow = TB.NewRow();

                    string tmpStrI = tmpRow[i];
                    if (tmpStrI.IndexOf("<tr") > -1)
                    {
                        tmpStrI = tmpStrI.Substring(tmpStrI.IndexOf("<tr"));
                        if (tmpStrI.IndexOf("display:none") < 0 || tmpStrI.IndexOf("display:none") > tmpStrI.IndexOf(">"))
                        {
                            tmpStrI = tmpStrI.Replace("</td>", "^");
                            string[] tmpField = tmpStrI.Split(Separator);

                            for (int j = 0; j < tmpField.Length - 1; j++)
                            {
                                tmpField[j] = RemoveString(tmpField[j], "<font>");
                                index = tmpField[j].LastIndexOf(">") + 1;
                                if (index > 0)
                                {
                                    string field = tmpField[j].Substring(index, tmpField[j].Length - index);
                                    if (existsSparator) field = field.Replace("^$&^", "^");
                                    if (i == 0)
                                    {
                                        string tmpFieldName = field;
                                        int sn = 1;
                                        while (TB.Columns.Contains(tmpFieldName))
                                        {
                                            tmpFieldName = field + sn.ToString();
                                            sn += 1;
                                        }
                                        TB.Columns.Add(tmpFieldName);
                                    }
                                    else
                                    {
                                        newRow[j] = field;
                                    }
                                }
                            }
                            if (i > 0)
                                TB.Rows.Add(newRow);
                        }
                    }
                }

                TB.AcceptChanges();
            }
            catch { }

            return TB;
        }

        /// <summary> 
        /// 从指定Html字符串中剔除指定的对象 
        /// </summary> 
        /// <param name="tmpHtml">Html字符串</param> 
        /// <param name="remove">需要剔除的对象--例如输入"《font》"则剔除"《font ???????》"和"《/font》"</param>  
        public static string RemoveString(string tmpHtml, string remove)
        {
            tmpHtml = tmpHtml.Replace(remove.Replace("<", "</"), "");
            tmpHtml = RemoveStringHead(tmpHtml, remove);
            return tmpHtml;
        }

        /// <summary> 
        /// 只供方法RemoveString()使用 
        /// </summary>
        /// <param name="tmpHtml">HTML文本</param>
        /// <param name="remove">移除内容</param>
        private static string RemoveStringHead(string tmpHtml, string remove)
        {
            //为了方便注释，假设输入参数remove="<font>" 
            if (remove.Length < 1) return tmpHtml;//参数remove为空：不处理返回 
            if ((remove.Substring(0, 1) != "<") || (remove.Substring(remove.Length - 1) != ">")) return tmpHtml;//参数remove不是<?????>：不处理返回 

            int IndexS = tmpHtml.IndexOf(remove.Replace(">", ""));//查找“<font”的位置 
            int IndexE = -1;
            if (IndexS > -1)
            {
                string tmpRight = tmpHtml.Substring(IndexS, tmpHtml.Length - IndexS);
                IndexE = tmpRight.IndexOf(">");
                if (IndexE > -1)
                    tmpHtml = tmpHtml.Substring(0, IndexS) + tmpHtml.Substring(IndexS + IndexE + 1);
                if (tmpHtml.IndexOf(remove.Replace(">", "")) > -1)
                    tmpHtml = RemoveStringHead(tmpHtml, remove);
            }

            return tmpHtml;
        }

        /// <summary> 
        /// 将指定Excel文件中读取第一张工作表的名称 
        /// </summary> 
        /// <param name="filePath">文件路径</param> 
        private static string GetSheetName(string filePath)
        {
            string sheetName = "";
            System.IO.FileStream tmpStream = File.OpenRead(filePath);
            byte[] fileByte = new byte[tmpStream.Length];
            tmpStream.Read(fileByte, 0, fileByte.Length);
            tmpStream.Close();

            byte[] tmpByte = new byte[]{Convert.ToByte(11),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0), 
Convert.ToByte(11),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0), 
Convert.ToByte(30),Convert.ToByte(16),Convert.ToByte(0),Convert.ToByte(0)};

            int index = GetSheetIndex(fileByte, tmpByte);
            if (index > -1)
            {

                index += 16 + 12;
                System.Collections.ArrayList sheetNameList = new System.Collections.ArrayList();

                for (int i = index; i < fileByte.Length - 1; i++)
                {
                    byte temp = fileByte[i];
                    if (temp != Convert.ToByte(0))
                        sheetNameList.Add(temp);
                    else
                        break;
                }
                byte[] sheetNameByte = new byte[sheetNameList.Count];
                for (int i = 0; i < sheetNameList.Count; i++)
                    sheetNameByte[i] = Convert.ToByte(sheetNameList[i]);

                sheetName = System.Text.Encoding.Default.GetString(sheetNameByte);
            }
            return sheetName;
        }


        /// <summary> 
        /// 只供方法GetSheetName()使用 
        /// </summary> 
        private static int GetSheetIndex(byte[] FindTarget, byte[] FindItem)
        {
            int index = -1;

            int FindItemLength = FindItem.Length;
            if (FindItemLength < 1) return -1;
            int FindTargetLength = FindTarget.Length;
            if ((FindTargetLength - 1) < FindItemLength) return -1;

            for (int i = FindTargetLength - FindItemLength - 1; i > -1; i--)
            {
                System.Collections.ArrayList tmpList = new System.Collections.ArrayList();
                int find = 0;
                for (int j = 0; j < FindItemLength; j++)
                {
                    if (FindTarget[i + j] == FindItem[j]) find += 1;
                }
                if (find == FindItemLength)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        /// <summary>
        /// 返回传入的excel是不是正确的时刻表格式
        /// </summary>
        /// <param name="absFileName">绝对路径文件名</param>
        /// <param name="htmStr">返回的HTML字符串用于生成Excel</param>
        /// <param name="excelDt">返回的DataTable 用于处理数据源</param>
        public static bool IsExcelFormatRight(string absFileName, out string htmStr, out DataTable excelDt)
        {
            // 时刻表
            DataTable timedataTable = new DataTable();
            // 用于输出的控件
            System.Web.UI.WebControls.GridView GridView1 = new System.Web.UI.WebControls.GridView();
            bool returnvalue = true;
            //把excel转成DataTable
            timedataTable = ExcelOperate.Import(absFileName);
            GridView1.DataSource = timedataTable;
            GridView1.DataBind();

            //开始创建用于输出的对像
            System.IO.StringWriter sW = new System.IO.StringWriter();
            Html32TextWriter hW = new Html32TextWriter(sW);
            //看看是不是正确的格式
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (i != 3 && i != 0)//3为地点
                        {
                            try
                            {
                                row.Cells[i].Text = DateTime.Parse(row.Cells[i].Text).ToString("HH:mm");
                            }
                            catch
                            {
                                row.Cells[i].Attributes.Add("style", "background:#ff0000;");
                                row.Attributes["title"] = "输入时间格式不正确";
                                returnvalue = false;
                            }
                        }
                    }
                }
            }
            GridView1.RenderControl(hW);
            //输出参数
            htmStr = sW.ToString();
            excelDt = timedataTable;
            return returnvalue;
        }

        /// <summary>
        /// 得到字符串输出Excel
        /// </summary>
        /// <param name="htmlStr">传入字符串用于输出</param>
        /// <param name="fileName">输出时刻表名称</param>
        public static void OutPutExcelGrid(string htmlStr, string fileName)
        {
            //定义输出编码
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName + ".xls", System.Text.Encoding.UTF8)); //filename="*.xls"; 
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
            System.Web.HttpContext.Current.Response.Write(htmlStr);
            System.Web.HttpContext.Current.Response.End();
        }
    }
}