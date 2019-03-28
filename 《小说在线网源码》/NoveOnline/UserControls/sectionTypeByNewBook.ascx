<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sectionTypeByNewBook.ascx.cs" Inherits="UserControls_sectionTypeByNewBook" %>

<script type="text/javascript">

    function st(div) {
        div.style.backgroundColor = "#D6D6D6";
    }

    function st1(div) {
        div.style.backgroundColor = "";
    }

</script>

<asp:Repeater ID="rep1" runat="server" onitemdatabound="rep1_ItemDataBound">
    <ItemTemplate>
        <div id="abc" style="list-style-type:none;height:19px;line-height:19px;width:70px;float:left;margin-bottom:8px;margin-top:12px;margin-left:21px;" onmouseover="st(this);" onmouseout="st1(this);">
            <asp:HiddenField ID="hfTypeId" runat="server" Value='<%# Eval("TypeId") %>' />
            <asp:HyperLink ID="hlTypeId" runat="server" Text='<%# string.Format("{0}类",Eval("TypeName")) %>' NavigateUrl='<%# string.Format("~/BooksAllInfo.aspx?bookTypeId={0}",Eval("TypeId")) %>'></asp:HyperLink>
        </div>
    </ItemTemplate>
</asp:Repeater>
