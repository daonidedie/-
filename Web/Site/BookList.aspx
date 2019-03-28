<%@ Page Title="" Language="C#" MasterPageFile="~/file/MasterPage.master" AutoEventWireup="true"
    CodeFile="BookList.aspx.cs" Inherits="file_BookList" %>
<%@ Import Namespace=" Skybot.Cache" %>
<%@ Import Namespace=" Skybot.Tong.CodeCharSet" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>
        <%=TypeName %>小说_好看的<%=TypeName %>小说_<%=DateTime.Now.ToString("yyyy") %><%=TypeName %>小说排行榜 
      听语阁文学 5200全文阅读,燃文 笔趣阁|免费小说网 ,八路中文网,侠客中文网</title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <meta name="keywords" content="武侠,言情,网游,玄幻,奇幻,免费小说" />
    <meta name="keywords" content="<%= string.Join(",", bookList.Select(p => p.书名).ToArray())%>" />
    <meta name="description" content="提供 <%= string.Join(",", bookList.Select(p => p.书名).ToArray())%> 小说在线阅读" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--main_list..开始 -->
    <!--列表开始 -->
    <div class="book_news">
        <div class="book_news_title title">
            <ul>
                <li>当前位置：<a href="/">听雨阁文学</a> &gt;
                    <%=TypeName %>
                </li>
            </ul>
        </div>
        <div class="book_news_style">
            <asp:Repeater ID="ShowNews" runat="server">
                <ItemTemplate>
                    <%if (!IsCresteHTMLPage)
                      { %>
                    <div class="book_news_style_form">
                        <div class="book_news_style_img">
                            <a title="<%#Eval("书名") %>" href="/Site/BookIndex.aspx?guid=<%#Eval("GUID") %>" target="_blank">
                                <img border="0" alt="<%#Eval("书名") %>" src="<%#ImgIsOK(Eval("配图").ToString(),((TygModel.书名表)GetDataItem())) %>" width="118" height="150" /></a></div>
                        <div class="book_news_style_text">
                            <h1>
                                <a href="/Site/BookIndex.aspx?guid=<%#Eval("GUID") %>" target="_blank">
                                    <%#Eval("书名") %>
                                </a>
                            </h1>
                            <h2>
                                作者：<a title="<%#Eval("作者名称")%>作品集" href="#"><%#Eval("作者名称")%></a>【<a href="/Site/BookList.aspx?lx=<%#(bool.Parse( Eval("完本")+""))?"-1":"-2"%><%=(IsCresteHTMLPage?"":string.Empty) %>"
                                    target="_blank"><%#(bool.Parse( Eval("完本")+""))?"完本":"连载"%></a>】&nbsp;<%#((DateTime)Eval("最后更新时间")).ToString("MM-dd HH:mm") %></h2>
                            <h3>
                                <%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("说明").ToString()),0,60)%>
                                <br />
                                
                                <a href="/Site/BookList.aspx?lx=<%#Eval("分类表ID") %><%=(IsCresteHTMLPage?"&html=true":string.Empty) %>"
                                    target="_blank">[<%#Eval("分类标识") %>]</a>
                            </h3>
                        </div>
                    </div>
                    <%}
                      else
                      {
                    %>
                    <div class="book_news_style_form">
                        <div class="book_news_style_img">
                            <a title="<%#Eval("书名") %>" href="<%#  ((TygModel.书名表)GetDataItem()).GetHTMLFilePath() %>"
                                target="_blank">
                                <img border="0" alt="<%#Eval("书名") %>" src="<%#ImgIsOK(Eval("配图").ToString(),((TygModel.书名表)GetDataItem())) %>" width="118" height="150" /></a></div>
                        <div class="book_news_style_text">
                            <h1>
                                <a href="<%#  ((TygModel.书名表)GetDataItem()).GetHTMLFilePath() %>" target="_blank">
                                    <%#Eval("书名") %>
                                </a>
                            </h1>
                            <h2>
                                作者：<a title="<%#Eval("作者名称")%>作品集" href="#"><%#Eval("作者名称")%></a>【<a href="/Site/BookList.aspx?lx=<%#(bool.Parse( Eval("完本")+""))?"-1":"-2"%><%=(IsCresteHTMLPage?"":string.Empty) %>"
                                    target="_blank"><%#(bool.Parse( Eval("完本")+""))?"完本":"连载"%></a>】&nbsp;<%#((DateTime)Eval("最后更新时间")).ToString("MM-dd HH:mm") %></h2>
                            <h3>
                                <%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("说明").ToString()),0,60)%>
                                <br />
                                <a href="/Book/<%#Eval("分类标识").ToString().ToPingYing() %>/<%#Eval("分类表ID") %>/index.aspx"
                                    target="_blank">[<%#Eval("分类标识") %>]</a>
                            </h3>
                        </div>
                    </div>
                    <%} %>
                </ItemTemplate>
            </asp:Repeater>
            <div class="clear">
            </div>
        </div>
        <!--翻页开始 -->
        <div id="fanye">
            <div class="fanyetxt">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="20" HorizontalAlign="Center"
                     EnableUrlRewriting="true"
                    UrlPaging="True" OnPageChanged="dfsd" CssClass="paginator" CurrentPageButtonClass="cpb"
                    AlwaysShow="True" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页"
                    ShowCustomInfoSection="Left" ShowInputBox="Never" CustomInfoTextAlign="Left"
                    LayoutType="Table">
                </webdiyer:AspNetPager>
            </div>
        </div>
        <!--翻页结束 -->
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
