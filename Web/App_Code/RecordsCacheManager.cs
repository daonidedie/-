using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using Skybot.Cache;

namespace Skybot.Cache
{
    using System.Linq;
    using ICSharpCode.SharpZipLib.Zip;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using Skybot.Tong.ZIP;
    using System.Timers;


    /// <summary>
    /// 记录缓存管理 单例
    /// 主要功能为更新数据,当数据无法写入到数据库时,记录到文件当中
    /// </summary>
    public class RecordsCacheManager
    {

        Timer timer = new Timer();

        private RecordsCacheManager()
        {
            //一小时执行一次
            //timer.Interval = 1000 * 3600;
            //timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        //时间到达后的事件
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //一周刚刚开始
            if (DateTime.Now.Hour == 0 && DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                //更新点击
                ClareBookWeekClick();
            }

            //写入缓存记录
            //更新记录到数据库中
            WriteCache();
        }




        #region 全局缓存数据
        /// <summary>
        /// 项缓存,用于所有项信息修改后都出现的东东
        /// </summary>
        internal System.Collections.Concurrent.ConcurrentDictionary<string, TabBookEntity> BooksCache = new ConcurrentDictionary<string, TabBookEntity>();

        /// <summary>
        /// 项缓存,用于所有分类的缓存 
        /// </summary>
        internal System.Collections.Concurrent.ConcurrentDictionary<decimal, TabClassEntity> ClassesCache = new ConcurrentDictionary<decimal, TabClassEntity>();


        #endregion


        #region 变量
        /// <summary>
        /// 数据库访问对像
        /// </summary>
        private TygModel.Entities tygdb = new TygModel.Entities();
        /// <summary>
        /// 数据库访问对像
        /// </summary>
        public TygModel.Entities Tygdb
        {
            get { return tygdb; }
        }

        #endregion




