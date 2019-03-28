/******************************************************
* 文件名：JS_Prompt.cs
* 文件功能描述：设计WEB系统 下拉提示框 命名空间 Skybot.Tong
* 
* 创建标识：周渊 2006-11-18
* 
* 修改标识：周渊 2008-4-26
* 修改描述：按代码编写规范改写部分代码
* 
******************************************************/

using System;

namespace Skybot.Tong
{
    #region　设计WEB系统 下拉提示框  namespace Bocom.Project.Library

    /// <summary>
    /// //开始JS下拉框的类
    /// </summary>
    public class JsPrompt
    {
        TongUse tong = new TongUse();

        /// <summary>
        /// js文件
        /// </summary>
        string js = "";

        /// <summary>
        /// 字段的值 
        /// </summary>
        string baValue = "";

        /// <summary>
        /// 字段
        /// </summary>
        string[] baItems;

        /// <summary>
        /// 参数数组
        /// </summary>
        String[] jscode;

        /// <summary>
        /// 输出对应 自己定义下拉提示 的基本函数  
        /// </summary>
        public string Zone_JsContTxT
        {//body 加上onLond 事件 test();
            get
            {
                js = "";
                js += "var intIndex=0;arrList = new Array();\r\n";
                js += "function dearray(aa)//定义array\r\n";
                js += "{\r\n";
                js += "arrList = aa.split(',');\r\n";
                js += "intIndex = arrList.length;\r\n";
                js += "}\r\n";

                //js += Input_FroAjax(str) + "\r\n"; //输出JS函数


                js += "function init() {\r\n";
                js += "if (arrList.constructor!=Array){alert('smanPromptList初始化失败:第一个参数非数组!');return ;}\r\n";
                js += "arrList.sort( function(a, b) {\r\n";
                js += " if(a.length>b.length)return 1;\r\n";
                js += "else if(a.length==b.length)return a.localeCompare(b);\r\n";
                js += "else return -1;\r\n";
                js += " }\r\n";
                js += "); \r\n";
                js += "}\r\n";


                js += "function smanPromptList(arrList,objInputId){\r\n";
                js += " var objouter=document.getElementById('__smanDisp') //显示的DIV对象\r\n";
                js += "var objInput = document.getElementById(objInputId); //文本框对象\r\n";
                js += "var selectedIndex=-1;\r\n";
                js += " var intTmp; //循环用的:)\r\n";

                js += "if (objInput==null) {alert('smanPromptList初始化失败:没有找到\"'+objInputId+'\"文本框');return ;}\r\n";
                js += " //文本框失去焦点\r\n";
                js += " objInput.onblur=function(){\r\n";
                js += "objouter.style.display='none';\r\n";
                js += "}\r\n";
                js += "//文本框按键抬起\r\n";
                js += "objInput.onkeyup=checkKeyCode;\r\n";
                js += "//文本框得到焦点\r\n";
                js += "objInput.onfocus=checkAndShow;\r\n";
                js += "function checkKeyCode(evt){\r\n";
                js += "evt = evt || window.event;\r\n";
                js += "var ie = (document.all)? true:false\r\n";
                js += "if (ie){\r\n";
                js += " var keyCode=evt.keyCode\r\n";
                js += "if (keyCode==40||keyCode==38){ //下上\r\n";
                js += "var isUp=false\r\n";
                js += "if(keyCode==40) isUp=true ;\r\n";
                js += "chageSelection(isUp)\r\n";
                js += " }else if (keyCode==13){//回车\r\n";
                js += "outSelection(selectedIndex);\r\n";
                js += "}else{\r\n";
                js += "checkAndShow(evt)\r\n";
                js += " }\r\n";
                js += "}else{\r\n";
                js += "checkAndShow(evt);\r\n";
                js += " }\r\n";
                js += " divPosition(evt);\r\n";
                js += "}\r\n";

                js += " function checkAndShow(evt){\r\n";
                js += "var strInput = objInput.value\r\n";
                js += "if (strInput!=''){\r\n";
                js += "divPosition(evt);\r\n";
                js += "selectedIndex=-1;\r\n";
                js += "objouter.innerHTML ='';\r\n";
                js += "for (intTmp=0;intTmp<arrList.length;intTmp++){\r\n";
                js += " if (arrList[intTmp].substr(0, strInput.length).toUpperCase()==strInput.toUpperCase()){\r\n";
                js += "addOption(arrList[intTmp]);\r\n";
                js += "}\r\n";
                js += " }\r\n";
                js += "objouter.style.display='';\r\n";
                js += "}else{\r\n";
                js += "objouter.style.display='none';\r\n";
                js += "}\r\n";
                js += " function addOption(value){\r\n";
                js += "objouter.innerHTML +=\"<div onmouseover=\\\"this.className='sman_selectedStyle'\\\" onmouseout=\\\"this.className=''\\\" onmousedown=\\\"document.getElementById('\"+objInputId+\"').value='\" + value + \"'\\\">\" + value + \"</div>\" \r\n";
                js += " }\r\n";
                js += "}\r\n";
                js += "function chageSelection(isUp){\r\n";
                js += "if (objouter.style.display=='none'){\r\n";
                js += "objouter.style.display='';\r\n";
                js += "}else{\r\n";
                js += " if (isUp)\r\n";
                js += "selectedIndex++\r\n";
                js += "else\r\n";
                js += "selectedIndex--\r\n";
                js += "}\r\n";
                js += "var maxIndex = objouter.children.length-1;\r\n";
                js += " if (selectedIndex<0){selectedIndex=0}\r\n";
                js += "if (selectedIndex>maxIndex) {selectedIndex=maxIndex}\r\n";
                js += "for (intTmp=0;intTmp<=maxIndex;intTmp++){\r\n";

                js += "if (intTmp==selectedIndex){\r\n";
                js += "objouter.children[intTmp].className=\"sman_selectedStyle\";\r\n";
                js += "}else{\r\n";
                js += "objouter.children[intTmp].className='';\r\n";
                js += "}\r\n";
                js += "}\r\n";
                js += "}\r\n";
                js += "function outSelection(Index){\r\n";
                js += "objInput.value = objouter.children[Index].innerText;\r\n";
                js += "objouter.style.display='none';\r\n";
                js += "}\r\n";
                js += "function divPosition(evt){\r\n";
                js += "var left = 0;\r\n";
                js += "var top  = 0;\r\n";
                js += "var e = objInput;\r\n";
                js += "while (e.offsetParent){\r\n";
                js += "left += e.offsetLeft + (e.currentStyle?(parseInt(e.currentStyle.borderLeftWidth)).NaN0():0);\r\n";
                js += "top  += e.offsetTop  + (e.currentStyle?(parseInt(e.currentStyle.borderTopWidth)).NaN0():0);\r\n";
                js += "e = e.offsetParent;\r\n";
                js += "}\r\n";

                js += "left += e.offsetLeft + (e.currentStyle?(parseInt(e.currentStyle.borderLeftWidth)).NaN0():0);\r\n";
                js += "top  += e.offsetTop  + (e.currentStyle?(parseInt(e.currentStyle.borderTopWidth)).NaN0():0);\r\n";

                js += "objouter.style.top    = (top  + objInput.clientHeight) + 'px' ;\r\n";
                js += "objouter.style.left    = left + 'px' ; \r\n";
                js += "objouter.style.width= objInput.clientWidth;\r\n";
                js += "}\r\n";
                js += "}\r\n";
                js += "    eval(document.write(\"<div id='__smanDisp'  style='position:absolute;display:none;FILTER: alpha(opacity=85)progid:DXImageTransform.Microsoft.Shadow(Color=#999999,Direction=120,strength=4);BORDER-RIGHT: #999999 1px solid;BORDER-TOP: #eeeeee 1px solid;BORDER-LEFT: #eeeeee 1px solid;BORDER-BOTTOM: #999999 1px solid;background-color:#ffffff;font-size:12px;font-family: Georgia, Times New Roman, Times, serif;' onbulr> </div>\"));\r\n";

                js += "eval(document.write(\"<style>.sman_selectedStyle{ background-color:#ffffff;font-family: Georgia, Times New Roman, Times, serif;background-color:#FF99CC;color:#FFFFFF; font-size:12px;}</style>\")); \r\n";

                js += "function getAbsoluteHeight(ob){\r\n";
                js += "return ob.offsetHeight\r\n";
                js += "}\r\n";
                js += "function getAbsoluteWidth(ob){\r\n";
                js += "return ob.offsetWidth\r\n";
                js += "}\r\n";
                js += "function getAbsoluteLeft(ob){\r\n";
                js += "var mendingLeft = ob .offsetLeft;\r\n";
                js += "while( ob != null && ob.offsetParent != null && ob.offsetParent.tagName != 'BODY' ){\r\n";
                js += "mendingLeft += ob .offsetParent.offsetLeft;\r\n";
                js += " mendingOb = ob.offsetParent;\r\n";
                js += "}\r\n";
                js += " return mendingLeft ;\r\n";
                js += "}\r\n";
                js += "function getAbsoluteTop(ob){\r\n";
                js += " var mendingTop = ob.offsetTop;\r\n";
                js += "while( ob != null && ob.offsetParent != null && ob.offsetParent.tagName != 'BODY' ){\r\n";
                js += "mendingTop += ob .offsetParent.offsetTop;\r\n";
                js += "ob = ob .offsetParent;\r\n";
                js += "}\r\n";
                js += "return mendingTop ;\r\n";
                js += "}\r\n";
                js += "Number.prototype.NaN0 = function()\r\n";
                js += "{\r\n";
                js += "return isNaN(this)?0:this;\r\n";
                js += "}\r\n";
                //初始化对象
                js += "window.onload=function(){test()};//初始化自定义下拉列表函数\r\n";

                return js;
            }
        }

