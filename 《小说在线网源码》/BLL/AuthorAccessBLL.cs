using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class AuthorAccessBLL : IDAL.IAuthor
    {

        IDAL.IAuthor IA = DalFactory.DateAccess.CreateIAuthor();
        //获取作者作品
        public List<Model.BooksInfo> getAuthorBooks(int AuthorId)
        {
            return IA.getAuthorBooks(AuthorId);
        }

        //根据书本ID获取书本卷
        public List<Model.VolumeInfo> getBookVolumes(int AuthorId, int bookId)
        {
            return IA.getBookVolumes(AuthorId, bookId);
        }

        //根据卷获得章
        public List<Model.SectionsInfo> getVolumeSecionts(int pageindex, int pagesize, int volumenid, out int recordCount)
        {
            return IA.getVolumeSecionts(pageindex, pagesize, volumenid,out recordCount);
        }


        //修改书本信息
        public int EditBookFace(Model.BooksInfo item)
        {
            return IA.EditBookFace(item);
        }

        //修改卷信息
        public int EidtVolume(string volumeName, int bookId, int volumeID)
        {
            return IA.EidtVolume(volumeName, bookId, volumeID);
        }

        //添加卷
        public int AuaddVolume(int bookId, string valumeName, int valumeNumber)
        {
            return IA.AuaddVolume(bookId,valumeName,valumeNumber);
        }

        public int AudelVolume(int volumeId)
        {
            return IA.AudelVolume(volumeId);
        }

        public int Auaddsection(int volumeId, string sectionTitle, int charnum, string Context)
        {
            return IA.Auaddsection(volumeId,sectionTitle,charnum,Context);
        }

        public int Audelsection(int section)
        {
            return IA.Audelsection(section);
        }


        //获取审核章节
        public List<Model.SectionsInfo> getExsetions(int userid)
        {
            return IA.getExsetions(userid);
        }



        //添加新书
        public int AuaddBook(Model.BooksInfo item)
        {
            return IA.AuaddBook(item);
        }

        //获取审核作者列表
        public List<Model.UsersInfo> getCheckAuthor(int pageindex,int pagesize,out int recordCount)
        {
            return IA.getCheckAuthor(pageindex,pagesize,out recordCount);
        }

        //作者审核通过
        public int AuthorCheckYes(int userId)
        {
            return IA.AuthorCheckYes(userId);
        }
        //作者审核批量通过
        public int AuthorCheckYes(int[] userId)
        {
            return IA.AuthorCheckYes(userId);
        }
        //作者审核不通过
        public int AuthorCheckNo(int userId)
        {
            return IA.AuthorCheckNo(userId);
        }
        //作者审核批量不通过
        public int AuthorCheckNo(int[] userId)
        {
            return IA.AuthorCheckNo(userId);
        }

        //删除一本小说
        public int delbook(int bookid)
        {
            IDAL.IAuthor ia = DalFactory.DateAccess.CreateIAuthor();
            return ia.delbook(bookid);
        }

        //获取审核章节
        public List<Model.SectionsInfo> getCheckSections(int pageindex, int pagesize, out int recoredCount)
        {
            return IA.getCheckSections(pageindex,pagesize,out recoredCount);
        }

        //章节审核通过
        public int SectionCheckYES(int sectionId, int bookId, string bookName)
        {
            return IA.SectionCheckYES(sectionId,bookId,bookName);
        }

        //章节审核不通过
        public int SectionCheckNo(int sectionId)
        {
            return IA.SectionCheckNo(sectionId);
        }


    }
}
