using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DalFactory;
using IDAL;

namespace BLL
{
    class NovelAccessBLL :IDAL.INovel
    {
        INovel novel = DalFactory.DateAccess.CreateINovel();


        /// <summary>
        /// 按类型获取最新章节
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="booktypeId"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<Model.SectionsInfo> getNewSectionsByType(int pageIndex,int pageSize, int booktypeId, out int pageCount)
        {
            return novel.getNewSectionsByType(pageIndex,pageSize,booktypeId, out pageCount);
        }




        //获取所有小说
        /*需要写存储过程分页*/
        public List<Model.BooksInfo> getNovels(int pageSize, int pageIndex, out int recordCount)
        {

            return novel.getNovels(pageSize, pageIndex,out recordCount);
        }

        //获取小说分类
        /*参考SQL语句： select * from bookType*/
        public List<Model.BookTypeInfo> getNovelType()
        {
            return novel.getNovelType();
        }


        //获取推荐小说
        /*SQL字段：books.Recommand = 1 ，需要写存储过程分页*/
        public List<Model.BooksInfo> getRemmendNovels(int pageIndex, int pageSize, out int pageCount)
        {
            return novel.getRemmendNovels(pageIndex,pageSize,out pageCount);
        }



        //获取热点小说
        /*
         * SQL字段：每一本小说的所有章节 Sections.ClickCount 的总和。
         * 提示：按VolumeId分组，然后统计点击量，并找出点击最高记录的100本，再根据查询结果的VolumeId查询出这100本小说的Volume表,
         * 最后根据Volume表的查询结果，可以找到这100本书的BookId，将这100本书的信息放入List中返回
         * */
        public List<Model.BooksInfo> getHotNovels()
        {
            return novel.getHotNovels();
        }


        //近期更新的小说章节
        /* 
         * 获取近半个月的或者1个月的，或者多少时间的由你定
         * 提示： 先获取当前系统日期，然后查找 当前日期 - Sections.AddTime > 15 的记录，既可得到最近2周的更新章节
         * 然后根据章节的VolumeId找到BookId，最后将这些章节放入List中，当然，你可能需要在章节的实体类添加BookId，BookName属性，因为前台会用
         * 一个GridView来呈现近期更新的章节，后面需要给出书本名称，用户点书本名称可以进入书的目录，所以BookId也是必须的.，而且，需要分页
         */
        public List<Model.SectionsInfo> getNewSections(int pageIndex, int pageSize, out int pageCount)
        {
            return novel.getNewSections(pageIndex,pageSize,out pageCount);
        }

        //获取本站新闻
        /*
            *将所有新闻获取出来，首页新闻小块显示最后10条新闻，可以按oder NewsId desc，然后top 10 *
        */
        public List<Model.NovelNewsInfo> getNovelNews()
        {
            return novel.getNovelNews();
        }


        //获取最新小说，既刚开始写的小说
        /*
         * 获取新书，按类型获取5部新小说，写一个存储过程，接收BookTypeID
         * 目前涉及12种类型的小说
        */
        public List<Model.BooksInfo> getNewBooksByType(int BookTypeId)
        {
            return novel.getNewBooksByType(BookTypeId);
        }


        /// <summary>
        /// 获取分卷所属章节信息
        /// </summary>
        /// <param name="NoelClassId">分卷Id</param>
        /// <returns></returns>
        public List<Model.SectionsInfo> getNovelSectionsInfo(int NoelClassId)
        {
            return novel.getNovelSectionsInfo(NoelClassId);
        }

        /// <summary>
        /// 获取所点击小说的分卷信息
        /// </summary>
        /// <param name="NoelId">BoosId</param>
        /// <returns></returns>
        public List<Model.VolumeInfo> getNovelClassInfo(int NoelId)
        {
            return novel.getNovelClassInfo(NoelId);
        }

        /// <summary>
        /// 获取点击小说周、月点击量表中数据
        /// </summary>
        /// <param name="BooksId">小说Id</param>
        /// <returns></returns>
        public List<Model.BooksCountQuantity> getBooksCountQuantity()
        {
            return novel.getBooksCountQuantity();
        }

