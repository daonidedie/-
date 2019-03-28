<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin_Index.aspx.cs" Inherits="Admin_Admin_Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>小说在线 - 后台管理</title>
    <link href="Ext3.2/resources/css/ext-all.css" rel="stylesheet" type="text/css" />
    <script src="Ext3.2/adapter/ext/ext-base.js" type="text/javascript"></script>
    <script src="Ext3.2/ext-all.js" type="text/javascript"></script>
    <script src="js_admin/Main.js" type="text/javascript"></script>

    <script src="JS/AddOrModifyNews.js" type="text/javascript"></script>
    <script src="JS/NovelNews.js" type="text/javascript"></script>
    <script src="js_admin/AdminIndex.js" type="text/javascript"></script>
    <script src="JS/EnterBook.js" type="text/javascript"></script>
    <script src="JS/AddBook.js" type="text/javascript"></script>
    <script src="JS/AddVolume.js" type="text/javascript"></script>
    <script src="JS/AddSections.js" type="text/javascript"></script>
    <script src="JS/ForbidUser.js" type="text/javascript"></script>
    <script src="JS/RnchainUser.js" type="text/javascript"></script>
    <script src="JS/CommendBooks.js" type="text/javascript"></script>
    <script src="JS/GetDelBookReplay.js" type="text/javascript"></script>
    <script src="JS/GetBookIdReplay.js" type="text/javascript"></script>
    <script src="JS/CheckAuthor.js" type="text/javascript"></script>
    <script src="JS/ModifyProp.js" type="text/javascript"></script>
    <script src="JS/VisitAuthor.js" type="text/javascript"></script>
    <script src="JS/ModifyOrAddVisitAuthor.js" type="text/javascript"></script>
    <script src="JS/getCheckSections.js" type="text/javascript"></script>
    <script src="FileUploadField.js" type="text/javascript"></script>
    <link href="file-upload.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var grid = new Ext.grid.GridPanel;
        //窗口加载
        window.onload = function () {
            //窗口最大化
            maximizeWindow();
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="north">
        <div id="logo"></div>
        <div id="splitter"></div>
    </div>
    </form>
</body>
</html>
