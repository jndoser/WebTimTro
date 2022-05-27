using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WebTimTro.Data
{
    public class NguoiDung: IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public DateTime RegristeredAt { get; set; }
        public string Intro { get; set; }
        public string Profile { get; set; }
        public string Avatar { get; set; }
        // 1 người dùng có thể có nhiều
        // bình luận
        public ICollection<BinhLuan> BinhLuans { get; set; }

        // 1 người dùng (chủ trọ) có thể tạo nhiều bài đăng về phòng trọ 
        public ICollection<PhongTro> PhongTros { get; set; }

        // 1 người dùng có thể có nhiều phòng trọ lưu trữ
        public ICollection<PhongTroLuuTru> PhongTroLuuTrus { get; set; }

        // 1 người dùng có thể quan tâm nhiều phòng trọ
        public ICollection<PhongTroQuanTam> PhongTroQuanTams { get; set; }

        // 1 người dùng chỉ có thể đặt được nhiều phòng trọ
        public ICollection<PhongTroBooking> PhongTroBookings { get; set; }
    }
}
