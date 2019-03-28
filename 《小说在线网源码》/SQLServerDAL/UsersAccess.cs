using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using Model;

namespace SQLServerDAL
{
    public class UsersAccess : IDAL.IUsers
    {

        //获取所有用户类型
        private const string PROC_GETUSERTYPE = "getUserType";

        //提升管理员
        private const string PROC_ADMINISTRATORSUSER = "AdministratorsUser";

        //解禁用户
        private const string PROC_RNCHAINUSER = "RnchainUser";

        //查看所有被禁用用户
        private const string PROC_GETRNCHAINUSER = "GetRnchainUser";

        //禁用用户
        private const string PROC_UPDATEUSER = "UpdateUser";
        //删除用户短消息
        private const string PROC_DELMSG = "delMsg";

        //添加书评论 
        private const string PROC_ADDBOOKREPLAY = "addReplayBook";

        //判断是否存在用户
        private const string PROC_HAVEUSER = "haveUser";

        //判断是否存在昵称
        private const string PROC_HAVEUSERNAME = "haveUserName";

        //作者申请卡使用
        private const string PROC_AUTHORCARD = "AuthorCard"; 

        //修改购物车中物品数量
        private const string PROC_UPDATEPROPNUMBER = "updateShopCartPropNumber";

        //用户登陆
        private const string PROC_USERLOGIN = "userLogin";

        //获取首页显示的作者专访
        private const string PROC_GETLASTVISITAUTHOR = "getLastAuthorVisit";

        //获取书评
        private const string PROC_GETBOOKREPLAY = "GetBookReplay";

        //获取用户购物车
        private const string PROC_GETUSERSHOPCART = "getUserShopCart";

        //删除购物车内道具
        private const string PROC_DELPROP = "delShopCartProp";

        //获取用户短消息
        private const string PEOC_GETUSERMESSAGE = "getUserMessage";

        //获取短消息发送者信息
        private const string PROC_GETSENDMESSAGEUSER = "getSendMessageUser";

        //购买商品
        private const string PROC_PAYPROPS = "payProps";

        //会员注册
        private const string PROC_USERLOGIN2 = "userLogin2";

        //修改密码
        private const string PROC_CHANGEUSERPASSWORD = "changeUserPassword";

        //获取所有作者专访
        private const string PROC_GETVISITAUTHOR = "getVisitAuthor";

        //添加作者专访 
        private const string PROC_ADDVISITAUTHOR = "addVisitAuthor";

        //修改作者专访
        private const string PROC_UPDATEVISITAUTHOR = "updateVisitAuthor";

        //删除作者专访
        private const string PROC_DELETEVISITAUTHOR = "deleteVisitAuthor";

        //获取专访作者
        private const string PROC_GETVISITAUTHORUSER = "getVisitAuthorUser";



        //获取最新作者专访
        /*
         * 获取最新的作者专访，order by VisitId desc，然后top 1
         */
        public List<Model.VisitAuthorInfo> getVisitAuthor()
        {
            List<Model.VisitAuthorInfo> VistList = new List<Model.VisitAuthorInfo>();

            SqlParameter parm = new SqlParameter("@userSex", -1);

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETLASTVISITAUTHOR, parm))
            {
                while (dr.Read())
                {
                    VisitAuthorInfo item = new VisitAuthorInfo();
                    item.UserId = Convert.ToInt32(dr["userId"]);
                    item.Contents = dr["contents"].ToString();
                    item.VisitDateString = Convert.ToDateTime(dr["visitDate"]).ToShortDateString();
                    item.VisitId = Convert.ToInt32(dr["visitID"]);
                    item.VisitTitle = dr["visitTitle"].ToString();
                    item.UserName = dr["UserName"].ToString();
                    item.UsetImage = dr["UsetImage"].ToString();
                    item.UserBrithdayString = Convert.ToDateTime(dr["UserBrithday"]).ToShortDateString();
                    item.UserSex = Convert.ToInt32(dr["UserSex"]);
                    item.userAddress = dr["userAddress"].ToString();
                    item.AuthorPhoto = dr["AuthorPhoto"].ToString();
                    VistList.Add(item);
                }
            }

