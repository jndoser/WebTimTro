using System.Collections.Generic;
using WebTimTro.Data;

namespace WebTimTro.Interfaces
{
    public interface IPhongTroDichVuRepository: IGenericRepository<PhongTroDichVu>
    {
        public List<DichVu> GetDichVusByPhongTroId(int phongTroId);
    }
}
