using WebTimTro.Data;

namespace WebTimTro.Interfaces
{
    public interface IPhongTroLuuTruRepository: IGenericRepository<PhongTroLuuTru>
    {
        public PhongTroLuuTru GetPhongTroLuuTruByNguoiDungIdAndPhongTroId(string nguoiDungId,
           int phongTroId);
        public bool IsSaved(string nguoiDungId, int phongTroId);
    }
}
