<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuthorVisited.aspx.cs" Inherits="AuthorVisited" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/Web.css" rel="stylesheet" type="text/css" />
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link href="Css/menu.css" rel="stylesheet" type="text/css" />
    <script src="Js/menu.js" type="text/javascript"></script>
    <script src="Js/UserPanel.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="width:948px;height:auto;border:1px solid #f5f5f5;margin:0px;"><uc:userpanel ID="userpanel" runat="server" /></div>
        <div style="width:948px;height:auto; border:1px solid #f5f5f5;margin-top:0px;"><uc:header ID="header" runat="server" /></div>
        <div id="boostype"><uc:booktype id="BookType" runat="server"/></div>
        <div id="serch">
            搜索
        </div>
        <div style="width:948px;height:300px;border:1px solid #e8e8e8;">女性作者专区</div>
        <div style="width:948px;height:300px;border:1px solid #e8e8e8;margin:2px 0px;">男性作者专区</div>
        <div style="width:948px;height:100px;border:1px solid #e8e8e8;">图片分割</div>
        <div style="width:948px;height:350px;border:1px solid #e8e8e8;">作者最新动态</div>
        <div style="width:948px;height:100px;border:1px solid #e8e8e8;">footer</div>
    </div>
    </form>
</body>
</html>
