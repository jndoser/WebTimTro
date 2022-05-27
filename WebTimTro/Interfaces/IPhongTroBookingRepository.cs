using WebTimTro.Data;

namespace WebTimTro.Interfaces
{
    public interface IPhongTroBookingRepository: IGenericRepository<PhongTroBooking>
    {
        public PhongTroBooking GetPhongTroBookingByPhongTroAndNguoiDungId(int phongTroId,
            string nguoiDungId);
        public bool IsDatPhong(int phongTroId, string nguoiDungId);
    }
}
