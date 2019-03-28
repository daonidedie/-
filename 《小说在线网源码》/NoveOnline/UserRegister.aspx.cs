using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IDAL;
using System.IO;
public partial class UserInfoLogin : System.Web.UI.Page
{
    INovel novel = BllFactory.BllAccess.CreateINovelBLL();
    IUsers novel2 = BllFactory.BllAccess.CreateIUsersBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlProvinceId.DataSource = novel.getProvince();
            ddlProvinceId.DataBind();

            //ddlArea.Items.Add("=请选择市区=");
            //ddlArea.DataSource = novel.getArea("");
            //ddlArea.DataBind();
            int provinceid = Convert.ToInt32(ddlProvinceId.SelectedItem.Value);
            ddlArea.DataSource = novel.getArea(novel.getProvinceID(provinceid));
            ddlArea.DataBind();
            ddlArea.Items.Add("=请选择市区=");
            ddlArea.SelectedIndex = ddlArea.Items.Count - 1;
        }
    }
    protected void btOK_Click(object sender, EventArgs e)
    {
        string fileName = "";
        if (fuUsetImage.HasFile)
        {
            switch (System.IO.Path.GetExtension(fuUsetImage.FileName).ToLower())
            {
                case ".jpg": fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(fuUsetImage.FileName);
                    fuUsetImage.SaveAs(Server.MapPath("~/Images/AuthorFace/" + fileName));
                    break;
                case ".gif": fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(fuUsetImage.FileName);
                    fuUsetImage.SaveAs(Server.MapPath("~/Images/AuthorFace/" + fileName));
                    break;
                default: ClientScript.RegisterStartupScript(this.GetType(), "error", "<script type='text/javascript'>alert('不支持此格式，请使用图片文件');</javascript>");
                    break;
            }
        }
        string userName = tbUserName.Text;
        string userName2 = tbUserName2.Text;
        string userPassword = tbUserPassword.Text;

        int sex = 0;
        string brithday = DateTime.Now.ToString();
        string identityCardNumber = "";
        int province = 0;
        int area = 0;
        string address = "";
        if (cbOK.Checked)
        {
            sex = rbSexM.Checked ? 0 : 1;
            brithday = tbBrithday.Text;
            identityCardNumber = tbIdentityCardNumber.Text;
            province = Convert.ToInt32(ddlProvinceId.SelectedItem.Value);
            area = Convert.ToInt32(ddlArea.SelectedItem.Value);
            address = tbAddress.Text;
        }
       int count = novel2.userLogin2(userName, userPassword, userName2, sex, brithday, fileName, identityCardNumber, province, area, address);
       if (count > 0)
       {
           ClientScript.RegisterClientScriptBlock(this.GetType(), "ok", "<script type='text/javascript'>alert('注册成功！');this.location.href='Default.aspx';</script>", false);
           
       }
       else
       {
           ClientScript.RegisterClientScriptBlock(this.GetType(), "no", "<script type='text/javascript'>alert('注册失败！');</script>", false);
       }

    }
    protected void ddlProvinceId_SelectedIndexChanged(object sender, EventArgs e)
    {
        int provinceid = Convert.ToInt32(ddlProvinceId.SelectedItem.Value);
        ddlArea.DataSource = novel.getArea(novel.getProvinceID(provinceid));
        ddlArea.DataBind();
        ddlArea.Items.Add("=请选择市区=");
        ddlArea.SelectedIndex = ddlArea.Items.Count - 1;
    }
}