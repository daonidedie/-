<%@ Control Language="C#" AutoEventWireup="true" CodeFile="serch.ascx.cs" Inherits="UserControls_serch" %>

<script type="text/javascript">

    function empfont(tx) {

        if (tx.value == "请输入您要搜索的关键字") {
            tx.value = "";
        }
    }

    function defaultfont(tx) {
        if (tx.value == "") {
            tx.value = "请输入您要搜索的关键字";
        }
    }

    function search() {
        var value = document.getElementById("serchCt_txSerch").value;
        var win = window.open("Search.aspx?search=" + encodeURI(value), "search");
        win.focus();

        return false;
    }

    function tbSearch_keydown() {
        if (event.keyCode == 13) {
            search();

            return false;
        }
    }
</script>

<div>
    <div style="margin-top:8px;text-align:left;padding-left:10px;width:506px;float:left;height:40px;">
        <asp:TextBox runat="server" ID="txSerch" Text="请输入您要搜索的关键字" Width="504px" style="height:21px;line-height:21px;font-size:12px;padding-top:2px;border:1px solid #B6AAA6;padding-left:2px;"
            onfocus="empfont(this);" onblur="defaultfont(this)" onkeydown = "return tbSearch_keydown()"
        ></asp:TextBox>
    </div>
    <div style="float:left;width:81px;height:35px;text-align:left;">
        <asp:ImageButton ImageUrl="~/Images/serch.png" runat="server" 
            style="margin-top:7px;height:28px;width:81px;" OnClientClick="return search()" />
    </div>
    <div style="float:left;width:320px;height:35px;text-align:left;line-height:35px;padding-top:4px;margin-left:15px;">关键字可以包含：作者名称，小说名称，小说类型</div>
</div>