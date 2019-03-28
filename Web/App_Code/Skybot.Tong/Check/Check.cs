/******************************************************
* 文件名：Check.cs
* 文件功能描述：URL FORM 检查
* 
* 创建标识：周渊 2005-11-4
* 
* 修改标识：周渊 2008-5-13
* 修改描述：按代码编写规范改写部分代码
 * 修改描述： 修改提示信息
*
 * 修改标识：洪益 2009-03-31
 * 修改描述：周渊 2009-4-1 修改路径 
******************************************************/

using System;

namespace Skybot.Tong.Check
{
    /// <summary>
    /// 检查类
    /// 查看URL和From的非法字符
    /// </summary>
    public class Check
    {
        /// <summary>
        /// 调用数据设计通用类
        /// 
        /// </summary>
        TongUse tong = new TongUse();
        /// <summary>
        /// 查看URL和From的非法字符
        /// </summary>
        public void CheckStr()//防止SQL注入
        {
            //string pos1,pos7;
            string url, ip, pos2, pos3, pos4, pos5, pos6, pos8;
            //ToUpper把字母變成大寫的
            url = tong.UrlData;//得到?后面的字符串
            ip = tong.Res.Request.ServerVariables["REMOTE_ADDR"].ToString();//取远程用户IP地址 
            // pos1 = url.IndexOf("%").ToString();//查找字串中指定字符或字串首次（最后一次）出现的位置,返回索引值
            pos2 = url.IndexOf("'").ToString();
            pos3 = url.IndexOf(";").ToString();
            pos4 = url.IndexOf("where").ToString();
            pos5 = url.IndexOf("select").ToString();
            pos6 = url.IndexOf("chr").ToString();
            //pos7 = url.IndexOf("/").ToString();
            pos8 = url.IndexOf("and").ToString();

            //SQL注入东东看看有没有 这个是URL的
            //以屏蔽％　　加入代码为：　(int.Parse(pos1) != -1) || 
            //以屏蔽＇　　加入代码为：　(int.Parse(pos2) != -1) ||
            //以屏蔽＇　　加入代码为：  || (int.Parse(pos8) != -1)  加在最后
            //以屏蔽/　　加入代码为：  || (int.Parse(pos7) != -1)  加在最后
            if ((int.Parse(pos3) != -1) || (int.Parse(pos4) != -1) || (int.Parse(pos5) != -1) || (int.Parse(pos6) != -1))
            {
                /*tong.Res.Response.Write(pos1);
                tong.Res.Response.Write(pos2);
                tong.Res.Response.Write(pos3);
                tong.Res.Response.Write(pos4);
                tong.Res.Response.Write(pos5);
                tong.Res.Response.Write(pos6);
                tong.Res.Response.Write(pos7);
                */
                tong.Res.Response.Write("<script language='javascript'>history.back(); alert(\"您所提交的数据中包含有\\r\\n系统已做为限制 的参数 如 \\r\\n select , and  , 等字符\\r\\n请 谅解! \");</script>");
                tong.Res.Response.End();
            }
            //tong.Res.Response.Write(url.ToUpper());关闭URL输出
            //以下开始表单里的数据;
            string 表单;
            int j, i;
            j = tong.Res.Request.Form.Count;//得到表单的总数

            for (i = 0; i < j; i++)
            {
                表单 = tong.Res.Request.Form[i].ToString();
                if ((表单.IndexOf("'") != -1) || (表单.IndexOf(",,,,,,,,,,,,,,,,") != -1))
                {

                    tong.Res.Response.Write("<script language='javascript'>history.back(); alert(\"您所提交的数据中包含有 \\\" '  \\\" 英文状态下的 单引号 \\r\\n 系统已把此做为限制提交的字符请 谅解! \");</script>");
                    tong.Res.Response.End();
                }
            }
            //Form的东东
        }

        /// <summary>
        /// 查看系统运行路径函数
        /// </summary>
        /// <param name="path">系统设定路径</param>
        public String Check_Paht(string path)
        {
            string Http_path, text = "";//Http_path 得到用户访问路径 , text 函数输出信息;

            Http_path = tong.Res.Request.RawUrl.ToString();//RawUrl 得于当间请求的URL

            Http_path = Http_path.Substring(0, path.Length);//按根目录对数据进行裁前
            if (Http_path == path)//如果设定目录等于得到目录
            {
                text = "";
            }
            else
            {
                text = "<script>alert('系统末能初始化\\r\\n请在Web.Config文件中配置 系统安装路径');history.go(-1);</script>";

            }
            return text;
        }
    }
}