        /// <summary>
        /// 输出对应 自己定义下拉提示 的基本函数 完整的   js 数据 
        /// </summary>
        public string jscont
        {
            get
            {
                string putOutText;//输出东东
                putOutText = "<!--输出下拉框代码开始-->\r\n";
                putOutText += "<script language=\"javascript\" type=\"text/javascript\" >\r\n";
                //去掉onLond事件 在 主要函数里加
                putOutText += Zone_JsContTxT.Replace("window.onload=function(){test()};//初始化自定义下拉列表函数\r\n", "") + "\r\n";//输出JS文件  jscont为基本函数  
                putOutText += "</script>\r\n<!--输出下拉框代码结束-->\r\n";

                return putOutText;
            }
        }

        /// <summary>
        /// 输出对应 文本框的 自己定义下拉提示
        /// </summary>
        /// <param name="str">传入文本参数</param>
        /// <![CDATA[ Pid=sfsd,2121,1343,454,dsfsd,aq,awqwq,safe,cxsdgf,ferew,tr,utyu,,uyuy,oo,gdfg,dsdf,,1,23,43,64,64574,756,7867,8087,07,5576,75&ccid=sfsd,2121,1343,454,dsfsd,aq,awqwq,safe,cxsdgf,ferew,tr,utyu,,uyuy,oo,gdfg,dsdf,,1,23,43,64,64574,756,7867,8087,07,5576,75
        /// ]]>
        public String Input_prompt(string str)
        {//body 加上onLond 事件 test();
            string putOutText;//输出东东

            putOutText = "<!--输出下拉框代码开始-->\r\n";
            putOutText += "<script language=\"javascript\" type=\"text/javascript\" >\r\n";
            putOutText += Input_FroAjax(str) + Zone_JsContTxT + "\r\n"; //输出JS函数
            putOutText += "</script>\r\n<!--输出下拉框代码结束-->\r\n";

            return putOutText;
        }

