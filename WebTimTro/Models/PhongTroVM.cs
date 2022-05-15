using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace WebTimTro.Models
{
    public class PhongTroVM
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string DiaChi { get; set; }
        public int SucChua { get; set; }
        public string MoTa { get; set; }
        public long Gia { get; set; }
        public DateTime NgayDang { get; set; }
        public DateTime NgayHetHan { get; set; }

        // Chủ trọ có thể viết và lưu lại để đăng sau 
        public bool TrangThaiDang { get; set; } = true;
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
