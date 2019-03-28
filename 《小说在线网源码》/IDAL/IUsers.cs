using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDAL
{
    public interface IUsers
    {

        //提升管理员
        int AdministratorsUser(int userId);

        //解禁被禁用用户
        int RnchainUser(int userId);

        //查看所有被禁用用户
        List<Model.UsersInfo> GetRnchainUser();

        //获取所有用户类型
        List<Model.UserTypeInfo> getUserTypeInfo();


        //禁用用户
        int ForbidUser(int userId);

        //判断是否存在用户
        int havaUser(string username);

        //判断是否存在昵称
        int haveUserName(string username);

        //查看作者是否在审核中
        int IsCheckAuthor(int userid);

        //修改用户密码
        int changeUserPassword(int userid, string newpassword);

        //获取用户书币
        List<Model.UsersInfo> getBookMoney(int userId);

        //获取短消息发送者的信息
        List<Model.UsersInfo> getSendMessageUser(int userId);

        //用户登陆
        List<Model.UsersInfo> UserLogin(string username, string password);

        //获取最新作者专访
        List<Model.VisitAuthorInfo> getVisitAuthor();

        List<Model.VisitAuthorInfo> getVisitAuthorPics();

        //获得最新书评
        List<Model.BookReplayInfo> getBookReplay();

        //获取购物车商品
        List<Model.ShoppingCartInfo> getUserShopCart(string cartID,int pageIndex,int pageSize,out int recordCount);

        //重载getVisitAuthor
        List<Model.VisitAuthorInfo> getVisitAuthor(int userSex);

        //获取用户短消息
         List<Model.UserMessageInfo> getUserMessage(int pageIndex, int pageSize, int userId, out int RecordCount);

        //删除购物车商品
        int deleteProp(string cartId,int propId);

        //修改购物车商品数量
        int updateProp(string cartId, int propId, int propNumber);

        //购买商品
        void payProps(string cartId, int userId, int PropId, int propNumber, int propPrice);

        //获取用户所有道具
        List<Model.UserPropInfo> getUserHaveProp(int userId, int pageIndex, int pageSize, out int recordCount);

        //会员注册
        int userLogin2(string AccountNumber, string AccountPassword, string UserName, int UserSex, string UserBrithday, string UsetImage, string IdentityCardNumber, int ProvinceId, int AreaId, string userAddress);

        //作者申请卡
        int AuthorCard(int userId);

        //鲜花使用
        int UserFlowerCard(int userid, int bookid);

        //书票使用
        int userBookTickedCard(int userid, int bookid);

        //添加书评
        int addReplayBook(int userid, int bookid, string context);

        //删除用户短消息
        int delMsg(int delMsg);

        //获取所有作者专访
        List<Model.VisitAuthorInfo> getVisitAuthor(int pageIndex, int PageSize, out int recordCount);

        //添加修改作者专访
        int ModifyVisitAuthor(Model.VisitAuthorInfo visit, bool b);

        //删除作者专访
        int DeleteVisitAuthor(int visitId);

        //获取专访用户
        List<Model.UsersInfo> getVisitAuthorUser();

        //用户冲值
        int UserPayMoney(int userid, int Money);
    }
}
