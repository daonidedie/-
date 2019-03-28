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