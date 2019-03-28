<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookIndex.aspx.cs" Inherits="Site_BookIndexPage"  MasterPageFile="~/file/MasterPage.master"%>
<%@ Import Namespace=" Skybot.Cache" %>
<%@ Import Namespace=" Skybot.Tong.CodeCharSet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 
        <title><%=BookName %>章节列表  <%=BookName %>最新章节 ,<%=Creater%>, 听语阁文学 5200全文阅读,燃文 笔趣阁|免费小说网 </title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content=" <%=string.Join("",BookName) %>小说  <%=string.Join("",BookName) %>下载,<%=Creater%>" />
    <meta name="description" content="听语阁文学提供|<%=Creater%>写作的<%=BookName %>最新章节更新列表,无错误无广告的<%=BookName %>.喜欢看<%=Creater%>的小说<%=BookName %>就来听语阁文学吧" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--main_list..开始 -->
    <!--列表开始 -->
    <div class="book_news">
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
  <div class="book_news_style">
                        <div class="book_news_style_form1">
                        <h1></h1>


                    </div>
                            <div class="book_article_title" style=" font-size:24px;">
                             &nbsp; <%=BookName %> 
                                  </div>
                    <div class="book_article_texttable">
                      <%--  <div class="book_article_texttitle">
                            第一卷穿越潮流</div>--%>
                        <div class="book_article_listtext">
                            <dl id="chapterlist">
                            <asp:Repeater ID="docList" runat="server">
                            <ItemTemplate>
                            <dd> <%if (!IsCresteHTMLPage)
                  { %>
                <a href="ShowDoc.aspx?guid=<%#Eval("ID") %>" target="_blank" title="<%=BookName %> <%#Eval("章节名") %>">
                    <%#Eval("章节名") %>
                </a>
                <%}
                  else
                  {
                %>
                <a href="<%#Skybot.Cache.TabBookEnityExpandMethod. GetHTMLFilePath(((BookDoc)GetDataItem()),currentBook.分类标识,BookName) %>" target="_blank" title="<%=BookName %> <%#Eval("章节名") %>">
                    <%#Eval("章节名") %>
                </a>

                <%} %></dd></ItemTemplate>
                            </asp:Repeater>
                            </dl>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    


        <!--列表结束 -->
    </div>
    <!--main_list结束 -->
    <!-- Baidu Button BEGIN -->
<script type="text/javascript" id="bdshare_js" data="type=slide&amp;mini=1&amp;img=6&amp;uid=6440311" ></script>
<script type="text/javascript" id="bdshell_js"></script>
<script type="text/javascript">
    document.getElementById("bdshell_js").src = "http://share.baidu.com/static/js/shell_v2.js?cdnversion=" + new Date().getHours();
</script>
<!-- Baidu Button END -->
</asp:Content>


 