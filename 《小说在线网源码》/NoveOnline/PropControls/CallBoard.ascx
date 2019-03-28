<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CallBoard.ascx.cs" Inherits="PropControls_CallBoard" %>


<div class="seamless" style="height:170px;font-size:12px;overflow:hidden;width:240px;margin:1em auto;" id="obj" delay="35">

<asp:Repeater runat="server" ID="repCallBoard"> </asp:Repeater>
	<p><span>道具购买教程</span></p>
	<p style="text-align:left;"><span>1.请先登陆本站</span></p>
	<p style="text-align:left"><span>2.冲值书币，冲值链接在页面右上方。</span></p>
	<p style="text-align:left"><span>3.点击“加入购物车”按钮</span></p>
	<p style="text-align:left"><span>4.在购物车中确认购买数量</span></p>
    <p style="text-align:left"><span>5.付款，在“我的道具”使用已购道具。</span></p>
</div>


<script type="text/javascript">
    (function (c) {
        var obj = document.getElementsByTagName("div");
        var _l = obj.length;
        var o;
        for (var i = 0; i < _l; i++) {
            o = obj[i];
            var n2 = o.clientHeight;
            var n3 = o.scrollHeight;
            o.s = 0;
            if (o.className.indexOf(c) >= 0) {
                if (n3 <= n2) { return false; }
                var delay = parseInt(o.getAttribute("delay"), 10);
                if (isNaN(delay)) { delay = 100; }
                if (o.className.indexOf("allowStop") >= 0) {
                    o.onmouseover = function () { this.Stop = true; }
                    o.onmouseout = function () { this.Stop = false; }
                }
                giveInterval(autoRun, delay, o);
                //关键之处！！
                o.innerHTML = o.innerHTML + o.innerHTML;
            }
        }
        //注册函数
        function giveInterval(funcName, time) { var args = []; for (var i = 2; i < arguments.length; i++) { args.push(arguments[i]); } return window.setInterval(function () { funcName.apply(this, args); }, time); }
        function autoRun(o, s) {
            if (o.Stop == true) { return false; }
            var n1 = o.scrollTop;
            var n3 = o.scrollHeight;
            o.s++;
            o.scrollTop = o.s;
            if (n1 >= n3 / 2) {
                o.scrollTop = 0;
                o.s = 0;

            }
        }
    })('seamless')
</script>

