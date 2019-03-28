using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace WebControlExtension
{
    //扩展TextBox控件，位其增加一个Value属性，该属性主要存放ID等主键值
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:TextBoxEx runat=server></{0}:TextBoxEx>")]
    public class TextBoxEx : TextBox
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string IDValue
        {
            get
            {
                String s = (String)ViewState["value"];
                return ((s == null) ? "[" + string.Empty + "]" : s);
            }

            set
            {
                ViewState["value"] = value;
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(IDValue);
        }
    }

    //扩展Label添加Value属性
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:LabelEx runat=server></{0};LabelEx>")]
    public class LabelEx : Label
    {
        [Bindable(true)]
        [Category("appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Value
        {
            get { String s = (String)ViewState["value"]; return ((s == null) ? "[" + string.Empty + "]" : s); }
            set { ViewState["value"] = value; }
        }
        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(Value);
        }
    }

    //扩展LinkButton添加Value属性
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:LinkButtonEx runat=server></{0};LinkButtonEx>")]
    public class LinkButtonEx : LinkButton
    {
        [Bindable(true)]
        [Category("appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Value
        {
            get { String s = (String)ViewState["value"]; return ((s == null) ? "[" + string.Empty + "]" : s); }
            set { ViewState["value"] = value; }
        }
    }
}