        /// <summary>
        /// 更新小说周、月点击量表中数据
        /// </summary>
        /// <param name="BooksId">BooksId</param>
        /// <param name="whenUpdate">判断是否更新</param>
        /// <returns></returns>
        public void updateooksCountQuantity(int whenUpdate, int Times2Update, int BooksId, int Times)
        {
            novel.updateooksCountQuantity(whenUpdate, Times2Update, BooksId, Times);
        }

        /// <summary>
        /// 更新BooksCountQuantity(点击量)
        /// </summary>
        /// <param name="BookId"></param>
        /// <param name="Times">当前时间所属当年第几周</param>
        public void updateBooksCountQuantitys(int BookId, int Times, int SectiuonId)
        {
            novel.updateBooksCountQuantitys(BookId, Times, SectiuonId);
        }

        public List<Model.BooksCountQuantity> getBooksCountQuantitys(int week)
        {
            return novel.getBooksCountQuantitys(week);
        }

        public List<Model.BooksCountQuantity> getBooksCountMonth()
        {
            return novel.getBooksCountMonth();
        }


        //小说书票排行
        public List<Model.BooksInfo> getTicketCompositor()
        {
            return novel.getTicketCompositor();
        }

        //小说鲜花排行
        public List<Model.BooksInfo> getFlowerCompositor()
        {
            return novel.getFlowerCompositor();
        }


        /// <summary>
        /// 新书介绍
        /// </summary>
        /// <param name="TypeId">书本类型Id</param>
        /// <returns></returns>
        public List<Model.NewBooksIntroduce> getNewBooksIntroduce(int TypeId, int Size)
        {
            return novel.getNewBooksIntroduce(TypeId, Size);
        }

        /// <summary>
        /// 小说分类
        /// </summary>
        /// <returns></returns>
        public List<Model.BookTypeInfo> getBookType()
        {
            return novel.getBookType();
        }

        /// <summary>
        /// 新书信息
        /// </summary>
        /// <param name="typeId">小说类型Id</param>
        /// <returns></returns>
        public List<Model.BooksInfo> getBookInfoss(int typeId)
        {
            return novel.getBookInfoss(typeId);
        }

        public List<Model.BooksInfo> getBookInfoss2(int typeId)
        {
            return novel.getBookInfoss2(typeId);
        }

        /// <summary>
        /// 获取指定小说类型推荐排行  ~
        /// </summary>
        /// <param name="typeId">TypeId</param>
        /// <returns></returns>
        public List<Model.BooksInfo> getAppointTypeBook(int typeId)
        {
            return novel.getAppointTypeBook(typeId);
        }

        /// <summary>
        /// 获取指定小说Id类型  ~
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public Model.BookTypeInfo getAppointTypeId(int typeId)
        {
            return novel.getAppointTypeId(typeId);
        }

        /// <summary>
        /// 获取指定类型前4本推荐小说  ~
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public List<Model.BooksInfo> getTypeIdUpFoure(int typeid)
        {
            return novel.getTypeIdUpFoure(typeid);
        }

        /// <summary>
        /// 获取指定类型小说鲜花和数票总和排行榜前10  ~
        /// </summary>
        /// <param name="typeid">typeId</param>
        /// <returns></returns>
        public List<Model.BooksInfo> getTicketOrFlower(int typeid)
        {
            return novel.getTicketOrFlower(typeid);
        }

        /// <summary>
        /// 获取指定类型小说新书推荐按录入时间和点击量  ~
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public List<Model.BooksInfo> getNewBooksCommend(int typeid)
        {
            return novel.getNewBooksCommend(typeid);
        }

        /// <summary>
        /// 按类型获取已完本小说按鲜花和月票排行  前6本  ~
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public List<Model.BooksInfo> getMemory(int typeId)
        {
            return novel.getMemory(typeId);
        }

        public List<Model.BooksInfo> getMemory3()
        {
            return novel.getMemory3();
        }

        public List<Model.BooksInfo> getMemory2()
        {
            return novel.getMemory2();
        }

        public List<Model.BooksInfo> getAllAppoint()
        {
            return novel.getAllAppoint();
        }

