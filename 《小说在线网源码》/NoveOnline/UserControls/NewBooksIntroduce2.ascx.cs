using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;
public partial class UserControls_NewBooksIntroduce2 : System.Web.UI.UserControl
{
    INovel novel = BllFactory.BllAccess.CreateINovelBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        repeater.DataSource = novel.getBookType();
        repeater.DataBind();
        
    }
    protected void repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            TextBox tbtypeId = e.Item.FindControl("typeId") as TextBox;
            int typeId = Convert.ToInt32(tbtypeId.Text);

            Repeater repeaterNoelSections = e.Item.FindControl("booksInfo") as Repeater;
            repeaterNoelSections.DataSource = novel.getBookInfoss(typeId);
            repeaterNoelSections.DataBind();

            Repeater repeaterNoelSections2 = e.Item.FindControl("booksInfo2") as Repeater;
            repeaterNoelSections2.DataSource = novel.getBookInfoss2(typeId);
            repeaterNoelSections2.DataBind();
        }
    }
}