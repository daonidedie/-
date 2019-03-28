/******************************************************
* 文件名：TongYong.cs
* 文件功能描述：web数据操作 通用 类型 命名空间 Skybot.Tong
* 
* 创建标识：周渊 2005-12-6
* 
* 修改标识：周渊 2008-4-26 
* 修改标识：周渊 2009-4-1 修改命名空间与目录
* 修改描述：按代码编写规范改写部分代码
* 修改标识：周渊 2011-10-29 将 ConfigurationManager.ConnectionStrings 修改为了属性 与httpContext也改成了属性
* 修改描述：SqlConnStr 等 
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

namespace Skybot.Tong
{
    /// <summary>
    /// TongYong 通用类
    /// 用于数据操作,字符串加密 选程读取数据等
    /// 2005-12-6加入
    /// </summary>
    public class TongUse
    {
        /*
              http://community.csdn.net/Expert/TopicView3.asp?id=5698126
              * 在选定的数据源上未找到名为“TitleSub”的字段或属性”的错误
              * 正常访问时页面无错误，但是在不停的刷新或者多人同时访问该页面时会发生“在选定的数据源上未找到名为“TitleSub”的字段或属性”的错误。我在GridView1.DataBind（）处捕获异常的结果是DataTable有数据，而GridView1为空。请问这是怎么回事呢？
              * 目前所有页面都存在这个问题
              * static string conString; //连接字符串
                 static DataSet ds; //DataSet数据集对象
                 static SqlConnection con; //数据库连接对象
                 static SqlDataAdapter da; //数据适配器对象
                 static SqlDataReader myReader; //数据库读取对象
                 static SqlCommand cmd; //执行SQL命令对象
              * 
              * 你可以把所有 static 都去掉，改成非静态的方法，应该不会再出问题  这样解决

              */

        #region 字段和属性

        /// <summary>
        /// 得到路径OleDb 要使用此功能请在 webConfig中加入以下    <add key="OleDbConn" value="Provider=Microsoft.Jet.OleDb.4.0;Data Source="/>
        /// </summary>
        public String OldDbdv = "Provider=Microsoft.Jet.OleDb.4.0;Data Source="; //ConfigurationManager.AppSettings["OleDbConn"].ToString();

        /// <summary>
        /// 得到相对路径OleDb
        /// </summary>
        public String PathOleDb = "";//ConfigurationManager.AppSettings["OleDbPath"].ToString();

        /// <summary>
        /// 得到SQL数据库联接字符串SQL
        /// </summary>
        public String SqlConnStr { get { return ConfigurationManager.ConnectionStrings["SqlConnection"].ToString(); } }

        /// <summary>
        /// 得到 程序编写 信息
        /// </summary>
        public static String CopyRight { get { return ConfigurationManager.AppSettings["CopyRight"].ToString(); } }

        /// <summary>
        /// 得到 界面設計 信息
        /// </summary>
        public static String CopyRight_jieM { get { return ConfigurationManager.AppSettings["CopyRight_jieM"].ToString(); } }

        /// <summary>
        /// 得到 版权所有 信息
        /// </summary>
        public static String COPY { get { return ConfigurationManager.AppSettings["COPY"].ToString(); } }

        /// <summary>
        /// 得到 版本 信息
        /// </summary>
        public static String Ver { get { return ConfigurationManager.AppSettings["Ver"].ToString(); } }

        /// <summary>
        /// 得到系统设定的错误处理页页面
        /// </summary>
        public static String ErrPaht { get { return ConfigurationManager.AppSettings["errPath"].ToString(); } }

        /// <summary>
        /// 得到系统基于网站路的绝对路径
        /// </summary>
        public static String SysPath { get { return ConfigurationManager.AppSettings["SysPath"].ToString(); } }

        /// <summary>
        /// 得到当前请求的页面文件名
        /// </summary>
        /// <returns></returns>
        public String CurrentPage
        {
            get
            {
                //Cuurl 当前ＵＲＬ变量
                string Cuurl = Res.Request.Url.ToString();
                int ur = Cuurl.Length - 1;
                int URL = int.Parse(Cuurl.LastIndexOf("/").ToString());//从最后一个开始查找 得到起始位置
                int jq = URL + 1;//载取字符串长度
                return Cuurl.Substring(jq, ur - URL);//得到当前请求的页面文件名
            }
        }

        /// <summary>
        /// 失敗表情
        /// </summary>
        public static String Face_Fail { get { return "<img src='" + SysPath + ConfigurationManager.AppSettings["Face_Fail"].ToString() + "'/> ";} }

        /// <summary>
        /// 成功表情
        /// </summary>
        public static String Face_Succery { get { return "<img src='" + SysPath + ConfigurationManager.AppSettings["Face_Succery"].ToString() + "'/> "; } }

        /// <summary>
        /// Res 在类里输出HTML或其它东东
        /// </summary>
        public HttpContext Res   { get { return HttpContext.Current; } }

        /// <summary>
        /// 得到 当前 URL 参数 数据 ?后面的数据
        /// </summary>
        public String UrlData { get { return HttpContext.Current.Request.QueryString.ToString(); } }

        /// <summary>
        ///建立OleDbConnection对象 MyOleDb
        /// </summary>
        //public static OleDbConnection MyOleDb = new OleDbConnection(dv+HttpContext.Current.Server.MapPath(path));//如果是虚拟路径的话

        public OleDbConnection MyOleDb
        {
            get
            {
                OleDbConnection MyOleDbConn = new OleDbConnection(OldDbdv + PathOleDb);//使用决对路路径
                return MyOleDbConn;
            }
        }

        /// <summary>
        /// 建立SqlConnection 字符串聯接 對象 MySql
        /// </summary>
        public SqlConnection MySql
        {
            get
            {
                SqlConnection MySqlConn = new SqlConnection(SqlConnStr);//使用SQLConnstring如果是SQL Serveric 的话就用Server=192.168.0.111; uid=sa, pwd=sa,DataBase=SqlDdataBase;
                //MySqlConn.Open();//打开数据库联接
                return MySqlConn;
            }
        }

        /// <summary>
        ///ResClass 在类里输出东东
        /// </summary>
        protected static HttpContext ResClass { get { return HttpContext.Current; } }

        /// <summary>
        /// 返回GUID .
        /// 一般在資料庫的ID中使用
        /// </summary>
        /// <returns>返回GUID</returns>
        public String Guidx
        {
            get //只能得到當前的GUID
            {
                Guid guidx = Guid.NewGuid();

                return guidx.ToString();
            }
        }

        #endregion

        #region    数据操作方法

        /// <summary>
        /// 二维数组换成DataTable
        /// </summary>
        /// <param name="array二维数组">二维数组string[,]</param>
        /// <returns></returns>
        public DataTable ArrayToDtaTable(string[,] array二维数组)
        {

            //二维数组的DataTable
            DataTable ArrayDataTable = new DataTable();
            DataRow ArrDataRows;
            for (int i = 0; i < array二维数组.GetLength(1); i++) //GetLength 得到指定维数中的原素数
            {
                ArrayDataTable.Columns.Add(i.ToString());
            }
            for (int i = 0; i < array二维数组.GetLength(0); i++)
            {
                ArrDataRows = ArrayDataTable.NewRow();
                for (int j = 0; j < array二维数组.GetLength(1); j++)
                {
                    ArrDataRows[j] = array二维数组[i, j];
                }
                ArrayDataTable.Rows.Add(ArrDataRows);
            }

            return ArrayDataTable;
        }

        /// <summary>
        /// 调用带参数的 存贮过程 返回DataSet 
        /// </summary>
        ///     ///设计思路
        ///*
        /// * 参数1, 所有参数的参数名 如 @parentId=0,parame2 = aspx 以,分开
        /// * 所有参数为String 数据类型
        /// * string StoreName,string pareme
        /// * 拆分字符串一定要使用单引号 ' ' "a a".Split(' ');
        /// *
        /// <param name="storName">调用的 存贮过程 名</param>
        /// <param name="tempparam">存贮过程 的 参数 多个参数以 " , " 格开 参数的值 用" = " 如:@p=12 参数格式为: @parentId=0,parame2 = aspx  </param>
        public DataSet SqlParameStor(string storName, string tempparam)
        {
            DataSet Stored_Ds = new DataSet();
            string[] StorPar;//拆分参数的数组  拆分 Tempparam

            try
            {
                SqlDataAdapter SqlSA_Stored = new SqlDataAdapter();
                SqlSA_Stored.SelectCommand = new SqlCommand();//建立sqlCommand 类的新实例
                SqlSA_Stored.SelectCommand.Connection = new SqlConnection(SqlConnStr);//初始化connection 属性
                SqlSA_Stored.SelectCommand.CommandText = storName;//StorName 为存贮过程 或  存贮过程('参数1',参数2)


                SqlSA_Stored.SelectCommand.CommandType = CommandType.StoredProcedure;//StoredProcedure 为指定数据操作方法为"调用存贮过程"

                if (tempparam.IndexOf(",") > -1 && tempparam != "")//看看此字符串能不能拆分
                {
                    StorPar = tempparam.Split(',');//以' , ' 拆分字符串返回数组

                    /*
                     *动态创建 存贮过程 参数 和 对应的值
                     * StorPar[i].Split('=')[0].ToString();为拆分后的 参数;
                     * StorPar[i].Split('=')[1].ToString();为拆分后的 参数值; 
                     */
                    for (int i = 0; i < StorPar.Length; i++)
                    {
                        SqlSA_Stored.SelectCommand.Parameters.Add(StorPar[i].Split('=')[0].ToString(), SqlDbType.Char);//添加一上参数
                        SqlSA_Stored.SelectCommand.Parameters[StorPar[i].Split('=')[0].ToString()].Value = StorPar[i].Split('=')[1].ToString();//设定这个参数对应的值
                    }

                }
                else//单个参数时执行
                {
                    string[] StorParValue;

                    StorParValue = tempparam.Split('=');//单个时只以等号拆分

                    SqlSA_Stored.SelectCommand.Parameters.Add(StorParValue[0].ToString(), SqlDbType.Char);//添加一上参数
                    SqlSA_Stored.SelectCommand.Parameters[StorParValue[0].ToString()].Value = StorParValue[1].ToString();//设定这个参数对应的值
                }


                SqlSA_Stored.Fill(Stored_Ds);// 存贮过程 数据,映照到 临时表  TempTable 中

                SqlSA_Stored = null;

            }
            catch (Exception ex)
            {

                Stored_Ds.Clear();
                Err(ex.Message);

            }

            if (MySql.State == ConnectionState.Open)//ConnectionState 得到當前資料源聯接的狀態
            {
                MySql.Close();

            }
            return Stored_Ds;
        }

        /// <summary>
        /// 调用存贮过程 返回DataSet 
        /// </summary>
        /// <param name="storName">存贮过程 名</param>
        public DataSet SqlStor(string storName)
        {
            SqlDataAdapter StoreDa = new SqlDataAdapter(storName, SqlConnStr);
            StoreDa.SelectCommand.CommandType = CommandType.StoredProcedure;//StoredProcedure 为　存贮过程
            DataSet StorDataSet = new DataSet();
            try
            {
                StoreDa.Fill(StorDataSet);
            }
            catch (SqlException ex)
            {
                //清空变量
                StoreDa = null;
                StorDataSet.Clear();
                StorDataSet = null;

                if (MySql.State == ConnectionState.Open)//ConnectionState 得到當前資料源聯接的狀態
                {
                    MySql.Close();

                }
                Err(ex.Message);
            }

            if (MySql.State == ConnectionState.Open)//ConnectionState 得到當前資料源聯接的狀態
            {
                MySql.Close();
            }

            StoreDa = null;

            return StorDataSet;
        }

        /// <summary>
        /// 数据处理函数 可删除，添加，修改，查询,创建存储过程 等。
        /// </summary>
        /// <param name="sql"> 请输入SQL语句</param>
        public void SqlDataSave(string sql)
        {
            SqlConnection SqlConn = new SqlConnection(SqlConnStr); //如果这里不用动态方法实例化就会出错
            SqlCommand SqlSql = new SqlCommand(sql, SqlConn);//得到一个可以执行的SQL东东

            if (SqlConn.State == ConnectionState.Open)//关闭数据库联接
            {
                SqlConn.Close();
            }
            SqlConn.Open();
            try
            {
                SqlSql.ExecuteNonQuery();
            }
            catch (Exception ex)//错误处理
            {
                Err(ex.Message);
                if (SqlConn.State == ConnectionState.Open)//关闭数据库联接
                {
                    SqlConn.Close();
                }
            }

            if (SqlConn.State == ConnectionState.Open)//关闭数据库联接
            {
                SqlConn.Close();
            }

            SqlConn.Close();
            SqlConn.Dispose();
        }

        /// <summary>
        /// 传入一个SQL代码 返回DataSet 
        ///  </summary>
        /// <param name="sql">参数SQL 输入SQL语句</param>
        /// <returns>返回一个填充过的DataSet</returns>
        public DataSet SqlataSet(string sql)
        {
            if (MySql.State == ConnectionState.Open)//ConnectionState 得到當前資料源聯接的狀態
            {
                MySql.Close();
            }
            SqlDataAdapter MyDa = new SqlDataAdapter(sql, MySql);//新建DataSet對象
            MyDa.SelectCommand.CommandType = CommandType.Text;
            DataSet MyDs = new DataSet();
            try
            {
                MyDa.Fill(MyDs);
            }
            catch (Exception ex)//資料填充時的異常處理
            {
                if (MySql.State == ConnectionState.Open)//ConnectionState 得到當前資料源聯接的狀態
                {
                    MySql.Close();

                }
                MyDs = null;
                Err(ex.Message);
            }
            if (MySql.State == ConnectionState.Open)//ConnectionState 得到當前資料源聯接的狀態
            {
                MySql.Close();

            }

            return MyDs;
        }

        /// <summary>
        /// 传入一个SQL代码 返回DataReader 要求手动關闭
        /// </summary>
        /// <param name="sql">SQL語句</param>
        /// <returns> 返回DataReader 資料集 </returns>
        public SqlDataReader SqlDataReader(string sql)
        {
            SqlCommand Cmd = new SqlCommand(sql, MySql);
            MySql.Open();//打开数据源
            SqlDataReader ObjDataReader = Cmd.ExecuteReader();//建立DataReader对象、
            try
            {
                return ObjDataReader;
            }
            catch (SqlException ex)//異常處理
            {
                MyOleDb.Close();
                Err(ex.Message);
                Res.Response.End();
            }

            return ObjDataReader;
        }

        /// <summary>
        /// 传入SQL返回dataSet
        /// OleDb 一般用于 access 数据库
        /// </summary>
        /// <param name="sql">传入sql</param>
        /// <returns></returns>
        public DataSet OleDbDataSet(string sql)
        {
            if (MyOleDb.State == ConnectionState.Open)//ConnectionState 得到當前資料源聯接的狀態
            {
                MyOleDb.Close();
            }
            OleDbDataAdapter MyDa = new OleDbDataAdapter(sql, MyOleDb);//新建DataSet對象
            MyDa.SelectCommand.CommandType = CommandType.Text;
            DataSet MyDs = new DataSet();
            try
            {
                MyDa.Fill(MyDs);
            }
            catch (Exception ex)//資料填充時的異常處理
            {
                if (MyOleDb.State == ConnectionState.Open)//ConnectionState 得到當前資料源聯接的狀態
                {
                    MyOleDb.Close();
                }
                MyDs = null;
                Err(ex.Message);
            }

            if (MyOleDb.State == ConnectionState.Open)//ConnectionState 得到當前資料源聯接的狀態
            {
                MyOleDb.Close();
            }

            return MyDs;
        }

        #endregion

        #region 事件 返回新实例

        /// <summary>
        /// 返回一个实例化的  Page 要传入当前页面的Ｐage
        /// </summary>
        /// <param name="page">this     当前页面的page类名一般为this </param>
        /// <returns>MasterPage类的操作函数</returns>
        public Page PagerX(Page page)
        {
            return page;
        }

        /// <summary>
        /// 返回一个实例化的  MasterPage
        /// </summary>
        /// <param name="msp">MasterPage实例 一定要设定masterPage为主控页面的页面下才能使用 </param>
        /// <returns>MasterPage类的操作函数</returns>
        public MasterPage mspage(MasterPage msp)
        {
            return msp;
        }

        #endregion

        #region  生成缩略图 SomePic

        /**/
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式 HW 指定高宽缩放（可能变形） ;W 指定宽，高按比例; H指定高，宽按比例; Cut指定高宽裁减（不变形）</param>    
        public static void SomePic(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        #endregion

        #region    字符串操作或 文档操作  输出  等其它函数

        /// <summary>
        /// 格式化字符串 ForMatText
        /// </summary>
        /// <param name="tt">参数tt为字符串(传入)</param>
        /// <param name="shu">参数shu为从第几个开始</param>
        /// <param name="endshu">参数Endshu为结取多少个字符</param>
        /// <returns>返回字符串</returns>
        public String ForMatText(string tt, int shu, int endshu)
        {
            string fanhui = "";
            try
            {
                if (tt.Length > (shu + endshu))
                {
                    fanhui = tt.Substring(shu, endshu).ToString() + "...";//结取字符
                    return fanhui;
                }
                else
                {
                    fanhui = tt.ToString();
                    return fanhui;
                }
            }
            catch
            {
                return fanhui;
            }
        }

        /// <summary>
        /// 错误处理数
        /// </summary>
        /// <param name="errText">错误信息描述</param>
        public void Err(string errText)
        {
            try
            {
                if (errText == "" || errText == null)//系统默认处理
                {

                    ResClass.Response.Redirect(ErrPaht + "?" + CodeUrlText("未知错误"));
                    //CodeUrlText  对URL字符串进行 % 编码
                }
                else
                {
                    Res.Response.Redirect(ErrPaht + "?Error=" + CodeUrlText(errText));
                    System.GC.Collect();//回收LJ
                }
            }
            catch (Exception ex)
            {
                Res.Response.Write(ex.Message);
            }
        }

        /// <summary>
        /// 返回指定个数的 指定字符串
        /// </summary>
        /// <param name="ii">指定返回个数</param>
        /// <param name="setstr">指定字符串</param>
        /// <returns>指定字符串</returns>
        public String EStr(int ii, string setstr)
        {
            string str = "";//返回空格;
            if (ii != 0 && setstr != "")
            {
                for (int i = 0; i < ii; i++)
                {
                    str += setstr;
                }

            }

            return str;
        }

        /// <summary>
        /// 对URL字符串进行 % 编码
        /// </summary>
        /// <param name="text">要编码的字符串</param>
        /// <returns>编码改写后的字符串</returns>
        public String CodeUrlText(string text)
        {
            text = Res.Server.UrlEncode(text);//对URL字符串进行 % 编码 Server.UrlEncode
            return text;//返回 值
        }

        /// <summary>
        /// 传入字符串 返回 # 编码
        /// </summary>
        /// <param name="tt">传入字符串</param>
        /// <returns>返回unicode编码</returns>
        public String CodeText(String tt)
        {
            string aa = "";
            char[] chs = Encoding.Unicode.GetChars(Encoding.Unicode.GetBytes(tt.ToCharArray()));
            for (int i = 0; i < chs.Length; i++)
            {
                aa += "&#" + ((int)chs[i]).ToString() + ";";
            }

            /*
            C#里面的字符串常量都是Unicode

            如果你需要Unicode字节：

            string test = "你好";

            byte[] testBytes = System.Text.Encoding.Unicode.GetBytes( test );

            字符：

            char c = 'A';

            int asciiOfA = (int)c;  //asciiOfA 就是 'A' 的ASCII码
             ////////////////////////////////////
            可以如下转换

            ASCII to unicode
            /////////////////////////////////
            System.IO.FileStream fs = new FileStream("F:\\test.txt",FileMode.Open );
            byte[] bt = new Byte[fs.Length];
            fs.Read(bt,0,bt.Length);
            Encoding CnEnconding	= Encoding.GetEncoding("GB18030");
            string str = CnEnconding.GetString(bt);

            unicode to ascii
            ///////////////////////////////////
            char[] chs = CnEnconding.GetChars("斯大林开发加速度");

             * 
             * 
             * 

            至于你的题目,大家说了这么多,你都没有听:
            string sHello	= "你好";	// unicode 编码了

            估计你是想加密吧,

            以下的方法,不知可用否,请试试:
            ================================================
            string sSou				= "用System.text 命名空间";
            Encoding CnEnconding	= Encoding.GetEncoding("GB18030");

            byte[]	bSou			= CnEnconding.GetBytes(sSou);		//转成数组

            string sUni				= Encoding.Unicode.GetString(bSou);

            //保存


            //取出

            //回复时 反操作
            byte[]	bUni2			= Encoding.Unicode.GetBytes(sUni);		//转成数组

            string	sDest			= CnEnconding.GetString(bUni2);


             */

            return aa;
        }

        /// <summary>
        /// 对字符进行js的escape编码
        /// </summary>
        /// <param name="tt">传入要编辑码的字符</param>
        public String CodeEscape(String tt)
        {
            if (tt.Length > 0)
            {
                tt = HttpUtility.UrlEncodeUnicode(tt);
            }

            return tt;
        }

        #region  字符串加密解密

        /// <summary>
        /// 使用缺省密钥字符串加密
        /// </summary>
        /// <param name="original">明文</param>
        /// <returns>密文</returns>
        public string Encrypt(string original)
        {
            return Encrypt(original, "skybot");
        }

        /// <summary>
        /// 使用缺省密钥解密
        /// </summary>
        /// <param name="original">密文</param>
        /// <returns>明文</returns>
        public string Decrypt(string original)
        {
            return Decrypt(original, "skybot", System.Text.Encoding.Default);
        }

        /// <summary>
        /// 使用给定密钥解密
        /// </summary>
        /// <param name="original">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public string Decrypt(string original, string key)
        {
            return Decrypt(original, key, System.Text.Encoding.Default);
        }

        /// <summary>
        /// 使用缺省密钥解密,返回指定编码方式明文
        /// </summary>
        /// <param name="original">密文</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>明文</returns>
        public string Decrypt(string original, Encoding encoding)
        {
            return Decrypt(original, "skybot", encoding);
        }

        /// <summary>
        /// 使用给定密钥加密
        /// </summary>
        /// <param name="original">原始文字</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public string Encrypt(string original, string key)
        {
            byte[] buff = System.Text.Encoding.Default.GetBytes(original);
            byte[] kb = System.Text.Encoding.Default.GetBytes(key);

            return Convert.ToBase64String(Encrypt(buff, kb));
        }

        /// <summary>
        /// 使用给定密钥解密
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码方案</param>
        /// <returns>明文</returns>
        public string Decrypt(string encrypted, string key, Encoding encoding)
        {
            byte[] buff = Convert.FromBase64String(encrypted);
            byte[] kb = System.Text.Encoding.Default.GetBytes(key);
            try
            {
                return encoding.GetString(Decrypt(buff, kb));
            }
            catch//解密码fail不成功
            {
                return SHA1Sign(encrypted.ToLower());
            }
        }

        /// <summary>
        /// 生成MD5摘要
        /// </summary>
        /// <param name="original">数据源</param>
        /// <returns>摘要</returns>
        public byte[] MakeMD5(byte[] original)
        {
            MD5CryptoServiceProvider hashmd = new MD5CryptoServiceProvider();
            byte[] keyhash = hashmd.ComputeHash(original);
            hashmd = null;

            return keyhash;
        }

        /// <summary>
        /// 使用给定密钥加密
        /// </summary>
        /// <param name="original">明文</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public byte[] Encrypt(byte[] original, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(key);
            des.Mode = CipherMode.ECB;

            return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
        }

        /// <summary>
        /// 使用给定密钥解密数据
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public byte[] Decrypt(byte[] encrypted, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(key);
            des.Mode = CipherMode.ECB;

            return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
        }

        /// <summary>
        /// 使用缺省密钥加密
        /// </summary>
        /// <param name="original">原始数据</param>
        /// <returns>密文</returns>
        public byte[] Encrypt(byte[] original)
        {
            byte[] key = System.Text.Encoding.Default.GetBytes("skybot");
            return Encrypt(original, key);
        }

        /// <summary>
        /// 使用缺省密钥解密数据
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <returns>明文</returns>
        public byte[] Decrypt(byte[] encrypted)
        {
            byte[] key = System.Text.Encoding.Default.GetBytes("skybot");
            try
            {
                return Decrypt(encrypted, key);
            }
            catch
            {
                return encrypted;
            }
        }

        /// <summary>
        ///计算Unicode字符串经过SHA-1算法加密后的密文,Unicode码的密码明文字符串
        /// </summary>   
        ///<param name="data"> 
        ///经过SHA-1算法加密得到的160位的密码密文
        ///</param>    
        public String SHA1Sign(string data)
        {
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            byte[] pwd = Encoding.Unicode.GetBytes(data);    //Unicode字符串转换为byte数组
            byte[] result = sha.ComputeHash(pwd);            //计算pwd的Hash散列码
            sha.Clear();                                      //释放Hash算法占用的所有资源
            //把byte数组转换为string，返回一个16进制数对，每对数由 “-” 分隔
            string passwd = BitConverter.ToString(result);
            passwd = passwd.Replace("-", "");              //去掉 “-” 号
            return passwd;
        }

        #endregion

        /// <summary>
        /// 输出Script 中的 Alert 对话提示
        /// </summary>
        /// <param name="str">要输出的 对话提示</param>
        /// <returns>输出Script 中的 Alert 对话提示</returns>
        public void ResonseAlert(string str)
        {
            if (str != "")
            {
                Res.Response.Write("<script>alert(\"" + str + "\")</script>");//输出Alert对话
            }
        }

        /// <summary>
        /// 格式化URL函数  urlX 传入相对URL objurl 传入绝对基URL  基URL 一定要带HTTP://
        /// </summary>
        /// <param name="urlX">传入单个的URL</param>
        /// <param name="objurl">
        /// 传入得到值的页面URL
        /// </param>
        public String FormAturl(String urlX, string objurl)
        {
            Uri baseUri = new Uri(objurl); // http://www.enet.com.cn/enews/inforcenter/designmore.jsp
            Uri absoluteUri = new Uri(baseUri, urlX);//相对绝对路径都在这里转 这里的urlx ="../test.html"
            return absoluteUri.ToString();//   http://www.enet.com.cn/enews/test.html   
        }

        /// <summary>
        /// 传入要不显示的ＵＲＬ键 返回ＵＲＬ除传入键外的值
        /// </summary>
        /// <param name="urlkey">传入键 如 pages=0 输入 pages</param>
        public String GetPageUrlKey(string urlkey)
        {
            string PageUrlData = "";
            //Response.Write(TextData.Res.Request.Url);//得到当前请求的ＵＲＬ信息
            string BaseUrl;//开始请求的基ＵＲＬ
            string TempUrl = "";//函数操作中的ＵＲＬ数据临时保存东东
            //string PageUrlData = "";//得到分页[page]在ＵＲＬ变量里的值
            BaseUrl = Res.Request.Url.PathAndQuery.Replace(Res.Request.Url.AbsolutePath, "");//得到当前请求的ＵＲＬ信息中的原始ＵＲi字符 URl.ToString()直接输出中文
            for (int i = 0; i < Res.Request.QueryString.Count; i++)
            {
                //TextData.Res.Response.Write("<br> 原始" + TextData.Res.Request.QueryString.Keys.Get(i) + "的值为" + TextData.Res.Request.QueryString.Get(i));
                if (Res.Request.QueryString.Keys.Get(i) == urlkey)
                {
                    //Request.QueryString.Keys.Get(i)  返回实例中的所有键 Get 获取集合的指定索引处的键。
                    //Request.QueryString.Get(i) 得到指定索引的值
                    PageUrlData = Res.Request.QueryString.Keys.Get(i) + "=" + Res.Request.QueryString.Get(i);
                    TempUrl = "有分页";
                }
            }
            //如果有分页和分页值的话
            if (TempUrl == "有分页" && PageUrlData != "")
            {
                BaseUrl = BaseUrl.Replace(PageUrlData, "");
                //处理后的ＵＲＬ
                // TextData.Res.Response.Write("<br>处理后的ＵＲＬ: " + BaseUrl);
                //看看最后是不是有 "&"  如是"-1"说明没有
                if (BaseUrl.LastIndexOf("&") != -1)
                {
                    BaseUrl = BaseUrl.Substring(0, (BaseUrl.Length - 1));
                    // TextData.Res.Response.Write("<br>处理 & 后的ＵＲＬ: " + BaseUrl);
                }
            }

            return BaseUrl;
        }

        /// <summary>
        /// 去除HTML标签 字符串格式化函数
        /// </summary>
        /// <param name="HTML">要去除的HTML代码</param>
        public String ForMatTextReplaceHTMLTags(string HTML)
        {
            if (HTML != "")
            {
                string HTMLTag = @"<\/*[^<>]*>";
                HTML = System.Text.RegularExpressions.Regex.Replace(HTML, HTMLTag, "");
            }

            return HTML;
        }

        /// <summary>
        /// 得到指定字符串 前后指定长度的字符串
        /// </summary>
        ///  <param name="text">搜索的字符串</param>
        /// <param name="tt">指定字符串</param>
        /// <param name="getnum">前后字符串 得到的最大长度</param>
        public String ForMatTextGetPointText(string text, string tt, int getnum)
        {
            string temptext = text;
            if (tt != "" && getnum < (text.Length / 2))
            {
                try
                {
                    string[] tempstr;
                    temptext = tt;
                    text = text.Replace(tt, "╄");//替换

                    tempstr = text.Split('╄');
                    //得到前页面的
                    tempstr[0] = tempstr[0].Substring((tempstr[0].Length - getnum), getnum);
                    //得到后面的
                    tempstr[1] = tempstr[1].Substring((tempstr[1].Length - getnum), getnum);

                    temptext = tempstr[0] + tt + tempstr[1];
                }
                catch { }
            }

            return temptext.Replace(tt, "<span style=\"color:#ff0000;\">" + tt + "</span>");
        }

        #endregion

        #region    HttpWebRequest读取网页 数据

        /// <summary>
        /// HttpWebRequest读取网页 utf-8 编码
        /// </summary>
        /// <param name="website">url</param>
        public string GetWeb(string website)
        {
            string strHtmlContent = "";
            //try
            //{
            if (website.IndexOf("http://") == -1)//如果米有HTTP
            {
                website = "http://" + Res.Request.Url.Authority + website;
            }
            System.Net.HttpWebRequest myrequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(website);
            myrequest.Timeout = 600000;//超时时间 10 分钟
            // myrequest.Headers.Set("Pragma", "no-cache");
            System.Net.HttpWebResponse myresponse = (System.Net.HttpWebResponse)myrequest.GetResponse();
            System.IO.Stream mystream = myresponse.GetResponseStream();
            System.Text.Encoding myencoding = System.Text.Encoding.GetEncoding("utf-8");
            System.IO.StreamReader mystreamreader = new System.IO.StreamReader(mystream, myencoding);
            strHtmlContent = mystreamreader.ReadToEnd();
            mystream.Close();
            mystreamreader.Dispose();
            mystream.Close();
            mystream.Dispose();

            //}
            //catch (Exception ex)
            //{
            //    // Err("读取错误" + ex.Message + website);
            //}

            return strHtmlContent;
        }

        /// <summary>
        /// 读取网页 数据 返回生成静态页面的路径
        /// </summary>
        /// <param name="getURL">要读取数据的网页</param>
        /// <param name="willWriteUrl">要写入数据的路径 绝对</param>
        public String GetWebDataWrite(string getURL, string willWriteUrl)
        {
            StreamWriter FileWrite = new StreamWriter(willWriteUrl, false, Encoding.UTF8);
            string StrCont = GetWeb(getURL);            //StrCoun内容

            #region 格式化代码 去掉FORM

            //去掉
            StrCont = System.Text.RegularExpressions.Regex.Replace(StrCont, @"<input\s{0,}type=['""]hidden['""]\s{0,}.+id=['""]__VIEWSTATE['""]\s{0,}value=['""].+/>", "", System.Text.RegularExpressions.RegexOptions.Multiline);
            //initalize
            //set default value
            string endform = "<form>";//最后一个结束的form
            string startform = "<form .+?>";//开始的第一个form
            //得到form <form .+?>
            startform = System.Text.RegularExpressions.Regex.Match(StrCont, @"<form.+?>", System.Text.RegularExpressions.RegexOptions.Singleline).Value;//得到开始的Form
            if (StrCont.Length > 0)
            {
                endform = StrCont.Substring(StrCont.LastIndexOf("</form>"), StrCont.Length - StrCont.LastIndexOf("</form>"));
            }
            StrCont = StrCont.Replace(endform, endform.Replace("</form>", ""));//替换最后的form结束标签
            StrCont = StrCont.Replace(startform, "");//替换第一个form在的东东

            #endregion

            FileWrite.Write(StrCont);
            FileWrite.Flush();
            FileWrite.Close();
            FileWrite.Dispose();
            FileWrite = null;

            return willWriteUrl;
        }

        /// <summary>
        /// 传入读取URL 与要 HTML 代码 返回 写入文件后的值 用 $ 分开
        /// </summary>
        /// <param name="webSiteurl">得到图片的网址</param>
        /// <param name="cont">要保存图片的HTML代码 如网页的HTML源代码</param>
        /// <param name="upDir">上传路径</param>
        public String GetWebImgSave(string webSiteurl, string cont, string upDir)
        {
            // 函数的返回值
            string GetWebImgSaveImgUrls = "";

            // 图片的 SRC
            string HTTpImgurl = "";

            //搜索内容
            string ImgCont = "";

            System.Text.RegularExpressions.MatchCollection HTTpImgurlS;

            // 得到所有IMG 的SRC
            string RegimgStr = @"(src\S+\.{1}(gif|jpg|png|bmp)(""|\')?)";

            // 得到 .gif 前面 没有用的东东 抱括 .
            string RegimgStrGetfront = @"\S+\.";

            // 得到文件的扩展名
            string RegingSrtGetimgExname = @".[a-z]{0,}";

            // 文件扩展名

            string imgExname = "";

            // 用于保存的Iimg文件名

            string HttpImgFileName = "";

            string imgurltemp = "";

            ImgCont = cont;
            HTTpImgurlS = System.Text.RegularExpressions.Regex.Matches(ImgCont, RegimgStr);
            string Uppath = upDir;//上传目录

            for (int k = 0; k < HTTpImgurlS.Count; k++)
            {
                HTTpImgurl = "";
                HTTpImgurl = HTTpImgurlS[k].Value;
                //try
                //{
                HTTpImgurl = HTTpImgurl.Replace("src", "").Replace("=", "").Replace("\"", "").Replace("'", "").Replace(" ", "");//干掉所有的 src = ""
                imgExname = System.Text.RegularExpressions.Regex.Replace(HTTpImgurl, RegimgStrGetfront, ".");
                imgExname = System.Text.RegularExpressions.Regex.Match(imgExname, RegingSrtGetimgExname).Value;//得到img 文件的扩展名
                HTTpImgurl = FormAturl(HTTpImgurl, webSiteurl);

                try
                {
                    HttpImgFileName = Uppath + Guidx + imgExname;//得到生成文件名
                    imgurltemp = HttpImgFileName + "$";//得到返回文件名
                    HttpImgFileName = Res.Server.MapPath(HttpImgFileName);
                    GetWebFile(HTTpImgurl, HttpImgFileName);//写入文件 保存图片
                    GetWebImgSaveImgUrls += imgurltemp;
                }
                catch { }
            }

            return GetWebImgSaveImgUrls;
        }

        /// <summary>
        /// 以下代码实现远程保存图片 及 文件 返回保存名
        /// </summary>
        /// <param name="fileUrl">得到文件的URL http//</param>
        /// <param name="willWriteFilename">要生成的文件名</param>
        public string GetWebFile(string fileUrl, string willWriteFilename)
        {
            System.Net.WebClient WC = new System.Net.WebClient();
            WC.DownloadFile(fileUrl, willWriteFilename);
            WC.Dispose();

            return willWriteFilename;
        }

        #endregion

        #region  文件夹操作方法

        /// <summary>
        /// 文件夹操作 创建一个指定目录的文件夹
        /// </summary>
        /// <param name="dirPath">目录路径</param>
        public bool DirCreate(string dirPath)
        {

            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                    return true;
                }
            }
            catch { }

            return false;
        }

        /// <summary>
        /// 文件夹操作 删除一个指定目录的文件夹
        /// </summary>
        /// <param name="dirPath">基于硬盘的目录路径</param>
        public bool DirDel(string dirPath)
        {
            try
            {
                if (Directory.Exists(dirPath))
                {
                    Directory.Delete(dirPath, true);
                    return true;
                }
            }
            catch { }

            return false;
        }

        #endregion
    }
}




