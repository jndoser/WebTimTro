using WebTimTro.Data;

namespace WebTimTro.Interfaces
{
    public interface IBinhLuanRepository: IGenericRepository<BinhLuan>
    {
        // Xoá các bình luân của phòng trọ với id của phòng trọ
        public void DeleteBinhLuansByPhongTroId(int id);
    }
}
