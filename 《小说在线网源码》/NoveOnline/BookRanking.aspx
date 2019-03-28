<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookRanking.aspx.cs" Inherits="BookRanking" %>

<%@ Register Assembly="PageControls" Namespace="PageControls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>小说排行</title>
    <link href="Css/Web.css" rel="stylesheet" type="text/css" />
    <link href="Css/tabs.css" rel="stylesheet" type="text/css" />
    <script src="Js/tabs.js" type="text/javascript"></script>

    <script src="Js/Dialog.js" type="text/javascript"></script>
    <script src="Js/UserPanel.js" type="text/javascript"></script>

    <script src="Js/jquery.min.js" type="text/javascript"></script>
    <link href="Css/menu.css" rel="stylesheet" type="text/css" />
    <script src="Js/menu.js" type="text/javascript"></script>

    <link href="Css/logo.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function datavalid() {
            var keyCode = event.keyCode; //获得输入的ASC码
            if ((keyCode < 48 || keyCode > 57) && keyCode != 8) { //判断是否数字 (event.keyCode < 48 || event.keyCode > 57) && event.keyCode != 8
                event.returnValue = false; //事件返回值false,在textbox中的onkeydown="return datavalid();"来接收返回值
            }        
        }
    
    </script>
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

        <div style="border:1px solid #D8D8D8;width:948px;margin-top:10px;display:table;">
            <div class="title">热门章节排行榜</div>
            <div style="height: 5px; background-image: url('Images/tab/boxbg05.gif')"></div>
            <div style="float:left;margin-top:12px;margin-left:25px;font-weight:bold;">选择小说类型：</div>

            <asp:Repeater ID="repeaterType" runat="server">
                <HeaderTemplate><div style="width:30px;float:left;margin-top:12px;font-size:12px;font-weight:bold;"><asp:HyperLink ID="hlBookAllNumber" runat="server" Text="全部" NavigateUrl="~/BookRanking.aspx?typeId=0"></asp:HyperLink></div></HeaderTemplate>
                <ItemTemplate>
                    <div style="width:30px;float:left;margin-top:12px;font-size:12px;font-weight:bold;"><asp:HyperLink ID="hlTypeName" runat="server" NavigateUrl='<%# string.Format("~/BookRanking.aspx?typeId={0}",Eval("TypeId")) %>' Text='<%# Eval("TypeName") %>'></asp:HyperLink></div>
                </ItemTemplate>
            </asp:Repeater>

            <div style="clear:both; height:10px;"></div>
            <asp:Repeater ID="repeaterTable" runat="server">
                <HeaderTemplate>
                    <table id="tbTypeNumber" style="width:928px;margin-left:10px;" cellspacing="0">
                    <tr>
                        <th class="myth" style="width:100px;">小说名称</th><th class="myth">最新章节</th><th style="width:110px;" class="myth">作者</th><th style="width:110px;" class="myth">更新</th><th style="width:60px;" class="myth">类型</th><th style="width:80px;" class="myth">状态</th>
                    </tr>
                </HeaderTemplate>
                <FooterTemplate></table></FooterTemplate>
                <ItemTemplate>
                    <tr onmouseover='this.style.backgroundColor="#DDD";this.style.cursor="hand"' onmouseout='this.style.backgroundColor=""'>
                        <td id="bookName" style="border-bottom:1px dotted #BBB;height:25px;line-height:25px;"><%# Eval("BookName")%></td>
                        <td style="border-bottom:1px dotted #BBB;height:25px;line-height:25px;">
                        <asp:HyperLink runat="server" ID="hsh" Text='<%#  Eval("Sections")%>' NavigateUrl='<%# string.Format("~/BookSectionsInfo.aspx?BookId={0}&&SectiuonId={1}&&inspect=0",Eval("BookId"),Eval("SectiuonId")) %>' Target="_blank"></asp:HyperLink>
                        </td>
                        <td style="border-bottom:1px dotted #BBB;height:25px;line-height:25px;"><%# Eval("UserName")%></td>
                        <td style="border-bottom:1px dotted #BBB;height:25px;line-height:25px;"><%# Eval("UpdateTime")%></td>
                        <td style="border-bottom:1px dotted #BBB;height:25px;line-height:25px;"><%# Eval("TypeName")%></td>
                        <td style="border-bottom:1px dotted #BBB;height:25px;line-height:25px;"><%# Eval("State")%></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <div style="clear:both;width:948px; background-color:#F4F4F4;height:40px;line-height:40px;border-top:1px solid #CCCCCC;margin-top:10px;" id="pagediv" runat="server">
                        <asp:Label runat="server" ID="lblPage"></asp:Label>
                        <asp:HyperLink Text="首页" runat="server" ID="LinkFirst"></asp:HyperLink>
                        <asp:HyperLink Text="上一页" runat="server" ID="LinkPrece"></asp:HyperLink>
                        <asp:HyperLink Text="下一页" runat="server" ID="LinkNext"></asp:HyperLink>
                        <asp:HyperLink Text="末页" runat="server" ID="LinkLast"></asp:HyperLink>
                        <span> 第 </span><asp:TextBox runat="server" ID="TxpageIndex" style="width:50px;text-align:center;" onkeydown="return datavalid();"></asp:TextBox><span> 页 </span> 
                        <asp:LinkButton Text="跳转" runat="server" ID="linkgo" onclick="linkgo_Click"></asp:LinkButton>

             </div>
        </div>


        <%--            <div style="clear:both;border:1px solid #D8D8D8;width:948px;height:350px;">
            <div class="title">排行榜</div>
            <div style="height: 5px; background-image: url('Images/tab/boxbg05.gif')"></div>
            <div style="border:1px solid #D8D8D8;height:300px;width:300px;margin-top:10px;float:left;margin-left:10px;">
                <div class="title">点击量</div>
                <asp:Repeater ID="repeaterCountSum" runat="server">
                    <ItemTemplate>
                    <div style="float:right;height:15px;font-size:12px;text-align:right;padding-right:20px;border-bottom:1px dotted #D8D8D8;"><%# Eval("AllSumClick")%></div>
                        <div style="height:15px;border-bottom:1px dotted #D8D8D8;text-align:left;margin-bottom:10px;font-size:12px;padding-left:10px;">
                            <asp:HyperLink ID="hlBookName" runat="server" Text='<%# string.Format("[{0}]《{1}》{2}",Eval("TypeName"),Eval("BookName"),Eval("UserName"))  %>' NavigateUrl="~/HotRemBooks.aspx"></asp:HyperLink>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>


            <div style="border:1px solid #D8D8D8;height:300px;width:300px;margin-top:10px;float:left;margin-left:10px;">
                <div class="title">鲜花</div>
                <asp:Repeater ID="repeaterFlowerNumber" runat="server">
                    <ItemTemplate>
                    <div style="float:right;height:15px;font-size:12px;text-align:right;padding-right:20px;border-bottom:1px dotted #D8D8D8;"><%# Eval("AllSumClick")%></div>
                        <div style="height:15px;border-bottom:1px dotted #D8D8D8;text-align:left;margin-bottom:10px;font-size:12px;padding-left:10px;">
                            <asp:HyperLink ID="hlBookName" runat="server" Text='<%# string.Format("[{0}]《{1}》{2}",Eval("TypeName"),Eval("BookName"),Eval("UserName"))  %>' NavigateUrl="~/HotRemBooks.aspx"></asp:HyperLink>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div style="border:1px solid #D8D8D8;height:300px;width:300px;margin-top:10px;float:left;margin-left:10px;">
                <div class="title">书票</div>
                <asp:Repeater ID="repeaterTicketNumber" runat="server">
                    <ItemTemplate>
                    <div style="float:right;height:15px;font-size:12px;text-align:right;padding-right:20px;border-bottom:1px dotted #D8D8D8;"><%# Eval("AllSumClick")%></div>
                        <div style="height:15px;border-bottom:1px dotted #D8D8D8;text-align:left;margin-bottom:10px;font-size:12px;padding-left:10px;">
                            <asp:HyperLink ID="hlBookName" runat="server" Text='<%# string.Format("[{0}]《{1}》{2}",Eval("TypeName"),Eval("BookName"),Eval("UserName"))  %>' NavigateUrl="~/HotRemBooks.aspx"></asp:HyperLink>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>--%>

                </div>
                </form>
                </div>
</center>
</body>
</html>