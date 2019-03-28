using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TygModel;
using System.Threading.Tasks;
using System.Data;

namespace 采集应用程序.Capability
{
    /// <summary>
    /// 从86中文网更新书的索引
    /// </summary>
    public class UpdateBookIndexFromdawenxue_org : System.Windows.DependencyObject, System.ComponentModel.INotifyPropertyChanged
    {

        private bool _IsNotBusy = true;
        /// <summary>
        /// 当前不忙
        /// </summary>
        public bool IsNotBusy
        {
            get { return _IsNotBusy; }
            set
            {
                _IsNotBusy = value;
                OnPropertyChanged("IsNotBusy");
            }
        }

        #region 实现 INotifyPropertyChanged
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 注册属性发生改变的事件
        /// </summary>
        /// <param name="PropertyName"></param>
        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(PropertyName));
            }


        }
        #endregion

        public void Update()
        {
            System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Factory.StartNew(new Action(delegate
               {
                   using (TygModel.Entities tygdb = new TygModel.Entities())
                   {
                       var books = tygdb.书名表.ToLookup(p => p.书名.Replace("》", "").Replace("《", "").Trim() + "|"
                           //+ (p.作者名称 == null ? "" : p.作者名称.Trim())
                           );

                       //要添加的书集合
                       List<书名表> WillAddBooks = new List<书名表>();



                       for (int classid = 1; classid <= 10; classid++)
                       {

                           System.Diagnostics.UDPGroup.SendStrGB2312("开始采集图书列表" + "http://www.dawenxue.org/book/sort" + classid + "/0/{0}.html");

                           IEnumerable<Skybot.Collections.Sites.AbstractBookInfo> alclass = new List<Skybot.Collections.Sites.AbstractBookInfo>();

                           Task<IEnumerable < Skybot.Collections.Sites.AbstractBookInfo >> taskClass = Task.Factory.StartNew<IEnumerable<Skybot.Collections.Sites.AbstractBookInfo>>(
                               () =>
                               {

                                   var al = new Skybot.Collections.Sites.BookListdawenxue_org() { BaseUrl = "http://www.dawenxue.org/book/sort" + classid + "/0/{0}.html" }.DoWork();
                                   return al;
                               });
                           try
                           {
                                 alclass =  taskClass.Result;
                           }
                           catch (AggregateException ex)
                           {

                               ex.Handle((exx) =>
                               {
                                   System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + exx.Message + "|||||" + (ex.StackTrace == null ? " " : " " + ex.StackTrace));
                                   return true;
                               });
                           }
                           
                           //开始写入数据库
                           var AllBooks = from doc in alclass select doc;

                           //添加到Book中
                           for (int k = 0; k < AllBooks.Count(); k++)
                           {
                               try
                               {

                                   var bookName = "";
                                   var creter = "";
                                   if (AllBooks.ElementAt(k).小说名称 != null)
                                   {
                                       bookName = AllBooks.ElementAt(k).小说名称;
                                   }

                                   if (AllBooks.ElementAt(k).作者 != null)
                                   {
                                       creter = AllBooks.ElementAt(k).作者;
                                   }

                                   //当前记录
                                   //var records = books.Where(p => p.书名.Replace("》", "").Replace("《", "").Trim() == bookName.Replace("》", "").Replace("《", "").Trim() && p.作者名称.Trim() == creter.Trim());
                                   TygModel.书名表 book = null;
                                   if (books.Contains(
                                      bookName.Replace("》", "").Replace("《", "").Trim() + "|"
                                       //+ (creter == null ? "" : creter.Trim())
                                       ))
                                   {


                                       //得到当前书更新记录
                                       TygModel.书名表 currentBook = tygdb.书名表.Where(p => p.书名.Replace("》", "").Replace("《", "").Trim() == bookName.Replace("》", "").Replace("《", "").Trim() && p.作者名称.Trim() == creter.Trim()).FirstOrDefault();

                                       books[bookName.Replace("》", "").Replace("《", "").Trim() + "|"
                                           //+ (creter == null ? "" : creter.Trim() )
                       ].ElementAt(0);
                                       // currentBook.分类表 = book.分类表;
                                       //currentBook.分类表ID = book.分类表ID;
                                       //currentBook.GUID = book.GUID;
                                       currentBook.采集用的URL1 = AllBooks.ElementAt(k).小说目录URL;
                                       currentBook.采集用的URL2 = AllBooks.ElementAt(k).小说简介URL;
                                       // currentBook.创建时间 = book.创建时间;
                                       currentBook.最新章节 = AllBooks.ElementAt(k).最新章节;
                                       //currentBook.作者名称 = book.作者名称;
                                       //currentBook.说明 = book.说明;
                                       // currentBook.书名 = book.书名;
                                       //currentBook.最后更新时间 = book.最后更新时间;
                                       currentBook.完本 = AllBooks.ElementAt(k).状态.Trim() == "完成" ? true : false;
                                       //currentBook.配图 = book.配图;

                                   }
                                   else
                                   {
                                       try
                                       {
                                           book = AllBooks.ElementAt(k).Convert();

                                           WillAddBooks.Add(book);
                                       }
                                       catch (Exception ex)
                                       {
                                           System.Diagnostics.UDPGroup.SendStrGB2312("转换书时出现问题"+ex.Message + (ex.StackTrace != null ? ex.StackTrace : ""));

                                       }
                                   }
                               }
                               catch (Exception ex)
                               {
                                   System.Diagnostics.UDPGroup.SendStrGB2312(ex.Message + (ex.StackTrace != null ? ex.StackTrace : ""));
                               }
                               System.Diagnostics.UDPGroup.SendStrGB2312("已经完成书" + k + "/" + AllBooks.Count());
                           }

                       }
                       //开始全部添加
                       try
                       {
                           WillAddBooks.ForEach((book) =>
                           {
                               //当前记录
                               // var records = books.Where(p => p.书名.Replace("》", "").Replace("《", "").Trim() == book.书名.Replace("》", "").Replace("《", "").Trim() && p.作者名称.Trim() == book.作者名称.Trim());

                               if (!books.Contains(
                                   book.书名.Replace("》", "").Replace("《", "").Trim() + "|"
                                   // + (book.作者名称 == null ? "" : book.作者名称.Trim())
                          ))
                               {
                                   tygdb.AddTo书名表(book);
                               }
                           });

                           tygdb.SaveChanges();
                       }
                       catch (Exception ex)
                       {
                           System.Diagnostics.UDPGroup.SendStrGB2312(ex.Message + (ex.StackTrace != null ? ex.StackTrace : ""));
                       }
                       finally
                       {
                           WillAddBooks.Clear();

                       }

                       System.Diagnostics.UDPGroup.SendStrGB2312("dawenxue 所有图书状态更新完成");
                   }
               }));
            Dispatcher.BeginInvoke(new Action(delegate
            {
                IsNotBusy = false;
            }));
            Task.Factory.StartNew(delegate
            {
                try
                {

                    task.Wait();
                }
                catch (AggregateException ex)
                {

                    ex.Handle((exx) =>
                    {
                        System.Diagnostics.UDPGroup.SendStrGB2312(DateTime.Now + exx.Message + "|||||" + (ex.StackTrace == null ? " " : " " + ex.StackTrace));
                        return true;
                    });
                }
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    IsNotBusy = true;
                }));
            });
        }


    }
}
