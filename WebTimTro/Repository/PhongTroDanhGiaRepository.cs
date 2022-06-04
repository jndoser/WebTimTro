using System.Linq;
using WebTimTro.Data;
using WebTimTro.Interfaces;

namespace WebTimTro.Repository
{
    public class PhongTroDanhGiaRepository : GenericRepository<PhongTroDanhGia>,
        IPhongTroDanhGiaRepository
    {
        public PhongTroDanhGiaRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Lấy ra rating của phòng trọ khi biết id của phòng trọ và người dùng
        public int GetPhongTroRatingByNguoiDungIdPhongTroId(string nguoiDungId, int phongTroId)
        {
            return _context.PhongTroDanhGias.FirstOrDefault(x => x.NguoiDungId.Equals(nguoiDungId)
            && x.PhongTroId == phongTroId).Rating;
        }

        // Lấy ra phòng trọ đánh giá khi biết id của phòng trọ và người dùng
        public PhongTroDanhGia GetPhongTroDanhGiaByNguoiDungIdPhongTroId(string nguoiDungId,
            int phongTroId)
        {
            return _context.PhongTroDanhGias.FirstOrDefault(x => x.NguoiDungId.Equals(nguoiDungId)
            && x.PhongTroId == phongTroId);
        }
    }
}
