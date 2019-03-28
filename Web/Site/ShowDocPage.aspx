<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowDocPage.aspx.cs" Inherits="Site_ShowDocPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=BookName%>
        <%=DocName%>
        <%=Creater%>
        听语阁文学 听语阁文学 5200全文阅读,燃文 笔趣阁|免费小说网 ,八路中文网,侠客中文网</title>
    <meta name="keywords" content="武侠,言情,网游,玄幻,奇幻,免费小说" />
    <meta name="keywords" content="<%= string.Join(",",new string[]{ BookName,DocName})%>" />
    <meta name="description" content="<%
string tempx=textData.ForMatTextReplaceHTMLTags(Content).Replace("&nbsp;",string.Empty);
 %><%=tempx.Length>150?tempx.Substring(0,150):tempx %>" />
    <script type="text/javascript">
<!-
<%if(IsCresteHTMLPage){ %>
    var preview_page = "<%=上一章 %>";
    var next_page = "<%=下一章 %>";
    var index_page = "index.html";
    <%}else{ %>
        var preview_page = "?guid=<%=上一章 %>";
    var next_page = "?guid=<%=下一章 %>";
    var index_page = "BookIndex.aspx?guid=<%=书id %>";

    <% }%>

    var article_id = "27070";
    var chapter_id = "4570677";

    function jumpPage() {
        if (event.keyCode == 37) location = preview_page;
        if (event.keyCode == 39) location = next_page;
        if (event.keyCode == 13) location = index_page;
    }
    document.onkeydown = jumpPage;
-->
    </script>
</head>
<body>
    <form id="form1" runat="server">
<div>
<a href="<%=(IsCresteHTMLPage?"index.html":"/Site/BookIndex.aspx?guid="+书id)%>"><%= BookName%> 目录 </a>
  <div>
        <%=上一页 %>
    </div>
    <div>
        <%=下一页 %>
    </div>
</div>
    <div>
        <h2 style="text-align: center;">
            <%= DocName%>
        </h2>
    </div>
    <div id="content">
        <%=Content
        .Replace("<div id=\"lastcp\">欢迎您访问www.yankuai.com,<a href=\"/register.php\" target=\"_blank\"><font color=\"red\">注册会员</font></a></div>", "")
        .Replace("（眼快看书&nbsp;www.Yankuai.com）","")
        .Replace("大文学 www.dawenxue.org","")
            %>
    </div>
    <div>
        <%=上一页 %>
    </div>
    <div>
        <%=下一页 %>
    </div>
    </form>
    <script type="text/javascript">
        var _bdhmProtocol = (("https:" == document.location.protocol) ? " https://" : " http://");
        document.write(unescape("%3Cscript src='" + _bdhmProtocol + "hm.baidu.com/h.js%3F0be493a74b9bb5dccd847270f853c20b' type='text/javascript'%3E%3C/script%3E"));
    </script>
    <!-- Baidu Button BEGIN -->
<script type="text/javascript" id="bdshare_js" data="type=slide&amp;mini=1&amp;img=6&amp;uid=6440311" ></script>
<script type="text/javascript" id="bdshell_js"></script>
<script type="text/javascript">
    document.getElementById("bdshell_js").src = "http://share.baidu.com/static/js/shell_v2.js?cdnversion=" + new Date().getHours();
</script>
<!-- Baidu Button END -->
</body>
</html>
