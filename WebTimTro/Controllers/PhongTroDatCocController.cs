using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebTimTro.Data;
using WebTimTro.Interfaces;
using WebTimTro.Models;
using WebTimTro.StaticData;

namespace WebTimTro.Controllers
{
    [Authorize(Roles = "Chutro")]
    public class PhongTroDatCocController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhongTroDatCocController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            TabIndex.Value = "PhongTroDatCoc";
            return View();
        }

        [HttpGet]
        public JsonResult GetAll(string txtSearch, int? page)
        {
            // Lấy ra id của người dùng hiện tại đang đăng nhập
            string nguoiDungId = _unitOfWork.NguoiDung.GetUserId();


            var phongTros = _unitOfWork.PhongTro.GetAll()
                .Where(x => x.ChuTroId.Equals(nguoiDungId));

            if (!string.IsNullOrEmpty(txtSearch))
            {
                long sucChua = 0;
                long gia = 0;
                if (long.TryParse(txtSearch, out sucChua) && long.TryParse(txtSearch, out gia))
                {
                    ViewBag.txtSearch = txtSearch;
                    phongTros = phongTros.Where(x => x.Ten.ToLower().Contains(txtSearch.ToLower())
                    || x.DiaChi.ToLower().Contains(txtSearch.ToLower())
                    || x.SucChua == sucChua
                    || x.Gia == gia);
                }
                else
                {
                    ViewBag.txtSearch = txtSearch;
                    phongTros = phongTros.Where(x => x.Ten.ToLower().Contains(txtSearch.ToLower())
                    || x.DiaChi.ToLower().Contains(txtSearch.ToLower()));
                }
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
            int totalPage = phongTros.Count();
            float totalNumsize = (totalPage / (float)7);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var dataPost = phongTros.OrderByDescending(x => x.Id)
                .Skip(start).Take(10);
            List<PhongTro> listPost = new List<PhongTro>();
            listPost = dataPost.ToList();
            List<PhongTroVM> listPostVM = _mapper
                .Map<List<PhongTroVM>>(listPost);

            // Lấy ra số lượng người đã đặt cọc tương ứng với danh sách
            // phòng trọ
            List<int> soLuongDatCocs = new List<int>();
            foreach(PhongTroVM phongTro in listPostVM)
            {
                int soLuong = _unitOfWork.PhongTroBooking
                    .GetSoLuongNguoiDatCocByPhongTroId(phongTro.Id);
                soLuongDatCocs.Add(soLuong);
            }

            return Json(new
            {
                status = "ok",
                phongTros = listPostVM,
                pageCurrent = page,
                numSize = numSize,
                datCocs = soLuongDatCocs
            });
        }

        [HttpGet]
        public IActionResult ViewNguoiDat(int id)
        {
            PhongTro phongTro = _unitOfWork.PhongTro.GetById(id);
            PhongTroVM phongTroVM = _mapper.Map<PhongTroVM>(phongTro);

            return View(phongTroVM);
        }

        [HttpGet]
        public JsonResult GetNguoiDungDatTro(string txtSearch, int phongTroId, int? page)
        {
            IEnumerable<NguoiDung> data = _unitOfWork.PhongTroBooking
                .GetNguoiDatCocByPhongTroId(phongTroId);
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
                .Skip(start).Take(7);
            List<NguoiDung> listNguoiDung = new List<NguoiDung>();
            listNguoiDung = dataNguoiDung.ToList();
            List<NguoiDungVM> listNguoiDungVM = _mapper
                .Map<List<NguoiDung>, List<NguoiDungVM>>(listNguoiDung);

            return Json(new { data = listNguoiDungVM, pageCurrent = page, numSize = numSize });
        }
    }
}
