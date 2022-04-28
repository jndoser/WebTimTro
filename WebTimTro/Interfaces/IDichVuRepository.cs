using WebTimTro.Data;

namespace WebTimTro.Interfaces
{
    public interface IDichVuRepository: IGenericRepository<DichVu>
    {
        public void DeleteDichVusByPhongTroId(int phongTroId);
    }
}
