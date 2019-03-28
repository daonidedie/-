<%@ WebHandler Language="C#" Class="HandlerPicValid" %>

using System;
using System.Web;


using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;
using System.Web.Services;

public class HandlerPicValid : IHttpHandler,IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {

        //存放4个随机字母
        string s = "";
        string picString = "";
        //实例一个随机数种子
        Random rd = new Random();

        //接收产生的随机数
        int rdnumber;

        //生成4个随机数放到字符串S中
        for (int i = 0; i < 4; i++)
        {
            ;
            rdnumber = rd.Next(26);
            rdnumber = rdnumber + 65;
            char c = Convert.ToChar(rdnumber);
            s += c.ToString() + " ";
            picString += c.ToString();
        }
        
        
        //生成随机图片
        Random rd1 = new Random();
        int picint = rd1.Next(3);
        picint = picint + 1;
        
        //把随机数放入SESSION中
       // context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        
        context.Session["pic"] = picString;

        string test = context.Session["pic"].ToString();
       
        //读取随机数背景图片
        
        Image imgold = Image.FromFile(context.Server.MapPath(picint + ".JPG"));

        //产生一个空图象
        Image img = new Bitmap(80, 20);

        //将空图象放入画布
        Graphics grp = Graphics.FromImage(img);

        //将背景图画入画布内
        grp.DrawImage(imgold,
            new Rectangle(0, 0, 130,20),
            new Rectangle(0, 0, img.Width, img.Height),
        GraphicsUnit.Pixel);
        

        //将生成的随机数画入画布内
        grp.DrawString(s, new Font("宋体", 16, FontStyle.Bold, GraphicsUnit.Pixel), new SolidBrush(Color.Black),5,2);

        ////画一根线在画布内
        grp.DrawLine(new Pen(Color.Black), new Point(0, 10), new Point(80, 10));

        //将画好的图象存入输出流中，格式是JPEG
        img.Save(context.Response.OutputStream, ImageFormat.Jpeg);
        
        
        //关闭使用的资源
        grp.Dispose();
        imgold.Dispose();
        img.Dispose();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}