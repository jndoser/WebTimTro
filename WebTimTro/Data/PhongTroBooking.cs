using System;

namespace WebTimTro.Data
{
    public class PhongTroBooking
    {
        public int PhongTroId { get; set; }
        public PhongTro PhongTro { get; set; }
        public string NguoiDungId { get; set; }
        public NguoiDung NguoiDung { get; set; }
        public DateTime NgayDatTro { get; set; }
    }
}
