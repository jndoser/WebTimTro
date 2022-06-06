using System;
using System.Collections.Generic;
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

        public int GetSoLuongNguoiDatCocByPhongTroId(int phongTroId)
        {
            return _context.PhongTroBookings.Where(x => x.PhongTroId == phongTroId)
                .Count();
        }

        public List<NguoiDung> GetNguoiDatCocByPhongTroId(int phongTroId)
        {
            List<NguoiDung> nguoiDungs = new List<NguoiDung>();
            List<string> nguoiDungIds = _context
                .PhongTroBookings.Where(x => x.PhongTroId == phongTroId)
                .Select(x => x.NguoiDungId).ToList();

            foreach(string id in nguoiDungIds)
            {
                NguoiDung nguoiDung = _context
                    .NguoiDungs.FirstOrDefault(x => x.Id.Equals(id));
                nguoiDungs.Add(nguoiDung);
            }

            return nguoiDungs;
        }

        public bool IsAdreadyBooking(string nguoiDungId)
        {
            return _context.PhongTroBookings
                .Any(x => x.NguoiDungId.Equals(nguoiDungId));
        }

        public bool IsQuaHanDatPhong(int phongTroId)
        {
            DateTime hanDat = _context
                .PhongTros.FirstOrDefault(x => x.Id == phongTroId)
                .NgayHetHan;

            if(DateTime.Compare(DateTime.Now, hanDat) > 0)
            {
                return true;
            }else
            {
                return false;
            }
        }

        // Lấy ra tất cả các phòng trọ đã được đặt bởi người dùng
        public List<PhongTro> GetPhongTrosByNguoiDungId(string nguoiDungId)
        {
            var phongTroIds = _context.PhongTroBookings
                .Where(x => x.NguoiDungId.Equals(nguoiDungId))
                .Select(x => x.PhongTroId);

            List<PhongTro> phongTros = new List<PhongTro>();
            foreach(var id in phongTroIds)
            {
                PhongTro phongTro = _context
                    .PhongTros.FirstOrDefault(x => x.Id == id);
                phongTros.Add(phongTro);
            }
            return phongTros;
        }
    }
}
