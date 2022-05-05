using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebTimTro.Data;
using WebTimTro.Interfaces;
using WebTimTro.Models;

namespace WebTimTro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            // Lấy thông tin tất cả các bài viết trong phòng trọ
            IEnumerable<PhongTro> phongTros = _unitOfWork.PhongTro.GetAll();
            IEnumerable<PhongTroVM> phongTroVMs = _mapper
                .Map<IEnumerable<PhongTroVM>>(phongTros);

            // Lấy tất cả các hình ảnh đầu tiên tương ứng với tất cả
            // các phòng trọ ở trên
            List<string> firstHinhAnhs = _unitOfWork.HinhAnh.GetFirstHinhAnhListOfPhongTroList();
            ViewBag.FirstHinhAnhs = firstHinhAnhs;

            // Lấy các dịch vụ tương ứng với tất cả các phòng trọ ở trên
            List<string> someDichVu = _unitOfWork.DichVu.GetSomeDichVuOfPhongTroList();
            ViewBag.SomeDichVu = someDichVu;

            return View(phongTroVMs);
        }
        
        public IActionResult Detail(int id)
        {
            PhongTro phongTro = _unitOfWork.PhongTro.GetById(id);
            PhongTroVM phongTroVM = _mapper
                .Map<PhongTroVM>(phongTro);

            List<string> hinhAnhs = _unitOfWork.HinhAnh
                .GetFirstHinhAnhListOfPhongTroList();
            ViewBag.HinhAnhs = hinhAnhs;

            ViewBag.Note = _unitOfWork.Note.GetNoteByPhongTroId(id);

            // Tìm các dịch vụ của phòng trọ và truyền đến view
            List<DichVu> dichVus = _unitOfWork.
                PhongTroDichVu.GetDichVusByPhongTroId(id);
            List<DichVuVM> dichVusVM = _mapper
                .Map<List<DichVu>, List<DichVuVM>>(dichVus);
            ViewBag.DichVuCuaPhongTro = dichVusVM;

            return View(phongTroVM);
        }

        public JsonResult SavePhongTro(int id)
        {
            string nguoiDungId = _unitOfWork.NguoiDung.GetUserId();
            PhongTroLuuTru phongTroLuuTru = new PhongTroLuuTru
            {
                NguoiDungId = nguoiDungId,
                PhongTroId = id
            };
            _unitOfWork.PhongTroLuuTru.Create(phongTroLuuTru);
            if (_unitOfWork.Save())
            {
                return new JsonResult(new { status = "ok" });
            } else
            {
                return new JsonResult(new { status = "err" });
            }
        }

        public JsonResult UnSavePhongTro(int id)
        {
            string nguoiDungId = _unitOfWork.NguoiDung.GetUserId();
            PhongTroLuuTru phongTroLuuTru = _unitOfWork
                .PhongTroLuuTru.GetPhongTroLuuTruByNguoiDungIdAndPhongTroId(nguoiDungId, id);

            _unitOfWork.PhongTroLuuTru.Delete(phongTroLuuTru);
            if (_unitOfWork.Save())
            {
                return new JsonResult(new { status = "ok" });
            }
            else
            {
                return new JsonResult(new { status = "err" });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