        /// <summary>
        /// 更新记录 
        /// </summary>
        /// <param name="recordType"></param>
        /// <param name="sign">标识 一般情况下为ID　也有可能是id</param>
        [System.ComponentModel.Composition.Export]
        public void UpdateRecord(RecordType recordType, string sign)
        {

            //得到枚举选项
            List<RecordType> result = EnumExtendMethod.GetEnumArray<RecordType>(recordType);

            decimal d;
            if (result.Count > 0)
            {
                switch (result[0])
                {
                    case RecordType.Page:
                        Func<TygModel.文章表, bool> fun = null;
                        //如果是数据ID
                        if (decimal.TryParse(sign, out d))
                        {
                            fun = p => p.ID == d;
                        }
                        else
                        {
                            fun = p => p.GUID == Guid.Parse(sign);
                        }

                        //得到对应的记录
                        var records = tygdb.文章表.Where(fun);

                        //初始化当前访问的页面
                        TabPageEntity page = null;

                        if (records.Count() > 0)
                        {
                            var record = records.FirstOrDefault();

                            #region 初始化页面对象数据
                            //如果当前缓存记录中存在这条记录
                            if (!BooksCache.ContainsKey(record.书名表.GUID.ToString()))
                            {
                                //页面所在的分类
                                TabClassEntity Typeclass = null;
                                //如果包括分类
                                if (ClassesCache.ContainsKey(record.书名表.分类表.ID))
                                {
                                    Typeclass = ClassesCache[record.书名表.分类表.ID];
                                }
                                else
                                {
                                    Typeclass = new TabClassEntity()
                                    {
                                        Modifyd = true,
                                    };
                                    Typeclass.Record = record.书名表.分类表;
                                    

                                    //添加到分类表
                                    ClassesCache.TryAdd(Typeclass.Record.ID, Typeclass);
                                }
                                //实例化书对像
                                TabBookEntity book = new TabBookEntity()
                                {
                                    Record = record.书名表,
                                    Typeclass = Typeclass,
                                    Modifyd = true,
                                };

                                //尝试添加到数据中
                                ClassesCache[record.书名表.分类表.ID].Books.TryAdd(record.书名表.GUID.ToString(), book);


                                //如果不包含
                                if (!book.Pages.ContainsKey(sign))
                                {
                                    page = new TabPageEntity()
                                    {
                                        Book = book,
                                        Issync = false,
                                        Modifyd = true,
                                        Record = record
                                    };

                                    //将页面添加到缓存集合
                                    book.Pages.TryAdd(record.本记录GUID.ToString(), page);
                                }
                                //添加到书的缓存集合
                                BooksCache.TryAdd(record.书名表.GUID.ToString(), book);
                            }
                            #endregion

                            //得到书本对象的数据
                            page = BooksCache[record.书名表.GUID.ToString()].Pages[record.本记录GUID.ToString()];
                            page.Record.总访问次数++;
                            page.Record.最后访问时间 = DateTime.Now;
                            page.Record.书名表.总点击++;
                            page.Record.书名表.周点击++;
                        }

                        break;

                    case RecordType.Book:

                        Func<TygModel.书名表, bool> bookfun = null;
                        //如果是数据ID
                        if (decimal.TryParse(sign, out d))
                        {
                            bookfun = p => p.ID == d;
                        }
                        else
                        {
                            bookfun = p => p.GUID == Guid.Parse(sign);
                        }
                        //得到对应的记录
                        var bookrecords = tygdb.书名表.Where(bookfun);
                        if (bookrecords.Count() > 0)
                        {
                            var record = bookrecords.FirstOrDefault();
                            #region 初始化页面对象数据
                            //如果当前缓存记录中存在这条记录
                            if (!BooksCache.ContainsKey(record.GUID.ToString()))
                            {
                                //页面所在的分类
                                TabClassEntity Typeclass = null;
                                //如果包括分类
                                if (ClassesCache.ContainsKey(record.分类表.ID))
                                {
                                    Typeclass = ClassesCache[record.分类表.ID];
                                }
                                else
                                {
                                    Typeclass = new TabClassEntity()
                                    {
                                        Modifyd = true,
                                    };
                                    Typeclass.Record = record.分类表;

                                    //添加到分类表
                                    ClassesCache.TryAdd(Typeclass.Record.ID, Typeclass);

                                }
                                //实例化书对像
                                TabBookEntity book = new TabBookEntity()
                                {
                                    Record = record,
                                    Typeclass = Typeclass,
                                    Modifyd = true,
                                };
                                BooksCache.TryAdd(record.GUID.ToString(), book);
                            }
                            //更新状态
                            BooksCache[record.GUID.ToString()].Modifyd = true;

                            #endregion
                            record.总点击++;
                            record.周点击++;
                        }


                        break;

                    case RecordType.ClassEntity:

                        break;

                }

            }

            #region 查看记录不是不和缓存数据中的记录相等　如果不相当则更新较旧的记录

            #endregion
        }


        /// <summary>
        /// 清空周点击
        /// </summary>
        public void ClareBookWeekClick()
        {

            //如果有过修改则需要重新写入
            //重得到一个复本,这样在下面的查询中就不会出现集合在别的位置修改后出错了
            var books = BooksCache.ToList();
            books.AsParallel().ForAll((o) =>
            {
                o.Value.Record.周点击 = 0;
                o.Value.Record.周鲜花 = 0;
            });

        }

        /// <summary>
        /// 更新页面记录
        /// </summary>
        /// <param name="page"></param>
        public void UpdateRecord(TabPageEntity page)
        {
            //如果文件不同步则
            if (!page.IsTabPageEntitySync())
            {
                try
                {
                    //将记录写入缓存
                    page.WriteCache();
                }
                //如果写入失败则过一会儿再写入
                catch
                {
                    System.Threading.Tasks.Task.Factory.StartNew(() => { System.Threading.Thread.Sleep(1); page.WriteCache(); });
                }
            }
        }
        /// <summary>
        /// 更新页面记录
        /// </summary>
        /// <param name="page"></param>
        public void UpdateRecord(TabBookEntity book)
        {
            //如果文件不同步则
            try
            {
                //将记录写入缓存
                book.WriteCache();
            }
            //如果写入失败则过一会儿再写入
            catch
            {
                System.Threading.Tasks.Task.Factory.StartNew(() => { System.Threading.Thread.Sleep(1); book.WriteCache(); });
            }

        }
     