            return VistList;
        }


        //用户登陆

        public List<Model.UsersInfo> UserLogin(string username, string password)
        {
            List<Model.UsersInfo> list = new List<UsersInfo>();
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@AccountNumber",username),
                new SqlParameter("@AccountPassword",password),
            };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_USERLOGIN, parm))
            {
                if (dr.Read())
                {
                    UsersInfo item = new UsersInfo();
                    item.UserName = dr["username"].ToString();
                    item.UserId = Convert.ToInt32(dr["userid"]);
                    item.UserType = Convert.ToInt32(dr["UserType"]);
                    item.AccountNumber = dr["AccountNumber"].ToString();

                    list.Add(item);
                }
            }
            return list;
        }


        //获得书评
        public List<BookReplayInfo> getBookReplay()
        {
            List<Model.BookReplayInfo> list = new List<Model.BookReplayInfo>();

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETBOOKREPLAY, null))
            {
                while (dr.Read())
                {
                    BookReplayInfo item = new BookReplayInfo();
                    item.BookName = dr["bookname"].ToString();
                    item.StateName = dr["StateName"].ToString();
                    item.TypeName = dr["TypeName"].ToString();
                    item.ReplayBookId = Convert.ToInt32(dr["ReplayBookId"]);
                    item.ReplayContext = dr["ReplayContext"].ToString();
                    item.ReplayId = Convert.ToInt32(dr["ReplayId"]);
                    item.ReplayTime = Convert.ToDateTime(dr["ReplayTime"]).ToShortDateString();
                    item.UserId = Convert.ToInt32(dr["UserId"]);
                    item.UserName = dr["UserName"].ToString();
                    item.Images = dr["images"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }





        //获取作者专访，2-7条
        public List<VisitAuthorInfo> getVisitAuthorPics()
        {
            List<Model.VisitAuthorInfo> VistList = new List<Model.VisitAuthorInfo>();

            SqlParameter parm = new SqlParameter("@UserSex", -1);

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETLASTVISITAUTHOR, parm))
            {
                while (dr.Read())
                {
                    VisitAuthorInfo item = new VisitAuthorInfo();
                    item.UserId = Convert.ToInt32(dr["userId"]);
                    item.Contents = dr["contents"].ToString();
                    item.VisitDateString = Convert.ToDateTime(dr["visitDate"]).ToShortDateString();
                    item.VisitId = Convert.ToInt32(dr["visitID"]);
                    item.VisitTitle = dr["visitTitle"].ToString();
                    item.UserName = dr["UserName"].ToString();
                    item.UsetImage = dr["UsetImage"].ToString();
                    item.UserBrithdayString = Convert.ToDateTime(dr["UserBrithday"]).ToShortDateString();
                    item.UserSex = Convert.ToInt32(dr["UserSex"]);
                    item.userAddress = dr["userAddress"].ToString();
                    item.AuthorPhoto = dr["AuthorPhoto"].ToString();
                    VistList.Add(item);
                }
            }
            return VistList;
        }

        //获取用户购物车
        public List<Model.ShoppingCartInfo> getUserShopCart(string cartID, int pageIndex, int pageSize, out int recordCount)
        {
            List<Model.ShoppingCartInfo> ShopList = new List<Model.ShoppingCartInfo>();

            SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@cartId",cartID),
                new SqlParameter("@pageIndex",pageIndex),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@recordCount",SqlDbType.Int,4)
            };

            parm[3].Direction = ParameterDirection.Output;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETUSERSHOPCART, parm))
            {
                while (dr.Read())
                {
                    Model.ShoppingCartInfo item = new ShoppingCartInfo();
                    item.PropId = Convert.ToInt32(dr["PropId"]);
                    item.Propnumber = Convert.ToInt32(dr["Propnumber"]);
                    item.createDate = dr["createDate"].ToString();
                    item.cartID = dr["cartID"].ToString();
                    item.PropName = dr["PropName"].ToString();
                    item.PropPrice = Convert.ToDecimal(dr["PropPrice"]);
                    item.PropImage = dr["PropImage"].ToString();

                    ShopList.Add(item);
                }
            }

            recordCount = Convert.ToInt32(parm[3].Value);
            return ShopList;
        }

        //获取所有作者专访
        public List<VisitAuthorInfo> getVisitAuthor(int userSex)
        {
            List<Model.VisitAuthorInfo> VistList = new List<Model.VisitAuthorInfo>();

            SqlParameter parm = new SqlParameter("@userSex", userSex);

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETLASTVISITAUTHOR, parm))
            {
                while (dr.Read())
                {
                    VisitAuthorInfo item = new VisitAuthorInfo();
                    item.UserId = Convert.ToInt32(dr["userId"]);
                    item.Contents = dr["contents"].ToString();
                    item.VisitDate = Convert.ToDateTime(dr["visitDate"]).ToShortDateString();
                    item.VisitId = Convert.ToInt32(dr["visitID"]);
                    item.VisitTitle = dr["visitTitle"].ToString();
                    item.UserName = dr["UserName"].ToString();
                    item.UsetImage = dr["UsetImage"].ToString();
                    item.UserBrithday = Convert.ToDateTime(dr["UserBrithday"]);
                    item.UserSex = Convert.ToInt32(dr["UserSex"]);
                    item.userAddress = dr["userAddress"].ToString();
                    item.AuthorPhoto = dr["AuthorPhoto"].ToString();
                    VistList.Add(item);
                }
            }
            return VistList;
        }


        //获取短消息发送者的信息
        public List<UsersInfo> getSendMessageUser(int userId)
        {
            List<UsersInfo> list = new List<UsersInfo>();
            SqlParameter parm = new SqlParameter("@SendUserId", userId);

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETSENDMESSAGEUSER, parm))
            {
                while (dr.Read())
                {
                    Model.UsersInfo item = new UsersInfo();
                    item.UserName = dr["UserName"].ToString();
                    list.Add(item);
                }
            }

            return list;
        }

        //获取用户短消息
        public List<UserMessageInfo> getUserMessage(int pageIndex, int pageSize, int userId, out int RecordCount)
        {
                        List<UserMessageInfo> list = new List<UserMessageInfo>();
            
            SqlParameter[] parm = new SqlParameter[]{
                 new SqlParameter("@reUserId",userId),
                 new SqlParameter("@pageIndex",pageIndex),
                 new SqlParameter("@pageSize",pageSize),
                 new SqlParameter("@RecordCount",SqlDbType.Int,4)
            };

            parm[3].Direction = ParameterDirection.Output;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PEOC_GETUSERMESSAGE, parm))
            {
                while (dr.Read())
                {
                    Model.UserMessageInfo item = new UserMessageInfo();
                    item.Contents = dr["Contents"].ToString();
                    item.MessageId = Convert.ToInt32(dr["MessageId"]);
                    item.MessageTitle = dr["MessageTitle"].ToString();
                    item.SendDate = Convert.ToDateTime(dr["SendDate"]).ToShortDateString();
                    item.SendUserId = Convert.ToInt32(dr["sendUserId"]);

                    List<UsersInfo> templist = getSendMessageUser(item.SendUserId);
                    item.SendUserName = templist[0].UserName;

                    list.Add(item);
                }
            }
            RecordCount = Convert.ToInt32(parm[3].Value);

            return list;
        }

        //删除购物车内物品
        public int deleteProp(string cartId, int propId)
        {

            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@cartId",cartId),
                new SqlParameter("@propId",propId)
            };

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_DELPROP, parm);
         
        }

        //修改购物车物品数量


        public int updateProp(string cartId, int propId, int propNumber)
        {
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@cartId",cartId),
                new SqlParameter("@propId",propId),
                new SqlParameter("@propNumber",propNumber)
            };

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_UPDATEPROPNUMBER, parm);
        }

        //获取用户书币和姓名
        public List<UsersInfo> getBookMoney(int userId)
        {
            SqlParameter parm = new SqlParameter("@userId", userId);
            List<Model.UsersInfo> list = new List<UsersInfo>();

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "getUserMoney", parm))
            {
                if (dr.Read())
                {
                    UsersInfo item = new UsersInfo();
                    item.UserName = dr["UserName"].ToString();
                    item.BookMoney = Convert.ToDecimal(dr["BookMoney"]);
                    list.Add(item);
                }
            }
            return list;
        }



        //购买商品
        public void payProps(string cartId, int userId, int PropId, int propNumber, int propPrice)
        {
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@propId",PropId),
                new SqlParameter("@UserId",userId),
                new SqlParameter("@propNumber",propNumber),
                new SqlParameter("@propPrice",propPrice),
                new SqlParameter("@cartID",cartId)
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_PAYPROPS, parm);
        }

        //获取用户道具
        public List<UserPropInfo> getUserHaveProp(int userId, int pageIndex, int pageSize, out int recordCount)
        {

            List<Model.UserPropInfo> list = new List<UserPropInfo>();

            SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@pageIndex",pageIndex),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@UserId",userId),
                new SqlParameter("@recordCount",SqlDbType.Int,4)
            };

            parm[3].Direction = ParameterDirection.Output;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "getUserProp", parm))
            {
                while (dr.Read())
                {
                    Model.UserPropInfo item = new UserPropInfo();
                    item.PropId = Convert.ToInt32(dr["PropId"]);
                    item.userID = Convert.ToInt32(dr["userID"]);
                    item.userPropNumber = Convert.ToInt32(dr["userPropNumber"]);
                    item.PropName = dr["PropName"].ToString();
                    item.PropIntroduction = dr["PropIntroduction"].ToString();
                    item.PropImage = dr["PropImage"].ToString();
                    item.PropPrice = Convert.ToDecimal(dr["PropPrice"]);
                    list.Add(item);
                }
            }

            recordCount = Convert.ToInt32(parm[3].Value);
            return list;
        }

        //用户注册
        public int userLogin2(string AccountNumber, string AccountPassword, string UserName, int UserSex, string UserBrithday, string UsetImage, string IdentityCardNumber, int ProvinceId, int AreaId, string userAddress)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@AccountNumber",AccountNumber),new SqlParameter("@AccountPassword",AccountPassword),new SqlParameter("@UserName",UserName),
                new SqlParameter("@UserSex",UserSex),new SqlParameter("@UserBrithday", UserBrithday),new SqlParameter("@UsetImage",UsetImage),new SqlParameter("@IdentityCardNumber",IdentityCardNumber),
                new SqlParameter("@userAddress",userAddress), 
                new SqlParameter("@ProvinceId",ProvinceId),new SqlParameter("@AreaId",AreaId)
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_USERLOGIN2, parm);
        }



       
        //修改用户密码
        public int changeUserPassword(int userid, string newpassword)
        {
            SqlParameter[] parm = new SqlParameter[] { 
            
                new SqlParameter("@userID",userid),
                new SqlParameter("@newPassword",newpassword)
            };

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_CHANGEUSERPASSWORD, parm);
        }



        //作者申请卡使用
        public int AuthorCard(int userId)
        {
            SqlParameter parm = new SqlParameter("@userID",userId);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_AUTHORCARD, parm);
        }

        //查看作者是否在审核中
        public int IsCheckAuthor(int userid)
        {
            SqlParameter parm = new SqlParameter("@UserId", userid);

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "ischeckauthor", parm))
            {
                if (dr.Read())
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            
        }



        //鲜花使用
        public int UserFlowerCard(int userid, int bookid)
        {
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@userID",userid),
                new SqlParameter("@bookID",bookid)
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "useFlower", parm);
        }

        //书票书用
        public int userBookTickedCard(int userid, int bookid)
        {
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@userID",userid),
                new SqlParameter("@bookID",bookid)
            };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "userBOOKCART", parm);
        }

        //判断是否存在用户
        public int havaUser(string username)
        {
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@username",username)
            };

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_HAVEUSER, parm))
            {
                if (dr.Read())
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }


        //判断是否存在昵称
        public int haveUserName(string username)
        {
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@username",username)
            };

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_HAVEUSERNAME, parm))
            {
                if (dr.Read())
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }



        //添加书评
        public int addReplayBook(int userid, int bookid, string context)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@userId",userid),
                new SqlParameter("@bookId",bookid),
                new SqlParameter("@replayContext",context)
            };

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_ADDBOOKREPLAY, parm);
        }

        //删除用户短消息
        public int delMsg(int delMsg)
        {
            SqlParameter parm = new SqlParameter("@delMsg", delMsg);

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_DELMSG, parm);
        }


        //获取所有作者专访
        public List<VisitAuthorInfo> getVisitAuthor(int pageIndex, int PageSize, out int recordCount)
        {
            List<VisitAuthorInfo> list = new List<VisitAuthorInfo>();
            SqlParameter[] parms = new SqlParameter[]{
                new SqlParameter("@pageIndex",pageIndex),
                new SqlParameter("@pageSize",PageSize),
                new SqlParameter("@recordCount",SqlDbType.Int,4)
            };
            parms[2].Direction = ParameterDirection.Output;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETVISITAUTHOR, parms))
            {
                while (dr.Read())
                {
                    Model.VisitAuthorInfo item = new VisitAuthorInfo();
                    item.VisitId = Convert.ToInt32(dr["VisitId"]);
                    item.UserName = dr["UserName"].ToString();
                    item.UserId = Convert.ToInt32(dr["UserId"]);
                    item.VisitTitle = dr["VisitTitle"].ToString();
                    item.Contents = dr["Contents"].ToString();
                    item.VisitDate = Convert.ToDateTime(dr["VisitDate"]).ToShortDateString();
                   
                    list.Add(item);
                }
                recordCount = Convert.ToInt32(parms[2].Value);
                return list;
            }


        }

        //修改或添加作者专访
        public int ModifyVisitAuthor(VisitAuthorInfo visit, bool b)
        {
            if (b)
            {
                SqlParameter[] parms = new SqlParameter[] { 
                new SqlParameter("@VisitId",visit.VisitId),
                new SqlParameter("@VisitTitle",visit.VisitTitle),
                new SqlParameter("@UserId",visit.UserId),
                new SqlParameter("@Contents",visit.Contents),
                new SqlParameter("@VisitDate",visit.VisitDate)

            };
                int i = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_UPDATEVISITAUTHOR, parms);
                return i;
            }
            else
            {
                SqlParameter[] parms = new SqlParameter[] { 
                new SqlParameter("@VisitTitle",visit.VisitTitle),
                new SqlParameter("@UserId",visit.UserId),
                new SqlParameter("@Contents",visit.Contents),
                new SqlParameter("@VisitDate",visit.VisitDate)
            };
                int i = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_ADDVISITAUTHOR, parms);
                return i;
            }
        }

        //删除专访
        public int DeleteVisitAuthor(int visitId)
        {
            SqlParameter parm = new SqlParameter("@VisitId", visitId);

            int i = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_DELETEVISITAUTHOR, parm);

            return i;
        }

        //获取专访作者
        public List<UsersInfo> getVisitAuthorUser()
        {
            List<Model.UsersInfo> list = new List<UsersInfo>();

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETVISITAUTHORUSER, null))
            {
                
                while (dr.Read())
                {
                    Model.UsersInfo user = new UsersInfo();
                    user.UserId = Convert.ToInt32(dr["UserId"]);
                    user.UserName = dr["UserName"].ToString();
                    list.Add(user);
                }
            }

            return list;

        }

        //禁用用户
        public int ForbidUser(int userId)
        {
            SqlParameter parm = new SqlParameter("@userId", userId);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_UPDATEUSER, parm);
        }
        //查看所有被禁用用户
        public List<UsersInfo> GetRnchainUser()
        {
            List<Model.UsersInfo> list = new List<UsersInfo>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETRNCHAINUSER, null))
            {
                while (dr.Read())
                {
                    Model.UsersInfo item = new UsersInfo();
                    item.UserId = Convert.ToInt32(dr["UserId"]);
                    item.UserName = dr["UserName"].ToString();
                    item.UserSex = Convert.ToInt32(dr["UserSex"]);
                    item.UserBrithday = Convert.ToDateTime(dr["UserBrithday"]);
                    item.BookMoney = Convert.ToDecimal(dr["BookMoney"]);
                    item.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                    item.UserTypeName = dr["UserTypeName"].ToString();
                    item.province = dr["province"].ToString();
                    item.city = dr["city"].ToString();

                    list.Add(item);
                }
            }
            return list;
        }
        //解禁用户
        public int RnchainUser(int userId)
        {
            SqlParameter parm = new SqlParameter("@userId", userId);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_RNCHAINUSER, parm);
        }
        //提升管理员
        public int AdministratorsUser(int userId)
        {
            SqlParameter parm = new SqlParameter("@userId", userId);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_ADMINISTRATORSUSER, parm);
        }


        //获取所有会员类型
        public List<UserTypeInfo> getUserTypeInfo()
        {
            List<Model.UserTypeInfo> list = new List<UserTypeInfo>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETUSERTYPE, null))
            {
                while (dr.Read())
                {
                    Model.UserTypeInfo item = new UserTypeInfo();
                    item.UserTypeId = Convert.ToInt32(dr["UserTypeId"]);
                    item.UserTypeName = dr["UserTypeName"].ToString();

                    list.Add(item);
                }
            }
            return list;
        }

        //用户冲值
        public int UserPayMoney(int userid, int Money)
        {
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@userId",userid),
                new SqlParameter("@money",Money)
            };

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction,CommandType.StoredProcedure,"UpdateUserMoney",parm);
        }

        
    }
}