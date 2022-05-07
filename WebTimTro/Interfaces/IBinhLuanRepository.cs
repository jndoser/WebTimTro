using System.Collections.Generic;
using WebTimTro.Data;

namespace WebTimTro.Interfaces
{
    public interface IBinhLuanRepository: IGenericRepository<BinhLuan>
    {
        // Xoá các bình luân của phòng trọ với id của phòng trọ
        public void DeleteBinhLuansByPhongTroId(int id);

        // Lấy ra id của bình luận gần đây nhất
        public long GetRecentCommentId();

        // Lấy ra tên của người dùng đã comment vào bài viết
        public string GetAuthorByCommentId(int id);

        // Lấy ra avatar của người dùng đã comment vào bài viết
        public string GetAvatarOfAuthorByCommentId(int commentId);

        // Lấy ra tất cả các comments của phòng trọ
        public List<BinhLuan> GetCommentsByPhongTroId(int phongTroId);

        // Get list of author name who write list of comment
        public List<string> GetListOfAuthorNameByListComment(List<BinhLuan> comments);

        // Get list of avatar of users
        public List<string> GetListOfAvatarByListComment(List<BinhLuan> comments);

        // Get all the comments that related to the comment
        // that have id in parameter
        public List<BinhLuan> GetSubCommentsById(int id);

        // Lấy ra username của bình luận mà bình luận hiện tại có id
        // truyền vào đang reply
        public string GetReplyUserNameByCommentId(int id);

        // Lấy ra id của bình luận mà đã
        // khơi nguồn cho bình luận với id truyền vào
        public int GetRootIdByCommentId(int id);

        // Get list of comment that user report
        public ICollection<BinhLuan> GetReportedComments();
    }
}
