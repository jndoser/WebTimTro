using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebTimTro.Data;
using WebTimTro.Interfaces;
using WebTimTro.Models;



namespace WebTimTro.Controllers
{
    public class PhongTroController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public PhongTroController(IUnitOfWork unitOfWork,
            IWebHostEnvironment env,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            // Tìm tất cả các dịch vụ có trong dữ liệu
            List<DichVu> allDichVu = _unitOfWork
                .DichVu.GetAll().ToList();
            List<DichVuVM> allDichVuVM = _mapper
                .Map<List<DichVu>, List<DichVuVM>>(allDichVu);
            ViewBag.TatCaDichVu = allDichVuVM;


            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var phongTro = _unitOfWork.PhongTro.GetById(id);
            var phongTroVM = _mapper.Map<PhongTro, PhongTroVM>(phongTro);

            // Tìm ghi chú của phòng trọ và truyền lại về phía view
            var ghiChuId = _unitOfWork.Note.GetNoteIdByPhongTroId(id);
            var ghiChu = _unitOfWork.Note.GetById(ghiChuId);
            var ghiChuVM = _mapper.Map<Note, NoteVM>(ghiChu);
            ViewBag.GhiChu = ghiChuVM;

            // Tìm các dịch vụ của phòng trọ và truyền đến view
            List<DichVu> dichVus = _unitOfWork.
                PhongTroDichVu.GetDichVusByPhongTroId(id);
            List<DichVuVM> dichVusVM = _mapper
                .Map<List<DichVu>, List<DichVuVM>>(dichVus);
            ViewBag.DichVuCuaPhongTro = dichVusVM;

            // Tìm tất cả các dịch vụ có trong dữ liệu
            List<DichVu> allDichVu = _unitOfWork
                .DichVu.GetAll().ToList();
            List<DichVuVM> allDichVuVM = _mapper
                .Map<List<DichVu>, List<DichVuVM>>(allDichVu);
            ViewBag.TatCaDichVu = allDichVuVM;

            // Tìm tất cả các file hình ảnh về phòng trọ
            // truyền đến view
            List<HinhAnh> hinhAnhs = _unitOfWork.PhongTroHinhAnh
                .GetHinhAnhsByPhongTroId(id);
            List<HinhAnhVM> hinhAnhsVM = _mapper.Map<List<HinhAnh>,
                List<HinhAnhVM>>(hinhAnhs);
            ViewBag.HinhAnh = hinhAnhsVM;

            return View(phongTroVM);
        }

        // Xoá bài viết về phòng trọ (cách thông thường)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            // Xoá các hình ảnh liên quan đến phòng trọ
            _unitOfWork.HinhAnh.DeleteHinhAnhsByPhongTroId(id);
            // Xoá các dịch vụ liên quan đến phòng trọ
            _unitOfWork.DichVu.DeleteDichVusByPhongTroId(id);
            // Xoá các ghi chú liên quan đến phòng trọ
            _unitOfWork.Note.DeleteNotesByPhongTroId(id);
            // Xoá phòng trọ
            var phongTro = _unitOfWork.PhongTro.GetById(id);
            _unitOfWork.PhongTro.Delete(phongTro);

            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult TestUploadFile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(List<int> dichVu, PhongTroVM phongTro,
            NoteVM ghiChu, List<string> uploadedFilenames)
        {

            // Lưu lại dữ liệu phòng trọ
            // Đầu tiên lấy id hiện tại của chủ trọ đang đăng nhập 
            var userId = _unitOfWork.NguoiDung.GetUserId();
            
            // Map dữ liệu ViewModel của phòng trọ sang dữ liệu phòng trọ 
            // có thể lưu trữ vào database 
            var phongTroData = _mapper.Map<PhongTroVM, PhongTro>(phongTro);
            phongTroData.ChuTroId = userId;
            _unitOfWork.PhongTro.Create(phongTroData);
            _unitOfWork.Save();

            // Lưu dữ liệu hình ảnh tương ứng với phòng trọ đã tạo 
            foreach(var item in uploadedFilenames)
            {
                _unitOfWork.HinhAnh.Create(new HinhAnh { Filename = item });
                _unitOfWork.Save();

                int addedHinhAnhId = _unitOfWork.HinhAnh.GetAddedHinhAnhId();
                var phongTroHinhAnh = new PhongTroHinhAnh
                {
                    PhongTroId = _unitOfWork.PhongTro.GetAddedPostId(),
                    HinhAnhId = addedHinhAnhId
                };
                _unitOfWork.PhongTroHinhAnh.Create(phongTroHinhAnh);
                _unitOfWork.Save();
            }

            // Lưu lại dữ liệu của các dịch vụ chủ trọ đã chọn 
            foreach(var item in dichVu)
            {
                var phongTroDichVu = new PhongTroDichVu
                {
                    PhongTroId = _unitOfWork.PhongTro.GetAddedPostId(),
                    DichVuId = item
                };
                _unitOfWork.PhongTroDichVu.Create(phongTroDichVu);
                _unitOfWork.Save();
            }

            // Lưu lại ghi chú chủ trọ đã viết
            var ghiChuData = _mapper.Map<NoteVM, Note>(ghiChu);
            _unitOfWork.Note.Create(ghiChuData);
            _unitOfWork.Save();

            // Lưu lại dữ liệu của ghi chú tương ứng với phòng trọ 
            var phongTroGhiChu = new PhongTroNote
            {
                PhongTroId = _unitOfWork.PhongTro.GetAddedPostId(),
                NoteId = _unitOfWork.Note.GetAddedNoteId()
            };
            _unitOfWork.PhongTroNote.Create(phongTroGhiChu);

            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        // Upload nhiều file ảnh của phòng trọ
        [HttpPost]
        public async Task<JsonResult> UploadFile(IList<IFormFile> files)
        {
            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile source in files)
            {
                string fileName = await UploadFileOnServer(source);
                uploadedFiles.Add(fileName);
            }

            return Json(new {status = "ok", uploadedfiles = uploadedFiles});
        }

