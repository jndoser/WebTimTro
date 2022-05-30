using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebTimTro.Data;
using WebTimTro.Interfaces;
using WebTimTro.Models;

namespace WebTimTro.Controllers
{
    public class ChuTroRegisterController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ChuTroRegisterController(IUnitOfWork unitOfWork,
            IMapper mapper,
            IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Lấy các người dùng đã được tạo, phân trang
        [HttpGet]
        public JsonResult GetAllChuTro(string txtSearch, int? page)
        {
            IEnumerable<NguoiDung> data =
                _unitOfWork.NguoiDung.GetAllNguoiDungDangKyTroThanhChuTro();
            if (!string.IsNullOrEmpty(txtSearch))
            {
                ViewBag.txtSearch = txtSearch;
                data = data.Where(x => (x.FirstName + " " + x.MiddleName + " " + x.LastName)
                .ToLower().Contains(txtSearch.ToLower()));
            }
            if (page > 0)
            {

            }
            else
            {
                page = 1;
            }
            int start = (int)(page - 1) * 7; // 7 is pageSize
            ViewBag.pageCurrent = page;
            int totalPage = data.Count();
            float totalNumsize = (totalPage / (float)7);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var dataNguoiDung = data.OrderByDescending(x => x.Id)
                .Skip(start).Take(10);
            List<NguoiDung> listNguoiDung = new List<NguoiDung>();
            listNguoiDung = dataNguoiDung.ToList();
            List<NguoiDungVM> listNguoiDungVM = _mapper
                .Map<List<NguoiDung>, List<NguoiDungVM>>(listNguoiDung);

            return Json(new { data = listNguoiDungVM, pageCurrent = page, numSize = numSize });
        }

        public IActionResult Duyet(string id)
        {
            if (_unitOfWork.NguoiDung.SetChuTroRoleToUser(id))
            {
                NguoiDung nguoiDung = _unitOfWork.NguoiDung.GetUserById(id);
                NguoiDung newNguoiDung = nguoiDung;
                newNguoiDung.ChuTroRegisterStatus = false;

                _unitOfWork.NguoiDung.Update(newNguoiDung);
                if (_unitOfWork.Save())
                {
                    return RedirectToAction("Index");
                } else
                {
                    return BadRequest();
                }               
            }
            return BadRequest();
        }
    }
}
