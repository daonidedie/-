using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDAL
{
    public interface INovel
    {
        //取消小说推荐
        int noRecommand(int bookId);


        //根据书评ID删除书评
        int DelBookReplay(int ReplayId);

        //根据BookId查看书评
        List<Model.BookReplayInfo> getBookIdReplay(out int recordCount, int pageSize, int pageIndex, int bookId);

        //推荐小说
        int Recommand(int bookId);

        //查看所有未被推荐的小说
        List<Model.BooksInfo> GetAllNoRecommand(out int recordCount, int pageSize, int pageIndex, int bookType);

        //查看所有未被禁用的用户
        List<Model.UsersInfo> getUserInfoAll(int bookType);

        //获取指定小说ID最后一卷ID
        int AddBookIdSections(int BookId);

        //给已有小说添加新章
        int addSections(Model.SectionsInfo item);

        //给已有小说添加新卷
        int addVolume(Model.VolumeInfo item);

        //添加小说
        int addBook(Model.BooksInfo item);

        //获取会员
        List<Model.UsersInfo> getUser();

        //查看小说
        List<Model.BooksInfo> getBooks(out int recordCount, int pageSize, int pageIndex, int bookType);

        //修改新闻
        int updateNovelNews(Model.NovelNewsInfo item);

        //添加新闻
        int addNovelNews(Model.NovelNewsInfo item);

        //删除新闻
        int deleteNovelNews(int NewsId);

        //查看本站所有新闻
        List<Model.NovelNewsInfo> getNovelNewss(out int recordCount, int pageSize, int pageIndex);


        //根据UserId查询作者所属小说
        Model.BooksInfo getAuthorIdBooksOne(int userId);
        List<Model.BooksInfo> getAuthorIdBooks(int userId);

        //给指定BookId添加书票
        int insertTicket(int userId, int bookId, int propId, int propAmount, out int whenHave);

        //获取指定书名ID的书票排行和书票数
        Model.BooksInfo getTicketNumber(int BookId);

        //获取指定书名ID的鲜花排行和书鲜花
        Model.BooksInfo getFlowerNumber(int BookId);

        //获取书评
        List<Model.BookReplayInfo> getBookReplayInfo(int BookId);

        //查询指定小说ID的最后一章
        Model.BooksInfo getBookFinallySection(int BookId);

        //根据书本ID查看该书本所得书票和鲜花的详细信息
        List<Model.UserPropInfo2> getUserPropInfo(int BookId);

        //获取小说封面基本数据
        Model.BooksInfo getBookCoverInfo(int BookId);



        //是否已经订阅
        int isReaddingBook(int userid, int bookid);

        //VIP卡使用,修改UserType,看有没有道具，并减去道具包里1个数量，如果数量=1,执行删除，>1执行更新，要改
        int userVIPCard(int userId);

        //订阅连载卡使用,加入连载表，减去道具包里1个数量，如果数量=1,执行删除，>1执行更新
        int userAfterBook(int userId, int bookId);

        //删除书架内书
        int delUserbookShelf(int userId, int bookId);

        //往书架添加书
        int adduserBookShelf(int userId, int bookID);

        //获取用户书架
        List<Model.UserBookShelfInfo> getUserBookShelf(int userId, int pageindex, int pagesize, int booktype, out int recordCount);

        //下一章
        Model.BooksInfo getAnteSections(int SectionsId, int bookId);

        //上一章
        Model.BooksInfo getNextSections(int SectionsId, int bookId);

        //显示书本章节
        Model.BooksInfo getSectionsInfo(int SectionsId);

        //按指定bookId查找
        Model.BooksInfo getBookIdBooksInfo(int bookId);

        //获取指定id的省份信息 ~
        string getProvinceID(int id);

        //获取省份信息~
        List<Model.ProvinceInfo> getProvince();

        //获取市区信息~
        List<Model.AreaInfo> getArea(string provinceID);

        //小说分类排行
        List<Model.BookNumberTable> getBookNumberTable(int pageIndex,int pageSize,out int RecordCount,int typeId);

        //小说所有类型排行
        List<Model.BookNumberTable> getAllBookNumberTable(int pageIndex,int pageSize,out int RecordCount);

        //获取所有类型小说的书票排行榜
        List<Model.BooksInfo> getMemory3();

        //获取所有类型小说的鲜花排行榜
        List<Model.BooksInfo> getMemory2();

        //获取所有类型小说的总点击排行榜
        List<Model.BooksInfo> getAllAppoint();

        //按类型获取已完本小说按鲜花和月票排行  前6本
        List<Model.BooksInfo> getMemory(int typeId);

        //获取指定类型小说新书推荐按录入时间和点击量
        List<Model.BooksInfo> getNewBooksCommend(int typeid);

        //获取指定类型小说鲜花和数票总和排行榜前10
        List<Model.BooksInfo> getTicketOrFlower(int typeid);

        //获取指定类型前4本推荐小说
        List<Model.BooksInfo> getTypeIdUpFoure(int typeid);

        //获取指定小说类型推荐排行
        List<Model.BooksInfo> getAppointTypeBook(int typeId);

        //获取指定Id小说类型
        Model.BookTypeInfo getAppointTypeId(int typeId);

        //获取所有小说
        /*需要写存储过程分页*/
        List<Model.BooksInfo> getNovels(int pageSize, int pageIndex, out int recordCount);

        //获取小说分类
        /*参考SQL语句： select * from bookType*/
        List<Model.BookTypeInfo> getNovelType();

        //获取推荐小说
        /*SQL字段：books.Recommand = 1 ，需要写存储过程分页*/
        List<Model.BooksInfo> getRemmendNovels(int pageIndex, int pageSize, out int pageCount);

        //获取热点小说
        /*
         * SQL字段：每一本小说的所有章节 Sections.ClickCount 的总和。
         * 提示：按VolumeId分组，然后统计点击量，并找出点击最高记录的100本，再根据查询结果的VolumeId查询出这100本小说的Volume表,
         * 最后根据Volume表的查询结果，可以找到这100本书的BookId，将这100本书的信息放入List中返回
         * */
        List<Model.BooksInfo> getHotNovels();



        //近期更新的小说章节
        /* 
         * 获取近半个月的或者1个月的，或者多少时间的由你定
         * 提示： 先获取当前系统日期，然后查找 当前日期 - Sections.AddTime > 15 的记录，既可得到最近2周的更新章节
         * 然后根据章节的VolumeId找到BookId，最后将这些章节放入List中，当然，你可能需要在章节的实体类添加BookId，BookName属性，因为前台会用
         * 一个GridView来呈现近期更新的章节，后面需要给出书本名称，用户点书本名称可以进入书的目录，所以BookId也是必须的.，而且，需要分页
         */
        List<Model.SectionsInfo> getNewSections(int pageIndex, int pageSize, out int pageCount);


        /// <summary>
        /// 按类型获取新章节
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        List<Model.SectionsInfo> getNewSectionsByType(int pageIndex, int pageSize, int booktypeId, out int pageCount);


        //获取本站新闻
        /*
            *将所有新闻获取出来，首页新闻小块显示最后10条新闻，可以按oder NewsId desc，然后top 10 *
        */
        List<Model.NovelNewsInfo> getNovelNews();


        //获取最新小说，既刚开始写的小说
        /*
         * 获取新书，按类型获取5部新小说，写一个存储过程，接收BookTypeID
         * 目前涉及12种类型的小说
        */
        List<Model.BooksInfo> getNewBooksByType(int BookTypeId);


        //查询上一月小说的周点击量
        List<Model.BooksCountQuantity> getBooksCountMonth();

        //查询上一周小说的周点击量
        List<Model.BooksCountQuantity> getBooksCountQuantitys(int week);

        /// <summary>
        /// 更新BooksCountQuantity(点击量)
        /// </summary>
        /// <param name="BookId"></param>
        /// <param name="Times">当前时间所属当年第几周</param>
        void updateBooksCountQuantitys(int BookId, int Times, int SectiuonId);

        /// <summary>
        /// 更新小说周、月点击量表中数据
        /// </summary>
        /// <param name="whenUpdate">判断是周点击量上加一还是归零</param>
        /// <param name="Times2Update">用于判断是在月点击量上加一还是归零</param>
        /// <param name="BooksId">小说Id</param>
        /// <param name="Times">添加时间在本年的周数</param>
        void updateooksCountQuantity(int whenUpdate, int Times2Update, int BooksId, int Times);

        //获取点击小说周、月点击量表中数据
        List<Model.BooksCountQuantity> getBooksCountQuantity();

        //获取分卷所属章节信息
        List<Model.SectionsInfo> getNovelSectionsInfo(int NoelClassId);

        //获取所点击小说的分卷信息
        List<Model.VolumeInfo> getNovelClassInfo(int NoelId);

        //小说书票排行
        List<Model.BooksInfo> getTicketCompositor();

        //小说鲜花排行
        List<Model.BooksInfo> getFlowerCompositor();


        //新书2
        List<Model.BooksInfo> getBookInfoss2(int typeId);

        //新书
        List<Model.BooksInfo> getBookInfoss(int typeId);

        //新书类型
        List<Model.BookTypeInfo> getBookType();

        //新书介绍
        List<Model.NewBooksIntroduce> getNewBooksIntroduce(int TypeId, int Size);

        //一章书的内容
        List<Model.SectionsInfo> getSelectedSections(int sectionId);

        //小说搜索
        List<Model.SectionsInfo> NovelSearch(int pageIndex, int pageSize, int descnLen, string[] words, out int recordCount); 

       //获取书本筛选
        List<Model.BooksInfo> getSelectBooks(string state, int bookType, int minCharnum, int maxCharnum, int pageIndex, int pageSize, out int recordCount);

        //查看某一章节是否属于VIP章节
        List<Model.SectionsInfo> sectionsVolumeNumber(int setionID);

        //章节点击量加1
        void sectionClickCount(int sectionId);

    }
}
