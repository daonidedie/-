using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class UserControls_AllBooks : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        IDAL.INovel novel = BllFactory.BllAccess.CreateINovelBLL();

        WebControlExtension.LinkButtonEx[] lkState = new WebControlExtension.LinkButtonEx[3];
        lkState[0] = s1;
        lkState[1] = s2;
        lkState[2] = s3;

        WebControlExtension.LinkButtonEx[] lkChar = new WebControlExtension.LinkButtonEx[6];
        lkChar[0] = c1;
        lkChar[1] = c2;
        lkChar[2] = c3;
        lkChar[3] = c4;
        lkChar[4] = c5;
        lkChar[5] = c6;

        WebControlExtension.LinkButtonEx[] lkBookType = new WebControlExtension.LinkButtonEx[13];
        lkBookType[0] = t1;
        lkBookType[1] = t2;
        lkBookType[2] = t3;
        lkBookType[3] = t4;
        lkBookType[4] = t5;
        lkBookType[5] = t6;
        lkBookType[6] = t7;
        lkBookType[7] = t8;
        lkBookType[8] = t9;
        lkBookType[9] = t10;
        lkBookType[10] = t11;
        lkBookType[11] = t12;
        lkBookType[12] = t13;

        int statenum = 0 ;
        string statestring="无";
        int charnum = 0;
        int typenum = 0;
        int mincharnum = 0;
        int maxcharnum = 0;
        int recordCount =0;
        int pageIndex = 1;

        if (Request.QueryString["state"] != null)
        {
            statenum = Convert.ToInt32(Request.QueryString["state"]);
            lkState[statenum].ForeColor = Color.Red;

            if (statenum == 0)
            {
                statestring = "无";
            }
            if (statenum == 1)
            {
                statestring = "连载中";
            }
            if (statenum == 2)
            {
                statestring = "已完本";
            }
        }

        if (Request.QueryString["charnum"] != null)
        {
            charnum = Convert.ToInt32(Request.QueryString["charnum"].ToString());
            lkChar[charnum].ForeColor = Color.Red;
            if(charnum == 0)
            {
                mincharnum = 0;
                maxcharnum = 0;
            }
            
            if(charnum == 1)
            {
                mincharnum = 300000;
                maxcharnum = 0;
            }

            if(charnum == 2)
            {
                mincharnum = 299999;
                maxcharnum = 500001;
            }

            if(charnum == 3)
            {
                mincharnum = 500000;
                maxcharnum = 1000001;
            }

            if(charnum == 4)
            {
                mincharnum = 1000000;
                maxcharnum = 2000001;
            }

            if(charnum == 5)
            {
                mincharnum = 0;
                maxcharnum = 2000000;
            }
        }

        if (Request.QueryString["booktype"] != null)
        {
            typenum = Convert.ToInt32(Request.QueryString["booktype"].ToString());
            lkBookType[typenum].ForeColor = Color.Red;
        }

        string url = "";
        if(Request.QueryString["page"] != null)
        {
            pageIndex = Convert.ToInt32(Request.QueryString["page"]);
        }
        
        gvboos.DataSource = novel.getSelectBooks(statestring, typenum, mincharnum, maxcharnum, pageIndex, 20, out recordCount);
        gvboos.DataBind();
        string urlparm = Request.Url.Query;
        urlparm = urlparm.Replace("&page=" + pageIndex, "");
        url = Request.Url.AbsolutePath;
        int pageNumber = 0;
        pageNumber = (int)Math.Ceiling((double)recordCount / 20);
        LinkFirst.NavigateUrl = url + urlparm + "&page=" + 1;
        LinkPrece.NavigateUrl = url + urlparm + "&page=" + (pageIndex - 1);
        LinkNext.NavigateUrl = url + urlparm + "&page=" + (pageIndex + 1);
        LinkLast.NavigateUrl = url + urlparm + "&page=" + pageNumber;

        if (pageIndex == 1)
        {
            LinkFirst.Visible = false;
            LinkPrece.Visible = false;
        }

        if (pageIndex == pageNumber)
        {
            LinkLast.Visible = false;
            LinkNext.Visible = false;
        }

        lblcount.Text = "检索共有 " + recordCount.ToString() + " 本小说，页面大小：20，第"+ pageIndex +"页/共" + pageNumber + "页";
        
    }


    protected void s1_Click(object sender, EventArgs e)
    {
        WebControlExtension.LinkButtonEx[] lkex = new WebControlExtension.LinkButtonEx[25];
        
        string url = Request.Url.AbsolutePath + "?rnd=1";
        //状态
        lkex[0] = s1;
        lkex[1] = s2;
        lkex[2] = s3;

        //字数
        lkex[3] = c1;
        lkex[4] = c2;
        lkex[5] = c3;
        lkex[6] = c4;
        lkex[7] = c5;
        lkex[8] = c6;

        //类型
        lkex[9] = t1;
        lkex[10] = t2;
        lkex[11] = t3;
        lkex[12] = t4;
        lkex[13] = t5;
        lkex[14] = t6;
        lkex[15] = t7;
        lkex[16] = t8;
        lkex[17] = t9;
        lkex[18] = t10;
        lkex[19] = t11;
        lkex[20] = t12;
        lkex[21] = t13;


        WebControlExtension.LinkButtonEx lkTemp = (WebControlExtension.LinkButtonEx)sender;
        int index = 0;
        for (int i = 0; i < 22; i++)
        {
            if (lkTemp.Value == lkex[i].Value)
            {
                index = i;
            }
        }

        if (index >= 0 && index <= 2)
        {
            for (int i = 0; i < 3; i++)
            {
                if (lkex[i].Value == lkTemp.Value)
                {
                    lkex[i].ForeColor = Color.Red;
                }
                else
                {
                    lkex[i].ForeColor = Color.Black;
                }
            }
        }

        if (index >=3 && index<=8)
        {
            for (int i = 3; i < 9; i++)
            {
                if (lkex[i].Value == lkTemp.Value)
                {
                    lkex[i].ForeColor = Color.Red;
                }
                else
                {
                    lkex[i].ForeColor = Color.Black;
                }
            }
        }

        if (index >= 9 && index <= 21)
        {
            for (int i = 9; i < 22; i++)
            {
                if (lkex[i].Value == lkTemp.Value)
                {
                    lkex[i].ForeColor = Color.Red;
                }
                else
                {
                    lkex[i].ForeColor = Color.Black;
                }
            }
        }


        for (int k = 0; k < lkex.Length; k++)
        {
            if (lkex[k] != null)
            {
                if (lkex[k].ForeColor == Color.Red)
                {
                    url += "&" + lkex[k].Value;
                }
            }
        }

        Response.Redirect(url);
    }

    protected void gvboos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.RowIndex;
            Label lb = (Label)e.Row.FindControl("lbrownum");
            lb.Text = (index + 1).ToString();

            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#BBB'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
        }
    }
    protected void linkgo_Click(object sender, EventArgs e)
    {
        string urlparm = Request.Url.Query;
        string url = Request.Url.AbsolutePath;
        int page = 1;
        
        if (Request.QueryString["page"] != null)
        {
            int pageIndex = Convert.ToInt32(Request.QueryString["page"]);
            urlparm = urlparm.Replace("&page=" + pageIndex, "");
        }

        if (TxpageIndex.Text != "")
        {
            page = Convert.ToInt32(TxpageIndex.Text);
        }

        Response.Redirect(url + urlparm + "&page=" + page);
        
    }
}