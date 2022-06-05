using System.Collections.Generic;
using WebTimTro.Data;

namespace WebTimTro.Interfaces
{
    public interface IPhongTroRepository: IGenericRepository<PhongTro>
    {
        // Lấy id của phòng trọ vừa được lưu vào database
        public int GetAddedPostId();

        // Lấy ra tất cả các phòng trọ mà người dùng đã lưu
        public List<PhongTro> GetPhongTrosByNguoiDungId(string nguoiDungId);

        // Lấy ra số lượng các phòng trọ thuộc địa điểm được truyền vào
        public int GetSoPhongTroThuocDiaDiem(string diaDiem);

        // Lấy ra id các phòng trọ thuộc địa điểm được truyền vào
        public List<int> GetPhongTroIdsThuocDiaDiem(string diaDiem);

        // Lấy ra id của tất cả các phòng trọ
        public List<int> GetPhongTroIds();

        // Lấy ra phòng trọ khi cho trước danh sách id
        public List<PhongTro> GetPhongTroByIds(List<int> ids);

        public string GetCurrentTabIndex();

        // Reset lại tab để không có tab nào đang được chọn
        public void ResetCurrentTab();
    }
}
