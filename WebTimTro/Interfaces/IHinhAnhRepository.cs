using System.Collections.Generic;
using WebTimTro.Data;

namespace WebTimTro.Interfaces
{
    public interface IHinhAnhRepository: IGenericRepository<HinhAnh>
    {
        public int GetAddedHinhAnhId();
        public List<string> GetListFileNameByPhongTroId(int phongTroId);
        // Xoá tất cả các hình ảnh của phòng trọ 
        // với phòng trọ Id
        public void DeleteHinhAnhsByPhongTroId(int phongTroId);
        public List<string> GetFirstHinhAnhListOfPhongTroList();
        public List<string> GetFirstHinhAnhListOfPhongTroList(IEnumerable<PhongTro> phongTros);
    }
}
