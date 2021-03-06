using System.Collections.Generic;
using System.Linq;
using WebTimTro.Data;
using WebTimTro.Interfaces;

namespace WebTimTro.Repository
{
    public class HinhAnhRepository: GenericRepository<HinhAnh>,
        IHinhAnhRepository
    {
        public HinhAnhRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        public int GetAddedHinhAnhId()
        {
            return _context.HinhAnhs.Select(x => x.Id).Max();
        }

        public List<string> GetListFileNameByPhongTroId(int phongTroId)
        {
            List<string> result = new List<string>();
            var hinhAnhsId = _context.PhongTroHinhAnhs
                .Where(x => x.PhongTroId == phongTroId)
                .Select(x => x.HinhAnhId);
            foreach(var item in hinhAnhsId)
            {
                var fileName = _context.HinhAnhs
                    .FirstOrDefault(x => x.Id == item)
                    .Filename;
                result.Add(fileName);
            }
            return result;
        }

        // Xoá tất cả các hình ảnh của phòng trọ 
        // với phòng trọ Id
        public void DeleteHinhAnhsByPhongTroId(int phongTroId)
        {
            // Bởi vì 1 phòng trọ có thể có nhiều hình ảnh
            // liên kết với nó nên ta sẽ lấy ra hết các hình
            // ảnh này
            var phongTroHinhAnhs = _context.PhongTroHinhAnhs
                .Where(x => x.PhongTroId == phongTroId);

            // Lấy ra id của các hình ảnh trong phòng trọ
            // để có thể xoá các hình ảnh này
            var hinhAnhsId = _context.PhongTroHinhAnhs
                .Where(x => x.PhongTroId == phongTroId)
                .Select(x => x.HinhAnhId);

            _context.PhongTroHinhAnhs.RemoveRange(phongTroHinhAnhs);

            // Xoá các hình ảnh có id trên
            foreach ( var item in hinhAnhsId)
            {
                var hinhAnh = _context.HinhAnhs
                    .FirstOrDefault(x => x.Id == item);
                _context.HinhAnhs.Remove(hinhAnh);
            }         
        }

        // Lấy ra danh sách các file hình ảnh - hình đầu tiên của 
        // tất cả các bài viết về phòng trọ mà người dùng đã viết
        // để hiển thị lên view
        public List<string> GetFirstHinhAnhListOfPhongTroList()
        {
            List<string> result = new List<string>();
            var phongTroIds = _context.PhongTros.OrderByDescending(x => x.Id)
                .Select(x => x.Id);
            foreach(var phongTroId in phongTroIds)
            {
                string firstHinhAnhUrl = GetListFileNameByPhongTroId(phongTroId)[0];
                result.Add(firstHinhAnhUrl);
            }
            return result;
        }


        // Lấy ra danh sách các file hình ảnh - hình đầu tiên của 
        // các bài viết về phòng trọ được truyền vào
        public List<string> GetFirstHinhAnhListOfPhongTroList(IEnumerable<PhongTro> phongTros)
        {
            List<string> result = new List<string>();
            var phongTroIds = phongTros.Select(x => x.Id);
            foreach (var phongTroId in phongTroIds)
            {
                string firstHinhAnhUrl = GetListFileNameByPhongTroId(phongTroId)[0];
                result.Add(firstHinhAnhUrl);
            }
            return result;
        }
    }
}
