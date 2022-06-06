using AutoMapper;
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
    public class LichSuDatPhongController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LichSuDatPhongController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            TabIndex.Value = "LichSuDatPhong";
            return View();
        }

        [HttpGet]
        public JsonResult GetAll(string txtSearch, int? page)
        {
            // Lấy ra id của người dùng hiện tại đang đăng nhập
            string nguoiDungId = _unitOfWork.NguoiDung.GetUserId();


            IEnumerable<PhongTro> phongTros = _unitOfWork.PhongTroBooking
                .GetPhongTrosByNguoiDungId(nguoiDungId);

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

            // Lấy ra ngày đặt cọc tương ứng với danh sách
            // phòng trọ
            List<DateTime> ngayDatCocs = new List<DateTime>();
            foreach (PhongTroVM phongTro in listPostVM)
            {
                PhongTroBooking ngayCoc = _unitOfWork.PhongTroBooking
                    .GetPhongTroBookingByPhongTroAndNguoiDungId(phongTro.Id, nguoiDungId);
                ngayDatCocs.Add(ngayCoc.NgayDatTro);
            }

            // Lấy ra tiền cọc tương ứng với ds phòng trọ
            List<double> tienCocs = new List<double>();
            foreach (PhongTroVM phongTro in listPostVM)
            {
                long gia = _unitOfWork.PhongTro.GetById(phongTro.Id).Gia;
                tienCocs.Add(Math.Round(gia * 0.3, 0));
            }

            return Json(new
            {
                status = "ok",
                phongTros = listPostVM,
                pageCurrent = page,
                numSize = numSize,
                ngayDatCocs = ngayDatCocs,
                tienCocs = tienCocs,
            });
        }
    }
}
