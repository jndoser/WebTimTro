using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WebTimTro.Data
{
    public class BinhLuan
    {
        public int Id { get; set; }
        public string NoiDung { get; set; }
        public int SoLuotLike { get; set; }
        public DateTime NgayDang { get; set; }
        // 1 bình luận có thể chứa 1
        // hoặc nhiều bình luận con trong nó
        // nên ta hình thành mối quản hệ phản
        // thân
        public int? ParentId { get; set; }
        public BinhLuan BinhLuanCha { get; set; }
        public ICollection<BinhLuan> BinhLuanCons { get; set; }
        // 1 bình luận do một người dùng 
        // đã tạo
        public string NguoiDungId { get; set; }
        public NguoiDung NguoiDung { get; set; }
        // 1 bình luận chỉ thuộc một phòng trọ
        public int PhongTroId { get; set; }
        public PhongTro PhongTro { get; set; }

        // Root Id là id trỏ đến id của bình luận gốc
        // là bình luận ban đầu đã khơi nguồn cho
        // bình luận hiện tại
        public int RootId { get; set; }

        // Lưu trạng thái đã report hay chưa của bình luận
        public bool IsReported { get; set; }
        // Lưu trạng thái có nên hiển thị bình luận này không
        public bool? IsShow { get; set; }
    }
}
