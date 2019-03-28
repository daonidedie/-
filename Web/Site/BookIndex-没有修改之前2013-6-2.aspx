<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookIndex-没有修改之前2013-6-2.aspx.cs" Inherits="Site_BookIndex" %>
<%@ Import Namespace=" Skybot.Cache" %>
<%@ Import Namespace=" Skybot.Tong.CodeCharSet" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
 <title>    <%=BookName %>章节列表  <%=BookName %>最新章节 ,<%=Creater%>, 听语阁文学 5200全文阅读,燃文 笔趣阁|免费小说网 ,八路中文网,侠客中文网</title>
<meta  http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="keywords" content=" <%=string.Join("",BookName) %>小说  <%=string.Join("",BookName) %>下载,<%=Creater%>" />
<meta name="description" content="听语阁文学提供|<%=Creater%>写作的<%=BookName %>最新章节更新列表,无错误无广告的<%=BookName %>.喜欢看<%=Creater%>的小说<%=BookName %>就来听语阁文学吧" />
 <style type="text/css">
  body{width:100%}
 
    </style>
</head>
<body>
    <div>
        <a  href="/">首页</a>&gt;&gt;
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
        <a href="<%=currentBook.GetHTMLFilePath() %>"target="_blank" title="<%=BookName %>>">
                  <%=BookName %>
                </a>
                <%} %>
    </div>
    <div id="List">
        <asp:DataList ID="docList" runat="server" CellPadding="4" ForeColor="#333333" ShowFooter="False"
            RepeatColumns="3" RepeatDirection="Horizontal">
            <AlternatingItemStyle BackColor="White" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderTemplate>
                <h1 style="text-align:center;">
                    <%=BookName %>  章节列表
                </h1>
                <div style="text-align:right;">
                最后更新：<%=LastTime%>
                </div>
            </HeaderTemplate>
            <ItemStyle BackColor="#EFF3FB" />
            <ItemTemplate>
            <%if (!IsCresteHTMLPage)
              { %>
                <a href="ShowDoc.aspx?guid=<%#Eval("本记录GUID") %>" target="_blank" title="<%=BookName %> <%#Eval("章节名") %>">
                    <%#Eval("章节名") %>
                </a>
                <%}
              else
              {
                  %>
             <a href="<%#((TygModel.文章表)GetDataItem()).GetHTMLFilePath() %>" target="_blank" title="<%=BookName %> <%#Eval("章节名") %>">
                    <%#Eval("章节名") %>
                </a>

                <%} %>
            </ItemTemplate>
            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:DataList>
    </div>
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
