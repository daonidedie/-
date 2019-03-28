<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserBookShelf.aspx.cs" Inherits="UserBookShelf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/Web.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="width: 160px; height: 382px; display: table; float: left;margin-top:5px;">
            <asp:Repeater ID="RebookType" runat="server">
                <HeaderTemplate>
                    <div style="width: 140px; float: left; border: 1px solid #BBB; height: 25px; line-height: 25px;
                        margin-top: 5.8px; text-align: center; margin-left: 10px; filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='1',startColorStr='#EBEBEB',endColorStr='#FAFAFA');">
                        <a href='UserBookShelf.aspx?type=0' style="color: black;">全部小说</a>
                    </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div style="width: 140px; float: left; border: 1px solid #BBB; height: 25px; line-height: 25px;
                        margin-top: 5.8px; text-align: center; margin-left: 10px; filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='1',startColorStr='#EBEBEB',endColorStr='#FAFAFA');">
                        <a href='<%# string.Format("UserBookShelf.aspx?type={0}",Eval("TypeId")) %>' style="color: black;">
                            <%# Eval("TypeName")%>小说</a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div style="float: left; width: 400px; border: 1px solid #BBB; height: 418px; margin-top: 10.8px;filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#FAFAFA',endColorStr='#EBEBEB');" id="hasbooks" runat="server"> 
            <div style="height: 30px; line-height: 30px; border-bottom: 1px solid #BBB;filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='1',startColorStr='#EBEBEB',endColorStr='#FAFAFA');">
                <asp:Label ID="lblUserShelf" runat="server"></asp:Label>
                <asp:HyperLink Text="首页" NavigateUrl="#" runat="server" ID="FirstPage"></asp:HyperLink>
                <asp:HyperLink Text="上一页" NavigateUrl="#" runat="server" ID="PrvPage"></asp:HyperLink>
                <asp:HyperLink Text="下一页" NavigateUrl="#" runat="server" ID="NewxtPage"></asp:HyperLink>
                <asp:HyperLink Text="尾页" NavigateUrl="#" runat="server" ID="LastPage"></asp:HyperLink>
            </div>
            <div>
                <asp:Repeater runat="server" ID="repBooksSelf" 
                    onitemcommand="repBooksSelf_ItemCommand">
                    <ItemTemplate>
                    
                        <div style="width:178px;float:left;height:176px;margin-top:10px;margin-left:12px;">
                            <div style="margin-top:5px;"><asp:Image runat="server" ImageUrl='<%# string.Format("~/Images/Books/{0}",Eval("Images"))  %>' ID="imgs" Width="135px" Height="150px" /></div>
                             <div style="margin-top:5px;">
                                 <asp:HyperLink ID="hyd22" runat="server" Text='<%# Eval("BookName") %>' Target="_blank" NavigateUrl='<%# string.Format("~/BookCover.aspx?bookId={0}",Eval("BookId")) %>'></asp:HyperLink>
                                 <asp:LinkButton runat="server" ID="delshelfbook" Text="移除" CommandName="delbook" CommandArgument='<%#Eval("BookID")%>'></asp:LinkButton>
                             </div>
                        </div>

                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <div id="nobooks" runat="server" visible="false" style="float: left; width: 400px; border: 1px solid #BBB; height: 418px; margin-top: 10.8px;">
    <div style="margin-top:20px;">
        <asp:Image runat="server" ImageUrl="~/Images/nobook.JPG" Width="200px" Height="300px" />
    </div>
        <div style="margin-top:10px;">该类别没有图书</div>
    </div>
    </form>
</body>
</html>
     