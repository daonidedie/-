using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using BLL;
using BllFactory;
using Model;
public partial class UserControls_ClickCount : System.Web.UI.UserControl
{
    IDAL.INovel dal = BllAccess.CreateINovelBLL();
    int week;
    LinkButton lbtemp1;
    LinkButton lbtemp2;

    protected void Page_Load(object sender, EventArgs e)
    {

        
        
        if (!IsPostBack)
        {
            getWeek();
            Common.Myweeks.Myweek = week;    
            
        }


        repeaterBooksCountQuantitys.DataSource = dal.getHotNovels();
        repeaterBooksCountQuantitys.DataBind();
        
        
    }

    private void getWeek()
    {
        int[] maxDay = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        DateTime intraday = DateTime.Now.Date;
        int frontYearSumDays = 0;
        int sumdays = 0;
        int year = intraday.Year;
        int month = intraday.Month;
        int day = intraday.Day;
        for (int i = 1900; i <= year - 1; i++)
        {
            if (i % 4 == 0 && i % 100 != 0 || i % 400 == 0)
                sumdays += 366;
            else
                sumdays += 365;
        }
        for (int i = 0; i <= month - 2; i++)
            sumdays += maxDay[i];
        //获取1900到当前号数的总天数
        sumdays += day;
        //获取1900到前一年的总天数
        for (int i = 1900; i <= year - 2; i++)
        {
            if (i % 4 == 0 && i % 100 != 0 || i % 400 == 0)
                frontYearSumDays += 366;
            else
                frontYearSumDays += 365;
        }
        //获取前一年最后一个星期剩下几天
        int spare = frontYearSumDays % 7;
        //当前周是所在年的第几周
        
        if (spare != 0)
            week = Convert.ToInt32(Math.Ceiling((sumdays - frontYearSumDays - (7 - spare)) / 7.0) + 1);
        else
            week = Convert.ToInt32(Math.Ceiling((sumdays - frontYearSumDays) / 7.0));

        List<Model.BooksCountQuantity> list = dal.getBooksCountQuantity();

        foreach (Model.BooksCountQuantity s in list)
        {
            if (s.Times == week)
                break;
            else if (s.Times < week)
            {
                dal.updateooksCountQuantity(0, 1, s.BookId, s.Times);
            }
        }
        foreach (Model.BooksCountQuantity s in list)
        {
            if (s.Times2.Month == DateTime.Now.Month)
                break;
            else if (s.Times2.Month < DateTime.Now.Month)
            {
                dal.updateooksCountQuantity(1, 0, s.BookId, s.Times);
            }
        }
    }

    protected void weeks_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        lb.ForeColor = Color.Red;

        lbtemp1 = (LinkButton)this.FindControl("month");
        lbtemp1.ForeColor = Color.Black;
        lbtemp2 = (LinkButton)this.FindControl("all");
        lbtemp2.ForeColor = Color.Black;
        int mweek = Common.Myweeks.Myweek;
        repeaterBooksCountQuantitys.DataSource = dal.getBooksCountQuantitys(mweek);
        repeaterBooksCountQuantitys.DataBind();

    }
    protected void month_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        lb.ForeColor = Color.Red;

        lbtemp1 = (LinkButton)this.FindControl("weeks");
        lbtemp1.ForeColor = Color.Black;
        lbtemp2 = (LinkButton)this.FindControl("all");
        lbtemp2.ForeColor = Color.Black;

        repeaterBooksCountQuantitys.DataSource = dal.getBooksCountMonth();
        repeaterBooksCountQuantitys.DataBind();
      
    }
    protected void all_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        lb.ForeColor = Color.Red;

        lbtemp1 = (LinkButton)this.FindControl("month");
        lbtemp1.ForeColor = Color.Black;
        lbtemp2 = (LinkButton)this.FindControl("weeks");
        lbtemp2.ForeColor = Color.Black;

        repeaterBooksCountQuantitys.DataSource = dal.getHotNovels();
        repeaterBooksCountQuantitys.DataBind();
    }

}