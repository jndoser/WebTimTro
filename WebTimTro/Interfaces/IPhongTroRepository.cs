using WebTimTro.Data;

namespace WebTimTro.Interfaces
{
    public interface IPhongTroRepository: IGenericRepository<PhongTro>
    {
        // Lấy id của phòng trọ vừa được lưu vào database
        public int GetAddedPostId();
    }
}
