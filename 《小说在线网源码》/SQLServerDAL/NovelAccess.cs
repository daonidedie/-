using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using System.Configuration;

namespace SQLServerDAL
{
    class NovelAccess : IDAL.INovel
    {

        //取消推荐小说
        private const string PROC_NORECOMMAND = "noRecommand";

        //根据书评ID删除书评
        private const string PROC_DELBOOKREPLAY = "delBookReplay";

        //根据BookId查看该书书评
        private const string PROC_GETBOOKIDREPLAY = "getBookIdReplay";

        //推荐小说
        private const string PROC_RECOMMAND = "Recommand";

        //查看所有未被推荐的小说
        private const string PROC_GETALLNORECOMMAND = "GetAllNoRecommand";

        //查看所有未被禁用的用户
        private const string PROC_FORBIDUSERALL = "ForbidUserAll";

        //获取指定小说ID最后一卷ID
        private const string PROC_ADDBOOKIDSECTIONS = "addBookIdSections";

        //给已有小说添加新章
        private const string PROC_ADDSECTIONS = "addSections";

        //给已有小说添加新卷
        private const string PROC_ADDVLOUME = "addVolume";

        //添加小说
        private const string PROC_ADDBOOK = "addBook";

        //获取会员
        private const string PROC_GETUSER = "getUser";

        //查看小说
        private const string PROC_GETBOOKS = "getBooks";

        //修改新闻
        private const string PROC_UPDATENOVELNEWS = "updateNovelNews";

        //添加新闻
        private const string PROC_ADDNOVELNEWS = "addNovelNews";

        //删除新闻
        private const string PROC_DELETENOVELNEWS = "deleteNovelNews";

        //获取本站所有新闻
        private const string PROC_GETNOVELNEWSS = "selectNovelNews";

        //根据UserId查询作者所属小说
        private const string PROC_GETAUTHORIDBOOKSONE = "getAuthorIdBooksOne";
        private const string PROC_GETAUTHORIDBOOKS = "getAuthorIdBooks";


        //给指定BookId添加书票
        private const string PROC_INSERTTICKET = "insertTicket";

        //获取指定书名ID的书票排行和书票数
        private const string PROC_GETTICKETNUMBER = "getTicketNumber";

        //获取指定书名ID的鲜花排行和书鲜花
        private const string PROC_GETFLOWERNUMBER = "getFlowerNumber";

        //获取书评
        private const string PROC_GETBOOKREPLAYINFO = "getBookReplayInfo";

        //查询指定小说ID的最后一章
        private const string PROC_GETBOOKFINALLYSECTION = "getBookFinallySection";

        //根据书本ID查看该书本所得书票和鲜花的详细信息
        private const string PROC_GETUSERPROPINFO = "getUserPropInfo";

        //获取小说封面基本数据
        private const string PROC_getBookCoverInfo = "getBookCoverInfo";




        //VIP卡使用,修改UserType,看有没有道具，并减去道具包里1个数量，如果数量=1,执行删除，>1执行更新，要改
        private const string PROC_USERVIPCARD = "userVIPCard";

        //订阅连载卡使用,加入连载表，减去道具包里1个数量，如果数量=1,执行删除，>1执行更新
        private const string PROC_USERAFTERBOOK = "userAfterBook";

        //删除书架内书
        private const string PROC_DELUSERBOOKSHELF = "delUserbookShelf";

        //往书架添加书
        private const string PROC_ADDUSERBOOKSHELF = "adduserBookShelf";

        //获取用户书架
        private const string PROC_GETUSERBOOKSHELF = "getUserBookShelf";

        //小说搜索
        private const string PROC_SEARCHNOVEL = "SearchNovel";

        //获取所有类型小说的总点击排行榜
        private const string PROC_GETALLAPPINT = "getAllAppoint";

        //获取所有类型小说的鲜花排行榜 ~
        private const string PROC_GETMEMORY2 = "getMemory2";

        //获取所有类型小说的书票排行榜 ~
        private const string PROC_GETMEMORY3 = "getMemory3";

        //小说分类排行~
        private const string PROC_GETBOOKNUMBERTABLE = "getBookNumberTable";

        //小说所有类型排行~
        private const string PROC_GETALLBOOKNUMBERTABLE = "getAllBookNumberTable";

        //按类型获取已完本小说按鲜花和月票排行  前6本  
        private const string PROC_GETMEMORY = "getMemory";

        //获取指定类型小说新书推荐按录入时间和点击量  
        private const string PROC_GETNEWBOOKSCOMMEND = "getNewBooksCommend";

        //获取指定类型小说鲜花和数票总和排行榜前10  
        private const string PROC_GETTICKETORFLOWER = "getTicketOrFlower";

        //获取指定类型前4本推荐小说  
        private const string PROC_GETTYPEIDUPFOURE = "getTypeIdUpFoure";

        //获取指定小说类型推荐排行  
        private const string PROC_GETAPPOINTTYPEBOOK = "getAppointTypeBook";

        //获取指定Id小说类型  
        private const string PROC_GETAPPOINTTYPEID = "getAppointTypeId2";

        //小说分类
        private const string PROC_GETBOOKSTYPE = "getBooksType";

        //新书介绍
        private const string PROC_GETNEWBOOKSINTRODUCE = "getNewBooksIntroduce";

        private const  string PROC_GETNOVELS="GetNovels";
        private const string PROC_GETNOVELTYPE = "GetNovelType";
        private const string PROC_GETNEWSECTIONS = "GetNewSections";

        //获取新小说，按类型
        private const string PROC_GETNEWBOOSBYTYPE = "GetNewsBookByType";
        //按类型获取最新章节        
        private const string PROC_GETNEWSECTIONSBYTYPE = "getNewSectionsByType";

        //小说书票排行
        private const string PROC_GETTICKETICKCOMPOSITOR = "getTicketCompositor";
        //小说鲜花排行
        private const string PROC_GETFLOWERCOMPOSITOR = "getFlowerCompositor";

        //获取推荐小说
        private const string PROC_GETREMMENDNOVELS = "getRemmendNovels";
        //获取本站新闻  最后10条
        private const string PROC_GETNOVELNEWS = "getNovelNews";
        //获得热点小说  前10条
        private const string PROC_GETHOTNOVELBOOK = "getHotNovelBook";

        //查询上一月小说的周点击量
        private const string PROC_GETBOOKSCOUNTMONTH = "getBooksCountMonth";
        //查询上一周小说的周点击量
        private const string PROC_GETBOOKSCOUNTQUANTITYS = "getBooksCountQuantitys";
        //更新BooksCountQuantity(点击量)
        private const string PROC_UPDATEBOOKSCOUNTQUANTITYS = "updateBooksCountQuantitys";
        //更新小说周、月点击量表中数据
        private const string PROC_UPDATEBOOKSCOUNTQUANTITY = "updateBooksCountQuantity";
        //获取点击小说周、月点击量表中数据
        private const string PROC_GETBOOKSCOUNTQUANTITY = "getBooksCountQuantity";
        //获取分卷所属章节信息
        private const string PROC_GETNOVELSECTIONS = "getNovelSections";
        //获取点击小说分卷信息
        private const string PROC_GETNOVELCLASSINFO = "getNovelClassInfo"; 

        //获取一章书
        private const string RPOC_GETSELECTEDSECTION = "getSelectedSection";

        //获取筛选书
        private const string PROC_GETSELECTBOOKS = "getbooksbyUser";