        /// <summary>
        /// 写入页面缓存到文件
        /// </summary>
        /// <param name="bookClasses">所有大分类</param>
        public void WriteCache()
        {
            //更新所有的更改
            tygdb.SaveChanges();

            //得到当前大分类的复本
            var bookClasses = ClassesCache.ToList();
            //开始并行化写入文件
            bookClasses.ForEach((o) =>
            {
                //如果有过修改则需要重新写入
                //重得到一个复本,这样在下面的查询中就不会出现集合在别的位置修改后出错了
                var books = o.Value.Books.ToList();
                //得到所有需要修改的记录
                //指定在查询要执行大量非计算绑定工作（如文件 I/O）的情况下，最好指定比计算机上的核心数大的并行度。 这里指定的是2个核心

                var list = books.AsParallel().WithDegreeOfParallelism(2).Where(p => p.Value.Modifyd);
                //对搜索到需要更新的记录进行更新.
                list.ForAll((p) =>
                {
                    var pages = p.Value.Pages.ToList();
                    //一本书用一个线程
                    pages.Where(x => x.Value.Modifyd).ToList().ForEach((x) =>
                    {
                        //更新页面记录
                        UpdateRecord(x.Value);
                    });

                    //更新书本缓存记录
                   UpdateRecord( p.Value);
                });
            });
        }


        #region 用于外部访问的单例


        /// <summary>
        /// 单例实体
        /// </summary>
        private static readonly RecordsCacheManager _Instance = new RecordsCacheManager();
        /// <summary>
        /// 记录缓存管理对像访问单例
        /// </summary>
        public static RecordsCacheManager Instance
        {
            get { return RecordsCacheManager._Instance; }
        }
        #endregion

    }

}

namespace System.Linq
{
    /// <summary>
    /// 枚举应用扩展方法
    /// </summary>
    public class EnumExtendMethod
    {
        /// <summary>
        /// 得到当前枚举的描述
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <typeparam name="TResult">所用的属性类型</typeparam>
        /// <param name="EnumInstance">指定的枚举项</param>
        /// <returns>返回属性对像,如果没有找到指定的属性对像则引发异常</returns>
        public static TResult GetAttribuate<T, TResult>(T EnumInstance) where TResult : Attribute, new()
        {
            //声明一个变量,用于后面的处理
            TResult attr = null;
            System.Reflection.FieldInfo fieldInfo = typeof(T).GetField(EnumInstance.ToString());
            object[] attribs = fieldInfo.GetCustomAttributes(typeof(TResult), true);
            if (attribs.Count() > 0)
            {
                attr = attribs.FirstOrDefault() as TResult;
            }
            else
            {
                throw new KeyNotFoundException("在枚举上,没有找到指定类型的 属性");
            }
            return attr;
        }

        /// <summary>
        /// 传入枚举参数值,如果这个参数包括多个将返回枚举参数的数组
        /// </summary>
        /// <typeparam name="EnumType">要处理的枚举类型</typeparam>
        /// <param name="enumInstance">传入的枚举参数</param>
        /// <returns>如果这个参数包括多个将返回枚举参数的数组,单个将返回自身</returns>
        public static List<EnumType> GetEnumArray<EnumType>(EnumType enumInstance)
        {
            Type enumType = typeof(EnumType);

            if (enumType != enumInstance.GetType())
            {
                throw new NotSupportedException("参数类型于指定类型不匹配");
            }

            //AttachedToParent | PreferFairness  to String 后是 AttachedToParent , PreferFairness
            //枚举选项
            IEnumerable<EnumType> options =
                from e in enumInstance.ToString().Split(',')
                select (EnumType)Enum.Parse(enumType, e.Trim());
            //枚举集合,用于返回
            List<EnumType> list = options.ToList();
            return list;
        }
    }

    /// <summary>
    /// 项目枚举扩展方法
    /// </summary>
    public static class ProjectExtendMethod
    {
        /// <summary>
        /// 得到当前枚举的特性描述,可用于输出枚举的说明与表示的意思
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <typeparam name="TResult">所用的属性类型</typeparam>
        /// <param name="EnumInstance">指定的枚举项</param>
        /// <returns>返回属性对像,如果没有找到指定的属性对像则引发异常</returns>
        public static System.ComponentModel.DescriptionAttribute GetAttribuate(this RecordType EnumInstance)
        {
            return EnumExtendMethod.GetAttribuate<RecordType, System.ComponentModel.DescriptionAttribute>(EnumInstance);
        }
    }
}