using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebTimTro.Interfaces;
using WebTimTro.Models;

namespace WebTimTro.Controllers
{
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
            var reportedComments = _unitOfWork.BinhLuan.GetReportedComments();
            var reportedCommentsModel = _mapper
                .Map<IEnumerable<ReportedBinhLuanVM>>(reportedComments);

            foreach (var comment in reportedCommentsModel)
            {
                comment.TenCuaNguoiDang = _unitOfWork.BinhLuan
                    .GetAuthorByCommentId(comment.Id);
            }

            return View(reportedCommentsModel);
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
