using System.Linq;
using WebTimTro.Data;
using WebTimTro.Interfaces;

namespace WebTimTro.Repository
{
    public class PhongTroBookingRepository: GenericRepository<PhongTroBooking>,
        IPhongTroBookingRepository
    {
        public PhongTroBookingRepository(ApplicationDbContext context)
            :base(context)
        {
            
        }
        
        public PhongTroBooking GetPhongTroBookingByPhongTroAndNguoiDungId(int phongTroId,
            string nguoiDungId)
        {
            return _context.PhongTroBookings.FirstOrDefault(x => x.PhongTroId == phongTroId
            && x.NguoiDungId.Equals(nguoiDungId));
        } 

        public bool IsDatPhong(int phongTroId, string nguoiDungId)
        {
            return _context.PhongTroBookings.Where(x => x.PhongTroId == phongTroId
            && x.NguoiDungId.Equals(nguoiDungId)).Count() > 0;
        }
    }
}
