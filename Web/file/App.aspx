<%@ Page Title="" Language="C#" MasterPageFile="~/file/MasterPage.master" AutoEventWireup="true" CodeFile="App.aspx.cs" Inherits="file_App" %>
<%@ Import Namespace=" Skybot.Cache" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title><%= string.Join(",", list.Select(p => p.书名).ToArray())%>,最新章节-听雨阁文学  </title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type"/>
    <meta name="baidu-site-verification" content="vkPuafOONSwVmWV6" />
    <meta name="keywords" content="武侠,言情,网游,玄幻,奇幻,免费小说,虚无之外" />
    <meta name="keywords" content="<%= string.Join(",", list.Select(p => p.书名).ToArray())%>" />
    <meta name="description" content="在线小说阅读网，好看的在线手机小说阅读，wap小说下载，移动书库提供言情小说，玄幻小说，武侠小说，军事小说，科幻小说,恐怖小说,灵异小说,热门小说，小说最新章节免费阅读。" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
<!-- Baidu Button BEGIN -->
<script type="text/javascript" id="bdshare_js" data="type=slide&amp;mini=1&amp;img=6&amp;uid=6440311" ></script>
<script type="text/javascript" id="bdshell_js"></script>
<script type="text/javascript">
    document.getElementById("bdshell_js").src = "http://share.baidu.com/static/js/shell_v2.js?cdnversion=" + new Date().getHours();
</script>
<!-- Baidu Button END -->
</asp:Content>

