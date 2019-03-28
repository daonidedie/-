<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"
    ViewStateMode="Disabled" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>小说在线</title>
    <script src="Js/tabs.js" type="text/javascript"></script>
    <script src="Js/Dialog.js" type="text/javascript"></script>
    <script src="Js/UserPanel.js" type="text/javascript"></script>
    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link href="Css/menu.css" rel="stylesheet" type="text/css" />
    <script src="Js/menu.js" type="text/javascript"></script>
    <link href="Css/logo.css" rel="stylesheet" type="text/css" />
    <link href="Css/Web.css" rel="stylesheet" type="text/css" />
    <link href="Css/tabs.css" rel="stylesheet" type="text/css" />
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
                    <uc:userpanel ID="userpanel" runat="server" />
                    <%
                        }                     
                    %>
                </div>
                <uc:header ID="header" runat="server" />
                <div id="boostype">
                    <uc:booktype ID="BookType" runat="server" />
                </div>
                <div id="serch">
                    <uc:Serch runat="server" ID="serchCt" />
                </div>
                <div id="right">
                    <uc:booksRemmend ID="booksremmend" runat="server" />
                </div>
                <div id="left" >
                    <div class="title" id="jstitle">
                        <div style="background-image:url(Images/title.jpg); background-repeat:no-repeat;text-align:left;color:White;padding-left:8px;" >商城热卖 </div>
                    </div>
                    <div style="height: 5px; background-image: url('Images/tab/boxbg05.gif')">
                    </div>
                    <uc:Ballot runat="server" ID="Ballot1" />
                </div>
                <div id="newbook">
                    <div class="title" style="width: 947px;" id="title">
                        <div style="background-image:url(Images/title1.jpg); background-repeat:no-repeat;text-align:left;color:White;padding-left:8px;text-indent:25px;" >新书一览</div>
                     </div>
                    <div style="height: 5px; background-image: url('Images/tab/boxbg05.gif')">
                    </div>
                    <uc:NewBooksIntroduce2 runat="server" ID="bkinf" />
                </div>
                <div id="guanggao">
                    <asp:Image runat="server" ID="imggg" ImageUrl="~/Images/logo/20110801125212.jpg"
                        Width="948px" />
                </div>
                <div id="clickBook">
                    <asp:UpdatePanel ID="u1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <uc:clickCount ID="ClickCount" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div id="booknews">
                    <div class="title" >
                        小说最新章节</div>
                    <div style="height: 5px; background-image: url('Images/tab/boxbg05.gif');">
                    </div>
                    <uc:NewSections runat="server" ID="newsections" />
                </div>
                <div id="ticket">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <uc:bookticket ID="bktk" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div id="visitAuthor">
                    <div class="title"  >
                        作者介绍</div>
                    <div style="height: 5px; background-image: url('Images/tab/boxbg05.gif')">
                    </div>
                    <uc:VisitAuthor runat="server" ID="VisitAuthor" />
                </div>
                <uc:foot ID="foot" runat="server" />
            </div>
            </form>
        </div>
    </center>
</body>
</html>
