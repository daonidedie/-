<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopCart.aspx.cs" Inherits="ShopCart"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


    <script src="Js/Dialog.js" type="text/javascript"></script>
    <script src="Js/UserPanel.js" type="text/javascript"></script>
<style type="text/css">
    body
    {
        font-size:12px;	
        background-color:#F4F4F4;
    }
    
    .rndnumber
    {
    	height:30px;
    	line-height:30px;
    	text-align:center;
    
    }

    .headerstyle
    {
    	background-color:#F4F4F4;
    }

    

</style>

<script type="text/javascript">
    
   

    function datavalied() {
        var keyCode = event.keyCode;
        if ((keyCode < 48 || keyCode > 57) && keyCode != 8 && keyCode != 13 && keyCode != 9) {
            event.returnValue = false;
        }
    }


    function haveMoney() {


        var txpropnumber1 = document.getElementById("repshopcart_txPropNumber_0");
        var txpropnumber2 = document.getElementById("repshopcart_txPropNumber_1");
        var txpropnumber3 = document.getElementById("repshopcart_txPropNumber_2");
        var txpropnumber4 = document.getElementById("repshopcart_txPropNumber_3");
        var txpropnumber5 = document.getElementById("repshopcart_txPropNumber_4");
        var txpropnumber6 = document.getElementById("repshopcart_txPropNumber_5");

        if (txpropnumber1 != null) {
            if (txpropnumber1.value < 1) {
                alert("购买数量有误！最少购买1个");
                return false;
            }
        }

        if (txpropnumber2 != null) {
            if (txpropnumber2.value < 1) {
                alert("购买数量有误！最少购买1个");
                return false;
            }
        }
        if (txpropnumber3 != null) {
            if (txpropnumber3.value < 1) {
                alert("购买数量有误！最少购买1个");
                return false;
            }
        }
        if (txpropnumber4 != null) {
            if (txpropnumber4.value < 1) {
                alert("购买数量有误！最少购买1个");
                return false;
            }
        }
        if (txpropnumber5 != null) {
            if (txpropnumber5.value < 1) {
                alert("购买数量有误！最少购买1个");
                return false;
            }
        }
        if (txpropnumber6 != null) {
            if (txpropnumber6.value < 1) {
                alert("购买数量有误！最少购买1个");
                return false;
            }
        }

        if (confirm("确认购买吗？")) {
            var payMoney = document.getElementById("lballprice").innerText; //需支付
            var haveMoney = document.getElementById("repallmoney_lblBookMoney_0").innerText; //剩余

            payMoney = payMoney.replace("需支付书币：", "");
            haveMoney = haveMoney.replace("剩余书币：", "");

            var intPayMoney = parseInt(payMoney);
            var intHaveMoney = parseInt(haveMoney);

            if (intPayMoney > intHaveMoney) {
                alert("书币不足以支付本次订单，请先冲值书币！");
                return false;
            }
            else {
                return true;
            }
        }
        else {
            return false;
        }
    }



</script>

</head>


<body id="bd">
    <form id="form1" runat="server">
    <div style="height:330px;border-left:1px solid #BBB;border-right:1px solid #BBB;width:542px;border-top:1px solid #BBB;filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#EBEBEB',endColorStr='#FFFFFF');">
        <asp:Repeater  runat="server" ID="repshopcart" 
            onitemcommand="repshopcart_ItemCommand" 
            onitemdatabound="repshopcart_ItemDataBound">
            <ItemTemplate>
            <div style="float:left;width:165px;height:150px;border:1px solid #BBB;margin-left:10px;margin-top:8px;background-color:White;">
            <div style="text-align:center;"><asp:Image ID="Image1" runat="server" ImageUrl='<%# string.Format(@"~/Images/prop/{0}",Eval("PropImage")) %>' Width="105" Height="105" /></div>
             <div style="text-align:center;margin-top:5px;">
             <asp:Label runat="server" ID="propName" Text='<%#  string.Format("{0}",Eval("PropName")) %>' ></asp:Label>
             &nbsp;&nbsp;<uc:LinkButtonEx ID="delbtn" runat="server" Text="移除" Value='<%# Eval("PropId") %>' Font-Underline="false" ForeColor="Black"  CommandName="delProp"  CommandArgument='<%# Eval("PropId") %>' OnClientClick='return confirm("确定要移除该道具吗？");'></uc:LinkButtonEx>
              <br />
               <asp:Label runat="server" ID="lblPrice" Text='<%#  string.Format("{0:f0}书币",Convert.ToDecimal(Eval("PropPrice")) * Convert.ToDecimal(Eval("PropNumber"))) %>' ></asp:Label>

               &nbsp;数量: <uc:TextBoxEx runat="server" ID="txPropNumber" Text='<%# Eval("PropNumber") %>' IDValue='<%# Eval("PropId") %>' 
               style="text-align:center;width:20px;height:12px;line-height:12px;margin-top:3px;" 
                OnTextChanged="txPropNumber_TextChanged" AutoPostBack="true" onkeydown="return datavalied();"></uc:TextBoxEx>

              
             </div>  
            </div>
                
            </ItemTemplate>
        </asp:Repeater>

        
    </div>
   
    
    <div style="height:30px;line-height:30px;background-color:#EBEBEB;border:1px solid #CCCCCC;text-align:center;filter: progid:DXImageTransform.Microsoft.Gradient(gradientType='0',startColorStr='#EBEBEB',endColorStr='#FAFAFA');">
            <asp:Repeater runat="server" ID="repallmoney">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# string.Format("用户名：{0}",Eval("UserName")) %>' ID="lbluserName"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" Text='<%# string.Format("剩余书币：{0:f0}",Eval("BookMoney")) %>' ID="lblBookMoney"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;
            </ItemTemplate>
            </asp:Repeater>
            
            <asp:Label runat="server" ID="lballprice"></asp:Label>
            [&nbsp;<uc:LinkButtonEx ID="enterPrice" runat="server" Text="支付书币" 
                onclick="enterPrice_Click" OnClientClick="return haveMoney()"  Font-Underline="false" style="Color:Red;"></uc:LinkButtonEx>&nbsp;]
    </div>
    </form>

    <div id="noprop" runat="server" visible="false" style="text-align:center;height:363px;background-color:White;width:544px;">
        <div style="padding:80px;">
            <asp:Image  runat="server" ImageUrl="~/Images/shopcart.jpg" Width="180px" Height="150px" /><br /><br />购物车中暂无商品！
            
        </div>
    </div>

    <script type="text/javascript">


    </script>

    </body>
</html>
