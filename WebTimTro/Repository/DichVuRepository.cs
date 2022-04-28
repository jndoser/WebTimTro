using System.Linq;
using WebTimTro.Data;
using WebTimTro.Interfaces;

namespace WebTimTro.Repository
{
    public class DichVuRepository: GenericRepository<DichVu>, IDichVuRepository
    {
        public DichVuRepository(ApplicationDbContext context): base(context)
        {

        }

        public void DeleteDichVusByPhongTroId(int phongTroId)
        {
            //var dichVusId =_context.PhongTroDichVus
            //    .Where(x => x.PhongTroId == phongTroId)
            //    .Select(x => x.DichVuId);

            var phongTroDichVus = _context.PhongTroDichVus
                .Where(x => x.PhongTroId == phongTroId);

            _context.PhongTroDichVus.RemoveRange(phongTroDichVus);

            // Xoá luôn nó bị đi luôn dịch vụ =))), chỉ cần xoá 
            // liên kết thôi vì dịch vụ đó dùng chung 
            //foreach(var item in dichVusId)
            //{
            //    var dichVu = _context.DichVus
            //        .FirstOrDefault(x => x.Id == item);
            //    _context.DichVus.Remove(dichVu);
            //}
        }
    }
}
