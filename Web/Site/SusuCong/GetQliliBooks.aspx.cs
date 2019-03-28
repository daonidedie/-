using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Skybot.Cache;
public partial class Site_SusuCong_GetQliliBooks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = string.Format("{0}/Site/SusuCong/Books.aspx?" + Guid.NewGuid(), System.Configuration.ConfigurationManager.AppSettings["baseSite"]);
            //Skybot.Cache.QliliHelper.BaseSite + "/Site/SusuCong/Books.aspx?" + Guid.NewGuid();
        System.Net.WebClient wc = new System.Net.WebClient();
       
        wc.DownloadFile(url, Server.MapPath("./books.xml"));

       //  var d=System.IO.File.ReadAllBytes();
         System.IO.StringReader sw = new System.IO.StringReader(System.IO.File.ReadAllText(Server.MapPath("./books.xml")));

         var seral = new System.Xml.Serialization.XmlSerializer(Skybot.Cache.QliliHelper.Books.GetType());
         var sd = seral.Deserialize(sw);
         Skybot.Cache.QliliHelper.Books = (List<TygModel.书名表>)sd;
         Skybot.Cache.QliliHelper.Books = Skybot.Cache.QliliHelper.Books.Where(p => System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory+p.GetHTMLFilePath())).ToList();


    }
}