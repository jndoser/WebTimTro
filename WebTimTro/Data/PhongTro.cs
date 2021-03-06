using System;
using System.Collections.Generic;

namespace WebTimTro.Data
{
    public class PhongTro
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
        public string District { get; set; }

        // 1 phòng trọ do 1 chủ trọ tạo
        public string ChuTroId { get; set; }
        public NguoiDung ChuTro { get; set; }

        // 1 phòng trọ có thể có nhiều 
        // bình luận về nó
        public ICollection<BinhLuan> BinhLuans { get; set; }
        // 1 phòng trọ có thể có nhiều dịch vụ
        public ICollection<PhongTroDichVu> PhongTroDichVus { get; set; }
        // 1 phòng trọ có thể có nhiều ghi chú
        public ICollection<PhongTroNote> PhongTroNotes { get; set; }
        // 1 phòng trọ có thể có nhiều hình ảnh 
        public ICollection<PhongTroHinhAnh> PhongTroHinhAnhs { get; set; }

        // 1 phòng trọ có thể có nhiều phòng trọ lưu trữ, tức là có thể
        // được lưu bởi nhiều người
        public ICollection<PhongTroLuuTru> PhongTroLuuTrus { get; set; }
        // 1 phòng trọ có thể được quan tâm bởi nhiều người
        public ICollection<PhongTroQuanTam> PhongTroQuanTams { get; set; }

        // 1 phòng trọ có thể thuộc nhiều booking
        public ICollection<PhongTroBooking> PhongTroBookings { get; set; }

        // 1 phòng trọ có thể được nhiều người đánh giá
        public ICollection<PhongTroDanhGia> PhongTroDanhGias { get; set; }
    }
}
