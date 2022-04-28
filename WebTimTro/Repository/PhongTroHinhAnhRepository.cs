using System.Collections.Generic;
using System.Linq;
using WebTimTro.Data;
using WebTimTro.Interfaces;

namespace WebTimTro.Repository
{
    public class PhongTroHinhAnhRepository: GenericRepository<PhongTroHinhAnh>,
        IPhongTroHinhAnhRepository
    {
        public PhongTroHinhAnhRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        public List<HinhAnh> GetHinhAnhsByPhongTroId(int phongTroId)
        {
            List<HinhAnh> result = new List<HinhAnh>();
            var hinhAnhsId = _context.PhongTroHinhAnhs
                .Where(x => x.PhongTroId == phongTroId).Select(x => x.HinhAnhId);

            foreach(var item in hinhAnhsId)
            {
                var hinhAnh = _context.HinhAnhs.FirstOrDefault(x => x.Id == item);
                result.Add(hinhAnh);
            }

            return result;
        }

        public void DeletePhongTroHinhAnhByHinhAnhId(int hinhAnhId)
        {
            // Bởi vì id của hình ảnh là khác nhau và mỗi hình ảnh
            // chỉ có thể liên kết với 1 phòng trọ nên ta lấy
            // ra mối quan hệ tương ứng để xoá
            var phongTroHinhAnh = _context.PhongTroHinhAnhs
                .FirstOrDefault(x => x.HinhAnhId == hinhAnhId);
            _context.PhongTroHinhAnhs.Remove(phongTroHinhAnh);
        }
    }
}
