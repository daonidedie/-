using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BookControls_addNewBook : System.Web.UI.UserControl
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        IDAL.INovel novel = BllFactory.BllAccess.CreateINovelBLL();
        dBookType.DataSource = novel.getBookType();
        dBookType.DataTextField = "TypeName";
        dBookType.DataValueField = "TypeId";
        dBookType.DataBind();
        
    }
    protected void Unnamed1_Click(object sender, ImageClickEventArgs e)
    {
        IDAL.IAuthor ia = BllFactory.BllAccess.CreateIAuthorBLL();
        int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

        Model.BooksInfo item = new Model.BooksInfo();
        item.BookName = TextBox1.Text;
        item.AuthorId = userId;
        item.BookIntroduction = fckAddbooks.Value;
        item.BookType = (dBookType.SelectedIndex) + 1;
        string fileName = "";
        if (f1.HasFile)
        {
            switch (System.IO.Path.GetExtension(f1.FileName).ToLower())
            {
                case ".jpg": fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(f1.FileName);
                    f1.SaveAs(Server.MapPath("~/Images/Books/" + fileName));
                    break;
                case ".gif": fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(f1.FileName);
                    f1.SaveAs(Server.MapPath("~/Images/Books/" + fileName));
                    break;
                default: 
                    ScriptManager.RegisterStartupScript(this.Page,this.GetType(), "error", "<script type='text/javascript'>alert('不支持此格式，请使用图片文件');</script>",false);
                    break;
            }
            item.Images = fileName;
        }


        if (fileName == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", "<script type='text/javascript'>alert('请选择书本封面');</script>", false);
            return;
        }

        int count = ia.AuaddBook(item);
        if (count > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ok", "<script type='text/javascript'>alert('添加成功！');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ok", "<script type='text/javascript'>alert('添加失败！');</script>", false);
        }

        
    }
}