        // Upload một file vào thư mục images trong wwwroot
        public async Task<string> UploadFileOnServer(IFormFile file)
        {
            string path = "";
            bool isCopied = false;
            string fileName = "";
            try
            {
                if (file.Length > 0)
                {
                    fileName = file.FileName;
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
            catch (Exception)
            {
                throw;
            }
            if (isCopied)
            {
                return fileName;
            }
            return "";
        }

        // Lấy các post đã được tạo, phân trang
        [HttpGet]
        public JsonResult GetAllPost(string txtSearch, int? page)
        {
            var data = _unitOfWork.PhongTro.GetAll();
            if (!string.IsNullOrEmpty(txtSearch))
            {
                ViewBag.txtSearch = txtSearch;
                data = data.Where(x => x.Ten.Contains(txtSearch));
            }
            if(page > 0)
            {
                
            }else
            {
                page = 1;
            }
            int start = (int)(page - 1) * 10; // 10 is pageSize
            ViewBag.pageCurrent = page;
            int totalPage = data.Count();
            float totalNumsize = (totalPage / (float)10);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var dataPost = data.OrderByDescending(x => x.Id)
                .Skip(start).Take(10);
            List<PhongTro> listPost = new List<PhongTro>();
            listPost = dataPost.ToList();
            List<PhongTroVM> listPostVM = _mapper
                .Map<List<PhongTro>, List<PhongTroVM>>(listPost);

            return Json(new { data = listPostVM, pageCurrent = page, numSize = numSize });
        }

        // Lấy các dịch vụ có trong phòng trọ với id của phòng trọ
        [HttpPost]
        public JsonResult GetDichVuByPhongTroId(int phongTroId)
        {
            List<DichVu> dichVus = _unitOfWork.
                PhongTroDichVu.GetDichVusByPhongTroId(phongTroId);
            List<DichVuVM> dichVusVM = _mapper
                .Map<List<DichVu>, List<DichVuVM>>(dichVus);

            return Json(new { data = dichVusVM });
        }

        // Xoá hình ảnh được yêu cầu từ view
        [HttpPost]
        public JsonResult DeleteHinhAnh(int id)
        {
            // Xoá mối quan hệ giữa hình ảnh với phòng trọ
            _unitOfWork.PhongTroHinhAnh.DeletePhongTroHinhAnhByHinhAnhId(id);
            // Xoá hình ảnh
            var hinhAnh = _unitOfWork.HinhAnh.GetById(id);
            _unitOfWork.HinhAnh.Delete(hinhAnh);
            if (_unitOfWork.Save())
            {
                return Json(new { status = "ok" });
            }
            return Json(new { status = "error" });
        }

        // Update lại dữ liệu phòng trọ
        [HttpPost]
        public IActionResult UpdatePhongTro(List<int> dichVu, PhongTroVM phongTro,
            NoteVM ghiChu, List<string> uploadedFilenames)
        {
            // Lưu lại dữ liệu phòng trọ
            // Đầu tiên lấy id hiện tại của chủ trọ đang đăng nhập 
            var userId = _unitOfWork.NguoiDung.GetUserId();

            // Map dữ liệu ViewModel của phòng trọ sang dữ liệu phòng trọ 
            // có thể update vào database 
            var phongTroData = _mapper.Map<PhongTroVM, PhongTro>(phongTro);
            phongTroData.ChuTroId = userId;
            _unitOfWork.PhongTro.Update(phongTroData);
            _unitOfWork.Save();

            // Update dữ liệu hình ảnh tương ứng với phòng trọ đã tạo 
            if(uploadedFilenames != null)
            {
                // Lấy ra các filename của những hình ảnh về
                // phòng trọ trước đây
                List<string> hinhAnhsFilename = _unitOfWork.HinhAnh
                    .GetListFileNameByPhongTroId(phongTro.Id);
                foreach (var item in uploadedFilenames)
                {
                    // Chỉ upload những hình ảnh mới
                    // và lưu nó vào phòng trọ
                    if (!hinhAnhsFilename.Contains(item))
                    {
                        _unitOfWork.HinhAnh.Create(new HinhAnh { Filename = item });
                        _unitOfWork.Save();

                        int addedHinhAnhId = _unitOfWork.HinhAnh.GetAddedHinhAnhId();
                        var phongTroHinhAnh = new PhongTroHinhAnh
                        {
                            PhongTroId = phongTro.Id,
                            HinhAnhId = addedHinhAnhId
                        };
                        _unitOfWork.PhongTroHinhAnh.Create(phongTroHinhAnh);
                        _unitOfWork.Save();
                    }          
                }
            }
            

            // Tìm các dịch vụ hiện tại của phòng trọ
            List<DichVu> dichVusHienTai = _unitOfWork.
                PhongTroDichVu.GetDichVusByPhongTroId(phongTro.Id);

            // Lưu lại dữ liệu của các dịch vụ mới chủ trọ đã chọn
            // các dịch vụ này khác so với các dịch vụ cũ
            foreach (var item in dichVu)
            {
                var dichVuTemp = _unitOfWork.DichVu.GetById(item);

                if (!dichVusHienTai.Contains(dichVuTemp))
                {
                    var phongTroDichVu = new PhongTroDichVu
                    {
                        PhongTroId = phongTro.Id,
                        DichVuId = item
                    };
                    _unitOfWork.PhongTroDichVu.Create(phongTroDichVu);
                    _unitOfWork.Save();
                }
            }

            // Xoá các dịch vụ cũ của phòng trọ mà người dùng đã bỏ chọn
            foreach (var item in dichVusHienTai)
            {
                if (!dichVu.Contains(item.Id))
                {
                    var phongTroDichVu = new PhongTroDichVu
                    {
                        PhongTroId = phongTro.Id,
                        DichVuId = item.Id
                    };
                    _unitOfWork.PhongTroDichVu.Delete(phongTroDichVu);
                    _unitOfWork.Save();
                }
            }

            // Lưu lại ghi chú chủ trọ đã viết
            var ghiChuData = _mapper.Map<NoteVM, Note>(ghiChu);
            // Lấy ra ghi chú hiện tại của phòng trọ
            var ghiChuId = _unitOfWork.Note.GetNoteIdByPhongTroId(phongTro.Id);
            ghiChuData.Id = ghiChuId;
            _unitOfWork.Note.Update(ghiChuData);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }


        // Xoá phòng trọ khi biết id của nó ( sử dụng ajax)
        [HttpPost]
        public JsonResult DeletePhongTro(int id)
        {
            // Xoá các hình ảnh liên quan đến phòng trọ
            _unitOfWork.HinhAnh.DeleteHinhAnhsByPhongTroId(id);
            // Xoá các dịch vụ liên quan đến phòng trọ
            _unitOfWork.DichVu.DeleteDichVusByPhongTroId(id);
            // Xoá các ghi chú liên quan đến phòng trọ
            _unitOfWork.Note.DeleteNotesByPhongTroId(id);
            // Xoá các bình luận của phòng trọ
            _unitOfWork.BinhLuan.DeleteBinhLuansByPhongTroId(id);
            // Xoá phòng trọ
            var phongTro = _unitOfWork.PhongTro.GetById(id);
            _unitOfWork.PhongTro.Delete(phongTro);

            if (_unitOfWork.Save())
            {
                return Json(new { status = "ok" });
            }

            return Json(new { status = "error" });
        }

        // Hiển thị các phòng trọ đã lưu
        // [Authorize(Roles = "nguoidung")]
        public IActionResult Saved()
        {
            string nguoiDungId = _unitOfWork.NguoiDung.GetUserId();
            List<PhongTro> phongTros = _unitOfWork.PhongTro
                .GetPhongTrosByNguoiDungId(nguoiDungId);
            List<PhongTroVM> phongTroVM = _mapper.Map<List<PhongTroVM>>(phongTros);

            return View(phongTroVM);
        }

        // Xoá bài viết mà người dùng đã lưu với id cho trước
        public JsonResult DeleteSavedPhongTro(int id)
        {
            string nguoiDungId = _unitOfWork.NguoiDung.GetUserId();
            PhongTroLuuTru phongTroLuuTru = _unitOfWork.PhongTroLuuTru
                .GetPhongTroLuuTruByNguoiDungIdAndPhongTroId(nguoiDungId, id);
            _unitOfWork.PhongTroLuuTru.Delete(phongTroLuuTru);
            if (_unitOfWork.Save())
            {
                return Json(new { status = "ok" });
            } else
            {
                return Json(new { status = "err" });
            }
        }
    }
}