        //搜索
        public List<Model.SectionsInfo> NovelSearch(int pageIndex, int pageSize, int descnLen, string[] words, out int recordCount)
        {
            return novel.NovelSearch(pageIndex, pageSize, descnLen, words, out recordCount);
        }


        //获取章节排行按类型
        public List<Model.BookNumberTable> getBookNumberTable(int pageIndex, int pageSize, out int RecordCount, int typeId)
        {
            return novel.getBookNumberTable(pageIndex,pageSize,out RecordCount,typeId);
        }

        //获取章节排行
        public List<Model.BookNumberTable> getAllBookNumberTable(int pageIndex,int pageSize,out int RecordCount)
        {
            return novel.getAllBookNumberTable(pageIndex,pageSize,out RecordCount);
        }

        //按用户给定条件筛选书库
        public List<Model.BooksInfo> getSelectBooks(string state, int bookType, int minCharnum, int maxCharnum, int pageIndex, int pageSize, out int recordCount)
        {
            return novel.getSelectBooks(state, bookType, minCharnum, maxCharnum, pageIndex, pageSize, out recordCount);
        }


        public List<Model.SectionsInfo> getSelectedSections(int sectionId)
        {
            return novel.getSelectedSections(sectionId);
        }

        /// <summary>
        /// 获取省份信息 ~
        /// </summary>
        /// <returns></returns>
        public List<Model.ProvinceInfo> getProvince()
        {
            return novel.getProvince();
        }

        /// <summary>
        /// 获取市区信息 ~
        /// </summary>
        /// <returns></returns>
        public List<Model.AreaInfo> getArea(string provinceID)
        {
            return novel.getArea(provinceID);
        }

        public string getProvinceID(int id)
        {
            return novel.getProvinceID(id);
        }

        public Model.BooksInfo getBookIdBooksInfo(int bookId)
        {
            return novel.getBookIdBooksInfo(bookId);
        }

        //显示书本章节
        public Model.BooksInfo getSectionsInfo(int SectionsId)
        {
            return novel.getSectionsInfo(SectionsId);
        }

        //上一章
        public Model.BooksInfo getAnteSections(int SectionsId, int bookId)
        {
            return novel.getAnteSections(SectionsId, bookId);
        }

        //下一章
        public Model.BooksInfo getNextSections(int SectionsId, int bookId)
        {
            return novel.getNextSections(SectionsId, bookId);
        }

        //获取用户书架
        public List<Model.UserBookShelfInfo> getUserBookShelf(int userId, int pageindex, int pagesize, int booktype, out int recordCount)
        {
            return novel.getUserBookShelf(userId, pageindex, pagesize, booktype, out recordCount);
        }

        //往书架添加书
        public int adduserBookShelf(int userId, int bookID)
        {
            return novel.adduserBookShelf(userId, bookID);
        }
        //删除书架内书
        public int delUserbookShelf(int userId, int bookId)
        {
            return novel.delUserbookShelf(userId, bookId);
        }
        //订阅连载卡使用,加入连载表，减去道具包里1个数量，如果数量=1,执行删除，>1执行更新
        public int userAfterBook(int userId, int bookId)
        {
            return novel.userAfterBook(userId, bookId);
        }
        //VIP卡使用,修改UserType,看有没有道具，并减去道具包里1个数量，如果数量=1,执行删除，>1执行更新，要改
        public int userVIPCard(int userId)
        {
            return novel.userVIPCard(userId);
        }

        //是否已经订阅
        public int isReaddingBook(int userid, int bookid)
        {
            return novel.isReaddingBook(userid, bookid);
        }

