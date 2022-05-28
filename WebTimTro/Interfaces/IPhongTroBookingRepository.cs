using System.Collections.Generic;
using WebTimTro.Data;

namespace WebTimTro.Interfaces
{
    public interface IPhongTroBookingRepository: IGenericRepository<PhongTroBooking>
    {
        public PhongTroBooking GetPhongTroBookingByPhongTroAndNguoiDungId(int phongTroId,
            string nguoiDungId);
        public bool IsDatPhong(int phongTroId, string nguoiDungId);
        public int GetSoLuongNguoiDatCocByPhongTroId(int phongTroId);
        public List<NguoiDung> GetNguoiDatCocByPhongTroId(int phongTroId);
        public bool IsAdreadyBooking(string nguoiDungId);
    }
}
