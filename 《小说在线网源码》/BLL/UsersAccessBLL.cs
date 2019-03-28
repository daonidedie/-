using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model;

namespace BLL
{
    class UsersAccessBLL : IDAL.IUsers
    {

        //创建一个数据访问层接口实例
        IUsers bll = DalFactory.DateAccess.CreateIUsers();

        //获取最新作者专访
        /*
         * 获取最新的作者专访，order by VisitId，top 2
         */
        public List<Model.VisitAuthorInfo> getVisitAuthor()
        {
            List<Model.VisitAuthorInfo> list = new List<Model.VisitAuthorInfo>();
            list.Add(bll.getVisitAuthor()[0]);
            list.Add(bll.getVisitAuthor()[1]);
            return list;
        }

        //获取作者2-7
        public List<Model.VisitAuthorInfo> getVisitAuthorPics()
        {
            List<Model.VisitAuthorInfo> list = new List<Model.VisitAuthorInfo>();
            List<Model.VisitAuthorInfo> temp = bll.getVisitAuthor();
            if (temp.Count > 6)
            {
                list.Add(temp[1]);
                list.Add(temp[2]);
                list.Add(temp[3]);
                list.Add(temp[4]);
                list.Add(temp[5]);
                list.Add(temp[6]);
            }
            return list;
        }


        //用户登陆
        public List<Model.UsersInfo> UserLogin(string username, string password)
        {
            return bll.UserLogin(username, password);
        }



        //获得书评
        public List<Model.BookReplayInfo> getBookReplay()
        {
            return bll.getBookReplay();
        }

       
        //获得用户购物车
        public List<Model.ShoppingCartInfo> getUserShopCart(string cartID, int pageIndex, int pageSize, out int recordCount)
        {
            return bll.getUserShopCart(cartID,pageIndex,pageSize,out recordCount);
        }

        
        public List<Model.VisitAuthorInfo> getVisitAuthor(int userSex)
        {
            return bll.getVisitAuthor(userSex);
        }


      


        //获取短消息发送者信息
        public List<UsersInfo> getSendMessageUser(int userId)
        {
            return bll.getSendMessageUser(userId);
        }


        //获取用户短消息
        public List<UserMessageInfo>  getUserMessage(int pageIndex, int pageSize, int userId, out int RecordCount)
        {
            return bll.getUserMessage(pageIndex, pageSize,userId,out RecordCount);
        }

        //删除购物车内道具
        public int deleteProp(string cartId, int propId)
        {
            return bll.deleteProp(cartId, propId);
        }

           //修改购物车中物品数量
        public int updateProp(string cartId, int propId, int propNumber)
        {
            return bll.updateProp(cartId, propId, propNumber);
        }



        
        //获取用户书币
        public List<UsersInfo> getBookMoney(int userId)
        {
            return bll.getBookMoney(userId);
        }



        //购买商品
        public void payProps(string cartId, int userId, int PropId, int propNumber, int propPrice)
        {
            bll.payProps(cartId, userId, PropId, propNumber, propPrice);
        }


        //获取用户道具
        public List<UserPropInfo> getUserHaveProp(int userId, int pageIndex, int pageSize, out int recordCount)
        {
            return bll.getUserHaveProp(userId, pageIndex, pageSize, out recordCount);
        }

        //用户注册
        public int userLogin2(string AccountNumber, string AccountPassword, string UserName, int UserSex, string UserBrithday, string UsetImage, string IdentityCardNumber, int ProvinceId, int AreaId, string userAddress)
        {
            return bll.userLogin2(AccountNumber, AccountPassword, UserName, UserSex, UserBrithday, UsetImage, IdentityCardNumber, ProvinceId, AreaId, userAddress);
        }


        //修改用户密码
        public int changeUserPassword(int userid, string newpassword)
        {
            return bll.changeUserPassword(userid, newpassword);
        }
        
        //作者申请卡使用
        public int AuthorCard(int userId)
        {
            return bll.AuthorCard(userId);
        }

        //作者是否审核中
        public int IsCheckAuthor(int userid)
        {
            return bll.IsCheckAuthor(userid);
        }


        //鲜花使用
        public int UserFlowerCard(int userid, int bookid)
        {
            return bll.UserFlowerCard(userid, bookid);
        }

        //书票使用
        public int userBookTickedCard(int userid, int bookid)
        {
            return bll.userBookTickedCard(userid, bookid);       
        }



        //是否存在用户
        public int havaUser(string username)
        {
            return bll.havaUser(username);
        }

        //是否存在昵称
        public int haveUserName(string username)
        {
            return bll.haveUserName(username);
        }


        //添加书评
        public int addReplayBook(int userid, int bookid, string context)
        {
            return bll.addReplayBook(userid, bookid, context);
        }

        //删除用户短消息
        public int delMsg(int delMsg)
        {
            return bll.delMsg(delMsg);
        }


              //获取所有作者专访
        public List<VisitAuthorInfo> getVisitAuthor(int pageIndex, int PageSize, out int recordCount)
        {
            return bll.getVisitAuthor(pageIndex, PageSize, out recordCount);
        }

        //修改或添加作者专访
        public int ModifyVisitAuthor(VisitAuthorInfo visit, bool b)
        {
            return bll.ModifyVisitAuthor(visit, b);
        }

        //删除专访
        public int DeleteVisitAuthor(int visitId)
        {
            return bll.DeleteVisitAuthor(visitId);
        }


        public List<UsersInfo> getVisitAuthorUser()
        {
            return bll.getVisitAuthorUser();
        }

        //获取所有用户类型
        public List<UserTypeInfo> getUserTypeInfo()
        {
            return bll.getUserTypeInfo();
        }

        //禁用用户
        public int ForbidUser(int userId)
        {
            return bll.ForbidUser(userId);
        }
        //查看所有被禁用用户
        public List<UsersInfo> GetRnchainUser()
        {
            return bll.GetRnchainUser();
        }
        //解禁用户
        public int RnchainUser(int userId)
        {
            return bll.RnchainUser(userId);
        }
        //提升管理员
        public int AdministratorsUser(int userId)
        {
            return bll.AdministratorsUser(userId);
        }

        //用户冲值
        public int UserPayMoney(int userid, int Money)
        {
            return bll.UserPayMoney(userid, Money);
        }

    }
}
