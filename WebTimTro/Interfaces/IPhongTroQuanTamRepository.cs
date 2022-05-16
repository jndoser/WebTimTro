using WebTimTro.Data;

namespace WebTimTro.Interfaces
{
    public interface IPhongTroQuanTamRepository: IGenericRepository<PhongTroQuanTam>
    {
        public PhongTroQuanTam GetPhongTroQuanTamByNguoiDungIdAndPhongTroId(string nguoiDungId,
            int phongTroId);
        public bool IsQuanTam(string nguoiDungId, int phongTroId);
        public int NumbersOfQuanTam(int phongTroId);
    }
}
