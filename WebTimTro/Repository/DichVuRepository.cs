using System.Collections.Generic;
using System.Linq;
using WebTimTro.Data;
using WebTimTro.Interfaces;

namespace WebTimTro.Repository
{
    public class DichVuRepository: GenericRepository<DichVu>, IDichVuRepository
    {
        public DichVuRepository(ApplicationDbContext context): base(context)
        {

        }

        public void DeleteDichVusByPhongTroId(int phongTroId)
        {
            //var dichVusId =_context.PhongTroDichVus
            //    .Where(x => x.PhongTroId == phongTroId)
            //    .Select(x => x.DichVuId);

            var phongTroDichVus = _context.PhongTroDichVus
                .Where(x => x.PhongTroId == phongTroId);

            _context.PhongTroDichVus.RemoveRange(phongTroDichVus);

            // Xoá luôn nó bị đi luôn dịch vụ =))), chỉ cần xoá 
            // liên kết thôi vì dịch vụ đó dùng chung 
            //foreach(var item in dichVusId)
            //{
            //    var dichVu = _context.DichVus
            //        .FirstOrDefault(x => x.Id == item);
            //    _context.DichVus.Remove(dichVu);
            //}
        }

        // Lấy id của dịch vụ mới thêm vào cơ sở dữ liệu
        public int GetAddedDichVuId()
        {
            if(_context.DichVus.ToList().Count == 0)
            {
                return 1;
            }
            return _context.DichVus.Select(x => x.Id).Max();
        }

        // Lấy ra 3 dịch vụ đầu tiên của mỗi phòng trọ
        // nối lại thành 1 chuỗi để in ra view
        public List<string> GetSomeDichVuOfPhongTroList()
        {
            List<string> result = new List<string>();
            var phongTroIds = _context.PhongTros.Select(x => x.Id);
            foreach(var id in phongTroIds)
            {
                List<string> tenDichVuList = GetTenDichVuListByPhongTroId(id);
                string dichVuStr = "";
                if (tenDichVuList.Count <= 3)
                {                   
                    foreach(string tenDichVu in tenDichVuList)
                    {
                        dichVuStr += tenDichVu + " * ";
                    }
                    result.Add(dichVuStr.Substring(0, dichVuStr.Length - 2));
                } else
                {
                    for(int i = 0; i <= 2; i++)
                    {
                        dichVuStr += tenDichVuList[i] + " * ";
                    }
                    result.Add(dichVuStr.Substring(0, dichVuStr.Length - 2));
                } 
            }
            return result;
        }

        // Lấy ra 3 dịch vụ đầu tiên của mỗi phòng trọ được truyền vào
        // nối lại thành 1 chuỗi để in ra view
        public List<string> GetSomeDichVuOfPhongTroList(IEnumerable<PhongTro> phongTros)
        {
            List<string> result = new List<string>();
            var phongTroIds = phongTros.Select(x => x.Id);
            foreach (var id in phongTroIds)
            {
                List<string> tenDichVuList = GetTenDichVuListByPhongTroId(id);
                string dichVuStr = "";
                if (tenDichVuList.Count <= 3)
                {
                    foreach (string tenDichVu in tenDichVuList)
                    {
                        dichVuStr += tenDichVu + " * ";
                    }
                    result.Add(dichVuStr.Substring(0, dichVuStr.Length - 2));
                }
                else
                {
                    for (int i = 0; i <= 2; i++)
                    {
                        dichVuStr += tenDichVuList[i] + " * ";
                    }
                    result.Add(dichVuStr.Substring(0, dichVuStr.Length - 2));
                }
            }
            return result;
        }

        // Lấy ra danh sách tên dịch vụ của phòng trọ với id cho trước
        private List<string> GetTenDichVuListByPhongTroId(int phongTroId)
        {
            List<string> result = new List<string>();
            var dichVuIds = _context.PhongTroDichVus.Where(x => x.PhongTroId == phongTroId)
                .Select(x => x.DichVuId);
            foreach(var id in dichVuIds)
            {
                string tenDichVu = _context.DichVus.FirstOrDefault(x => x.Id == id).Ten;
                result.Add(tenDichVu);
            }
            return result;
        }
    }
}
