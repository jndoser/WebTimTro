using System;
using System.Collections.Generic;
using System.Linq;
using WebTimTro.Data;
using WebTimTro.Interfaces;
using WebTimTro.Models;

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

        // Lấy ra tất cả các phòng trọ mà người dùng đã lưu
        public List<PhongTro> GetPhongTrosByNguoiDungId(string nguoiDungId)
        {
            List<PhongTro> result = new List<PhongTro>();
            var phongTroIds = _context.PhongTroLuuTrus.Where(x => x.NguoiDungId.Equals(nguoiDungId))
                .Select(x => x.PhongTroId);
            foreach(var item in phongTroIds)
            {
                PhongTro phongTro = _context.PhongTros.FirstOrDefault(x => x.Id == item);
                result.Add(phongTro);
            }

            return result;
        }

        // Lấy ra số lượng các phòng trọ thuộc địa điểm được truyền vào
        public int GetSoPhongTroThuocDiaDiem(string diaDiem)
        {
            return _context.PhongTros
                .Where(x => x.DiaChi.Contains(diaDiem)).Count();
        }

        // Lấy ra id các phòng trọ thuộc địa điểm được truyền vào
        public List<int> GetPhongTroIdsThuocDiaDiem(string diaDiem)
        {
            List<int> result = new List<int>();
            var phongTroIds = _context.PhongTros
                .Where(x => x.District.Equals(diaDiem)).Select(x => x.Id);

            foreach(var id in phongTroIds)
            {
                result.Add(id);
            }
            return result;
        }

        // Lấy ra id của tất cả các phòng trọ
        public List<int> GetPhongTroIds()
        {
            List<int> phongTroIds = _context
                .PhongTros.Select(x => x.Id).ToList();
            return phongTroIds;
        }

        // Lấy ra phòng trọ khi cho trước danh sách id
        public List<PhongTro> GetPhongTroByIds(List<int> ids)
        {
            List<PhongTro> phongTros = new List<PhongTro>();
            foreach(int item in ids)
            {
                PhongTro phongTro = _context
                    .PhongTros.FirstOrDefault(x => x.Id == item);
                phongTros.Add(phongTro);
            }

            return phongTros;
        }
    }
}
