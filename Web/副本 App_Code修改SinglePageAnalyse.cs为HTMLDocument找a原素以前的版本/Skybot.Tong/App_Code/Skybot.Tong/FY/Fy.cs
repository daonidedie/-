/******************************************************
* 文件名：Fy.cs
* 文件功能描述：分页方法 分页控件  命名空间 Fy
* 
* 创建标识：周渊 2005-12-10
* 
* 修改标识：周渊 2008-4-26
* 修改描述：按代码编写规范改写部分代码
* 
******************************************************/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Skybot.Tong.FY
{
    #region	可以返回值的分页方法 分页控件.DataSource=Fy(string sql, int 显示数); 注意前台还要有一个层 id为Div

    //using Tong;
    /// <summary>
    /// 分页类 可以在子类中重写
    /// </summary>
    public class FenYe
    {
        //定义分页代码 PageDataSource

        #region	可以返回值的分页方法 要用时 分页控件.DataSource=Fy(string sql, int 显示数); 注意前台还要有一个层啊id为Div

        int 页码, 总页数, 总记录, 每页显示数;
        /// <summary>
        /// 返回分页的数据类
        /// 要在前台有一个层的ID为Div , runat = server 
        /// 要用时 分页控件.DataSource=Fy(string sql, int 显示数)
        /// </summary>
        /// <param name="recordCount">记录总数</param>
        /// <param name="显示数">你要每页显示的条数</param>
        public  virtual PagedDataSource Fy(int recordCount, int 显示数)
        {
            TongUse FyData = new TongUse();
            //int 总记录, 总页数;
            int 余数;
            PagedDataSource 返回;
            //int 页码;
            //定义分页变量
            this.总记录 = recordCount;
            每页显示数 = 显示数;
            余数 = 总记录 % 每页显示数;
            this.总页数 = 总记录 / 每页显示数;
            if (余数 != 0)
            {
                this.总页数++;
            }
            //定义变量结束


            //开始定义分页的类函数
            PagedDataSource 分页主变量 = new PagedDataSource();
            //分页主变量.DataSource = FyData.SqlataSet(sql).Tables[0].DefaultView;

            if (总记录 > 0)//看看是不是有记录
            {
                分页主变量.AllowPaging = true;//定义可以分页
                分页主变量.PageSize = 每页显示数;
                //分页类函数的变量以初始化

                //开始从URL得到页面的当前页码

                try
                {
                    页码 = int.Parse(FyData.Res.Request.QueryString["page"].ToString());//可以得到页面传来的东东
                }
                catch
                {
                    页码 = 1;
                }
                try
                {
                    if (页码 <= 0)//捕捉页码出现的异常
                    {
                        页码 = 1;
                    }
                    if (页码 >= 总页数)
                    {
                        页码 = 总页数;
                    }
                }
                //如果出错的话就...把出错东东打出来
                catch (Exception ex)
                {
                    FyData.Res.Response.Write(ex.Message.ToString());
                    页码 = 1;
                }

                分页主变量.CurrentPageIndex = 页码 - 1;//分页主变量的当前页面;的数据
            }
            返回 = 分页主变量;

            //这两名很重要是前台数据显示的最重要的东东
            //return 返回;
            //这里写完了就用return  分页主变量 把数据的东东输出去.
            //结速
            //以下代码开始控制 前台HTML代码的输出
            //如果是2003要加上这个;
            //protected System.Web.UI.HtmlControls.HtmlGenericControl Div;
            //定义HTML 输出页面变量 "输出"
            //DivSource(this.页码, this.总页数, this.总记录, 每页显示数);把HTML输出的东东输出

            return 返回;
        }

        /// <summary>
        /// 回HTML输出
        /// </summary>
        /// <value>回HTML输出</value>
        public String GetHTML
        {
            get
            {
                return DivSource(this.页码, this.总页数, this.总记录, 每页显示数);
            }
        }

        /// <summary>
        /// 返回HTML输出
        /// </summary>
        /// <param name="页码">得到当前页面的URL页码</param>
        /// <param name="总页数">得到总页数</param>
        /// <param name="总记录"> 得到当前的总记录</param>
        /// <param name="每页显示数">每页显示数 PageSize</param>
        /// <returns>返回HTML代码</returns>
        public virtual String DivSource(int 页码, int 总页数, int 总记录, int 每页显示数)
        {
            string div;
            if (总记录 <= 每页显示数)
            {
                div = "首页 上一页 下一页 尾页";
            }
            else
            {
                if (GetPageUrl.IndexOf("?") != -1)
                {
                    div = "<a href='" + GetPageUrl + "&page=1'> 首页 </a> ";
                    div += "<a href ='" + GetPageUrl + "&page=" + Convert.ToString((页码 - 1)) + "'>上一页</a> ";
                    div += "<a href ='" + GetPageUrl + "&page=" + Convert.ToString((页码 + 1)) + "'>下一页</a> ";
                    div += "<a href ='" + GetPageUrl + "&page=" + Convert.ToString(总页数) + "'>尾页</a> ";
                    div += "共" + 页码 + "/" + 总页数 + "页 ";
                    div += " 共" + 总记录 + "条记录";
                }
                else
                {
                    div = "<a href='" + GetPageUrl + "?page=1'> 首页 </a> ";
                    div += "<a href ='" + GetPageUrl + "?page=" + Convert.ToString((页码 - 1)) + "'>上一页</a> ";
                    div += "<a href ='" + GetPageUrl + "?page=" + Convert.ToString((页码 + 1)) + "'>下一页</a> ";
                    div += "<a href ='" + GetPageUrl + "?page=" + Convert.ToString(总页数) + "'>尾页</a> ";
                    div += "共" + 页码 + "/" + 总页数 + "页 ";
                    div += " 共" + 总记录 + "条记录";
                }
            }
            return div;
        }

        /// <summary>
        ///  得到分页[page]在ＵＲＬ变量里的值
        /// </summary>
        /// <returns></returns>
        public string PageUrlData = "";//得到分页[page]在ＵＲＬ变量里的值

        /// <summary>
        /// 得到数据请求的ＵＲＬ不包括page
        /// </summary>
        /// <returns></returns>
        public String GetPageUrl
        {
            get
            {
                TongUse TextData = new TongUse();
                //Response.Write(TextData.Res.Request.Url);//得到当前请求的ＵＲＬ信息

                string BaseUrl;//开始请求的基ＵＲＬ
                string TempUrl = "";//函数操作中的ＵＲＬ数据临时保存东东
                //string PageUrlData = "";//得到分页[page]在ＵＲＬ变量里的值
                BaseUrl = TextData.Res.Request.Url.OriginalString;//得到当前请求的ＵＲＬ信息中的原始ＵＲi字符 URl.ToString()直接输出中文
                for (int i = 0; i < TextData.Res.Request.QueryString.Count; i++)
                {
                    //TextData.Res.Response.Write("<br> 原始" + TextData.Res.Request.QueryString.Keys.Get(i) + "的值为" + TextData.Res.Request.QueryString.Get(i));

                    if (TextData.Res.Request.QueryString.Keys.Get(i) == "page")
                    {
                        //Request.QueryString.Keys.Get(i)  返回实例中的所有键 Get 获取集合的指定索引处的键。
                        //Request.QueryString.Get(i) 得到指定索引的值
                        PageUrlData = TextData.Res.Request.QueryString.Keys.Get(i) + "=" + TextData.Res.Request.QueryString.Get(i);
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
        }

        #endregion
        //分页代码结束

        #region AspNetPager 分页数据源   返回 填充表 "FyNetPager"DataSet  用法 FyNetPager(AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1),AspNetPager1.PageSize,sql)

        ///// <summary>
        ///// AspNetPage 分页数据源 返回 填充表 "FyNetPager" 的 DataSet
        ///// </summary>
        ///// <param name="CruuShu">当前记录条数  AspNetPager1.PageSize * (AspNetPager1.CurrentPageIndex - 1)</param>
        ///// <param name="PageSize">每页显示条数 AspNetPager1.PageSize</param>
        ///// <param name="sql">传入SQL</param>
        ///// <returns></returns>
        //public virtual DataSet FyNetPager(int CruuShu, int PageSize, string sql)
        //{
        //    //string where = "";
        //    //int rowCount = 0;
        //    //Bocom.Project.Entity.Condition.PagerCondition cond = new Bocom.Project.Entity.Condition.PagerCondition();
        //    //Bocom.Project.BLL.MultiTableBLL.GetCurrentPage(Bocom.Project.Entity.Enumerator.EnumMethodName.GetBCChar,
        //    //    where, cond, out rowCount);
        //    TongUse FyData = new TongUse();

        //    SqlDataAdapter NetPagerDa = new SqlDataAdapter(sql, FyData.MySql);
        //    NetPagerDa.SelectCommand.CommandType = CommandType.Text;//选择执行命令的方式
        //    DataSet MyDs = new DataSet();

        //    NetPagerDa.Fill(MyDs, CruuShu, PageSize, "FyNetPager");
        //    NetPagerDa = null;

        //    return MyDs;

        //}

        #endregion
    }

    #endregion
}

