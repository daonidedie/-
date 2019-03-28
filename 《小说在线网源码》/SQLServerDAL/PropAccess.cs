using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SQLServerDAL
{
    public class PropAccess :IDAL.IProp
    {
        //获取商品存储过程
        private const string PROC_GETPROPS = "getProps";

        //获取热卖商品
        private const string RPOC_GETHOTPROPS = "getHotProps";

        //后台商品管理
        private const string PROC_MODIFYPROP = "ModifyProp";


        //获取商品
        public List<Model.PropInfo> getProps(int pageSize,int pageIndex,out int RecordCount)
        {
            List<Model.PropInfo> list = new List<Model.PropInfo>();

            SqlParameter[] parm = new SqlParameter[] { 
            
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@pageIndex",pageIndex),
                new SqlParameter("@RecordCount",SqlDbType.Int,4)
            };

            parm[2].Direction = ParameterDirection.Output;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETPROPS, parm))
            {
                while (dr.Read())
                {
                    Model.PropInfo item = new Model.PropInfo();
                    item.createDate = dr["createDate"].ToString();
                    item.outNumber = Convert.ToInt32(dr["outNumber"]);
                    item.PropId = Convert.ToInt32(dr["PropId"]);
                    item.PropImage = dr["PropImage"].ToString();
                    item.PropIntroduction = dr["PropIntroduction"].ToString();
                    item.PropName = dr["PropName"].ToString();
                    item.PropPrice = Convert.ToDecimal(dr["PropPrice"]);
                    list.Add(item);
                }
            }

            RecordCount = Convert.ToInt32(parm[2].Value);
            return list;
        }

        //获取热卖商品
        public List<Model.PropInfo> getHotProps()
        {
            List<Model.PropInfo> list = new List<Model.PropInfo>();

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, RPOC_GETHOTPROPS, null))
            {
                while (dr.Read())
                {
                    Model.PropInfo item = new Model.PropInfo();
                    item.createDate = dr["createDate"].ToString();
                    item.outNumber = Convert.ToInt32(dr["outNumber"]);
                    item.PropId = Convert.ToInt32(dr["PropId"]);
                    item.PropImage = dr["PropImage"].ToString();
                    item.PropIntroduction = dr["PropIntroduction"].ToString();
                    item.PropName = dr["PropName"].ToString();
                    item.PropPrice = Convert.ToDecimal(dr["PropPrice"]);
                    list.Add(item);
                }
            }

            return list;
        }

        //获取一个商品信息
        public List<Model.PropInfo> getOnePropInfo(int propId)
        {
            SqlParameter parm = new SqlParameter("@prioID", propId);
            List<Model.PropInfo> list = new List<Model.PropInfo>();

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "getOnePropInfo", parm))
            {
                if (dr.Read())
                {
                    Model.PropInfo item = new Model.PropInfo();
                    item.createDate = dr["createDate"].ToString();
                    item.outNumber = Convert.ToInt32(dr["outNumber"]);
                    item.PropId = Convert.ToInt32(dr["PropId"]);
                    item.PropImage = dr["PropImage"].ToString();
                    item.PropIntroduction = dr["PropIntroduction"].ToString();
                    item.PropName = dr["PropName"].ToString();
                    item.PropPrice = Convert.ToDecimal(dr["PropPrice"]);
                    list.Add(item);
                }
            }
            return list;
        }


        //后台商品管理

        public int updatePropInfo(Model.PropInfo prop)
        {
            SqlParameter[] parms = new SqlParameter[]{
                new SqlParameter("@PropId ",prop.PropId),
                new SqlParameter("@PropName  ",prop.PropName),
                new SqlParameter("@PropPrice  ",prop.PropPrice),
                new SqlParameter("@PropIntroduction  ",prop.PropIntroduction),
                new SqlParameter("@CreateDate  ",prop.createDate),
                new SqlParameter("@OutNumber  ",prop.outNumber),
                new SqlParameter("@PropImage  ",prop.PropImage),
            };

            int i = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_MODIFYPROP, parms);
            return i;
        }

        
    }
}
