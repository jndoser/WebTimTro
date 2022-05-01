using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebTimTro.Data;
using WebTimTro.Interfaces;
using WebTimTro.Models;

namespace WebTimTro.Controllers
{
    public class ChuTroController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ChuTroController(IUnitOfWork unitOfWork,
            IMapper mapper,
            IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }
        public IActionResult Index()
        {
            var users = _unitOfWork.NguoiDung.GetAllNguoiDungWithChuTroRole();
            var usersModel = _mapper.Map<IEnumerable<NguoiDung>, IEnumerable<NguoiDungVM>>(users);

            return View(usersModel);
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
                    path = Path.Combine(_env.WebRootPath, "assets/images");
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
