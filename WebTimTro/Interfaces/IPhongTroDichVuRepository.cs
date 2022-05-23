using System.Collections.Generic;
using WebTimTro.Data;

namespace WebTimTro.Interfaces
{
    public interface IPhongTroDichVuRepository: IGenericRepository<PhongTroDichVu>
    {
        public List<DichVu> GetDichVusByPhongTroId(int phongTroId);
        // Lấy ra list các id dịch vụ có trong phòng trọ với id của phòng trọ
        public List<int> GetDichVuIdsByPhongTroId(int phongTroId);
    }
}
