<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bookface.ascx.cs" Inherits="BookControls_bookface" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>



<div style="border:1px solid #BBB;height:30px;width:696px;line-height:30px;">
<div style="float:left;padding-left:10px;">当前：小说封面设置</div>
<div style="float:right;padding-right:10px;">
<asp:HyperLink runat="server" ID="face" Text="封面设置" ></asp:HyperLink>
<asp:HyperLink runat="server" ID="vs" Text="卷章管理" ></asp:HyperLink>
</div>
</div>

<div style="width:540px;display:table;height:250px;margin-left:20px;text-align:center;padding:10px;">
    
    <div style="display:table;width:543px;height:30px;">
        <div style="float:left;width:543px;margin-top:10px;text-align:left;padding-left:0px;">
            书本名称：<asp:TextBox runat="server" ID="TextBox1" Width="300px"></asp:TextBox> 
        </div>
    </div>
    
    <div style="display:table;width:543px;height:30px;">
        <div style="float:left;width:543px;margin-top:10px;text-align:left;padding-left:0px;">
            书本类型：<asp:DropDownList runat="server" ID="dBookType" Width="200px"></asp:DropDownList>
        </div>
    </div>

        <div style="display:table;width:543px;height:30px;">
        <div style="float:left;width:543px;margin-top:10px;text-align:left;padding-left:0px;">
            书本状态：<asp:DropDownList runat="server" ID="DropDownList1" Width="200px"></asp:DropDownList>
        </div>
    </div>
        <div style="display:table;width:543px;height:30px;">
        <div style="float:left;width:543px;margin-top:10px;text-align:left;padding-left:0px;">
            书本简介：<br /><br />
            <FCKeditorV2:FCKeditor runat="server" ID="fckAddbooks" Width="645px" Height="210px"></FCKeditorV2:FCKeditor>
        </div>
    </div>

    <div style="height:30px;;line-height:30px;text-align:left;width:643px;text-align:center;margin-top:10px;padding-left:10px;">
    <asp:ImageButton runat="server" ID="imgbtEdit" ImageUrl="~/Images/Submit.gif" 
            onclick="imgbtEdit_Click"/>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/Images/Reset.gif"/>
    </div>
    
</div>