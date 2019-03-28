<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SiteMap.aspx.cs" Inherits="file_SiteMap" %>
<%@ Import Namespace=" Skybot.Cache" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=DateTime.Now.ToString("yyyy-MM-dd") %>最新更新 听语阁文学 5200全文阅读,燃文 笔趣阁|免费小说网 ,八路中文网,侠客中文网</title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type"/>
</head>
<body>

    <div>
        <ul>
            <asp:Repeater ID="ShowDocs" runat="server">
                <ItemTemplate>
                      <%if (!IsCresteHTMLPage)
              { %>
                                <li>
                                    <span class="date"><%#((DateTime)Eval("创建时间")).ToString("yyyy-MM-dd")%></span>
                                    《<a title="<%#Eval("书名") %>" href="/Site/BookIndex.aspx?guid=<%#Eval("GUID") %>"  target="_blank"><%#Eval("书名") %></a>》 
                                    <a title="<%#Eval("章节名") %>" href="/Site/ShowDoc.aspx?guid=<%#Eval("本记录GUID") %>" target="_blank"><%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("章节名").ToString()),0,120)%> </a>
                                </li>
                                  <%}
              else
              {
                  %>  
                                <li>
                                    <span class="date"><%#((DateTime)Eval("创建时间")).ToString("yyyy-MM-dd") %></span>
                                    《<a title="<%#Eval("书名") %>" href="<%#  ((TygModel.文章表)GetDataItem()).书名表.GetHTMLFilePath() %>"  target="_blank"><%#Eval("书名") %></a>》 
                                    <a title="<%#Eval("章节名") %>" href="<%#  ((TygModel.文章表)GetDataItem()).GetHTMLFilePath() %>" target="_blank"><%#textData.ForMatText(textData.ForMatTextReplaceHTMLTags(  Eval("章节名").ToString()),0,120)%> </a>
                                </li>
                  <%} %>
                </ItemTemplate>
            </asp:Repeater>
           

        </ul>


<%--    <% foreach(var i in System.Linq.Enumerable.Range(1,113))
       { %>
siteMap<%=i %>.txt
       <%} %>--%>
    </div>

</body>
</html>
