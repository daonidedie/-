using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using IDAL;
using System.Reflection;


namespace BllFactory
{
    public abstract class BllAccess
    {
        //获得业务逻辑层命名空间(数据库类型)
        private static readonly string path = ConfigurationManager.AppSettings["WebBLL"];


        //获得一个小说相关的业务逻辑接口实例
        public static INovel CreateINovelBLL()
        {
            string typeName = path + ".NovelAccessBLL";
            INovel InBLL = (INovel)Assembly.Load(path).CreateInstance(typeName);
            return InBLL;
        }

        //获得一个用户相关的业务逻辑层接口实例
        public static IUsers CreateIUsersBLL()
        {
            string typeName = path + ".UsersAccessBLL";
            IUsers IuBll = (IUsers)Assembly.Load(path).CreateInstance(typeName);
            return IuBll;
        }

        //获得一个商城相关的业务逻辑层接口实例
        public static IProp CreateIPropBLL()
        {
            string typeName = path + ".PropAccessBLL";
            IProp IPBLL = (IProp)Assembly.Load(path).CreateInstance(typeName);
            return IPBLL;
        }

        //获得一个作者专区的业务逻辑层接口实例
        public static IAuthor CreateIAuthorBLL()
        {
            string typeName = path + ".AuthorAccessBLL";
            IAuthor IA = (IAuthor)Assembly.Load(path).CreateInstance(typeName);
            return IA;
        }
    }
}
