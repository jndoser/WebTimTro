using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebTimTro.Interfaces;
using WebTimTro.Models;

namespace WebTimTro.Controllers
{
    public class TimKiemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TimKiemController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetPhongTroLanCan()
        {
            List<string> coords = new List<string>();
            var phongTros = _unitOfWork.PhongTro.GetAll();

            foreach(var phongTro in phongTros)
            {
                string coord = phongTro.Latitude + "," + phongTro.Longitude;
                coords.Add(coord);
            }

            return Json(new { status = "ok", data = coords });
        }

        [HttpGet]
        public async Task<JsonResult> GetPhongTroNearby(decimal lat, decimal lng)
        {
            DistanceResponseModel distances = new DistanceResponseModel();
            var phongTros = _unitOfWork.PhongTro.GetAll();
            string apiKey = "E5ycgkTiRNvLJ7BqyM2H4kC0LJWGqbJKK2eVWUQH";
            string destinationUrlPart = "";
            foreach(var phongTro in phongTros)
            {
                destinationUrlPart += phongTro.Latitude + "," + phongTro.Longitude + "%7C";
            }
            destinationUrlPart = destinationUrlPart.Substring(0, destinationUrlPart.Length - 4);
            string restAPIUrl = $"https://rsapi.goong.io/DistanceMatrix?origins={lat},{lng}&destinations={destinationUrlPart}&vehicle=car&api_key={apiKey}";
            //string apiResponse = "";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(restAPIUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    distances = JsonConvert.DeserializeObject<DistanceResponseModel>(apiResponse);
                }
            }

            // Lấy ra khoảng cách đến các phòng trọ khác
            List<int> khoangCachs = new List<int>();
            foreach(var item in distances.rows[0].elements)
            {
                khoangCachs.Add(item.distance.value);
            }

            // Lấy tất cả các tên file hình ảnh đầu tiên của phòng trọ
            IEnumerable<string> firstHinhAnhs = _unitOfWork.HinhAnh
                .GetFirstHinhAnhListOfPhongTroList();
            List<string> firstHinhAnhList = firstHinhAnhs.Reverse().ToList();

            return Json(new { status = "ok", phongTros = phongTros, khoangCachs = khoangCachs,
                firstHinhAnhList = firstHinhAnhList
            });
        }
    }
}
