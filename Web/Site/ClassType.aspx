<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClassType.aspx.cs" Inherits="Site_ClassType" %>
<%@ Import Namespace=" Skybot.Cache" %>
<%@ Import Namespace=" Skybot.Tong.CodeCharSet" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>类型列表 听语阁文学 </title>
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
    <meta name="keywords" content="武侠,言情,网游,玄幻,奇幻,免费小说" />
    <meta name="description" content="在线小说阅读网，好看的在线手机小说阅读，wap小说下载，移动书库提供言情小说，玄幻小说，武侠小说，军事小说，科幻小说,恐怖小说,灵异小说,热门小说，小说最新章节免费阅读。坚决抵制成人小说、情色小说、黄色小说等成人文学和色情小说" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:DataList ID="docList" runat="server" RepeatColumns="5" Font-Bold="False" 
            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
            Font-Underline="False">
        <ItemTemplate>
            <%if (!IsCresteHTMLPage)
                          { %>
                       <a href="/Site/BookList.aspx?lx=<%#Eval("ID") %><%=(IsCresteHTMLPage?"&html=true":"&html=false") %>" target="_blank">[<%#Eval("分类标识") %>]</a>
                       
                        
                        <%}
                          else
                          {
                            
                               %>
                   <a href="/Book/<%#Eval("分类标识").ToString().ToPingYing() %>/<%#Eval("ID") %>/index.aspx"> <%#Eval("分类标识") %></a>
             
                        
                        <%      } %>
        </ItemTemplate>
    </asp:DataList>
    </div>
    
    </form>
</body>
</html>
