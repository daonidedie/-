<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewSections.aspx.cs" Inherits="NewSections" %>

<%@ Register Assembly="PageControls" Namespace="PageControls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/Web.css" rel="stylesheet" type="text/css" />
    <link href="Css/tabs.css" rel="stylesheet" type="text/css" />
    <script src="Js/tabs.js" type="text/javascript"></script>
    
    <script src="Js/Dialog.js" type="text/javascript"></script>
    <script src="Js/UserPanel.js" type="text/javascript"></script>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link href="Css/menu.css" rel="stylesheet" type="text/css" />
    <script src="Js/menu.js" type="text/javascript"></script>
    <link href="Css/logo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <center>
        <div id="index">
            <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div id="main">
                <div>
                    <%
                        if (User.Identity.Name != "" && Session["NewUser"] != null)
                        {
                    %>
                    <uc:LoginUserPanel ID="loginInUserPanel" runat="server" />
                    <% 
                        }
                        else
                        {
                    %>
                    <uc:userpanel ID="userpanel1" runat="server" />
                    <%
                        }                     
                    %>
                </div>
                <uc:header ID="header" runat="server" />
                <div id="boostype">
                    <uc:booktype ID="BookType" runat="server" />
                </div>
                <div id="serch">
                    <uc:Serch runat="server" id="serchCt"/>
                </div>
                    <div style="width:180px;clear:both;float:left;text-align:left;border:1px solid #BBB;height:459px;">
                    <div class="title">
                    <div style="background-image:url(Images/title.jpg); background-repeat:no-repeat;text-align:left;color:White;padding-left:8px;" >小说分类 </div>
                    </div>
                    <div style="height: 5px; background-image: url('Images/tab/boxbg05.gif')"></div>
                        <uc:sectionType ID="ust" runat="server" />
                    </div>
                    

                    <div style="width:760px;float:right;border:1px solid #BBB;height:434px;border-bottom:0px;">
                    <div class="title">最新连载章节</div>
                    <div style="height: 5px; background-image: url('Images/tab/boxbg05.gif')"></div>
                     <uc:section ID="us" runat="server" />                       
                    </div>
                    <div style="float:right;height:24px;margin-bottom:5px;border:1px solid #BBB;width:760px;">
                    <cc1:PageControl ID="PageControl1" runat="server" ControlWidth="760px;" ControlID="gv1" TypeName="SQLServerDAL.NovelAccess" MethodName="getNewSectionsByType" ShowPage="10" PageSize="13" StyleNumber="2"/>
                    </div>
                    

                    <uc:foot runat="server" ID="ft"/>
                  
                
                </div>
                <div style="clear: both;"></div>
            </form>
        </div>
    </center>
</body>
</html>
