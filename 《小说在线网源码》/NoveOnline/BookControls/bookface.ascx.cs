using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookControls_bookface : System.Web.UI.UserControl
{
    IDAL.INovel nover = BllFactory.BllAccess.CreateINovelBLL();
    IDAL.IAuthor Ia = BllFactory.BllAccess.CreateIAuthorBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int bookId = Convert.ToInt32(Request.QueryString["BookId"]);
        face.NavigateUrl = Request.Url.AbsolutePath + "?type=2&ct=1&bookId=" + bookId;
        vs.NavigateUrl = Request.Url.AbsolutePath + "?type=2&ct=2&bookId=" + bookId;

        //书本状态
        List<Model.BookStateInfo> state = new List<Model.BookStateInfo>();
        Model.BookStateInfo item = new Model.BookStateInfo();
        item.StateId = 1;
        item.StateName = "连载中";

        Model.BookStateInfo item1 = new Model.BookStateInfo();
        item1.StateId = 2;
        item1.StateName = "已完本";

        state.Add(item);
        state.Add(item1);

        DropDownList1.DataSource = state;
        DropDownList1.DataTextField = "StateName";
        DropDownList1.DataValueField = "StateId";
        DropDownList1.DataBind();

        //书本类型
        IDAL.INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        dBookType.DataSource = novel.getBookType();
        dBookType.DataTextField = "TypeName";
        dBookType.DataValueField = "TypeId";
        dBookType.DataBind();

        //书本名称
        Model.BooksInfo bookItem = novel.getBookIdBooksInfo(bookId);
        TextBox1.Text = bookItem.BookName;
        fckAddbooks.Value = bookItem.BookIntroduction;
        dBookType.SelectedIndex = bookItem.BookType - 1;
        DropDownList1.SelectedIndex = bookItem.BookState - 1;

        

        
    }
    protected void imgbtEdit_Click(object sender, ImageClickEventArgs e)
    {
        Model.BooksInfo item = new Model.BooksInfo();
        item.BookId = Convert.ToInt32(Request.QueryString["BookId"]);
        item.BookName = TextBox1.Text;
        item.BookIntroduction = fckAddbooks.Value;
        item.BookType = dBookType.SelectedIndex + 1;
        item.BookState = DropDownList1.SelectedIndex + 1;

        int count = Ia.EditBookFace(item);

        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "ok", "<script type='text/javascript'>alert('修改成功！');</script>", false);
    }
}