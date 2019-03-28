using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using System.Reflection;
using IDAL;

namespace DalFactory
{
    public abstract class DateAccess
    {
        //获得数据访问层命名空间
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];


        //制造一个数据访问层的小说相关接口实例
        public static INovel CreateINovel()
        {
            string typeName = path + ".NovelAccess";
            INovel In = (INovel)Assembly.Load(path).CreateInstance(typeName);
            return In;
        }

        //制造一个数据访问层的本站用户相关接口实例 
        public static IUsers CreateIUsers()
        {
            string typeName = path + ".UsersAccess";
            IUsers IU = (IUsers)Assembly.Load(path).CreateInstance(typeName);
            return IU;
        }


        //制造一个数据访问层的商城相关接口实例
        public static IProp CreateIProp()
        {
            string typeName = path + ".PropAccess";
            IProp IP = (IProp)Assembly.Load(path).CreateInstance(typeName);
            return IP;
        }

        //制作一个作者专区数据访问层的接口实例
        public static IAuthor CreateIAuthor()
        {
            string typeName = path + ".AuthorAccess";
            IAuthor IA = (IAuthor)Assembly.Load(path).CreateInstance(typeName);
            return IA;
        }
    }
}
