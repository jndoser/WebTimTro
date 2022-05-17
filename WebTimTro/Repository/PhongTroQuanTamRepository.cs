using System.Linq;
using WebTimTro.Data;
using WebTimTro.Interfaces;

namespace WebTimTro.Repository
{
    public class PhongTroQuanTamRepository: GenericRepository<PhongTroQuanTam>,
        IPhongTroQuanTamRepository
    {
        public PhongTroQuanTamRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        public PhongTroQuanTam GetPhongTroQuanTamByNguoiDungIdAndPhongTroId(string nguoiDungId,
            int phongTroId)
        {
            var result = _context.PhongTroQuanTams
                .FirstOrDefault(x => x.NguoiDungId.Equals(nguoiDungId) &&
            x.PhongTroId == phongTroId);

            return result;
        }

        // Kiểm tra liệu người dùng đã quan tâm phòng trọ này hay chưa
        public bool IsQuanTam(string nguoiDungId, int phongTroId)
        {
            int count = _context.PhongTroQuanTams
                .Where(x => x.NguoiDungId.Equals(nguoiDungId) &&
            x.PhongTroId == phongTroId).Count();

            return count > 0;
        }

        // Đếm số lượng quan tâm của một bài viết
        public int NumbersOfQuanTam(int phongTroId)
        {
            return _context.PhongTroQuanTams
                .Where(x => x.PhongTroId == phongTroId)
                .Count();
        }

        // Count numbers of care in a post
        public int GetNumbersOfCareByPostId(params int[] postId)
        {
            int tongQuanTam = 0;
            foreach (var id in postId)
            {
                int soQuanTam = _context.PhongTroQuanTams
                .Where(x => x.PhongTroId == id).Count();

                tongQuanTam += soQuanTam;
            }
            return tongQuanTam;
        }
    }
}
