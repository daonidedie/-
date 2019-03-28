using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;

namespace BLL
{
    public class PropAccessBLL:IDAL.IProp
    {

        //获取数据访问层接口实例
        IProp IP = DalFactory.DateAccess.CreateIProp();

        //获取商品
        List<Model.PropInfo> IProp.getProps(int pageSize, int pageIndex, out int RecordCount)
        {
            return IP.getProps(pageSize, pageIndex, out RecordCount);
        }

        //获取热卖商品
        public List<Model.PropInfo> getHotProps()
        {
            return IP.getHotProps();
        }



        //获取一个商品信息
        public List<Model.PropInfo> getOnePropInfo(int propId)
        {
            return IP.getOnePropInfo(propId);
        }

        //后台商品管理
        public int updatePropInfo(Model.PropInfo prop)
        {
            return IP.updatePropInfo(prop);
        }
    }
}
