using WebTimTro.Data;

namespace WebTimTro.Interfaces
{
    public interface IPhongTroDanhGiaRepository: IGenericRepository<PhongTroDanhGia>
    {
        // Lấy ra rating của phòng trọ khi biết id của phòng trọ và người dùng
        public int GetPhongTroRatingByNguoiDungIdPhongTroId(string nguoiDungId, int phongTroId);
        // Lấy ra phòng trọ đánh giá khi biết id của phòng trọ và người dùng
        public PhongTroDanhGia GetPhongTroDanhGiaByNguoiDungIdPhongTroId(string nguoiDungId,
            int phongTroId);
    }
}