        //下一章
        private const string PROC_GETANTESECTIONS = "getAnteSections";

        //上已章
        private const string PROC_GETNEXTSECTIONS = "getNextSections";

        //显示书本章节
        private const string PROC_GETSECTIONSINFO = "getSectionsInfo";

        //按指定bookId查找
        private const string PROC_GETBOOKIDBOOKSINFO = "getBookIdBooksInfo";

        //获取指定id的省份信息 ~
        private const string PROC_GETPROVINCEID = "getProvinceID";

        //获取市区信息 ~
        private const string PROC_GETAREA = "getArea";

        //获取省份信息 ~
        private const string PROC_GETPROVINCE = "getProvince";



        //获取所有小说
        /*需要写存储过程分页*/
        public List<Model.BooksInfo> getNovels(int pageSize,int pageIndex,out int recordCount)
        {
            List<BooksInfo> bookList = new List<BooksInfo>();
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@pageIndex",pageIndex),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@recordCount",SqlDbType.Int,4)
            };

            param[2].Direction = ParameterDirection.Output;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETNOVELS,param))
            {
                while (dr.Read())
                {
                    BooksInfo bi = new BooksInfo();
                    bi.BookId = Convert.ToInt32(dr["BookId"]);
                    bi.StateName = dr["StateName"].ToString();
                    bi.TypeName = dr["TypeName"].ToString();
                    bi.UserName = dr["UserName"].ToString();
                    bi.ticketNumber = Convert.ToInt32(dr["ticketNumber"]);
                    bi.Recommand = Convert.ToInt32(dr["Recommand"]);
                    bi.flowerNumber = Convert.ToInt32(dr["flowerNumber"]);
                    bi.Images = dr["Images"].ToString();
                    bi.BookName = dr["BookName"].ToString();
                    bi.BookIntroduction = dr["BookIntroduction"].ToString();
                    bookList.Add(bi);
                }
            }
            recordCount = Convert.ToInt32(param[2].Value);
            return bookList;
        }

        //获取小说分类
        /*参考SQL语句： select * from bookType*/
        public List<Model.BookTypeInfo> getNovelType()
        {
            List<BookTypeInfo> bookTypeList = new List<BookTypeInfo>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETNOVELTYPE, null))
            {
                while (dr.Read())
                {
                    BookTypeInfo bti = new BookTypeInfo();
                    bti.TypeId = Convert.ToInt32(dr["TypeId"]);
                    bti.TypeName = dr["TypeName"].ToString();
                    bookTypeList.Add(bti);
                }
            }
            return bookTypeList;
        }


        //获取推荐小说
        /*SQL字段：books.Recommand = 1 ，需要写存储过程分页*/
        public List<Model.BooksInfo> getRemmendNovels(int pageIndex, int pageSize,out int pageCount)
        {
            List<Model.BooksInfo> booksInfos = new List<Model.BooksInfo>();
            SqlParameter[] pram = new SqlParameter[] { new SqlParameter("@pageIndex", pageIndex), new SqlParameter("@pageSize", pageSize), new SqlParameter("@recordCount", SqlDbType.Int, 4) };
            pram[2].Direction = ParameterDirection.Output;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETREMMENDNOVELS, pram))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new Model.BooksInfo();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.Images = dr["Images"].ToString();
                    item.BookIntroduction = dr["BookIntroduction"].ToString();
                    item.AddTimeString = Convert.ToDateTime(dr["AddTime"]).ToShortDateString();
                    item.StateName = dr["StateName"].ToString();
                    item.UserName = dr["UserName"].ToString();
                    item.TypeName = dr["TypeName"].ToString();

                    booksInfos.Add(item);
                }
                 
            }
            pageCount = Convert.ToInt32(pram[2].Value);   
            return booksInfos;
        }



        //获取热点小说
        /*
         * SQL字段：每一本小说的所有章节 Sections.ClickCount 的总和。
         * 提示：按VolumeId分组，然后统计点击量，并找出点击最高记录的100本，再根据查询结果的VolumeId查询出这100本小说的Volume表,
         * 最后根据Volume表的查询结果，可以找到这100本书的BookId，将这100本书的信息放入List中返回
         * */
        public List<Model.BooksInfo> getHotNovels()
        {
            List<Model.BooksInfo> list = new List<Model.BooksInfo>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETHOTNOVELBOOK, null))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new Model.BooksInfo();
                    item.AllSumClick = dr["allsumClick"].ToString();
                    item.Weeks = Convert.ToInt32(dr["allsumClick"]);
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.Images = dr["Images"].ToString();
                    item.BookIntroduction = dr["BookIntroduction"].ToString();
                    item.AddTime = Convert.ToDateTime(dr["AddTime"]);
                    item.StateName = dr["StateName"].ToString();
                    item.TypeName = dr["TypeName"].ToString();
                    item.Recommand = Convert.ToInt32(dr["Recommand"]);
                    item.ticketNumber = Convert.ToInt32(dr["TicketNumber"]);
                    item.flowerNumber = Convert.ToInt32(dr["FlowerNumber"]);
                    item.UserName = dr["UserName"].ToString();

                    list.Add(item);
                }
            }
            return list;
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
            List<SectionsInfo> sectionList = new List<SectionsInfo>();

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@pageIndex",pageIndex),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@recordCount",SqlDbType.Int,4)
            };

            param[2].Direction = ParameterDirection.Output;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETNEWSECTIONS, param))
            {
                while (dr.Read())
                {
                    SectionsInfo si = new SectionsInfo();
                    si.SectiuonId = Convert.ToInt32(dr["SectiuonId"]);
                    si.StateName = dr["StateName"].ToString();
                    si.TypeName = dr["TypeName"].ToString();
                    si.BookName = dr["BookName"].ToString();
                    si.BookIntroduction = dr["BookIntroduction"].ToString();
                    si.UserName = dr["UserName"].ToString();
                    si.ValumeName = dr["ValumeName"].ToString();
                    si.SectionTitle = dr["SectionTitle"].ToString();
                    si.Contents = dr["Contents"].ToString();
                    si.ShortAddTime = Convert.ToDateTime(dr["AddTime"]).ToShortDateString();
                    si.VolumeId = Convert.ToInt32(dr["VolumeId"]);
                    si.BookId = Convert.ToInt32(dr["BookId"]);
                    sectionList.Add(si);
                }
            }
            pageCount = Convert.ToInt32(param[2].Value);
            return sectionList;
        }


        //获取最新小说章节按类型
        public List<SectionsInfo> getNewSectionsByType(int pageIndex,int pageSize, int booktypeId, out int pageCount)
        {
            List<Model.SectionsInfo> list = new List<SectionsInfo>();

            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@pageIndex",pageIndex),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@booktypeId",booktypeId),
                new SqlParameter("@recordCount",SqlDbType.Int,4)
            };

            parm[3].Direction = ParameterDirection.Output;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETNEWSECTIONSBYTYPE, parm))
            {
                while (dr.Read())
                {
                    SectionsInfo item = new SectionsInfo();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.VolumeId = Convert.ToInt32(dr["VolumeId"]);
                    item.ValumeName = dr["ValumeName"].ToString();
                    item.SectiuonId = Convert.ToInt32(dr["SectiuonId"]);
                    item.SectionTitle = dr["SectionTitle"].ToString();
                    item.ShortAddTime = Convert.ToDateTime(dr["AddTime"]).ToShortDateString();
                    item.TypeName = dr["TypeName"].ToString();
                   
                    list.Add(item);
                }
            }
            pageCount = Convert.ToInt32(parm[3].Value);
            return list;
        }


        //获取本站新闻
        /*
            *将所有新闻获取出来，首页新闻小块显示最后10条新闻，可以按oder NewsId desc，然后top 10 *
        */
        public List<Model.NovelNewsInfo> getNovelNews()
        {
            List<Model.NovelNewsInfo> list = new List<Model.NovelNewsInfo>();

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETNOVELNEWS, null))
            {
                while (dr.Read())
                {
                    Model.NovelNewsInfo item = new Model.NovelNewsInfo();
                    item.NewsId = Convert.ToInt32(dr["NewsId"]);
                    item.NewsTitle = dr["NewsTitle"].ToString();
                    item.NewsContens = dr["NewsContens"].ToString();
                    item.NewsImages = dr["NewsImages"].ToString();
                    item.FromWhere = dr["FromWhere"].ToString();
                    item.AddTimeString = Convert.ToDateTime(dr["AddTime"]).ToShortDateString();

                    list.Add(item);
                }
            }
            return list;
        }


        //获取最新小说，既刚开始写的小说
        /*
         * 获取新书，按类型获取5部新小说，写一个存储过程，接收BookTypeID
         * 目前涉及12种类型的小说
        */
        public List<Model.BooksInfo> getNewBooksByType(int BookTypeId)
        {
            List<Model.BooksInfo> list = new List<BooksInfo>();

            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@bookTypeID",BookTypeId)
            };

            using (SqlDataReader dr =  SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETNEWBOOSBYTYPE, parm))
            {
                while (dr.Read())
                {
                    BooksInfo bi = new BooksInfo();
                    bi.BookId = Convert.ToInt32(dr["BookId"]);
                    bi.StateName = dr["StateName"].ToString();
                    bi.TypeName = dr["TypeName"].ToString();
                    bi.UserName = dr["UserName"].ToString();
                    bi.ticketNumber = Convert.ToInt32(dr["ticketNumber"]);
                    bi.Recommand = Convert.ToInt32(dr["Recommand"]);
                    bi.flowerNumber = Convert.ToInt32(dr["flowerNumber"]);
                    bi.Images = dr["Images"].ToString();
                    bi.BookName = dr["BookName"].ToString();
                    bi.BookIntroduction = dr["BookIntroduction"].ToString();
                    list.Add(bi);
                }
                return list;
            }
        }


        /// <summary>
        /// 获取分卷所属章节信息
        /// </summary>
        /// <param name="NoelClassId">分卷Id</param>
        /// <returns></returns>
        public List<SectionsInfo> getNovelSectionsInfo(int NoelClassId)
        {
            List<Model.SectionsInfo> list = new List<Model.SectionsInfo>();
            SqlParameter parm = new SqlParameter("@NovelClassId", NoelClassId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETNOVELSECTIONS, parm))
            {
                while (dr.Read())
                {
                    Model.SectionsInfo item = new Model.SectionsInfo();
                    item.SectiuonId = Convert.ToInt32(dr["SectiuonId"]);
                    item.VolumeId = Convert.ToInt32(dr["VolumeId"]);
                    item.SectionTitle = dr["SectionTitle"].ToString();
                    item.CharNum = Convert.ToInt32(dr["CharNum"]);
                    item.Contents = dr["Contents"].ToString();
                    item.AddTime = Convert.ToDateTime(dr["AddTime"]);
                    item.ClickCount = Convert.ToInt32(dr["ClickCount"]);
                    item.Examine = Convert.ToInt32(dr["Examine"]);

                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取所选择小说的分卷信息
        /// </summary>
        /// <param name="NoelId">BoosId</param>
        /// <returns></returns>
        public List<VolumeInfo> getNovelClassInfo(int NoelId)
        {
            List<Model.VolumeInfo> list = new List<Model.VolumeInfo>();
            SqlParameter parm = new SqlParameter("@NoelId", NoelId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETNOVELCLASSINFO, parm))
            {
                while (dr.Read())
                {
                    Model.VolumeInfo item = new Model.VolumeInfo();
                    item.VolumeId = Convert.ToInt32(dr["VolumeId"]);
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.ValumeName = dr["ValumeName"].ToString();
                    item.ValumeNumber = Convert.ToInt32(dr["ValumeNumber"]);

                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取点击小说周、月点击量表中数据
        /// </summary>
        /// <param name="BooksId">小说Id</param>
        /// <returns></returns>
        public List<BooksCountQuantity> getBooksCountQuantity()
        {
            List<Model.BooksCountQuantity> list = new List<BooksCountQuantity>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETBOOKSCOUNTQUANTITY, null))
            {
                while (dr.Read())
                {
                    Model.BooksCountQuantity item = new BooksCountQuantity();
                    item.Id = Convert.ToInt32(dr["Id"]);
                    item.BookId = Convert.ToInt32(dr["BooksId"]);
                    item.Times = Convert.ToInt32(dr["Times"]);
                    item.Weeks = Convert.ToInt32(dr["Weeks"]);
                    item.Months = Convert.ToInt32(dr["Months"]);
                    item.Times2 = Convert.ToDateTime(dr["Times2"]);

                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 更新小说周、月点击量表中数据
        /// </summary>
        /// <param name="BooksId">BooksId</param>
        /// <param name="whenUpdate">判断是否更新</param>
        /// <returns></returns>
        public void updateooksCountQuantity(int whenUpdate, int Times2Update, int BooksId, int Times)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@whenUpdate", whenUpdate), new SqlParameter("@Times2Update", Times2Update), new SqlParameter("@BooksId", BooksId), new SqlParameter("@Times", Times) };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_UPDATEBOOKSCOUNTQUANTITY, parm);
        }

        /// <summary>
        /// 更新BooksCountQuantity(点击量)
        /// </summary>
        /// <param name="BookId"></param>
        /// <param name="Times">当前时间所属当年第几周</param>
        public void updateBooksCountQuantitys(int BookId, int Times, int SectiuonId)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@BookId", BookId), new SqlParameter("@Times", Times), new SqlParameter("@SectiuonId", SectiuonId) };
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_UPDATEBOOKSCOUNTQUANTITYS, parm);
        }

        //查询上一周小说的周点击量
        public List<BooksCountQuantity> getBooksCountQuantitys(int week)
        {
            SqlParameter parm = new SqlParameter("@week", week);
            List<Model.BooksCountQuantity> list = new List<BooksCountQuantity>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETBOOKSCOUNTQUANTITYS, parm))
            {
                while (dr.Read())
                {
                    Model.BooksCountQuantity item = new BooksCountQuantity();
                    item.Id = Convert.ToInt32(dr["Id"]);
                    item.BookId = Convert.ToInt32(dr["BooksId"]);
                    item.Times = Convert.ToInt32(dr["Times"]);
                    item.Weeks = Convert.ToInt32(dr["Weeks"]);
                    item.Times2 = Convert.ToDateTime(dr["WeeksTime"]);
                    item.BookName = dr["BookName"].ToString();
                    item.TypeName = dr["TypeName"].ToString();

                    list.Add(item);
                }
            }

            return list;
        }

        //查询上一月小说的周点击量
        public List<BooksCountQuantity> getBooksCountMonth()
        {
            List<Model.BooksCountQuantity> list = new List<BooksCountQuantity>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETBOOKSCOUNTMONTH, null))
            {
                while (dr.Read())
                {
                    Model.BooksCountQuantity item = new BooksCountQuantity();
                    item.Id = Convert.ToInt32(dr["Id"]);
                    item.BookId = Convert.ToInt32(dr["BooksId"]);
                    item.Weeks = Convert.ToInt32(dr["Months"]);
                    item.Times2 = Convert.ToDateTime(dr["MonthsTime"]);
                    item.BookName = dr["BookName"].ToString();
                    item.TypeName = dr["TypeName"].ToString();

                    list.Add(item);
                }
            }

            return list;
        }



        //小说书票排行
        public List<Model.BooksInfo> getTicketCompositor()
        {
            List<Model.BooksInfo> list = new List<Model.BooksInfo>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETTICKETICKCOMPOSITOR, null))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new Model.BooksInfo();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.ticketNumber = Convert.ToInt32(dr["ticketNumber"]);
                    item.TypeName = dr["TypeName"].ToString();
                    list.Add(item);
                }
            }

            return list;
        }

        //小说鲜花排行
        public List<Model.BooksInfo> getFlowerCompositor()
        {
            List<Model.BooksInfo> list = new List<Model.BooksInfo>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETFLOWERCOMPOSITOR, null))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new Model.BooksInfo();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.ticketNumber = Convert.ToInt32(dr["FlowerNumber"]);
                    item.TypeName = dr["TypeName"].ToString();
                    list.Add(item);
                }
            }

            return list;
        }


        /// <summary>
        /// 新书介绍
        /// </summary>
        /// <param name="TypeId">书本类型ID</param>
        /// <returns></returns>
        public List<NewBooksIntroduce> getNewBooksIntroduce(int TypeId, int Size)
        {
            List<Model.NewBooksIntroduce> list = new List<NewBooksIntroduce>();
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@TypeId", TypeId), new SqlParameter("@Size", Size) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETNEWBOOKSINTRODUCE, parm))
            {
                while (dr.Read())
                {
                    Model.NewBooksIntroduce item = new NewBooksIntroduce();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.UserName = dr["UserName"].ToString();
                    item.Images = dr["Images"].ToString();
                    item.BookIntroduction = dr["BookIntroduction"].ToString();
                    item.AddTime = Convert.ToDateTime(dr["AddTime"]);
                    item.TypeName = dr["TypeName"].ToString();
                    item.StateName = dr["StateName"].ToString();

                    list.Add(item);
                }
            }
            return list;
        }


        /// <summary>
        /// 小说分类
        /// </summary>
        /// <returns></returns>
        public List<BookTypeInfo> getBookType()
        {
            List<Model.BookTypeInfo> list = new List<BookTypeInfo>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETBOOKSTYPE, null))
            {
                while (dr.Read())
                {
                    Model.BookTypeInfo item = new BookTypeInfo();
                    item.TypeId = Convert.ToInt32(dr["TypeId"]);
                    item.TypeName = dr["TypeName"].ToString();

                    list.Add(item);
                }
            }

            return list;
        }

        /// <summary>
        /// 新书信息
        /// </summary>
        /// <param name="typeId">小说类型Id</param>
        /// <returns></returns>
        public List<BooksInfo> getBookInfoss(int typeId)
        {
            List<Model.BooksInfo> list = new List<BooksInfo>();
            SqlParameter parm = new SqlParameter("@typeId", typeId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "getBooksType2", parm))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new BooksInfo();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.BookIntroduction = dr["BookIntroduction"].ToString();
                    item.Images = dr["images"].ToString();
                    item.UserName = dr["UserName"].ToString();

                    list.Add(item);
                }
            }
            return list;
        }

        //获取新书信息
        public List<BooksInfo> getBookInfoss2(int typeId)
        {
            List<Model.BooksInfo> list = new List<BooksInfo>();
            SqlParameter parm = new SqlParameter("@typeId", typeId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "getBooksType3", parm))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new BooksInfo();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.BookIntroduction = dr["BookIntroduction"].ToString();
                    item.TypeName = dr["TypeName"].ToString();
                    item.UserName = dr["UserName"].ToString();

                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取指定小说Id类型  ~
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public Model.BookTypeInfo getAppointTypeId(int typeId)
        {
            Model.BookTypeInfo item = new BookTypeInfo();
            SqlParameter parm = new SqlParameter("@typeId", typeId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETAPPOINTTYPEID, parm))
            {
                while (dr.Read())
                {
                    item.TypeId = Convert.ToInt32(dr["TypeId"]);
                    item.TypeName = dr["TypeName"].ToString();
                }
            }
            return item;
        }

        /// <summary>
        /// 获取指定小说类型推荐排行  ~
        /// </summary>
        /// <param name="typeId">TypeId</param>
        /// <returns></returns>
        public List<Model.BooksInfo> getAppointTypeBook(int typeId)
        {
            List<Model.BooksInfo> list = new List<BooksInfo>();
            SqlParameter parm = new SqlParameter("@typeId", typeId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETAPPOINTTYPEBOOK, parm))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new BooksInfo();
                    item.AllSumClick = dr["allsumClick"].ToString();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.TypeName = dr["TypeName"].ToString();
                    item.UserName = dr["UserName"].ToString();

                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取指定类型前4本推荐小说  ~
        /// </summary>
        /// <param name="typeid">TypeId</param>
        /// <returns></returns>
        public List<BooksInfo> getTypeIdUpFoure(int typeid)
        {
            List<Model.BooksInfo> list = new List<BooksInfo>();
            SqlParameter parm = new SqlParameter("@typeId", typeid);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETTYPEIDUPFOURE, parm))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new BooksInfo();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.Images = dr["Images"].ToString();
                    item.BookIntroduction = dr["BookIntroduction"].ToString();

                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取指定类型小说鲜花和数票总和排行榜前10  ~
        /// </summary>
        /// <param name="typeid">typeId</param>
        /// <returns></returns>
        public List<BooksInfo> getTicketOrFlower(int typeid)
        {
            List<Model.BooksInfo> list = new List<BooksInfo>();
            SqlParameter parm = new SqlParameter("@typeId", typeid);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETTICKETORFLOWER, parm))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new BooksInfo();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.UserName = dr["UserName"].ToString();
                    item.ticketNumber = Convert.ToInt32(dr["number"]);

                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取指定类型小说新书推荐按录入时间和点击量  ~
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        public List<BooksInfo> getNewBooksCommend(int typeid)
        {
            List<Model.BooksInfo> list = new List<BooksInfo>();
            SqlParameter parm = new SqlParameter("@typeId", typeid);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETNEWBOOKSCOMMEND, parm))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new BooksInfo();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.TypeName = dr["TypeName"].ToString();
                    item.UserName = dr["UserName"].ToString();
                    item.BookIntroduction = dr["BookIntroduction"].ToString();
                    item.Images = dr["Images"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        public List<BooksInfo> getMemory(int typeId)
        {
            List<Model.BooksInfo> list = new List<BooksInfo>();
            SqlParameter parm = new SqlParameter("@typeId", typeId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETMEMORY, parm))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new BooksInfo();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.UserName = dr["UserName"].ToString();
                    item.ticketNumber = Convert.ToInt32(dr["number"]);
                    item.Images = dr["Images"].ToString();

                    list.Add(item);
                }
            }
            return list;
        }




        public List<BooksInfo> getMemory3()
        {
            List<Model.BooksInfo> list = new List<BooksInfo>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETMEMORY3, null))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new BooksInfo();
                    item.AllSumClick = dr["TicketNumber"].ToString();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.BookIntroduction = dr["BookIntroduction"].ToString();
                    item.TypeName = dr["TypeName"].ToString();
                    item.UserName = dr["UserName"].ToString();
                    item.TypeId = Convert.ToInt32(dr["TypeId"]);

                    list.Add(item);
                }
            }
            return list;
        }

        public List<BooksInfo> getMemory2()
        {
            List<Model.BooksInfo> list = new List<BooksInfo>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETMEMORY2, null))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new BooksInfo();
                    item.AllSumClick = dr["FlowerNumber"].ToString();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.BookIntroduction = dr["BookIntroduction"].ToString();
                    item.TypeName = dr["TypeName"].ToString();
                    item.UserName = dr["UserName"].ToString();
                    item.TypeId = Convert.ToInt32(dr["TypeId"]);

                    list.Add(item);
                }
            }
            return list;
        }

        public List<BooksInfo> getAllAppoint()
        {
            List<Model.BooksInfo> list = new List<BooksInfo>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETALLAPPINT, null))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new BooksInfo();
                    item.AllSumClick = dr["allsumClick"].ToString();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.BookIntroduction = dr["BookIntroduction"].ToString();
                    item.TypeName = dr["TypeName"].ToString();
                    item.UserName = dr["UserName"].ToString();
                    item.TypeId = Convert.ToInt32(dr["TypeId"]);

                    list.Add(item);
                }
            }
            return list;
        }

        //获取一章书
        public List<SectionsInfo> getSelectedSections(int sectionId)
        {
            List<Model.SectionsInfo> list = new List<SectionsInfo>();
            SqlParameter parm = new SqlParameter("@sectionID",sectionId);
            using(SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction,CommandType.StoredProcedure,RPOC_GETSELECTEDSECTION,parm))
            {
                if(dr.Read())
                {
                    SectionsInfo item = new SectionsInfo();
                    item.BookName = dr["BookName"].ToString();
                    item.CharNum = Convert.ToInt32(dr["CharNum"]);
                    item.ValumeName = dr["ValumeName"].ToString();
                    item.UserName = dr["UserName"].ToString();
                    item.SectionTitle = dr["sectionTitle"].ToString();
                    list.Add(item);
                }
            }

            return list;
        }



        //搜索
        public List<SectionsInfo> NovelSearch(int pageIndex, int pageSize, int descnLen, string[] words, out int recordCount)
        {
            List<Model.SectionsInfo> list = new List<Model.SectionsInfo>();

            SqlParameter[] parms = new SqlParameter[4 + words.Length];
            parms[0] = new SqlParameter("@pageIndex", pageIndex);
            parms[1] = new SqlParameter("@pageSize", pageSize);
            parms[2] = new SqlParameter("@descnLen", descnLen);
            parms[3] = new SqlParameter("@recordCount", SqlDbType.Int, 4);
            parms[3].Direction = ParameterDirection.Output;

            for (int i = 0; i < words.Length; i++)
            {
                parms[4 + i] =
                    new SqlParameter("@word" + (i + 1), words[i]);
            }

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_SEARCHNOVEL, parms))
            {
                while (dr.Read())
                {
                    SectionsInfo si = new SectionsInfo();
                    si.BookId = Convert.ToInt32(dr["BookId"]);
                    si.BookName = dr["BookName"].ToString();
                    si.BookIntroduction = dr["descn"].ToString();
                    si.BookImage = dr["Images"].ToString();
                    si.ShortAddTime = Convert.ToDateTime(dr["AddTime"]).ToShortDateString();
                    si.TypeName = dr["TypeName"].ToString();
                    si.StateName = dr["StateName"].ToString();
                    si.UserName = dr["UserName"].ToString();
                    list.Add(si);
                }
            }
            recordCount = Convert.ToInt32(parms[3].Value);
            return list;
        }



        //读取章节信息
        private static void NewMethod(List<Model.BookNumberTable> list, SqlDataReader dr)
        {
            while (dr.Read())
            {
                Model.BookNumberTable item = new BookNumberTable();
                item.BookId = Convert.ToInt32(dr["BookId"]);
                item.BookName = dr["BookName"].ToString();
                item.Sections = dr["ValumeName"].ToString() + "/" + dr["SectionTitle"].ToString();
                item.UserName = dr["UserName"].ToString();
                item.TypeName = dr["TypeName"].ToString();
                item.State = dr["StateName"].ToString();
                item.SectiuonId = dr["SectiuonId"].ToString();
                item.UpdateTime = Convert.ToDateTime(dr["AddTime"]).ToShortDateString();

                list.Add(item);
            }
        }

        //获取章节点击排行按类型
        public List<Model.BookNumberTable> getBookNumberTable(int pageIndex, int pageSize, out int RecordCount, int typeId)
        {
            List<Model.BookNumberTable> list = new List<BookNumberTable>();
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@typeId", typeId),
                new SqlParameter("@pageIndex",pageIndex),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@recordCount",SqlDbType.Int,4)
            };

            parm[3].Direction = ParameterDirection.Output;


            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETBOOKNUMBERTABLE, parm))
            {
                NewMethod(list, dr);
                dr.Close();
            }

            RecordCount = Convert.ToInt32(parm[3].Value);
            return list;
        }

        //获取章节排行
        public List<Model.BookNumberTable> getAllBookNumberTable(int pageIndex, int pageSize, out int RecordCount)
        {
            List<Model.BookNumberTable> list = new List<BookNumberTable>();
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@pageIndex",pageIndex),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@recordCount",SqlDbType.Int,4)
            };
            parm[2].Direction = ParameterDirection.Output;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETALLBOOKNUMBERTABLE, parm))
            {
                NewMethod(list, dr);
                dr.Close();
            }

            RecordCount = Convert.ToInt32(parm[2].Value);
            return list;
        }



        //按用户筛选书库 
        public List<BooksInfo> getSelectBooks(string state, int bookType, int minCharnum, int maxCharnum, int pageIndex, int pageSize, out int recordCount)
        {

            List<BooksInfo> list = new List<BooksInfo>();

            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@statetype",state),
                new SqlParameter("@booktype",bookType),
                new SqlParameter("@minCharNum",minCharnum),
                new SqlParameter("@maxCharNum",maxCharnum),
                new SqlParameter("@pageIndex",pageIndex),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@RecordCount",SqlDbType.Int,4)
            };

            parm[6].Direction = ParameterDirection.Output;


            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETSELECTBOOKS, parm))
            {
                while (dr.Read())
                {
                    BooksInfo item = new BooksInfo();
                    item.StateName = dr["StateName"].ToString();
                    item.AllSumClick = dr["SumCharNum"].ToString();
                    item.BookName = dr["BookName"].ToString();
                    item.BookId = Convert.ToInt32(dr["bookid"]);
                    item.UserName = dr["UserName"].ToString();
                    item.TypeName = dr["TypeName"].ToString();
                    item.AddTimeString = Convert.ToDateTime(dr["addtime"]).ToShortDateString();
                    item.LastSection = dr["SectionTitle"].ToString();
                    list.Add(item);
                }
            }

            try
            {
                recordCount = Convert.ToInt32(parm[6].Value);
            }
            catch
            {
                recordCount = 0;
            }

            return list;
        }


        /// <summary>
        /// 获取省份信息 ~
        /// </summary>
        /// <returns></returns>
        public List<ProvinceInfo> getProvince()
        {
            List<Model.ProvinceInfo> list = new List<ProvinceInfo>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETPROVINCE, null))
            {
                while (dr.Read())
                {
                    Model.ProvinceInfo item = new ProvinceInfo();
                    item.id = Convert.ToInt32(dr["id"]);
                    item.province = dr["province"].ToString();
                    item.provinceID = dr["provinceID"].ToString();

                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取市区信息 ~
        /// </summary>
        /// <returns></returns>
        public List<AreaInfo> getArea(string provinceID)
        {
            List<Model.AreaInfo> list = new List<AreaInfo>();
            SqlParameter parm = new SqlParameter("@provinceID", provinceID);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETAREA, parm))
            {
                while (dr.Read())
                {
                    Model.AreaInfo item = new AreaInfo();
                    item.id = Convert.ToInt32(dr["id"]);
                    item.cityID = dr["cityID"].ToString();
                    item.city = dr["city"].ToString();
                    item.father = dr["father"].ToString();

                    list.Add(item);
                }
            }
            return list;
        }

        public string getProvinceID(int id)
        {
            string provinceID = "";
            SqlParameter parm = new SqlParameter("@id", id);
            provinceID = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETPROVINCEID, parm).ToString();
            return provinceID;
        }

        //根据指定BookId查询
        public Model.BooksInfo getBookIdBooksInfo(int bookId)
        {
            Model.BooksInfo list = new BooksInfo();
            SqlParameter parm = new SqlParameter("@BookId", bookId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETBOOKIDBOOKSINFO, parm))
            {
                while (dr.Read())
                {
                    list.BookId = Convert.ToInt32(dr["BookId"]);
                    list.BookName = dr["BookName"].ToString();
                    list.UserName = dr["UserName"].ToString();
                    list.BookIntroduction = dr["BookIntroduction"].ToString();
                    list.AddTime = Convert.ToDateTime(dr["AddTime"]);
                    list.TypeName = dr["TypeName"].ToString();
                    list.StateName = dr["StateName"].ToString();
                    list.BookType = Convert.ToInt32(dr["booktype"]);
                    list.BookState = Convert.ToInt32(dr["bookstate"]);
                }
            }
            return list;
        }

        //显示书本章节
        public BooksInfo getSectionsInfo(int SectionsId)
        {
            Model.BooksInfo item = new BooksInfo();
            SqlParameter parm = new SqlParameter("@SectionsId", SectionsId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETSECTIONSINFO, parm))
            {
                while (dr.Read())
                {
                    item.SectiuonId = Convert.ToInt32(dr["SectiuonId"]);
                    item.SectionTitle = dr["SectionTitle"].ToString();
                    item.Contents = dr["Contents"].ToString();
                    item.ValumeName = dr["ValumeName"].ToString();
                    item.VolumeId = Convert.ToInt32(dr["VolumeId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.AddTime = Convert.ToDateTime(dr["AddTime"]);
                    item.UserName = dr["UserName"].ToString();
                }
            }
            return item;
        }

        //下一章
        public BooksInfo getNextSections(int SectionsId, int bookId)
        {
            Model.BooksInfo item = null;
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@SectionsId", SectionsId), new SqlParameter("@bookId", bookId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETNEXTSECTIONS, parm))
            {
                while (dr.Read())
                {
                    item = new Model.BooksInfo();
                    item.SectiuonId = Convert.ToInt32(dr["SectiuonId"]);
                    item.SectionTitle = dr["SectionTitle"].ToString();
                    item.Contents = dr["Contents"].ToString();
                    item.ValumeName = dr["ValumeName"].ToString();
                    item.VolumeId = Convert.ToInt32(dr["VolumeId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.AddTime = Convert.ToDateTime(dr["AddTime"]);
                    item.UserName = dr["UserName"].ToString();
                }
            }
            return item;
        }

        //上一章
        public BooksInfo getAnteSections(int SectionsId, int bookId)
        {
            Model.BooksInfo item = null;
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@SectionsId", SectionsId), new SqlParameter("@bookId", bookId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETANTESECTIONS, parm))
            {
                while (dr.Read())
                {
                    item = new Model.BooksInfo();
                    item.SectiuonId = Convert.ToInt32(dr["SectiuonId"]);
                    item.SectionTitle = dr["SectionTitle"].ToString();
                    item.Contents = dr["Contents"].ToString();
                    item.ValumeName = dr["ValumeName"].ToString();
                    item.VolumeId = Convert.ToInt32(dr["VolumeId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.AddTime = Convert.ToDateTime(dr["AddTime"]);
                    item.UserName = dr["UserName"].ToString();
                }
            }
            return item;
        }

        //获取用户书架
        public List<Model.UserBookShelfInfo> getUserBookShelf(int userId, int pageindex, int pagesize, int booktype, out int recordCount)
        {
            List<Model.UserBookShelfInfo> list = new List<UserBookShelfInfo>();
            SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@userId",userId),new SqlParameter("@pageindex",pageindex),new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@booktype",booktype),new SqlParameter("@recordCount",SqlDbType.Int,4)
            };
            parm[4].Direction = ParameterDirection.Output;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETUSERBOOKSHELF, parm))
            {
                while(dr.Read())
                {
                    Model.UserBookShelfInfo item = new UserBookShelfInfo();
                    item.BookName = dr["BookName"].ToString();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.Images = dr["images"].ToString();
                    list.Add(item);
                }
            }

            try
            {
                recordCount = Convert.ToInt32(parm[4].Value);
            }
            catch
            {
                recordCount = 0;
            }
            return list;
        }

        //往书架添加书
        public int adduserBookShelf(int userId, int bookID)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@userId", userId), new SqlParameter("bookId", bookID) };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_ADDUSERBOOKSHELF, parm);
        }

        //删除书架内书
        public int delUserbookShelf(int userId, int bookId)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@userId", userId), new SqlParameter("@bookId", bookId) };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_DELUSERBOOKSHELF, parm);
        }
        //订阅连载卡使用,加入连载表，减去道具包里1个数量，如果数量=1,执行删除，>1执行更新
        public int userAfterBook(int userId, int bookId)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("userId", userId), new SqlParameter("bookId", bookId) };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_USERAFTERBOOK, parm);
        }
        //VIP卡使用,修改UserType,看有没有道具，并减去道具包里1个数量，如果数量=1,执行删除，>1执行更新，要改
        public int userVIPCard(int userId)
        {
            SqlParameter parm = new SqlParameter("@userId", userId);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_USERVIPCARD, parm);
        }

        //判断是否订阅
        public int isReaddingBook(int userid, int bookid)
        {
            List<Model.AfterBookInfo> list = new List<AfterBookInfo>();

            SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@userId",userid),
                new SqlParameter("@bookId",bookid)
            };

            using(SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction,CommandType.StoredProcedure,"isreaddingbook",parm))
            {
                if(dr.Read())
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }




        /// <summary>
        /// 获取小说封面基本信息
        /// </summary>
        /// <param name="BookId"></param>
        /// <returns></returns>
        public BooksInfo getBookCoverInfo(int BookId)
        {
            Model.BooksInfo item = new BooksInfo();
            SqlParameter parm = new SqlParameter("@BookId", BookId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_getBookCoverInfo, parm))
            {
                if (dr.Read())
                {
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.UserName = dr["UserName"].ToString();
                    item.BookIntroduction = dr["BookIntroduction"].ToString();
                    item.AddTime = Convert.ToDateTime(dr["AddTime"]);
                    item.TypeName = dr["TypeName"].ToString();
                    item.StateName = dr["StateName"].ToString();
                    item.Images = dr["Images"].ToString();
                    item.AllSumClick = dr["allsumClick"].ToString();
                    item.ticketNumber = Convert.ToInt32(dr["TicketNumber"]);
                    item.flowerNumber = Convert.ToInt32(dr["FlowerNumber"]);
                }
            }
            return item;
        }

        /// <summary>
        /// 根据书本ID查看该书本所得书票和鲜花的详细信息
        /// </summary>
        /// <param name="BookId"></param>
        /// <returns></returns>
        public List<UserPropInfo2> getUserPropInfo(int BookId)
        {
            List<Model.UserPropInfo2> list = new List<UserPropInfo2>();
            SqlParameter parm = new SqlParameter("@BookId", BookId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETUSERPROPINFO, parm))
            {
                while (dr.Read())
                {
                    Model.UserPropInfo2 item = new UserPropInfo2();
                    item.UserName = dr["UserName"].ToString();
                    item.BookName = dr["BookName"].ToString();
                    item.bookId = Convert.ToInt32(dr["bookId"]);
                    item.PropName = dr["PropName"].ToString();
                    item.propId = Convert.ToInt32(dr["PropId"]);
                    item.propAmount = Convert.ToInt32(dr["propAmount"]);
                    item.propTime = Convert.ToDateTime(dr["propTime"]).ToShortDateString();

                    list.Add(item);
                }
            }
            return list;
        }
        /// <summary>
        /// 查询指定小说ID的最后一章
        /// </summary>
        /// <param name="BookId"></param>
        /// <returns></returns>
        public BooksInfo getBookFinallySection(int BookId)
        {
            Model.BooksInfo item = new BooksInfo();
            SqlParameter parm = new SqlParameter("@BookId", BookId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETBOOKFINALLYSECTION, parm))
            {
                if (dr.Read())
                {
                    item.SectiuonId = Convert.ToInt32(dr["SectiuonId"]);
                    item.SectionTitle = dr["SectionTitle"].ToString();
                    item.ValumeName = dr["ValumeName"].ToString();
                }
            }
            return item;
        }

        //根据小说ID查看该小说书评
        public List<BookReplayInfo> getBookReplayInfo(int BookId)
        {
            List<Model.BookReplayInfo> list = new List<BookReplayInfo>();
            SqlParameter parm = new SqlParameter("BookId", BookId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETBOOKREPLAYINFO, parm))
            {
                while (dr.Read())
                {
                    Model.BookReplayInfo item = new BookReplayInfo();
                    item.ReplayContext = dr["ReplayContext"].ToString();
                    item.ReplayTime = Convert.ToDateTime(dr["ReplayTime"]).ToShortDateString();
                    item.UserName = dr["UserName"].ToString();
                    item.BookName = dr["BookName"].ToString();
                    item.ReplayBookId = Convert.ToInt32(dr["ReplayBookId"]);
                    item.UsetImage = dr["UsetImage"].ToString();
                    item.UserTypeName = dr["UserTypeName"].ToString();

                    list.Add(item);
                }
            }

            return list;
        }
        //获取指定书名ID的书票排行和书票数
        public BooksInfo getTicketNumber(int BookId)
        {
            Model.BooksInfo item = new BooksInfo();
            SqlParameter parm = new SqlParameter("@BookId", BookId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETTICKETNUMBER, parm))
            {
                if (dr.Read())
                {
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.ticketNumber = Convert.ToInt32(dr["TicketNumber"]);
                    item.flowerNumber = Convert.ToInt32(dr["rowNumber"]);
                }
            }
            return item;
        }
        //获取指定书名ID的鲜花排行和书鲜花
        public BooksInfo getFlowerNumber(int BookId)
        {
            Model.BooksInfo item = new BooksInfo();
            SqlParameter parm = new SqlParameter("@BookId", BookId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETFLOWERNUMBER, parm))
            {
                if (dr.Read())
                {
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.flowerNumber = Convert.ToInt32(dr["FlowerNumber"]);
                    item.ticketNumber = Convert.ToInt32(dr["rowNumber"]);
                }
            }
            return item;
        }
        //给指定BookId添加书票
        public int insertTicket(int userId, int bookId, int propId, int propAmount, out int whenHave)
        {
            SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@userId",userId),
                new SqlParameter("@bookId",bookId),
                new SqlParameter("@propId",propId),
                new SqlParameter("@propAmount",propAmount),
                new SqlParameter("@whenHave",SqlDbType.Int, 4)
            };
            parm[4].Direction = ParameterDirection.Output;

            int i = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_INSERTTICKET, parm);
            whenHave = Convert.ToInt32(parm[4].Value);
            return i;
        }
        //根据作者ID查看该作者所有作品
        public BooksInfo getAuthorIdBooksOne(int bookid)
        {
            Model.BooksInfo item = new BooksInfo();
            SqlParameter parm = new SqlParameter("@bookID", bookid);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETAUTHORIDBOOKSONE, parm))
            {
                if (dr.Read())
                {
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.Images = dr["Images"].ToString();
                    item.TypeName = dr["TypeName"].ToString();
                    item.BookIntroduction = dr["BookIntroduction"].ToString();
                    item.AuthorId = Convert.ToInt32(dr["AuthorId"]);
                }
            }

            return item;
        }

        public List<Model.BooksInfo> getAuthorIdBooks(int userId)
        {
            List<Model.BooksInfo> list = new List<BooksInfo>();
            SqlParameter parm = new SqlParameter("@userId", userId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETAUTHORIDBOOKS, parm))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new BooksInfo();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.TypeName = dr["TypeName"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        //查看某一章节是否属于VIP章节
        public List<SectionsInfo> sectionsVolumeNumber(int setionID)
        {
            List<SectionsInfo> list = new List<SectionsInfo>();
            SqlParameter parm = new SqlParameter("@sectionID",setionID);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "sectionsVolumeId", parm))
            {
                if (dr.Read())
                {
                    Model.SectionsInfo item = new SectionsInfo();
                    item.VolumeId = Convert.ToInt32(dr["ValumeNumber"]);
                    list.Add(item);
                }
            }
            return list;
        }


        //章节点击加1
        public void sectionClickCount(int sectionId)
        {
            SqlParameter parm = new SqlParameter("@setioncId",sectionId);
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction,CommandType.StoredProcedure,"sectionClickCount",parm);
        }


        //获取本站所有新闻
        public List<NovelNewsInfo> getNovelNewss(out int recordCount, int pageSize, int pageIndex)
        {
            List<Model.NovelNewsInfo> list = new List<NovelNewsInfo>();

            SqlParameter[] parms = new SqlParameter[]{
                new SqlParameter("@recordCout",SqlDbType.Int,4),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@recordIndex",pageIndex)
            };

            parms[0].Direction = ParameterDirection.Output;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETNOVELNEWSS, parms))
            {
                while (dr.Read())
                {
                    Model.NovelNewsInfo item = new NovelNewsInfo();
                    item.NewsId = Convert.ToInt32(dr["NewsId"]);
                    item.NewsTitle = dr["NewsTitle"].ToString();
                    item.NewsContens = dr["NewsContens"].ToString();
                    item.NewsImages = dr["NewsImages"].ToString();
                    item.FromWhere = dr["FromWhere"].ToString();
                    item.AddTimeString = Convert.ToDateTime(dr["AddTime"]).ToShortDateString();

                    list.Add(item);
                }
            }
            recordCount = Convert.ToInt32(parms[0].Value);
            return list;
        }

        //删除新闻
        public int deleteNovelNews(int NewsId)
        {
            SqlParameter pram = new SqlParameter("@NewsId", NewsId);
            try
            {
                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_DELETENOVELNEWS, pram);
            }
            catch
            {
                
                return 0;

            }
        }

        //添加新闻
        public int addNovelNews(Model.NovelNewsInfo item)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@NewsTitle",item.NewsTitle),new SqlParameter("@NewsContens",item.NewsContens),
                new SqlParameter("@NewsImages",item.NewsImages),new SqlParameter("@FromWhere",item.FromWhere)
            };
            try
            {
                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_ADDNOVELNEWS, parm);
            }
            catch
            {
                return 0;
            }
        }
        //修改新闻
        public int updateNovelNews(Model.NovelNewsInfo item)
        {

            SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@NewsId",item.NewsId),new SqlParameter("@NewsTitle",item.NewsTitle),new SqlParameter("@NewsContens",item.NewsContens),
                new SqlParameter("@NewsImages",item.NewsImages),new SqlParameter("@FromWhere",item.FromWhere),new SqlParameter("@AddTime",item.AddTime)
            };
            try
            {
                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_UPDATENOVELNEWS, parm);
            }
            catch
            {
                return 0;
            }
        }
        //查看所有非完本小说
        public List<BooksInfo> getBooks(out int recordCount, int pageSize, int pageIndex, int bookType)
        {
            List<Model.BooksInfo> list = new List<BooksInfo>();
            SqlParameter[] parms = new SqlParameter[]{
                new SqlParameter("@recordCout",SqlDbType.Int,4),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@recordIndex",pageIndex),
                new SqlParameter("@BookType", bookType)
            };
            parms[0].Direction = ParameterDirection.Output;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETBOOKS, parms))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new BooksInfo();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.UserName = dr["UserName"].ToString();
                    item.Images = dr["Images"].ToString();
                    item.BookIntroduction = dr["BookIntroduction"].ToString();
                    item.AddTime = Convert.ToDateTime(dr["AddTime"]);
                    item.StateName = dr["StateName"].ToString();
                    item.TypeName = dr["TypeName"].ToString();
                    item.Recommand = Convert.ToInt32(dr["Recommand"]);

                    list.Add(item);
                }
            }
            recordCount = Convert.ToInt32(parms[0].Value);
            return list;
        }

        //获取会员
        public List<UsersInfo> getUser()
        {
            List<Model.UsersInfo> list = new List<UsersInfo>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETUSER, null))
            {
                while (dr.Read())
                {
                    Model.UsersInfo item = new UsersInfo();
                    item.UserName = dr["UserName"].ToString();
                    item.UserId = Convert.ToInt32(dr["UserId"]);

                    list.Add(item);
                }
            }
            return list;
        }
        //添加新书
        public int addBook(BooksInfo item)
        {
            SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@BookName",item.BookName),
                new SqlParameter("@AuthorId",item.AuthorId),
                new SqlParameter("@Images",item.Images),
                new SqlParameter("@BookIntroduction",item.BookIntroduction),
                new SqlParameter("@BookType",item.BookType),
            };
            try
            {
                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_ADDBOOK, parm);
            }
            catch
            {
                return 0;
            }
        }
        //添加新卷
        public int addVolume(VolumeInfo item)
        {
            SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@BookId",item.BookId),new SqlParameter("@ValumeName",item.ValumeName),
            };

            try
            {
                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_ADDVLOUME, parm);
            }
            catch
            {
                return 0;
            }
        }
        //添加新章
        public int addSections(SectionsInfo item)
        {
            SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@VolumeId",item.VolumeId),new SqlParameter("@SectionTitle",item.SectionTitle),new SqlParameter("@CharNum",item.CharNum),new SqlParameter("@Contents",item.Contents),
            };

            try
            {
                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_ADDSECTIONS, parm);
            }
            catch
            {
                return 0;
            }
        }


        //获取指定小说ID最后一卷ID
        public int AddBookIdSections(int BookId)
        {
            SqlParameter parm = new SqlParameter("@BookId", BookId);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_ADDBOOKIDSECTIONS, parm));
        }
        //查看所有未被禁用的用户
        public List<UsersInfo> getUserInfoAll(int userType)
        {
            List<Model.UsersInfo> list = new List<UsersInfo>();
            SqlParameter parm = new SqlParameter("@userType", userType);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_FORBIDUSERALL, parm))
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
        //查看所有未被推荐的小说
        public List<BooksInfo> GetAllNoRecommand(out int recordCount, int pageSize, int pageIndex, int bookType)
        {
            List<Model.BooksInfo> list = new List<BooksInfo>();
            SqlParameter[] parms = new SqlParameter[]{
                new SqlParameter("@recordCout",SqlDbType.Int,4),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@recordIndex",pageIndex),
                new SqlParameter("@BookType", bookType)
            };
            parms[0].Direction = ParameterDirection.Output;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETALLNORECOMMAND, parms))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new BooksInfo();
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.UserName = dr["UserName"].ToString();
                    item.Images = dr["Images"].ToString();
                    item.BookIntroduction = dr["BookIntroduction"].ToString();
                    item.AddTimeString = Convert.ToDateTime(dr["AddTime"]).ToShortDateString();
                    item.StateName = dr["StateName"].ToString();
                    item.TypeName = dr["TypeName"].ToString();
                    item.Recommand = Convert.ToInt32(dr["Recommand"]);

                    list.Add(item);
                }
            }
            recordCount = Convert.ToInt32(parms[0].Value);
            return list;
        }
        //推荐小说
        public int Recommand(int bookId)
        {
            SqlParameter pram = new SqlParameter("@bookId", bookId);
            try
            {
                return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_RECOMMAND, pram);
            }
            catch
            {
                return 0;
            }
        }
        //根据BookId查看该书所有书评
        public List<BookReplayInfo> getBookIdReplay(out int recordCount, int pageSize, int pageIndex, int bookId)
        {
            SqlParameter[] parms = new SqlParameter[]{
                new SqlParameter("@recordCout",SqlDbType.Int,4),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@recordIndex",pageIndex),
                new SqlParameter("@bookId", bookId)
            };
            parms[0].Direction = ParameterDirection.Output;
            List<Model.BookReplayInfo> list = new List<BookReplayInfo>();

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_GETBOOKIDREPLAY, parms))
            {
                while (dr.Read())
                {
                    Model.BookReplayInfo item = new BookReplayInfo();
                    item.ReplayId = Convert.ToInt32(dr["ReplayId"]);
                    item.ReplayContext = dr["ReplayContext"].ToString();
                    item.ReplayTime = dr["ReplayTime"].ToString();
                    item.UserName = dr["UserName"].ToString();

                    list.Add(item);
                }
            }
            recordCount = Convert.ToInt32(parms[0].Value);
            return list;
        }

        public int DelBookReplay(int ReplayId)
        {
            SqlParameter parm = new SqlParameter("@ReplayId", ReplayId);

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_DELBOOKREPLAY, parm);
        }


        //取消推荐小说
        public int noRecommand(int bookId)
        {
            SqlParameter parm = new SqlParameter("@bookId", bookId);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, PROC_NORECOMMAND, parm);
        }
        
    }
}
