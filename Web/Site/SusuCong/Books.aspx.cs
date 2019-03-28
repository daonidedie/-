using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Documents;
using TygModel;

public partial class Site_SusuCong_Books : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        List<书名表> books = new List<书名表>();

        System.Xml.Serialization.XmlSerializer xmlSer = new System.Xml.Serialization.XmlSerializer(books.GetType());
        
        /// <summary>
        /// 数据库访问对像
        /// </summary>
        using (TygModel.Entities Tygdb = new TygModel.Entities())
        {

            books = Tygdb.书名表.ToList();
            System.IO.StringWriter sw = new System.IO.StringWriter();
            xmlSer.Serialize(sw, books);

            //System.IO.File.WriteAllText(Server.MapPath("./books.html"),sw.ToString());
            Response.Write(sw.ToString());
            

            Tygdb.Connection.Close();
        }

    }
}