        /// <summary>
        /// 返回东东 数组化后的js 代码 
        /// </summary>
        /// <param name="code">JS数组参数</param>
        /// <returns></returns>
        protected String JsArray(string code)
        {
            // 文件内容
            string cont = "";//内容
            jscode = code.Split('&');//以","开始分割
            if (jscode.Length > 0)
            {
                for (int i = 0; i < jscode.Length; i++)
                {
                    baItems = jscode[i].Split('=');//得到JS代码的Input字段
                    try
                    {
                        baValue = baItems[1].Substring(0, (baItems[1].Length - 1)); //得到 对应Input的字段的值 .Substring(0, (BaItems[1].Length - 1)) 减去最后的个" '"
                    }
                    catch
                    {
                        baValue = baItems[0];
                    }
                    cont += "dearray(\"" + baValue + "\");\r\n smanPromptList(arrList,\"" + baItems[0] + "\");\r\n";
                }
            }

            return cont;//返回JS数据代码
        }

        /// <summary>
        /// 传入文本框名和提示内容  输出对应 文本框的 自己定义下拉提示函数  如Pid=sfsd,2121 (按Shift+7) aa=sdf,sdfsdk,
        /// </summary>
        /// <param name="str">传入文本框名和提示内容</param>
        /// <returns></returns>
        public String Input_FroAjax(string str)
        {
            string MainJs = "";
            MainJs += "\r\n//TextBox主要的数组函数　在页面上使用//\r\n"; //ForAjax里使用 主函数 开始 test();
            MainJs += "function test()\r\n";
            MainJs += "{\r\n";
            MainJs += "init();\r\n";
            MainJs += " //开始定义东东数组\r\n";

            MainJs += JsArray(str) + "\r\n"; //输出JS函数
            MainJs += "}\r\n";
            //加入主函数,初始化
            MainJs += "window.onload=function(){test()};\r\n";//初始化自定义下拉列表函数
            MainJs += "//TextBox主要的数组函数　在页面上使用//\r\n"; //ForAjax里使用 主函数 结束

            return MainJs;
        }
    }

    #endregion
}