        //获取本站所有小说
        public List<Model.NovelNewsInfo> getNovelNewss(out int recordCount, int pageSize, int pageIndex)
        {
            return novel.getNovelNewss(out recordCount, pageSize, pageIndex);
        }
        //删除新闻
        public int deleteNovelNews(int NewsId)
        {
            return novel.deleteNovelNews(NewsId);
        }
        //添加新闻
        public int addNovelNews(Model.NovelNewsInfo item)
        {
            return novel.addNovelNews(item);
        }
        //修改新闻
        public int updateNovelNews(Model.NovelNewsInfo item)
        {
            return novel.updateNovelNews(item);
        }
        //获取小说
        public List<Model.BooksInfo> getBooks(out int recordCount, int pageSize, int pageIndex, int bookType)
        {
            return novel.getBooks(out recordCount, pageSize, pageIndex, bookType);
        }
        //获取会员
        public List<Model.UsersInfo> getUser()
        {
            return novel.getUser();
        }
        //添加新书
        public int addBook(Model.BooksInfo item)
        {
            return novel.addBook(item);
        }
        //给已有小说添加新卷
        public int addVolume(Model.VolumeInfo item)
        {
            return novel.addVolume(item);
        }
        //给已有小说添加新章
        public int addSections(Model.SectionsInfo item)
        {
            return novel.addSections(item);
        }




        //小说封面基本信息
        public Model.BooksInfo getBookCoverInfo(int BookId)
        {
            return novel.getBookCoverInfo(BookId);
        }
        /// <summary>
        /// 根据书本ID查看该书本所得书票和鲜花的详细信息
        /// </summary>
        /// <param name="BookId"></param>
        /// <returns></returns>
        public List<Model.UserPropInfo2> getUserPropInfo(int BookId)
        {
            return novel.getUserPropInfo(BookId);
        }
        //查询指定小说ID的最后一章
        public Model.BooksInfo getBookFinallySection(int BookId)
        {
            return novel.getBookFinallySection(BookId);
        }

        //获取书评
        public List<Model.BookReplayInfo> getBookReplayInfo(int BookId)
        {
            return novel.getBookReplayInfo(BookId);
        }
        //获取指定书名ID的书票排行和书票数
        public Model.BooksInfo getTicketNumber(int BookId)
        {
            return novel.getTicketNumber(BookId);
        }
        //获取指定书名ID的鲜花排行和书鲜花
        public Model.BooksInfo getFlowerNumber(int BookId)
        {
            return novel.getFlowerNumber(BookId);
        }
        //给指定BookId添加书票
        public int insertTicket(int userId, int bookId, int propId, int propAmount, out int whenHave)
        {
            return novel.insertTicket(userId, bookId, propId, propAmount, out whenHave);
        }
        //根据作者ID查看该作者所有作品
        public Model.BooksInfo getAuthorIdBooksOne(int bookId)
        {
            return novel.getAuthorIdBooksOne(bookId);
        }
        public List<Model.BooksInfo> getAuthorIdBooks(int userId)
        {
            return novel.getAuthorIdBooks(userId);
        }

        //查看某一章节是否为VIP章节
        public List<Model.SectionsInfo> sectionsVolumeNumber(int setionID)
        {
            return novel.sectionsVolumeNumber(setionID);
        }


        //章节点击加1
        public void sectionClickCount(int sectionId)
        {
            novel.sectionClickCount(sectionId);
        }

    
        //获取指定小说ID最后一卷ID
        public int AddBookIdSections(int BookId)
        {
            return novel.AddBookIdSections(BookId);
        }
        //获取所有未被禁用的用户
        public List<Model.UsersInfo> getUserInfoAll(int userType)
        {
            return novel.getUserInfoAll(userType);
        }
        //获取所有未被推荐的小说
        public List<Model.BooksInfo> GetAllNoRecommand(out int recordCount, int pageSize, int pageIndex, int bookType)
        {
            return novel.GetAllNoRecommand(out recordCount, pageSize, pageIndex, bookType);
        }
        //推荐小说
        public int Recommand(int bookId)
        {
            return novel.Recommand(bookId);
        }
        //根据BookId查看所有书评
        public List<Model.BookReplayInfo> getBookIdReplay(out int recordCount, int pageSize, int pageIndex, int bookId)
        {
            return novel.getBookIdReplay(out recordCount, pageSize, pageIndex, bookId);
        }
        //根据书评ID删除书评
        public int DelBookReplay(int ReplayId)
        {
            return novel.DelBookReplay(ReplayId);
        }

        //取消小说推荐
        public int noRecommand(int bookId)
        {
            return novel.noRecommand(bookId);
        }

    }
}
