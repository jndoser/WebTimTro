using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WebTimTro.Data;
using WebTimTro.Interfaces;

namespace WebTimTro.Repository
{
    public class NguoiDungRepository: GenericRepository<NguoiDung>, INguoiDungRepository
    {
        private readonly IHttpContextAccessor _httpContext;

        public NguoiDungRepository(ApplicationDbContext context,
            IHttpContextAccessor httpContext) : base(context)
        {
            _httpContext = httpContext;
        }

        public NguoiDung GetUserById(string id)
        {
            var user = _context.NguoiDungs.AsNoTracking()
                .FirstOrDefault(x => x.Id.Equals(id));
            return user;
        }

        public override void Delete(NguoiDung entity)
        {
            // Xoá các hàng liên quan trong 
            // bảng UserRoles
            var userRole = _context.UserRoles.FirstOrDefault(x => x.UserId.Equals(entity.Id));
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();

            //// Xoá các bình luận của người dùng
            //var binhLuans = _context.BinhLuans.Where(x => x.NguoiDungId.Equals(entity.Id));
            //_context.BinhLuans.RemoveRange(binhLuans);
            //_context.SaveChanges();

            //// Xoá các phòng trọ lưu trữ của người dùng
            //var phongTroLuuTrus = _context.PhongTroLuuTrus.Where(x => x.NguoiDungId.Equals(entity.Id));
            //_context.PhongTroLuuTrus.RemoveRange(phongTroLuuTrus);
            //_context.SaveChanges();

            //// Xoá các phòng trọ booking của người dùng
            //var phongTroBookings = _context.PhongTroBookings.Where(x => x.NguoiDungId.Equals(entity.Id));
            //_context.PhongTroBookings.RemoveRange(phongTroBookings);
            //_context.SaveChanges();

            //// Xoá các phòng trọ người dùng quan tâm
            //var phongTroQuanTams = _context.PhongTroQuanTams.Where(x => x.NguoiDungId.Equals(entity.Id));
            //_context.PhongTroQuanTams.RemoveRange(phongTroQuanTams);
            //_context.SaveChanges();

            //// Xoá phòng trọ mà người dùng đã tạo ra


            // Xoá user 
            _context.Users.Remove(entity);
        }

        public override ICollection<NguoiDung> GetAll()
        {
            var users = _context.Users
                .AsNoTracking().ToList();
            return users;
        }

        public string GetAvatarByName(string name)
        {
            if (name == null)
            {
                return string.Empty;
            }
            return _context.Users.FirstOrDefault(x => x.UserName == name).Avatar;
        }

        public string GetNameByUserName(string username)
        {
            if (username == null)
            {
                return string.Empty;
            }
            return _context.Users.FirstOrDefault(x => x.UserName.Equals(username)).LastName;
        }

        public string GetUserId()
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public bool IsAuthenticated()
        {
            return _httpContext.HttpContext.User.Identity.IsAuthenticated;
        }

        // Lấy ra tất cả người dùng với role là người dùng
        public ICollection<NguoiDung> GetAllNguoiDungWithNguoiDungRole()
        {
            ICollection<NguoiDung> result = new List<NguoiDung>();
            var userIds = _context.UserRoles
                .Where(x => x.RoleId.Equals("nguoidung"))
                .Select(x => x.UserId);

            foreach(var id in userIds)
            {
                var nguoiDung = _context.NguoiDungs
                    .FirstOrDefault(x => x.Id.Equals(id));
                result.Add(nguoiDung);
            }

            return result;
        }

        // Lấy ra tất cả người dùng với role là chủ trọ
        public ICollection<NguoiDung> GetAllNguoiDungWithChuTroRole()
        {
            ICollection<NguoiDung> result = new List<NguoiDung>();
            var userIds = _context.UserRoles
                .Where(x => x.RoleId.Equals("chutro"))
                .Select(x => x.UserId);

            foreach (var id in userIds)
            {
                var nguoiDung = _context.NguoiDungs
                    .FirstOrDefault(x => x.Id.Equals(id));
                result.Add(nguoiDung);
            }

            return result;
        }

        // Get tất cả người dùng đã đăng ký muốn trở thành chủ trọ
        public ICollection<NguoiDung> GetAllNguoiDungDangKyTroThanhChuTro()
        {
            return _context.NguoiDungs
                .Where(x => x.ChuTroRegisterStatus == true).ToList();
        }

        public bool SetChuTroRoleToUser(string nguoiDungId)
        {
            // Xoá Role hiện tại của người dùng là role Nguoidung
            var userRole = _context.UserRoles.FirstOrDefault(x => x.UserId.Equals(nguoiDungId));
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();

            // Lấy ra roleId của Role Chutro
            var chuTroRoleId = _context.Roles.FirstOrDefault(x => x.Name.Equals("Chutro")).Id;

            // Thêm một UserRole mới với Role là Chutro
            var newRole = new IdentityUserRole<string>()
            {
                RoleId = chuTroRoleId,
                UserId = nguoiDungId
            };

            _context.UserRoles.Add(newRole);

            if (_context.SaveChanges() > 0)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
