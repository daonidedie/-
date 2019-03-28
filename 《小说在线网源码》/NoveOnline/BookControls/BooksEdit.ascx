<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BooksEdit.ascx.cs" Inherits="BookControls_BooksEdit"%>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>


<div style="width: 948px; display: table; height: 30px; text-align: center;
    line-height: 30px; background-color: #EBEBEB;
    border-bottom: 1px solid #BBB;font-weight:bold;">
    书本目录管理
</div>
<div style="width: 200px;height: 400px; text-align: center;
    border: 1px solid #CCCCCC; padding: 10px; float: left;overflow:auto;background-color:White;border-top:0px;border-left:0px;">
    <asp:Repeater runat="server" ID="repbooks">
        <ItemTemplate>
            <div style="float:left;width:190px;background-color:White;margin-top:10px;">
                <div><asp:Image runat="server" ImageUrl='<%# string.Format("~/Images/books/{0}",Eval("Images"))  %>' Width="130" Height="150"/></div>
                <div style="margin-top:3px;"><asp:HyperLink runat="server" id="hyp1" Text='<%# Eval("BookName") %>' NavigateUrl='<%# string.Format("~/AuthorIndex.aspx?type=2&ct=1&bookId={0}",Eval("BookId")) %>'></asp:HyperLink></div>
            </div>
        </ItemTemplate>
    
    </asp:Repeater>
</div>

<div style="width: 672px; display: table; height: 400px; margin-left: 0px; text-align: center;
padding: 10px; float: left; border-left: 0px;" id="divedit" runat="server">
    <div>
        
    </div>
    <div>
    </div>
</div>

