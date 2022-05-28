using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebTimTro.Interfaces;

namespace WebTimTro.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ThongKeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ThongKeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ThongKeBinhLuanTheoDiaDiem()
        {
            List<int> totalNumbersOfBinhLuan = new List<int>();
            totalNumbersOfBinhLuan.Add(0);

            List<string> diaDiems = new List<string>();
            diaDiems.Add("Quận 1");
            diaDiems.Add("Thủ Đức");
            diaDiems.Add("Quận 3");
            diaDiems.Add("Quận 4");
            diaDiems.Add("Quận 5");
            diaDiems.Add("Quận 6");
            diaDiems.Add("Quận 7");
            diaDiems.Add("Quận 8");
            diaDiems.Add("Quận 10");
            diaDiems.Add("Quận 11");
            diaDiems.Add("Quận 12");
            diaDiems.Add("Bình Thạnh");
            diaDiems.Add("Bình Tân");
            diaDiems.Add("Gò Vấp");
            diaDiems.Add("Phú Nhuận");
            diaDiems.Add("Tân Bình");
            diaDiems.Add("Tân Phú");
            diaDiems.Add("Bình Chánh");
            diaDiems.Add("Cần Giờ");
            diaDiems.Add("Củ Chi");
            diaDiems.Add("Hóc Môn");
            diaDiems.Add("Nhà Bè");

            foreach(string diaDiem in diaDiems)
            {
                List<int> ids = _unitOfWork.PhongTro
                    .GetPhongTroIdsThuocDiaDiem(diaDiem);
                int numberOfBinhLuan = _unitOfWork.BinhLuan
                    .GetNumbersOfCommentByPostId(ids.ToArray());
                totalNumbersOfBinhLuan.Add(numberOfBinhLuan);
            }

            return Json(new {status = "ok", data = totalNumbersOfBinhLuan});
        }

        [HttpGet]
        public JsonResult ThongKeQuanTamTheoDiaDiem()
        {
            List<int> totalNumbersOfQuanTam = new List<int>();
            totalNumbersOfQuanTam.Add(0);

            List<string> diaDiems = new List<string>();
            diaDiems.Add("Quận 1");
            diaDiems.Add("Thủ Đức");
            diaDiems.Add("Quận 3");
            diaDiems.Add("Quận 4");
            diaDiems.Add("Quận 5");
            diaDiems.Add("Quận 6");
            diaDiems.Add("Quận 7");
            diaDiems.Add("Quận 8");
            diaDiems.Add("Quận 10");
            diaDiems.Add("Quận 11");
            diaDiems.Add("Quận 12");
            diaDiems.Add("Bình Thạnh");
            diaDiems.Add("Bình Tân");
            diaDiems.Add("Gò Vấp");
            diaDiems.Add("Phú Nhuận");
            diaDiems.Add("Tân Bình");
            diaDiems.Add("Tân Phú");
            diaDiems.Add("Bình Chánh");
            diaDiems.Add("Cần Giờ");
            diaDiems.Add("Củ Chi");
            diaDiems.Add("Hóc Môn");
            diaDiems.Add("Nhà Bè");

            foreach (string diaDiem in diaDiems)
            {
                List<int> ids = _unitOfWork.PhongTro
                    .GetPhongTroIdsThuocDiaDiem(diaDiem);
                int numbersOfQuanTam = _unitOfWork.PhongTroQuanTam
                    .GetNumbersOfCareByPostId(ids.ToArray());
                totalNumbersOfQuanTam.Add(numbersOfQuanTam);
            }

            return Json(new { status = "ok", data = totalNumbersOfQuanTam });
        }
    }
}
