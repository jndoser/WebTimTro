using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebTimTro.Data;
using WebTimTro.Interfaces;
using WebTimTro.Models;

namespace WebTimTro.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class NguoiDungController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public NguoiDungController(IUnitOfWork unitOfWork,
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
        public JsonResult GetAllNguoiDung(string txtSearch, int? page)
        {
            IEnumerable<NguoiDung> data = 
                _unitOfWork.NguoiDung.GetAllNguoiDungWithNguoiDungRole();
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

        public IActionResult Edit(string id)
        {
            var user = _unitOfWork.NguoiDung.GetUserById(id);
            var userModel = _mapper.Map<NguoiDung, NguoiDungVM>(user);

            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NguoiDungVM newUser)
        {
            var oldUser = _unitOfWork.NguoiDung.GetUserById(newUser.Id);
            var userModel = _mapper.Map<NguoiDungVM, NguoiDung>(newUser);
            var savedUser = oldUser;
            if (newUser.UploadFile == null)
            {
                savedUser.FirstName = userModel.FirstName;
                savedUser.MiddleName = userModel.MiddleName;
                savedUser.LastName = userModel.LastName;
                savedUser.Mobile = userModel.Mobile;
                savedUser.Profile = userModel.Profile;
                savedUser.Intro = userModel.Intro;

                _unitOfWork.NguoiDung.Update(savedUser);
                if (_unitOfWork.Save())
                {
                    
                    return RedirectToAction("Index");
                    
                }
            }
            else
            {
                var saveResult = await UploadFileOnServer(newUser.UploadFile);
                if (saveResult)
                {

                    savedUser.FirstName = userModel.FirstName;
                    savedUser.MiddleName = userModel.MiddleName;
                    savedUser.LastName = userModel.LastName;
                    savedUser.Mobile = userModel.Mobile;
                    savedUser.Profile = userModel.Profile;
                    savedUser.Intro = userModel.Intro;
                    savedUser.Avatar = newUser.UploadFile.FileName;

                    _unitOfWork.NguoiDung.Update(savedUser);
                    if (_unitOfWork.Save())
                    {
                        
                        return RedirectToAction("Index");
                                            
                    }
                }
            }


            return BadRequest();
        }

        public IActionResult Delete(string id)
        {
            var user = _unitOfWork.NguoiDung.GetUserById(id);
            _unitOfWork.NguoiDung.Delete(user);
            if (_unitOfWork.Save())
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }

        // Lưu file avatar vào thư mục images trong wwwroot
        public async Task<bool> UploadFileOnServer(IFormFile file)
        {
            string path = "";
            bool isCopied = false;
            try
            {
                if (file.Length > 0)
                {
                    string fileName = file.FileName;
                    path = Path.Combine(_env.WebRootPath, "images");
                    using (var filestream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                    }
                    isCopied = true;
                }
                else
                {
                    isCopied = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isCopied;
        }
    }
}
