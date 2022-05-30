using System.Collections.Generic;
using WebTimTro.Data;

namespace WebTimTro.Interfaces
{
    public interface INguoiDungRepository: IGenericRepository<NguoiDung>
    {
        public NguoiDung GetUserById(string id);
        public string GetAvatarByName(string name);
        public string GetNameByUserName(string username);
        public string GetUserId();
        public bool IsAuthenticated();
        public ICollection<NguoiDung> GetAllNguoiDungWithNguoiDungRole();
        public ICollection<NguoiDung> GetAllNguoiDungWithChuTroRole();
        public bool SetChuTroRoleToUser(string nguoiDungId);
        // Get tất cả người dùng đã đăng ký muốn trở thành chủ trọ
        public ICollection<NguoiDung> GetAllNguoiDungDangKyTroThanhChuTro();
    }
}
