<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookList1.aspx.cs" Inherits="Site_BookList" %>
<%@ Import Namespace=" Skybot.Cache" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>图书列表 听语阁文学 </title>
    <style type="text/css">
        ul
        {
            clear: both;
        }
        li
        {
            float: left;
            width: 24%;
            float: left;
        }
    </style>
    <meta name="baidu-site-verification" content="vkPuafOONSwVmWV6" />
    <meta name="keywords" content="武侠,言情,网游,玄幻,奇幻,免费小说" />
    <meta name="keywords" content="<%= string.Join(",", list.Select(p => p.书名).ToArray())%>" />
    <meta name="description" content="在线小说阅读网，好看的在线手机小说阅读，wap小说下载，移动书库提供言情小说，玄幻小说，武侠小说，军事小说，科幻小说,恐怖小说,灵异小说,热门小说，小说最新章节免费阅读。坚决抵制成人小说、情色小说、黄色小说等成人文学和色情小说" />
</head>
<body>
    <form id="form1" runat="server" defaultbutton="search" defaultfocus="TextBox1">
    <div>
        搜索（请输入文章名）：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button runat="server" Text="搜索" ID="search" OnClick="Unnamed1_Click"></asp:Button> <a href="/Site/ClassType.aspx">类型列表</a>
    </div>
    <div>
        <ul>
            <asp:Repeater ID="ShowNews" runat="server">
                <ItemTemplate>
                    <%--                [ID]
      ,[GUID]
      ,[书名]
      ,[分类标识]
      ,[分类表ID]
      ,[说明]
      ,[作者名称]
      ,[创建时间]
      ,[配图]
      ,[周点击]
      ,[总点击]
      ,[周鲜花]
      ,[总鲜花]
      ,[免费]
      ,[完本]
      ,[最后更新时间]
      ,[最新章节]--%>
                    <ul>
                        <%if (!IsCresteHTMLPage)
                          { %>
                        <li><a href="/Site/BookList.aspx?lx=<%#Eval("分类表ID") %>" target="_blank">[<%#Eval("分类标识") %>]</a>
                        </li>
                        <li><a href="/Site/BookIndex.aspx?guid=<%#Eval("GUID") %>" target="_blank">
                            <%#Eval("书名")%></a> </li>
                        <li>[<%#Eval("作者名称")%>] </li>
                        <li>[<a href="/Site/BookList.aspx?lx=<%#(bool.Parse( Eval("完本")+""))?"-1":"-2"%>"
                            target="_blank"><%#(bool.Parse( Eval("完本")+""))?"完本":"连载"%></a>] </li>
                        <%}
                          else
                          {
                            
                               %>
                        <li><a href="/Site/BookList.aspx?lx=<%#Eval("分类表ID") %><%=(IsCresteHTMLPage?"&html=true":string.Empty) %>" target="_blank">[<%#Eval("分类标识") %>]</a>
                        </li>
                        <li><a href="<%#  ((TygModel.书名表)GetDataItem()).GetHTMLFilePath() %>" target="_blank">
                            <%#Eval("书名")%></a> </li>
                        <li>[<%#Eval("作者名称")%>] </li>
                        <li>[<a href="/Site/BookList.aspx?lx=<%#(bool.Parse( Eval("完本")+""))?"-1":"-2"%><%=(IsCresteHTMLPage?"&html=true":string.Empty) %>"
                            target="_blank"><%#(bool.Parse( Eval("完本")+""))?"完本":"连载"%></a>] </li>
                        <%      } %>
                    </ul>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="list-page">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="50" HorizontalAlign="Center"
            UrlPaging="True" OnPageChanged="dfsd" EnableUrlRewriting="false" Width="630px"
            FirstPageText=' <font face="webdings">9</font>' LastPageText='<font face="webdings">:</font>'
            NextPageText='<font face="webdings">4</font>' PrevPageText='<font face="webdings">3</font>'>
        </webdiyer:AspNetPager>
    </div>
        <div>        <a href="http://hao.360.cn/" target="_blank" >360安全网址导航</a></div>

    </form>
    <script type="text/javascript">
        var _bdhmProtocol = (("https:" == document.location.protocol) ? " https://" : " http://");
        document.write(unescape("%3Cscript src='" + _bdhmProtocol + "hm.baidu.com/h.js%3F0be493a74b9bb5dccd847270f853c20b' type='text/javascript'%3E%3C/script%3E"));
    </script>
</body>
</html>
