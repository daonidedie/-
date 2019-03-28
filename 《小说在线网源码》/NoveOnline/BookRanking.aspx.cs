using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IDAL;

public partial class BookRanking : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        //repeaterCountSum.DataSource = novel.getAllAppoint();
        //repeaterCountSum.DataBind();

        //repeaterFlowerNumber.DataSource = novel.getMemory2();
        //repeaterFlowerNumber.DataBind();

        //repeaterTicketNumber.DataSource = novel.getMemory3();
        //repeaterTicketNumber.DataBind();

            repeaterType.DataSource = novel.getBookType();
            repeaterType.DataBind();

            if (Request.QueryString["typeId"] == null || Convert.ToInt32(Request.QueryString["typeId"]) == 0)
            {
                int Recordcount;
                int pageIndex;
                string url = Request.Url.AbsolutePath + "?typeId=0" + "&page=";
                if (Request.QueryString["page"] == null)
                {
                    pageIndex = 1;
                    repeaterTable.DataSource = novel.getAllBookNumberTable(pageIndex, 20, out Recordcount);
                    repeaterTable.DataBind();
                }
                else
                {
                    pageIndex = Convert.ToInt32(Request.QueryString["page"]);
                    repeaterTable.DataSource = novel.getAllBookNumberTable(pageIndex, 20, out Recordcount);
                    repeaterTable.DataBind();
                }

                int pageNumber = (int)Math.Ceiling((double)Recordcount / 20);
                lblPage.Text = "第 " + pageIndex + @" 页 / 共 " + pageNumber + " 页";
                LinkFirst.NavigateUrl = url + 1;
                LinkPrece.NavigateUrl = url + (pageIndex - 1);
                LinkNext.NavigateUrl = url + (pageIndex + 1);
                LinkLast.NavigateUrl = url + pageNumber;
                if (pageIndex == 1)
                {
                    LinkFirst.Visible = false;
                    LinkPrece.Visible = false;
                }

                if (pageIndex == pageNumber)
                {
                    LinkNext.Visible = false;
                    LinkLast.Visible = false;
                }

            }
            else if (Request.QueryString["typeId"] != null)
            {
                string typeid = Request.QueryString["typeId"].ToString();
                string urlByType = Request.Url.AbsolutePath + "?typeId=" + typeid + "&page=";
                int RecordcountbyType;
                int pageIndex;
                if (Request.QueryString["page"] == null)
                {
                    pageIndex = 1;
                    repeaterTable.DataSource = novel.getBookNumberTable(pageIndex, 20, out RecordcountbyType, Convert.ToInt32(Request.QueryString["typeId"]));
                    repeaterTable.DataBind();
                }
                else
                {
                    pageIndex = Convert.ToInt32(Request.QueryString["page"]);
                    repeaterTable.DataSource = novel.getBookNumberTable(pageIndex, 20, out RecordcountbyType, Convert.ToInt32(Request.QueryString["typeId"]));
                    repeaterTable.DataBind();
                }

                int pageNumber = (int)Math.Ceiling((double)RecordcountbyType / 20);
                lblPage.Text = "第 " + pageIndex + @" 页 / 共 " + pageNumber + " 页";
                LinkFirst.NavigateUrl = urlByType + 1;
                LinkPrece.NavigateUrl = urlByType + (pageIndex - 1);
                LinkNext.NavigateUrl = urlByType + (pageIndex + 1);
                LinkLast.NavigateUrl = urlByType + pageNumber;

                if (pageIndex == 1)
                {
                    LinkFirst.Visible = false;
                    LinkPrece.Visible = false;
                }

                if (pageIndex == pageNumber)
                {
                    LinkNext.Visible = false;
                    LinkLast.Visible = false;
                }
            }
    }

    protected void linkgo_Click(object sender, EventArgs e)
    {
        INovel novel = BllFactory.BllAccess.CreateINovelBLL();



        if (Request.QueryString["typeId"] == null || Request.QueryString["typeId"] == "0")
        {
            int pageIndex = Convert.ToInt32(TxpageIndex.Text);
            string url = Request.Url.AbsolutePath + "?page=";
            if (pageIndex.ToString() != "")
            {
                Response.Redirect(url + pageIndex);
            }
            else
            {
                Response.Redirect(url + 1);
            }
        }
        else
        {
            int pageIndex = Convert.ToInt32(TxpageIndex.Text);
            string typeid = Request.QueryString["typeId"].ToString();
            string urlByType = Request.Url.AbsolutePath + "?typeId=" + typeid + "&page=";

            if (pageIndex.ToString() != "")
            {
                Response.Redirect(urlByType + pageIndex);
            }
            else
            {
                Response.Redirect(urlByType + 1);
            }
        }

    }
    
}