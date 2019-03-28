<%@ Control Language="C#" AutoEventWireup="true" CodeFile="addNewBook.ascx.cs" Inherits="BookControls_addNewBook" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<div style="width:948px;display:table;height:30px;text-align:center;line-height:30px;background-color:#EBEBEB;border-bottom:1px solid #BBB;font-weight:bold;bo">
    添加新书
</div>

<div style="width:740px;display:table;height:300px;margin-left:15px;text-align:center;padding:10px;">
    
    <div style="display:table;width:893px;height:30px;">
        <div style="float:left;width:840px;margin-top:10px;text-align:left;padding-left:0px;">
            书本名称：<asp:TextBox runat="server" ID="TextBox1" Width="300px"></asp:TextBox> 
        </div>
    </div>
    
    <div style="display:table;width:740px;height:30px;">
        <div style="float:left;width:740px;margin-top:10px;text-align:left;padding-left:0px;">
            书本类型：<asp:DropDownList runat="server" ID="dBookType" Width="200px"></asp:DropDownList>
        </div>
    </div>

        <div style="display:table;width:740px;height:30px;">
        <div style="float:left;width:740px;margin-top:10px;text-align:left;padding-left:0px;">
            书本封面：<asp:FileUpload runat="server" Width="400px"  ID="f1"/>
        </div>
    </div>


        <div style="display:table;width:740px;height:30px;">
        <div style="float:left;width:740px;margin-top:10px;text-align:left;padding-left:0px;">
            书本简介：<br /><br />
            <FCKeditorV2:FCKeditor runat="server" ID="fckAddbooks"></FCKeditorV2:FCKeditor>
        </div>
    </div>

    <div style="height:30px;;line-height:30px;text-align:left;width:700px;text-align:center;margin-top:10px;padding-left:10px;">
    
    <asp:ImageButton runat="server" ImageUrl="~/Images/Submit.gif" 
            onclick="Unnamed1_Click"/>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:ImageButton runat="server" ImageUrl="~/Images/Reset.gif"/>
    
    </div>
    
</div>