using Microsoft.AspNetCore.Http;
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
    }
}
