using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IDAL;
using System.Text;

public partial class Admin_Process_getPropInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IProp p = BllFactory.BllAccess.CreateIPropBLL();
            int recordCount;
            int recordIndex = Convert.ToInt32(Request["start"]);
            int pageSize = Convert.ToInt32(Request["limit"]);
            if (recordIndex == 0)
            {
                recordIndex = 0;
            }
            if (pageSize == 0)
            {
                pageSize = 10;
            }
            
            int pageIndex=recordIndex/pageSize+1;

            List<Model.PropInfo> list=p.getProps(pageSize, pageIndex, out recordCount);

            StringBuilder sb = new StringBuilder("{recordCount:" + recordCount + ",result:[");

            foreach (Model.PropInfo item in list)
            {
                sb.Append("{PropId:")
                    .Append(item.PropId)
                    .Append(",PropName:'")
                    .Append(item.PropName)
                    .Append("',PropIntroduction:'")
                    .Append(item.PropIntroduction)
                    .Append("',PropImage:'")
                    .Append(item.PropImage)
                    .Append("',PropPrice:'")
                    .Append(item.PropPrice)
                    .Append("',createDate:'")
                    .Append(item.createDate)
                    .Append("',outNumber:'")
                    .Append(item.outNumber)
                    .Append("'},");
            }
            sb.Remove(sb.Length - 1, 1).Append("]}");

            Response.Write(sb.ToString());
        }
    }
}