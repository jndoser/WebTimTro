using System.Collections.Generic;
using WebTimTro.Data;

namespace WebTimTro.Interfaces
{
    public interface IPhongTroHinhAnhRepository
        : IGenericRepository<PhongTroHinhAnh>
    {
        public List<HinhAnh> GetHinhAnhsByPhongTroId(int phongTroId);
        public void DeletePhongTroHinhAnhByHinhAnhId(int hinhAnhId);
    }
}
