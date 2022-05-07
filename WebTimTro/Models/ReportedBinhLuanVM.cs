using System;

namespace WebTimTro.Models
{
    public class ReportedBinhLuanVM
    {
        public int Id { get; set; }
        public int PhongTroId { get; set; }
        public DateTime NgayDang { get; set; }
        public string NoiDung { get; set; }
        public bool? IsShow { get; set; }
        public string TenCuaNguoiDang { get; set; }
    }
}
