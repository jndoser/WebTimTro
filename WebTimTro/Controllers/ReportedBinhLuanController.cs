using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebTimTro.Interfaces;
using WebTimTro.Models;

namespace WebTimTro.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ReportedBinhLuanController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportedBinhLuanController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        // Lấy các bình luận bị report, phân trang
        [HttpGet]
        public JsonResult GetAllReportedBinhLuan(string txtSearch, int? page)
        {
            var reportedComments = _unitOfWork.BinhLuan.GetReportedComments();
            var data = _mapper
                .Map<IEnumerable<ReportedBinhLuanVM>>(reportedComments);

            foreach (var comment in data)
            {
                comment.TenCuaNguoiDang = _unitOfWork.BinhLuan
                    .GetAuthorByCommentId(comment.Id);
            }

            if (!string.IsNullOrEmpty(txtSearch))
            {
                ViewBag.txtSearch = txtSearch;
                data = data.Where(x => x.NoiDung.ToLower().Contains(txtSearch.ToLower()));
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
            var dataBinhLuan = data.OrderByDescending(x => x.Id)
                .Skip(start).Take(7);
            List<ReportedBinhLuanVM> listBinhLuan = new List<ReportedBinhLuanVM>();
            listBinhLuan = dataBinhLuan.ToList();
            

            return Json(new { data = listBinhLuan, pageCurrent = page, numSize = numSize });
        }

        public IActionResult Hide(int id)
        {
            var comment = _unitOfWork.BinhLuan.GetById(id);
            comment.IsShow = false;
            _unitOfWork.BinhLuan.Update(comment);
            if (_unitOfWork.Save())
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }

        public IActionResult Show(int id)
        {
            var comment = _unitOfWork.BinhLuan.GetById(id);
            comment.IsShow = true;
            _unitOfWork.BinhLuan.Update(comment);
            if (_unitOfWork.Save())
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }
    }
}
