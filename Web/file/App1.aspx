<%@ Page Language="C#" AutoEventWireup="true" CodeFile="App1.aspx.cs" Inherits="file_App1" %>
<%@ Import Namespace=" Skybot.Cache" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%= string.Join(",", list.Select(p => p.书名).ToArray())%>,最新章节-听雨阁文学</title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type"/>
    <meta name="baidu-site-verification" content="vkPuafOONSwVmWV6" />
    <meta name="keywords" content="武侠,言情,网游,玄幻,奇幻,免费小说" />
    <meta name="keywords" content="<%= string.Join(",", list.Select(p => p.书名).ToArray())%>" />
    <meta name="description" content="在线小说阅读网，好看的在线手机小说阅读，wap小说下载，移动书库提供言情小说，玄幻小说，武侠小说，军事小说，科幻小说,恐怖小说,灵异小说,热门小说，小说最新章节免费阅读。坚决抵制成人小说、情色小说、黄色小说等成人文学和色情小说" />
        <link rel="stylesheet" type="text/css" href="/file/default.css"/>
</head>
<body>
    <div id="container">
        <div id="book_header" class="header">
            <div id="book_topimg">
                <div id="book_topbg">
                    <div id="book_logo">
                    </div>
                    <div id="baidu_pro">
                    </div>
                </div>
            </div>
            <div id="book_menubg">
                <div id="book_menu">
                    <ul>
                        <li><a href="/">网站首页</a></li>
                        <li class="fenge"></li>
                        <asp:Repeater ID="Nav" runat="server">
                            <ItemTemplate>
                                <li><a href="/Site/BookList.aspx?lx=<%#Eval("ID") %>">
                                    <%#Eval("分类标识") %></a></li>
                                <li class="fenge"></li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <li><a href="/Site/BookList.aspx">排行榜</a></li>
                        <li class="fenge"></li>
                        <li><a href="/Site/BookList.aspx?lx=-1">全本小说</a></li>
                    </ul>
                </div>
            </div>
            <div class="search">
                <div class="w960 center">
                    <span class="search_l"></span><span class="search_r"></span>
                    <form name="formsearch" action="/Site/BookList.aspx">
                    <div class="form">
                        <h4>
                            搜索</h4>
                        <input id="keyword" class="search-keyword" name="keyword" type="text" /><select id="t"
                            class="search-option" name="t"><option selected="1" value="1">仅搜索书名</option>
                            <option value="2">仅搜索作者</option>
                        </select>
                        <button class="search-submit" type="submit">
                            搜索</button></div>
                    </form>
                    <!-- /form -->
                    <div class="tags">
                        <h4>
                            热门标签</h4>
                    </div>
                    <div class="sitemap">
                        <a onclick="javascript:window.external.AddFavorite('http://www.qlili.com', '听雨阁文学·锦衣夜行、校花的贴身高手')"
                            href="http://www.qlili.com/#">加入收藏</a><a onclick="this.style.behavior='url(#default#homepage)';this.setHomePage('http://www.qlili.com');"
                                href="http://www.qlili.com/#">设为首页</a></div>
                    <!-- /tags -->
                </div>
            </div>
        </div>
        <div style="margin-top: 5px;">
        </div>
        <div style="margin: 0px auto; border: 1px solid rgb(197, 197, 197); width: 888px;
            text-align: left;">
        </div>
        <div style="margin-bottom: 5px;">
        </div>
        <div id="main">
            <div id="book_main">
                <div class="book_news">
                    <div class="book_news_title title">
                        <ul>
                            <li></li>
                        </ul>
                    </div>
                    <div class="book_news_style">
                        
                        <asp:Repeater ID="TopBooks" runat="server">
                            <ItemTemplate>
            <%if (!IsCresteHTMLPage)
              { %>
              <div class="book_news_style_form">
                <div class="book_news_style_img">
                                <a title="<%#Eval("书名") %>" href="/Site/BookIndex.aspx?guid=<%#Eval("GUID") %>" target="_blank">
                                    <img border="0" alt="<%#Eval("书名") %>" src="<%#Eval("配图") %>" width="118" height="150" /></a></div>
                            <div class="book_news_style_text">
                                <h1>
                                    <a href="/Site/BookIndex.aspx?guid=<%#Eval("GUID") %>" target="_blank"><%#Eval("书名") %> </a>
                                </h1>
                                <h2>
                                    作者：<a title="<%#Eval("作者名称")%>作品集" href="#"><%#Eval("作者名称")%></a></h2>
                                <h3>
                               <%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("说明").ToString()),0,60)%></h3>
                            </div>
                        </div>
                <%}
              else
              {
                  %>
            <div class="book_news_style_form">
                <div class="book_news_style_img">
                                <a title="<%#Eval("书名") %>" href="<%#  ((TygModel.书名表)GetDataItem()).GetHTMLFilePath() %>" target="_blank">
                                    <img border="0" alt="<%#Eval("书名") %>" src="<%#Eval("配图") %>" width="118" height="150" /></a></div>
                            <div class="book_news_style_text">
                                <h1>
                                    <a href="<%#  ((TygModel.书名表)GetDataItem()).GetHTMLFilePath() %>" target="_blank"><%#Eval("书名") %> </a>
                                </h1>
                                <h2>
                                    作者：<a title="<%#Eval("作者名称")%>作品集" href="#"><%#Eval("作者名称")%></a></h2>
                                <h3>
                                 <%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("说明").ToString()),0,60)%></h3>
                            </div>
                        </div>

                <%} %>
                            
                        </ItemTemplate>
                        </asp:Repeater>
                        
                        <div class="clear">
                        </div>
                    </div>
                </div>
                <div class="listbox">
                    <dl class="tbox">
                        <dt><strong>玄幻奇幻小说</strong></dt>
                        <dd>
                            <ul class="d1 ico3">
                       <asp:Repeater ID="Docs1" runat="server">
                            <ItemTemplate>
            <%if (!IsCresteHTMLPage)
              { %>
                                <li>
                                    <span class="date"><%#((DateTime)Eval("创建时间")).ToString("yyyy-MM-dd")%></span>
                                    《<a title="<%#Eval("书名") %>" href="/Site/BookIndex.aspx?guid=<%#Eval("GUID") %>"  target="_blank"><%#Eval("书名") %></a>》 
                                    <a title="<%#Eval("章节名") %>" href="/Site/ShowDoc.aspx?guid=<%#Eval("本记录GUID") %>" target="_blank"><%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("章节名").ToString()),0,12)%> </a>
                                </li>
                                  <%}
              else
              {
                  %>  
                                <li>
                                    <span class="date"><%#((DateTime)Eval("创建时间")).ToString("yyyy-MM-dd") %></span>
                                    《<a title="<%#Eval("书名") %>" href="<%#  ((TygModel.文章表)GetDataItem()).书名表.GetHTMLFilePath() %>"  target="_blank"><%#Eval("书名") %></a>》 
                                    <a title="<%#Eval("章节名") %>" href="<%#  ((TygModel.文章表)GetDataItem()).GetHTMLFilePath() %>" target="_blank"><%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("章节名").ToString()),0,12)%> </a>
                                </li>
                  <%} %>
                            
                        </ItemTemplate>
                        </asp:Repeater>
                        
                            </ul>
                        </dd>
                    </dl>
                    <dl class="rbox">
                        <dt><strong>武侠仙侠小说</strong></dt>
                        <dd>
                            <ul class="d1 ico3">
                       <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
            <%if (!IsCresteHTMLPage)
              { %>
                                <li>
                                    <span class="date"><%#((DateTime)Eval("创建时间")).ToString("yyyy-MM-dd")%></span>
                                    《<a title="<%#Eval("书名") %>" href="/Site/BookIndex.aspx?guid=<%#Eval("GUID") %>"  target="_blank"><%#Eval("书名") %></a>》 
                                    <a title="<%#Eval("章节名") %>" href="/Site/ShowDoc.aspx?guid=<%#Eval("本记录GUID") %>" target="_blank"><%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("章节名").ToString()),0,12)%> </a>
                                </li>
                                  <%}
              else
              {
                  %>  
                                <li>
                                    <span class="date"><%#((DateTime)Eval("创建时间")).ToString("yyyy-MM-dd") %></span>
                                    《<a title="<%#Eval("书名") %>" href="<%#  ((TygModel.文章表)GetDataItem()).书名表.GetHTMLFilePath() %>"  target="_blank"><%#Eval("书名") %></a>》 
                                    <a title="<%#Eval("章节名") %>" href="<%#  ((TygModel.文章表)GetDataItem()).GetHTMLFilePath() %>" target="_blank"><%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("章节名").ToString()),0,12)%> </a>
                                </li>
                  <%} %>
                            
                        </ItemTemplate>
                        </asp:Repeater>
                        
                            </ul>
                        </dd>                    </dl>
                    <dl class="tbox">
                        <dt><strong>都市言情小说</strong></dt>
                        <dd>
                            <ul class="d1 ico3">
                       <asp:Repeater ID="Repeater2" runat="server">
                            <ItemTemplate>
            <%if (!IsCresteHTMLPage)
              { %>
                                <li>
                                    <span class="date"><%#((DateTime)Eval("创建时间")).ToString("yyyy-MM-dd")%></span>
                                    《<a title="<%#Eval("书名") %>" href="/Site/BookIndex.aspx?guid=<%#Eval("GUID") %>"  target="_blank"><%#Eval("书名") %></a>》 
                                    <a title="<%#Eval("章节名") %>" href="/Site/ShowDoc.aspx?guid=<%#Eval("本记录GUID") %>" target="_blank"><%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("章节名").ToString()),0,12)%> </a>
                                </li>
                                  <%}
              else
              {
                  %>  
                                <li>
                                    <span class="date"><%#((DateTime)Eval("创建时间")).ToString("yyyy-MM-dd") %></span>
                                    《<a title="<%#Eval("书名") %>" href="<%#  ((TygModel.文章表)GetDataItem()).书名表.GetHTMLFilePath() %>"  target="_blank"><%#Eval("书名") %></a>》 
                                    <a title="<%#Eval("章节名") %>" href="<%#  ((TygModel.文章表)GetDataItem()).GetHTMLFilePath() %>" target="_blank"><%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("章节名").ToString()),0,12)%> </a>
                                </li>
                  <%} %>
                            
                        </ItemTemplate>
                        </asp:Repeater>
                        
                            </ul>
                        </dd>                    </dl>
                    <dl class="rbox">
                        <dt><strong>历史军事小说</strong></dt>
                        <dd>
                            <ul class="d1 ico3">
                       <asp:Repeater ID="Repeater3" runat="server">
                            <ItemTemplate>
            <%if (!IsCresteHTMLPage)
              { %>
                                <li>
                                    <span class="date"><%#((DateTime)Eval("创建时间")).ToString("yyyy-MM-dd")%></span>
                                    《<a title="<%#Eval("书名") %>" href="/Site/BookIndex.aspx?guid=<%#Eval("GUID") %>"  target="_blank"><%#Eval("书名") %></a>》 
                                    <a title="<%#Eval("章节名") %>" href="/Site/ShowDoc.aspx?guid=<%#Eval("本记录GUID") %>" target="_blank"><%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("章节名").ToString()),0,12)%> </a>
                                </li>
                                  <%}
              else
              {
                  %>  
                                <li>
                                    <span class="date"><%#((DateTime)Eval("创建时间")).ToString("yyyy-MM-dd") %></span>
                                    《<a title="<%#Eval("书名") %>" href="<%#  ((TygModel.文章表)GetDataItem()).书名表.GetHTMLFilePath() %>"  target="_blank"><%#Eval("书名") %></a>》 
                                    <a title="<%#Eval("章节名") %>" href="<%#  ((TygModel.文章表)GetDataItem()).GetHTMLFilePath() %>" target="_blank"><%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("章节名").ToString()),0,12)%> </a>
                                </li>
                  <%} %>
                            
                        </ItemTemplate>
                        </asp:Repeater>
                        
                            </ul>
                        </dd>                    </dl>
                    <dl class="tbox">
                        <dt><strong>科幻灵异小说</strong></dt>
                        <dd>
                            <ul class="d1 ico3">
                       <asp:Repeater ID="Repeater4" runat="server">
                            <ItemTemplate>
            <%if (!IsCresteHTMLPage)
              { %>
                                <li>
                                    <span class="date"><%#((DateTime)Eval("创建时间")).ToString("yyyy-MM-dd")%></span>
                                    《<a title="<%#Eval("书名") %>" href="/Site/BookIndex.aspx?guid=<%#Eval("GUID") %>"  target="_blank"><%#Eval("书名") %></a>》 
                                    <a title="<%#Eval("章节名") %>" href="/Site/ShowDoc.aspx?guid=<%#Eval("本记录GUID") %>" target="_blank"><%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("章节名").ToString()),0,12)%> </a>
                                </li>
                                  <%}
              else
              {
                  %>  
                                <li>
                                    <span class="date"><%#((DateTime)Eval("创建时间")).ToString("yyyy-MM-dd") %></span>
                                    《<a title="<%#Eval("书名") %>" href="<%#  ((TygModel.文章表)GetDataItem()).书名表.GetHTMLFilePath() %>"  target="_blank"><%#Eval("书名") %></a>》 
                                    <a title="<%#Eval("章节名") %>" href="<%#  ((TygModel.文章表)GetDataItem()).GetHTMLFilePath() %>" target="_blank"><%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("章节名").ToString()),0,12)%> </a>
                                </li>
                  <%} %>
                            
                        </ItemTemplate>
                        </asp:Repeater>
                        
                            </ul>
                        </dd>                    </dl>
                    <dl class="rbox">
                        <dt><strong>网游竞技小说</strong></dt>
                        <dd>
                            <ul class="d1 ico3">
                       <asp:Repeater ID="Repeater5" runat="server">
                            <ItemTemplate>
            <%if (!IsCresteHTMLPage)
              { %>
                                <li>
                                    <span class="date"><%#((DateTime)Eval("创建时间")).ToString("yyyy-MM-dd")%></span>
                                    《<a title="<%#Eval("书名") %>" href="/Site/BookIndex.aspx?guid=<%#Eval("GUID") %>"  target="_blank"><%#Eval("书名") %></a>》 
                                    <a title="<%#Eval("章节名") %>" href="/Site/ShowDoc.aspx?guid=<%#Eval("本记录GUID") %>" target="_blank"><%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("章节名").ToString()),0,12)%> </a>
                                </li>
                                  <%}
              else
              {
                  %>  
                                <li>
                                    <span class="date"><%#((DateTime)Eval("创建时间")).ToString("yyyy-MM-dd") %></span>
                                    《<a title="<%#Eval("书名") %>" href="<%#  ((TygModel.文章表)GetDataItem()).书名表.GetHTMLFilePath() %>"  target="_blank"><%#Eval("书名") %></a>》 
                                    <a title="<%#Eval("章节名") %>" href="<%#  ((TygModel.文章表)GetDataItem()).GetHTMLFilePath() %>" target="_blank"><%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("章节名").ToString()),0,12)%> </a>
                                </li>
                  <%} %>
                            
                        </ItemTemplate>
                        </asp:Repeater>
                        
                            </ul>
                        </dd>                    </dl>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="flink w890 center clear">
            <dl class="linkbox">
                <dt><strong>友情链接：</strong></dt>
                <dd>
                    <ul class="f5">
