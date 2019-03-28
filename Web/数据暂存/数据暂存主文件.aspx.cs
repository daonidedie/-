using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Skybot.Cache;

public partial class 数据暂存_数据暂存主文件 : System.Web.UI.Page
{

    string str = @"壹一两
            贰
            二
            叁
            三
            肆
            四 
            伍
            五               
            陆
            六
            柒

            七
 
            捌

            八
 
            玖

            九

               
            拾

            十
 

            佰

            百
 

            仟

            千

                

            萬

            万

 

            零";

    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Write(str.Replace(" ",""));

    }
}


