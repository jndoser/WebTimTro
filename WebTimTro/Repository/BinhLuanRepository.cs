using System.Linq;
using WebTimTro.Data;
using WebTimTro.Interfaces;

namespace WebTimTro.Repository
{
    public class BinhLuanRepository: GenericRepository<BinhLuan>, IBinhLuanRepository
    {
        public BinhLuanRepository(ApplicationDbContext context): base(context)
        {

        }

        // Xoá các bình luân của phòng trọ với id của phòng trọ
        public void DeleteBinhLuansByPhongTroId(int id)
        {
            var binhLuansCount = _context
                .BinhLuans.Where(x => x.PhongTroId == id).Count();
            if(binhLuansCount == 0)
            {
                return;
            }
            var binhLuans = _context
                .BinhLuans.Where(x => x.PhongTroId == id);
            
            _context.BinhLuans.RemoveRange(binhLuans);
        }
    }
}