<%--                        <li><a href="http://www.qlili.com/xkzw11117/" target="_blank">天圣</a></li>
                        <li><a href="http://www.qlili.com/xkzw10910/" target="_blank">大道修行者</a></li>
                        <li><a href="http://www.hwafa.com/" target="_blank">天才相师</a></li>
                        <li><a href="http://www.qlili.com/xkzw10909/" target="_blank">红色仕途</a></li>
                        <li><a href="http://www.qlili.com/xkzw10810/" target="_blank">仙河风暴</a></li>
                        <li><a href="http://www.qlili.com/xkzw9858/" target="_blank">洪荒之太上剑圣</a></li>
                        <li><a href="http://www.qlili.com/xkzw9794/" target="_blank">奸臣</a></li>
                        <li><a href="http://www.qlili.com/xkzw10267/" target="_blank">雄霸蛮荒</a></li>
                        <li><a href="http://www.qlili.com/xkzw9815/" target="_blank">一剑凌尘</a></li>
--%>                        <li><a href="http://www.baishuzhai.com/" target="_blank">官家</a></li>
                        <li><a href="http://www.niaoyan.net/" target="_blank">神印王座最新章节</a></li>
                        <li><a href="http://www.6yzw.com/" target="_blank">言情小说</a></li>
                        <li><a href="http://www.d5wx.com/" target="_blank">裁决</a></li>
                        <li><a href="http://www.llwx.net/" target="_blank">将夜</a></li>
                        <li><a href="http://www.35xs.com/" target="_blank">将夜</a></li>
                        <li><a href="http://www.niubb.net/" target="_blank">牛bb小说阅读网</a></li>
                        <li><a href="http://www.juexiang.com/" target="_blank">心情日志</a></li>
                        <li><a href="http://www.sj131.com/" target="_blank">穿越小说排行榜</a></li>
                        <li><a href="http://www.htzw.net/" target="_blank">宋时行</a></li>
                        <li><a href="http://www.bookzx.net/" target="_blank">小说者</a></li>
                        <li><a href="http://www.d2zw.com/" target="_blank">吞噬星空最新章节列表</a></li>
                        <li><a href="http://www.hxsk.net/" target="_blank">遮天</a></li>
                        <li><a href="http://www.xs8.com.cn/" target="_blank">言情小说吧</a></li>
                        <li><a href="http://www.suimeng.com/" target="_blank">神印王座</a></li>
                        <li><a href="http://www.51zw.net/" target="_blank">都市言情小说</a></li>
                        <li><a href="http://www.21zw.net/" target="_blank">21中文网</a></li>
                        <li><a href="http://www.yueshuba.com/" target="_blank">阅书吧</a></li>
                        <li><a href="http://www.kewaishu.net/" target="_blank">课外书阅读</a></li>
                        <li><a href="http://www.hqread.com/" target="_blank">虹桥书吧</a></li>
                        <li><a href="http://www.93wx.com/" target="_blank">神印王座</a></li>
                        <li><a href="http://www.92wx.org/" target="_blank">就爱文学</a></li>
                        <li><a href="http://www.tyxs.com/" target="_blank">天元小说</a></li>
                        <li><a href="http://www.114zw.com/" target="_blank">仙府之缘</a></li></ul>
                </dd>
            </dl>
        </div>
        <div class="footer w890 center mt1 clear">
            <p class="copyright">
            </p>
        </div>
    </div>
</body>
</html>
