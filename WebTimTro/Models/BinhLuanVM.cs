using System;

namespace WebTimTro.Models
{
    public class BinhLuanVM
    {
        public int Id { get; set; }
        public string NoiDung { get; set; }
        public int SoLuotLike { get; set; }
        public DateTime NgayDang { get; set; }  
        public int? ParentId { get; set; }      
        public string NguoiDungId { get; set; }       
        public int PhongTroId { get; set; }       
        public long RootId { get; set; }
        public bool IsReported { get; set; }
        public bool? IsShow { get; set; }
    }
}
