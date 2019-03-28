using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDAL
{
    public interface IProp
    {
        //获取所有商品
        List<Model.PropInfo> getProps(int pageSize, int pageIndex, out int RecordCount);

        //获取热卖商品
        List<Model.PropInfo> getHotProps();

        //获取一个商品信息
        List<Model.PropInfo> getOnePropInfo(int propId);

        //后台商品管理
        int updatePropInfo(Model.PropInfo prop);
    }
}
