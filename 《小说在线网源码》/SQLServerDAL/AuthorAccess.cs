using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SQLServerDAL
{
    public class AuthorAccess : IDAL.IAuthor
    {
        //根据作者ID获取作者作品
        public List<Model.BooksInfo> getAuthorBooks(int AuthorId)
        {

            List<Model.BooksInfo> list = new List<Model.BooksInfo>();
            SqlParameter parm = new SqlParameter("@AuthorId",AuthorId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "getAuthorBooks", parm))
            {
                while (dr.Read())
                {
                    Model.BooksInfo item = new Model.BooksInfo();
                    item.BookId = Convert.ToInt32(dr["bookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.BookType = Convert.ToInt32(dr["BookType"]);
                    item.BookState = Convert.ToInt32(dr["BookState"]);
                    item.Images = dr["Images"].ToString();

                    list.Add(item);
                }
            }
            return list;
        }

        //根据书本ID获取书本卷
        public List<Model.VolumeInfo> getBookVolumes(int AuthorId, int bookId)
        {
            List<Model.VolumeInfo> list = new List<Model.VolumeInfo>();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@AuthorId",AuthorId),
                new SqlParameter("@bookId",bookId)
            };

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "getBookVolumes", parm))
            {
                while (dr.Read())
                {
                    Model.VolumeInfo item = new Model.VolumeInfo();
                    item.ValumeName = dr["valumeName"].ToString();
                    item.VolumeId = Convert.ToInt32(dr["volumeId"]);
                    item.ValumeNumber = Convert.ToInt32(dr["ValumeNumber"]);
                    item.BookId = Convert.ToInt32(dr["BookId"]);
                    item.BookName = dr["BookName"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }


        //根据卷获得章
        public List<Model.SectionsInfo> getVolumeSecionts(int pageindex, int pagesize, int volumenid, out int recordCount)
        {
            List<Model.SectionsInfo> list = new List<Model.SectionsInfo>();
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pagesize",pagesize),
                new SqlParameter("@volumenid",volumenid),
                new SqlParameter("@recoredCount",SqlDbType.Int,4)
            };

            parm[3].Direction = ParameterDirection.Output;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "getVolumeSecionts", parm))
            {
                while (dr.Read())
                {
                    Model.SectionsInfo item = new Model.SectionsInfo();
                    item.SectiuonId = Convert.ToInt32(dr["SectiuonId"]);
                    item.VolumeId = Convert.ToInt32(dr["VolumeId"]);
                    item.SectionTitle = dr["SectionTitle"].ToString();
                    item.CharNum = Convert.ToInt32(dr["CharNum"]);
                    list.Add(item);
                }
            }

            recordCount = Convert.ToInt32(parm[3].Value);
            return list;
        }


        //修改书本信息
        public int EditBookFace(Model.BooksInfo item)
        {
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@bookId",item.BookId),
                new SqlParameter("@bookName",item.BookName),
                new SqlParameter("@BookIntroduction",item.BookIntroduction),
                new SqlParameter("@bookState",item.BookState),
                new SqlParameter("@booktType",item.BookType)
             };

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "EditBookFace", parm);
        }



        //修改卷信息
        public int EidtVolume(string volumeName, int bookId, int volumeID)
        {
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@bookId",bookId),
                new SqlParameter("@volumeName",volumeName),
                new SqlParameter("@ValuemeNumber",volumeID)
             };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "EditVolumeName", parm);
        }

        
        //添加卷
        public int AuaddVolume(int bookId, string valumeName, int valumeNumber)
        {
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@bookid",bookId),
                new SqlParameter("@valumeName",valumeName),
                new SqlParameter("@valumeNumber",valumeNumber)
             };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "AuaddVolume", parm);
        }

        //删除卷
        public int AudelVolume(int volumeId)
        {
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@volumeId",volumeId)
             };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "AudelVolume", parm);
        }

        //添加章
        public int Auaddsection(int volumeId, string sectionTitle, int charnum, string Context)
        {
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@volumeId",volumeId),
                new SqlParameter("@sectionTitle",sectionTitle),
                new SqlParameter("@charNum",charnum),
                new SqlParameter("@contents",Context)
             };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "Auaddsection", parm);
        }

        //删除章
        public int Audelsection(int section)
        {
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@section",section)
             };
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "AudelSection", parm);
        }

        //(作者专区)获取审核章节    
        public List<Model.SectionsInfo> getExsetions(int userid)
        {
            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@userid",userid)
             };

            List<Model.SectionsInfo> list = new List<Model.SectionsInfo>();

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "isExamine", parm))
            {
                while (dr.Read())
                {
                    Model.SectionsInfo item = new Model.SectionsInfo();
                    item.BookName = dr["BookName"].ToString();
                    item.CharNum = Convert.ToInt32(dr["CharNum"]);                    
                    item.SectionTitle = dr["sectionTitle"].ToString();
                    item.ValumeName = dr["ValumeName"].ToString();
                    item.ShortAddTime = Convert.ToDateTime(dr["addtime"]).ToShortDateString();
                    list.Add(item);
                }
            }
            return list;
        }

        //添加新书
        public int AuaddBook(Model.BooksInfo item)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@bookName",item.BookName),
                new SqlParameter("@authorId",item.AuthorId),
                new SqlParameter("@images",item.Images),
                new SqlParameter("@BookIntroDuction",item.BookIntroduction),
                new SqlParameter("@bookType",item.BookType)
            };

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "AuAddNewBook", parm);
        }

        //获取申请作者的审核列表
        public List<Model.UsersInfo> getCheckAuthor(int pageindex,int pagesize,out int recordCount)
        {
            List<Model.UsersInfo> list = new List<Model.UsersInfo>();

                SqlParameter[] parm = new SqlParameter[] {
                    new SqlParameter("@pageindex",pageindex),
                    new SqlParameter("@pagesize",pagesize),
                    new SqlParameter("@recordeCount",SqlDbType.Int,4)
                };
                parm[2].Direction = ParameterDirection.Output;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "getCheckAuthor", parm))
            {
                while (dr.Read())
                {
                    Model.UsersInfo item = new Model.UsersInfo();
                    item.UserName = dr["username"].ToString();
                    item.UserId = Convert.ToInt32(dr["userId"]);
                    item.UserSex = Convert.ToInt32(dr["userSex"]);
                    item.UserTypeName = dr["usertypeName"].ToString();                    
                    item.userAddress = dr["userAddress"].ToString();
                    item.IdentityCardNumber = dr["IdentityCardNumber"].ToString();
                    list.Add(item);
                }
            }

            recordCount = Convert.ToInt32(parm[2].Value);
            return list;
        }



        //作者审核通过
        public int AuthorCheckYes(int userId)
        {
            SqlParameter parm = new SqlParameter("@userid",userId);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "AuthorCheckYes", parm);
        }

        //作者审核批量通过
        public int AuthorCheckYes(int[] userId)
        {
            int count = 0;
            for(int i =0;i<userId.Length;i++)
            { 
                 SqlParameter parm = new SqlParameter("@userid",userId[i]);
                 SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "AuthorCheckYes", parm);
                 count++;
            }
            return count;
        }


        //作者审核不通过
        public int AuthorCheckNo(int userId)
        {
            SqlParameter parm = new SqlParameter("@userId", userId);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "AuthorCheckNo", parm);
        }

        //作者审核批量不通过
        public int AuthorCheckNo(int[] userId)
        {
            int count = 0;
            for (int i = 0; i < userId.Length; i++)
            {
                SqlParameter parm = new SqlParameter("@userId", userId[i]);
                SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "AuthorCheckNo", parm);
                count++;
            }
            return count;
        }

        //删除一本小说
        public int delbook(int bookid)
        {
            SqlParameter parm = new SqlParameter("@bookId", bookid);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "delbook", parm);
        }


        //(后台)获取要审核的章节
        public List<Model.SectionsInfo> getCheckSections(int pageindex, int pagesize, out int recoredCount)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@pageindex",pageindex),
                new SqlParameter("@pageSize",pagesize),
                new SqlParameter("@recordCount",SqlDbType.Int,4)
            };

            parm[2].Direction = ParameterDirection.Output;

            List<Model.SectionsInfo> list = new List<Model.SectionsInfo>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "getCheckSections", parm))
            {
                while (dr.Read())
                {
                    Model.SectionsInfo item = new Model.SectionsInfo();
                    item.BookId = Convert.ToInt32(dr["bookId"]);
                    item.BookName = dr["BookName"].ToString();
                    item.SectionTitle = dr["SectionTitle"].ToString();
                    item.Contents = dr["Contents"].ToString();
                    item.CharNum = Convert.ToInt32(dr["CharNum"]);
                    item.ShortAddTime = Convert.ToDateTime(dr["addtime"]).ToShortDateString();
                    item.StateName = dr["stateName"].ToString();
                    item.SectiuonId = Convert.ToInt32(dr["SectiuonId"]);
                    item.BookId = Convert.ToInt32(dr["BookId"]);

                    list.Add(item);
                }
            }
            recoredCount = Convert.ToInt32(parm[2].Value);
            return list;
        }


        //章节审核通过
        public int SectionCheckYES(int sectionId, int bookId, string bookName)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@sectionId",sectionId),
                new SqlParameter("@bookId",bookId),
                new SqlParameter("@bookName",bookName)
            };

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "SectionCheckYES", parm);
        }


        //章节审核不通过
        public int SectionCheckNo(int sectionId)
        {
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@sectionsId",sectionId)
            };

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "SectionCheckNo", parm);
        }

    }
}
