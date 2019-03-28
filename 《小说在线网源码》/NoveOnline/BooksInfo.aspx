<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BooksInfo.aspx.cs" Inherits="BooksInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>小说在线 - 书本目录</title>
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
           
            <div id="main">
                <div>
                    <%
                        if (User.Identity.Name != ""  && Session["NewUser"] != null)
                        {
                     %>
                            <uc:LoginUserPanel ID="loginInUserPanel" runat="server"/>
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
                    <uc:Serch runat="server" id="serchCt"/>
                </div>


        <div style="border:1px solid #CCCCCC;border-bottom:0px;height:140px;filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#FBFBFB',endColorStr='#FBFBFB');">
           
   
            <div style="font-size:24px;text-align:center; font-family:华文隶书; color:black;margin-bottom:20px;margin-top:30px;"><asp:Label ID="lbBookName" runat="server"></asp:Label></div>
            <div style="font-size:12px;text-align:center;margin-bottom:10px;"><asp:Label ID="lbUserName" runat="server"></asp:Label>　　<asp:Label ID="lbUpdateTime" runat="server"></asp:Label></div>
            <div style="font-size:12px;text-align:center;margin-top:10px;">
                【 
                <asp:LinkButton ID="hlTicket" runat="server" Text="给本书投书票" 
                    onclick="hlTicket_Click"></asp:LinkButton> 】
                【 <asp:LinkButton ID="hlFlower" runat="server" Text="给本书送鲜花" 
                    onclick="hlFlower_Click"></asp:LinkButton> 】
                【 <asp:LinkButton ID="hlBookcase" runat="server" Text="放入书架" 
                    onclick="hlBookcase_Click" ></asp:LinkButton> 】
                【 <asp:LinkButton ID="linkreadingbook" runat="server" Text="订阅该书" 
                    onclick="linkreadingbook_Click" ></asp:LinkButton> 】
            </div>
        </div>

               

        <div style="font-size:12px;width:950px;display:table;border-top:0px;">
            <asp:Repeater ID="repeater" runat="server" 
                onitemdatabound="repeater_ItemDataBound" 
                onitemcreated="repeater_ItemCreated">
                <ItemTemplate>
                    <div style="clear:both;"></div>

                    <div style="font-size:12px;font-family:宋体;margin-top:0px;margin-bottom:0px;border:1px solid #CCCCCC;display:table;margin-bottom:5px;background-color:#FAFAFA;width:948px;">
                       <div style="height:50px;line-height:50px;font-weight:bold;">
                            <uc:LabelEx ForeColor="red" ID="lbeNovelClassId" runat="server" Text='<%# Eval("VolumeId")%>' Value='<%# Eval("ValumeName") %>'></uc:LabelEx>
                       </div>
                    <div>
                    <asp:Repeater OnItemCreated="Abc" OnItemDataBound="Bcd" ID="repeaterNovelSectionsInfo" runat="server">
                        <ItemTemplate>
                        
                            <div style="width:225px;float:left;text-align:center;height:30px;line-height:30px;margin-left:7.7px;border:1px dotted #BBB;margin-bottom:10px;background-color:White;">
                                <asp:TextBox ID="TextBox1" runat="server" Visible="false" Text='<%# Eval("SectiuonId") %>'></asp:TextBox>
                                <asp:HyperLink ID="hyperLink" runat="server" Text='<%# string.Format("{0}",Eval("SectionTitle").ToString().Length > 16 ? Eval("SectionTitle").ToString().Substring(0,14) + "……" : Eval("SectionTitle") )  %>'></asp:HyperLink>
                            </div>
                            
                        </ItemTemplate>
                        <FooterTemplate>
                            <br />
                        </FooterTemplate>
                    </asp:Repeater>
                    </div>

                    </div>
                </ItemTemplate>
            </asp:Repeater>
            </div>
           
         <uc:foot runat="server" ID="ftvs"/>
    </div>
    </form>
    </div>
    </center>
</body>
</html>
