<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowDoc.aspx.cs" Inherits="Site_ShowDoc"  MasterPageFile="~/file/MasterPage.master"%>

<%@ Import Namespace=" Skybot.Cache" %>
<%@ Import Namespace=" Skybot.Tong.CodeCharSet" %>
<asp:content id="Content1" contentplaceholderid="head" runat="Server">
        <title><%=BookName%>
        <%=DocName%>
        <%=Creater%>
        听语阁文学 听语阁文学 5200全文阅读,燃文 笔趣阁|免费小说网 </title>
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
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <!--main_list..开始 -->
    <!--列表开始 -->
    <div class="book_news" style="text-align:left;">
        <div class="book_news_title title"> 
            <ul>
                <li>当前位置： <a href="/">首页</a>&gt;&gt;
                   <%if (!IsCresteHTMLPage)
                     { %>
        <a href="/Site/BookList.aspx?lx=<%=currentBook.分类表ID %><%=(IsCresteHTMLPage?"&html=true":"&html=false") %>" target="_blank"><%=currentBook.分类标识 %></a>
        &gt;&gt;
                <a href="ShowDoc.aspx?guid=<%=currentBook.GUID %>" target="_blank" title="<%=BookName %>>">
                    <%=BookName %>
                </a>
        <%}
                     else
                     {
        %>
        <a href="/Book/<%=currentBook.分类标识.ToString().ToPingYing() %>/<%=currentBook.分类表ID %>/index.aspx" target="_blank"><%=currentBook.分类标识 %></a>
        &gt;&gt;     
        <a href="<%=currentBook.GetHTMLFilePath() %>" target="_blank" title="<%=BookName %>>">
            <%=BookName %>
        </a>
        <%} %>
                </li>
            </ul>
        </div>
  <div class="book_news_style" >
    <div>
        <%=上一页 %>
    </div>
    <div>
        <%=下一页 %>
    </div>
</div>
  <div>

  <div class="center" style="text-align:center; font-size:36px;  font-family:微软雅黑;">
                             &nbsp; <%=DocName %> 
                                  </div>
                                  <br />
                                  <hr />
<div class="clear"></div>
    <div id="content"  style=" font-size:24px; line-height:32px; font-family:微软雅黑;">
        <%=Content
        .Replace("<div id=\"lastcp\">欢迎您访问www.yankuai.com,<a href=\"/register.php\" target=\"_blank\"><font color=\"red\">注册会员</font></a></div>", "")
        .Replace("（眼快看书&nbsp;www.Yankuai.com）","")
                    .Replace("大文学 www.dawenxue.org", "").Replace("（记住本站网址，Ｗｗｗ．XS52．Ｃｏｍ，方便下次阅读，或且百度输入“ xs52 ”，就能进入本站）", "")
            %>
    </div>
    <div>
        <%=上一页 %>
    </div>
    <div>
        <%=下一页 %>
    </div>


        <!--列表结束 -->
    </div>
    </div>
    <!--main_list结束 -->
    <!-- Baidu Button BEGIN -->
<script type="text/javascript" id="bdshare_js" data="type=slide&amp;mini=1&amp;img=6&amp;uid=6440311" ></script>
<script type="text/javascript" id="bdshell_js"></script>
<script type="text/javascript">
    document.getElementById("bdshell_js").src = "http://share.baidu.com/static/js/shell_v2.js?cdnversion=" + new Date().getHours();
</script>
<!-- Baidu Button END -->
</asp:content>
