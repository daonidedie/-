using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDAL
{
    public interface IAuthor
    {
        //获取作者作品
        List<Model.BooksInfo> getAuthorBooks(int AuthorId);

        //根据书本ID获取书本卷
        List<Model.VolumeInfo> getBookVolumes(int AuthorId, int bookId);

        //根据卷获得章
        List<Model.SectionsInfo> getVolumeSecionts(int pageindex,int pagesize,int volumenid,out int recordCount);

        //修改书本信息
        int EditBookFace(Model.BooksInfo item);

        //修改卷信息
        int EidtVolume(string volumeName, int bookId, int volumeID);

        //添加卷
        int AuaddVolume(int bookId, string valumeName, int valumeNumber);
        
        //删除卷
        int AudelVolume(int volumeId);

        //添加章节
        int Auaddsection(int volumeId, string sectionTitle, int charnum, string Context);

        //删除章节
        int Audelsection(int section);

        //获取审核章节
        List<Model.SectionsInfo> getExsetions(int userid);

        //添加新书
        int AuaddBook(Model.BooksInfo item);

        //获取申请作者列表
        List<Model.UsersInfo> getCheckAuthor(int pageindex,int pagesize,out int recordCount);

        //作者审核通过
        int AuthorCheckYes(int userId);
        int AuthorCheckYes(int[] userId);

        //作者审核不通过
        int AuthorCheckNo(int userId);
        int AuthorCheckNo(int[] userId);

        //删除一本小说
        int delbook(int bookid);

        //获取要审核的章节
        List<Model.SectionsInfo> getCheckSections(int pageindex, int pagesize, out int recoredCount);

        //章节审核通过        
        int SectionCheckYES(int sectionId, int bookId, string bookName);

        //章节审核不通过
        int SectionCheckNo(int sectionId);

    }
}
