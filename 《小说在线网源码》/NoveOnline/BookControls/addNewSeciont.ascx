<%@ Control Language="C#" AutoEventWireup="true" CodeFile="addNewSeciont.ascx.cs" Inherits="BookControls_addNewSeciont" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<div style="width: 948px;height: 30px; text-align: center;
    border-top: 1px solid #CCCCCC; line-height: 30px; margin-top: 5px; background-color: #EBEBEB;border-bottom: 1px solid #CCCCCC;
    font-weight:bold;">
    添加新章节
</div>
<div style="text-align:left;">
    <div style="margin-top:10px;padding-left:15px;">书本名称：<asp:DropDownList 
            ID="DropDownList2" runat="server" Width="220px" 
            onselectedindexchanged="DropDownList2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></div>
    <div style="margin-top:10px;padding-left:15px;">所属分卷：<asp:DropDownList ID="DropDownList1" runat="server" Width="220px" AutoPostBack="true"></asp:DropDownList></div>
    <div style="margin-top:10px;padding-left:15px;">章节名称：<asp:TextBox runat="server" id="sectionname" Width="400px"></asp:TextBox></div>
</div>
<div>
    <div style="margin-top:10px;padding-left:15px;text-align:left;">章节内容：</div>
    <div style="margin-top:10px;"><FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" Width="920px" Height="500px"></FCKeditorV2:FCKeditor></div>
    <div style="text-align:center;margin-top:5px;">
        <asp:ImageButton runat="server" ImageUrl="~/Images/Submit.gif" 
            onclick="Unnamed1_Click"/>
    </div>
</div>