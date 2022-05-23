using System.Collections.Generic;
using System.Linq;
using WebTimTro.Data;
using WebTimTro.Interfaces;

namespace WebTimTro.Repository
{
    public class PhongTroDichVuRepository: GenericRepository<PhongTroDichVu>,
        IPhongTroDichVuRepository
    {
        public PhongTroDichVuRepository(ApplicationDbContext context): base(context)
        {

        }

        // Lấy ra list các dịch vụ có trong phòng trọ với id của phòng trọ
        public List<DichVu> GetDichVusByPhongTroId(int phongTroId)
        {
            List<DichVu> result = new List<DichVu>();
            var dichVusId = _context.PhongTroDichVus
                .Where(x => x.PhongTroId == phongTroId)
                .Select(x => x.DichVuId);

            foreach(var item in dichVusId)
            {
                var dichVu = _context.DichVus.FirstOrDefault(x => x.Id == item);
                result.Add(dichVu);
            }

            return result;
        }

        // Lấy ra list các id dịch vụ có trong phòng trọ với id của phòng trọ
        public List<int> GetDichVuIdsByPhongTroId(int phongTroId)
        {
            var dichVusId = _context.PhongTroDichVus
                .Where(x => x.PhongTroId == phongTroId)
                .Select(x => x.DichVuId).ToList();
            return dichVusId;
        }
    }
}
