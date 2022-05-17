using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebTimTro.Data;
using WebTimTro.Interfaces;
using WebTimTro.Models;

namespace WebTimTro.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class DichVuController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DichVuController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            //List<DichVu> dichVus = _unitOfWork.DichVu.GetAll().ToList();
            //List<DichVuVM> dichVusVM = _mapper.Map<List<DichVuVM>>(dichVus);

            //return View(dichVusVM);
            return View();
        }

        // Lấy các post đã được tạo, phân trang
        [HttpGet]
        public JsonResult GetAllDichVu(string txtSearch, int? page)
        {
            var data = _unitOfWork.DichVu.GetAll();
            if (!string.IsNullOrEmpty(txtSearch))
            {
                ViewBag.txtSearch = txtSearch;
                data = data.Where(x => x.Ten.ToLower().Contains(txtSearch.ToLower()));
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
            var dataDichVu = data.OrderBy(x => x.Id)
                .Skip(start).Take(7);
            List<DichVu> listDichVu = new List<DichVu>();
            listDichVu = dataDichVu.ToList();
            List<DichVuVM> listDichVuVM = _mapper
                .Map<List<DichVuVM>>(listDichVu);

            return Json(new { data = listDichVuVM, pageCurrent = page, numSize = numSize });
        }

        public JsonResult SaveDichVu(DichVuVM dichVu)
        {
            DichVu dichVuData = _mapper.Map<DichVu>(dichVu);
            _unitOfWork.DichVu.Create(dichVuData);
            if (_unitOfWork.Save())
            {
                int addedId = _unitOfWork.DichVu.GetAddedDichVuId();
                DichVu addedDichVu = _unitOfWork.DichVu.GetById(addedId);
                DichVuVM addedDichVuVM = _mapper.Map<DichVuVM>(addedDichVu);

                return Json(new { status = "ok", data = addedDichVuVM });
            }
            return Json(new { status = "error" });
        }

        public JsonResult GetDichVuById(int id)
        {
            DichVu dichVuData = _unitOfWork.DichVu.GetById(id);
            DichVuVM dichVuVM = _mapper.Map<DichVuVM>(dichVuData);
            if(dichVuVM == null)
            {
                return Json(new { status = "error" });
            }
            return Json(new { status = "ok", data = dichVuVM });
        }

        public JsonResult UpdateDichVu(DichVuVM dichVu)
        {
            DichVu dichVuData = _mapper.Map<DichVu>(dichVu);
            _unitOfWork.DichVu.Update(dichVuData);
            if (_unitOfWork.Save())
            {
                return Json(new { status = "ok" });
            }
            return Json(new { status = "err" });
        }

        public JsonResult DeleteDichVuById(int id)
        {
            DichVu dichVu = _unitOfWork.DichVu.GetById(id);
            _unitOfWork.DichVu.Delete(dichVu);
            if (_unitOfWork.Save())
            {
                return Json(new { status = "ok" });
            }
            return Json(new { status = "err" });
        }
    }
}
