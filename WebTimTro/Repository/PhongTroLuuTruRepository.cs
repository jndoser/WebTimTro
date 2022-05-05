using System.Linq;
using WebTimTro.Data;
using WebTimTro.Interfaces;

namespace WebTimTro.Repository
{
    public class PhongTroLuuTruRepository: GenericRepository<PhongTroLuuTru>,
        IPhongTroLuuTruRepository
    {
        public PhongTroLuuTruRepository(ApplicationDbContext context)
            :base(context)
        {

        }

        public PhongTroLuuTru GetPhongTroLuuTruByNguoiDungIdAndPhongTroId(string nguoiDungId,
            int phongTroId)
        {
            var result = _context.PhongTroLuuTrus.FirstOrDefault(x => x.NguoiDungId.Equals(nguoiDungId) &&
            x.PhongTroId == phongTroId);

            return result;
        }

        // Kiểm tra liệu người dùng đã lưu phòng trọ này hay chưa
        public bool IsSaved(string nguoiDungId, int phongTroId)
        {
            int count = _context.PhongTroLuuTrus.Where(x => x.NguoiDungId.Equals(nguoiDungId) &&
            x.PhongTroId == phongTroId).Count();

            return count > 0;
        }
    }
}
