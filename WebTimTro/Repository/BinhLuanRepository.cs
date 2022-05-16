using System.Collections.Generic;
using System.Linq;
using WebTimTro.Data;
using WebTimTro.Interfaces;

namespace WebTimTro.Repository
{
    public class BinhLuanRepository: GenericRepository<BinhLuan>, IBinhLuanRepository
    {
        public BinhLuanRepository(ApplicationDbContext context): base(context)
        {

        }

        // Xoá các bình luân của phòng trọ với id của phòng trọ
        public void DeleteBinhLuansByPhongTroId(int id)
        {
            var binhLuansCount = _context
                .BinhLuans.Where(x => x.PhongTroId == id).Count();
            if(binhLuansCount == 0)
            {
                return;
            }
            var binhLuans = _context
                .BinhLuans.Where(x => x.PhongTroId == id);
            
            _context.BinhLuans.RemoveRange(binhLuans);
        }

        // Lấy ra id của bình luận gần đây nhất
        public long GetRecentCommentId()
        {
            var commentId = (_context.BinhLuans.Select(x => x.Id).Count() <= 0) ? 0 :
                _context.BinhLuans.Select(x => x.Id).Max();
            return commentId;
        }

        // Lấy ra tên của người dùng đã comment vào bài viết
        public string GetAuthorByCommentId(int id)
        {
            // Get id of user who write this comment
            string userId = _context.BinhLuans.FirstOrDefault(x => x.Id == id).NguoiDungId;

            // Lấy ra id của chủ phòng trọ mà comment này comment vào
            // Nếu user hiện tại là chủ phòng trọ thì trả về tên + chủ trọ
            int phongTroId = _context.BinhLuans.FirstOrDefault(x => x.Id == id).PhongTroId;
            PhongTro phongTro = _context.PhongTros.FirstOrDefault(x => x.Id == phongTroId);
            string idNguoiDang = phongTro.ChuTroId;

            string name = "";
            if (userId == idNguoiDang)
            {
                name = _context.Users.FirstOrDefault(x => x.Id == userId).LastName + " * chủ trọ";
            } else
            {
                // Get last name of this user id
                name = _context.Users.FirstOrDefault(x => x.Id == userId).LastName;
            }
            return name;
        }

        // Lấy ra avatar của người dùng đã comment vào bài viết
        public string GetAvatarOfAuthorByCommentId(int commentId)
        {
            var userId = _context.BinhLuans.FirstOrDefault(x => x.Id == commentId).NguoiDungId;
            return _context.Users.FirstOrDefault(x => x.Id == userId).Avatar;
        }

        // Lấy ra tất cả các comments của phòng trọ
        public List<BinhLuan> GetCommentsByPhongTroId(int phongTroId)
        {
            // Get list comments of the post
            // These comment is commented on post
            // So that, their RootId = 0
            var comments = _context.BinhLuans
                .Where(x => x.PhongTroId == phongTroId && x.RootId == 0 && x.IsShow == true).ToList();
            return comments;
        }

        // Get list of author name who write list of comment
        public List<string> GetListOfAuthorNameByListComment(List<BinhLuan> comments)
        {
            List<string> nameOfAuthors = new List<string>();
            foreach (var comment in comments)
            {
                nameOfAuthors.Add(GetAuthorByCommentId(comment.Id));
            }
            return nameOfAuthors;
        }

        // Get list of avatar of users
        public List<string> GetListOfAvatarByListComment(List<BinhLuan> comments)
        {
            List<string> nameOfAuthors = new List<string>();
            foreach (var comment in comments)
            {
                nameOfAuthors.Add(GetAvatarOfAuthorByCommentId(comment.Id));
            }
            return nameOfAuthors;
        }

        // Get all the comments that related to the comment
        // that have id in parameter
        public List<BinhLuan> GetSubCommentsById(int id)
        {
            var subComments = _context.BinhLuans.Where(x => x.RootId == id && x.IsShow == true).ToList();
            return subComments;
        }

        // Lấy ra username của bình luận mà bình luận hiện tại có id
        // truyền vào đang reply
        public string GetReplyUserNameByCommentId(int id)
        {
            // Get the id of the post that this comment reply to
            var parentPostId = _context.BinhLuans.FirstOrDefault(x => x.Id == id).ParentId;
            // Get the user id of the parent comment 
            var userIdOfParentPost = _context.BinhLuans.FirstOrDefault(x => x.Id == parentPostId).NguoiDungId;

            // Lấy ra id của chủ phòng trọ mà comment này comment vào
            // Nếu user hiện tại là chủ phòng trọ thì trả về tên + chủ trọ
            int phongTroId = _context.BinhLuans.FirstOrDefault(x => x.Id == id).PhongTroId;
            PhongTro phongTro = _context.PhongTros.FirstOrDefault(x => x.Id == phongTroId);
            string idNguoiDang = phongTro.ChuTroId;

            string name = "";
            if (userIdOfParentPost == idNguoiDang)
            {
                name = _context.Users.FirstOrDefault(x => x.Id == userIdOfParentPost).LastName + " * chủ trọ";
            }
            else
            {
                // Get last name of this user id
                name = _context.Users.FirstOrDefault(x => x.Id == userIdOfParentPost).LastName;
            }

            //// Get last name of author that write the parent comment
            //var name = _context.Users.FirstOrDefault(x => x.Id.Equals(userIdOfParentPost)).LastName;
            return name;
        }

        // Lấy ra id của bình luận mà đã
        // khơi nguồn cho bình luận với id truyền vào
        public int GetRootIdByCommentId(int id)
        {
            var rootId = _context.BinhLuans
                .FirstOrDefault(p => p.Id == id).RootId;
            if (rootId == 0)
            {
                return id;
            }
            return rootId;
        }

        // Get list of comment that user report
        public ICollection<BinhLuan> GetReportedComments()
        {
            return _context.BinhLuans.Where(x => x.IsReported == true).ToList();
        }

        // Count numbers of comment in a post
        public int GetNumbersOfCommentByPostId(int postId)
        {
            return _context.BinhLuans
                .Where(x => x.PhongTroId == postId && x.IsShow == true).Count();
        }
    }
}
