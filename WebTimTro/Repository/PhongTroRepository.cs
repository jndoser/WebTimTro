using System;
using System.Linq;
using WebTimTro.Data;
using WebTimTro.Interfaces;

namespace WebTimTro.Repository
{
    public class PhongTroRepository: GenericRepository<PhongTro>,
        IPhongTroRepository
    {
        public PhongTroRepository(ApplicationDbContext context): base(context)
        {
            
        }

        // Override lại phương thức create mặc định để
        // tạo phòng trọ theo logic khác 
        public override void Create(PhongTro entity)
        {
            entity.NgayDang = DateTime.Now;
            entity.NgayHetHan = DateTime.Now;
            _context.PhongTros.Add(entity);
        }

        // Lấy id của phòng trọ vừa được lưu vào database
        public int GetAddedPostId()
        {
            return _context.PhongTros.Select(x => x.Id).Max();
        }
    }
}
