using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Collections.Specialized;

namespace PageControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:PageControl runat=server></{0}:PageControl>")]
    public class PageControl : WebControl
    {
        public string ControlID
        {
            get 
            {
                object obj = ViewState["ControlID"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set
            {
                ViewState["ControlID"] = value;
            }
        }

        public string ControlWidth
        {
            get
            {
                object obj = ViewState["ControlWidth"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set
            {
                ViewState["ControlWidth"] = value;
            }
        }

        

        public string PControlID
        {
            get
            {
                object obj = ViewState["PControlID"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set
            {
                ViewState["PControlID"] = value;
            }
        }

        public string TypeName
        {
            get
            {
                object obj = ViewState["TypeName"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set
            {
                ViewState["TypeName"] = value;
            }
        }

        public string MethodName
        {
            get 
            {
                object obj = ViewState["MethodName"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set
            {
                ViewState["MethodName"] = value;
            }
        }

        public string PageSize
        {
            get
            {
                object obj = ViewState["PageSize"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set
            {
                ViewState["PageSize"] = value;
            }
        }

        public string StyleNumber
        {
            get
            {
                object obj = ViewState["StyleNumber"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set
            {
                ViewState["StyleNumber"] = value;
            }
        }

        public string ShowPage
        {
            get
            {
                object obj = ViewState["ShowPage"];
                return obj == null ? string.Empty : obj.ToString();
            }
            set
            {
                ViewState["ShowPage"] = value;
            }
        }

        public string BKColor
        {
            get 
            {
                object obj = ViewState["BkColor"];
                return obj == null ? "#F4F4F4" : obj.ToString();
            }
            set 
            {
                ViewState["BkColor"] = value;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }

        //查找该页面控件
        private void FindC(Control ctl,MethodInfo mi,Type type,object[] arr)
        {
            Control control = ctl.FindControl(ControlID);
            if(control!=null)
            {
                Type t=control.GetType();
                if (t != null)
                {
                    object obj = Activator.CreateInstance(type);
                    PropertyInfo pi = t.GetProperty("DataSource");
                    object ob = mi.Invoke(obj, arr);
                    pi.SetValue(control, ob, null);

                    t.GetMethod("DataBind").Invoke(control, null);
                    return;
                }
            }
            else
            {
                foreach(Control c in ctl.Controls)
                {
                    FindC(c,mi,type,arr);
                }
            }
        }

        //创建分页控件的样式
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            string w=ControlWidth;
            Panel p = new Panel();
            p.ID = "panel1";
            p.Style.Add(HtmlTextWriterStyle.Width,w);
            p.HorizontalAlign = HorizontalAlign.Center;
            p.Style.Add("height", "24px");
            p.Style.Add("line-height", "24px");
            p.Style.Add(HtmlTextWriterStyle.BackgroundColor, BKColor);
            p.Style.Add("Color","Black");
            if (StyleNumber == "")
            {
                StyleNumber = "1";
            }

            int result;
            if (int.TryParse(StyleNumber,out result))
            {   
                //无页码分页
                #region
                if (result==1)
                {
                    Label lbl = new Label();
                    lbl.ID = "Label1";

                    TextBox tb = new TextBox();
                    tb.Style.Add(HtmlTextWriterStyle.Width, "16px");
                    tb.Style.Add(HtmlTextWriterStyle.Height, "12px");
                    tb.ID = "tb1";

                    HyperLink linkFirst = new HyperLink();
                    linkFirst.ID = "linkFirst";
                    linkFirst.Text = "首页";

                    HyperLink linkNext = new HyperLink();
                    linkNext.ID = "linkNext";
                    linkNext.Text = "下一页";

                    HyperLink linkPrev = new HyperLink();
                    linkPrev.ID = "linkPrev";
                    linkPrev.Text = "上一页";

                    HyperLink linkLast = new HyperLink();
                    linkLast.ID = "linkLast";
                    linkLast.Text = "末页";

                    Button bt = new Button();
                    bt.ID = "button1";
                    bt.Click += new EventHandler(bt_Click);
                    bt.Text = "GO";

                    Literal literal1 = new Literal();
                    literal1.ID = "literal1";
                    literal1.Text = "&nbsp;";

                    Literal literal2 = new Literal();
                    literal1.ID = "literal2";
                    literal2.Text = "&nbsp;";

                    Literal literal3 = new Literal();
                    literal1.ID = "literal3";
                    literal3.Text = "&nbsp;";

                    Literal literal4 = new Literal();
                    literal4.ID = "literal4";
                    literal4.Text = "&nbsp;";

                    Literal literal5 = new Literal();
                    literal4.ID = "literal5";
                    literal5.Text = "&nbsp;";

                    p.Controls.Add(linkFirst);
                    p.Controls.Add(literal1);
                    p.Controls.Add(linkPrev);
                    p.Controls.Add(literal2);
                    p.Controls.Add(linkNext);
                    p.Controls.Add(literal3);
                    p.Controls.Add(linkLast);
                    p.Controls.Add(literal4);
                    p.Controls.Add(lbl);
                    p.Controls.Add(literal5);
                    p.Controls.Add(tb);
                    p.Controls.Add(bt);

                    this.Controls.Add(p);

                    AppDomain app = AppDomain.CurrentDomain;

                    HttpRequest request = HttpContext.Current.Request;
                    NameValueCollection queryString = request.QueryString;
                    foreach (Assembly ass in app.GetAssemblies())
                    {
                        Type type = ass.GetType(TypeName);
                        if (type != null)
                        {
                            MethodInfo mi = type.GetMethod(MethodName);

                            object[] arr = new object[mi.GetParameters().Length];
                            if (queryString["pageIndex"] == null)
                            {
                                arr[0] = 1;
                            }
                            else
                            {
                                arr[0] = Convert.ToInt32(queryString["pageIndex"]);
                            }

                            if (PageSize == "")
                            {
                                PageSize = "10";
                            }

                            arr[1] = Convert.ToInt32(PageSize);

                            for (int i = 2; i < mi.GetParameters().Length; i++)
                            {
                                arr[i] = Convert.ToInt32(queryString[mi.GetParameters()[i].Name]);
                            }

                            FindC(this.Page,mi,type,arr);
                            
                            int recordCount = Convert.ToInt32(arr[mi.GetParameters().Length - 1]);
                            int pageCount;

                            if (recordCount % Convert.ToInt32(PageSize) == 0)
                            {
                                    pageCount = recordCount / Convert.ToInt32(PageSize);
                            }
                            else
                            {
                                pageCount =((recordCount / Convert.ToInt32(PageSize))+1);
                            }
                            
                            lbl.Text = string.Format("第{0}页/共{1}页", arr[0], pageCount);

                            string newQueryString = "?";

                            for (int i = 0; i < queryString.Count; i++)
                            {
                                if (queryString.AllKeys[i].ToLower() != "pageindex")
                                {
                                    newQueryString += queryString.AllKeys[i] + "=" + HttpContext.Current.Server.UrlEncode(queryString[i]) + "&";
                                }
                            }

                            linkFirst.NavigateUrl = request.Url.AbsolutePath + newQueryString + "pageIndex=1";
                            linkLast.NavigateUrl = request.Url.AbsolutePath + newQueryString + "pageIndex=" + pageCount;
                            linkNext.NavigateUrl = request.Url.AbsolutePath + newQueryString + "pageIndex=" + (Convert.ToInt32(arr[0]) + 1);
                            linkPrev.NavigateUrl = request.Url.AbsolutePath + newQueryString + "pageIndex=" + (Convert.ToInt32(arr[0]) - 1);
                            
                            if (Convert.ToInt32(arr[0]) == 1)
                            {
                                linkFirst.Enabled = false;
                                linkPrev.Enabled = false;
                            }
                            if (Convert.ToInt32(arr[0]) == pageCount)
                            {
                                linkLast.Enabled = false;
                                linkNext.Enabled = false;
                            }
                            break;
                        }
                    }
                }
                #endregion

                //有页码分页
                #region
                if (result==2)
                {
                    Label lbl = new Label();
                    lbl.ID = "Label1";

                    AppDomain app = AppDomain.CurrentDomain;
                    HttpRequest request = HttpContext.Current.Request;
                    NameValueCollection queryString = request.QueryString;
                    foreach (Assembly ass in app.GetAssemblies())
                    {
                        Type type = ass.GetType(TypeName);
                        if (type != null)
                        {
                            object obj = Activator.CreateInstance(type);
                            MethodInfo mi = type.GetMethod(MethodName);

                            object[] arr=new object[mi.GetParameters().Length];

                            if (queryString["pageIndex"] == null)
                            {
                                arr[0] = 1;
                            }
                            else
                            {
                                arr[0] = Convert.ToInt32(queryString["pageIndex"]);
                            }
                            if (PageSize == "")
                            {
                                PageSize = "10";
                            }

                            arr[1] =Convert.ToInt32(PageSize);
                            for (int i = 2; i < mi.GetParameters().Length; i++)
                            {
                                arr[i] =Convert.ToInt32(queryString[mi.GetParameters()[i].Name]);
                            }

                            FindC(this.Page, mi, type, arr);

                            int recordCount = Convert.ToInt32(arr[mi.GetParameters().Length - 1]);
                            int pageCount;

                            if (recordCount % Convert.ToInt32(PageSize) == 0)
                            {
                                pageCount = recordCount / Convert.ToInt32(PageSize);
                            }
                            else
                            {
                                pageCount = ((recordCount / Convert.ToInt32(PageSize)) + 1);
                            }

                            lbl.Text = string.Format("第{0}页/共{1}页", arr[0], pageCount);

                            string newQueryString = "?";

                            for (int i = 0; i < queryString.Count; i++)
                            {
                                if (queryString.AllKeys[i].ToLower() != "pageindex")
                                {
                                    newQueryString += queryString.AllKeys[i] + "=" + HttpContext.Current.Server.UrlEncode(queryString[i]) + "&";
                                }
                            }

                            if(ShowPage=="")
                            {
                                ShowPage="10";
                            }

                            HyperLink linkF = new HyperLink();
                            linkF.Text = "首页";
                            linkF.NavigateUrl = request.Url.AbsolutePath + newQueryString + "pageIndex=1";

                            HyperLink linkP = new HyperLink();
                            linkP.Text = "上一页";

                            p.Controls.Add(linkF);

                            Literal literal3 = new Literal();
                            literal3.Text = "&nbsp;";

                            p.Controls.Add(literal3);

                            p.Controls.Add(linkP);

                            Literal literal5 = new Literal();
                            literal5.Text = "&nbsp;";

                            p.Controls.Add(literal5);

                            HyperLink linkN = new HyperLink();
                            linkN.Text = "下一页";
                           

                            HyperLink linkL = new HyperLink();
                            linkL.Text = "末页";
                            linkL.NavigateUrl=request.Url.AbsolutePath+newQueryString+"pageIndex="+pageCount;
                            int max = 0; 
                            int min = 0;


                            if (Convert.ToInt32(arr[0]) % Convert.ToInt32(ShowPage) == 1)
                            {
                                min = Convert.ToInt32(arr[0])/Convert.ToInt32(ShowPage)*Convert.ToInt32(ShowPage) + 1;
                                max = (Convert.ToInt32(arr[0]) / Convert.ToInt32(ShowPage) + 1) * Convert.ToInt32(ShowPage);
                            }
                            else if (Convert.ToInt32(arr[0]) % Convert.ToInt32(ShowPage) == 0)
                            {
                                min = (Convert.ToInt32(arr[0]) / Convert.ToInt32(ShowPage) - 1)*Convert.ToInt32(ShowPage) + 1;
                                max = Convert.ToInt32(arr[0]);
                            }
                            else
                            {
                                min = Convert.ToInt32(arr[0]) + 1;
                                max = (Convert.ToInt32(arr[0]) / Convert.ToInt32(ShowPage) + 1) * Convert.ToInt32(ShowPage);
                            }
                            if (max >= pageCount)
                            {
                                max = pageCount;
                            }

                            if (Convert.ToInt32(arr[0]) == 1)
                            {
                                linkF.Visible = false;
                                linkP.Visible = false;
                            }

                            if (Convert.ToInt32(arr[0]) == pageCount)
                            {
                                linkL.Visible = false;
                                linkN.Visible = false;
                            }



                            for (int i = min; i <= max; i++)
                            {
                                linkP.NavigateUrl = request.Url.AbsolutePath + newQueryString + "pageIndex=" + (Convert.ToInt32(arr[0]) - 1);
                                linkN.NavigateUrl = request.Url.AbsolutePath + newQueryString + "pageIndex=" + (Convert.ToInt32(arr[0]) + 1);

                                HyperLink link = new HyperLink();
                                link.Text = i.ToString();
                                link.NavigateUrl = request.Url.AbsolutePath + newQueryString + "pageIndex=" + i;
                                p.Controls.Add(link);

                                Literal literal1 = new Literal();
                                literal1.Text = "&nbsp;";
                                p.Controls.Add(literal1);
                            }


                            
                            p.Controls.Add(linkN);

                            Literal literal2 = new Literal();
                            literal2.Text = "&nbsp;";

                            p.Controls.Add(literal2);
                            p.Controls.Add(linkL);

                            Literal literal4 = new Literal();
                            literal4.Text = "&nbsp;";

                            p.Controls.Add(literal4);
                            p.Controls.Add(lbl);
                            this.Controls.Add(p);
                        }
                    }
                }
                #endregion
            }                
            else
            {
                HttpContext.Current.Response.Write("StyleNumber参数错误！");
            }
        }

        //点击事件
        void bt_Click(object sender, EventArgs e)
        {
           
        }
    }
}
