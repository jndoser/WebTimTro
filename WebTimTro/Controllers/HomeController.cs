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
            //// Lấy thông tin tất cả các bài viết trong phòng trọ
            //IEnumerable<PhongTro> phongTros = _unitOfWork.PhongTro.GetAll();
            //IEnumerable<PhongTroVM> phongTroVMs = _mapper
            //    .Map<IEnumerable<PhongTroVM>>(phongTros);

            //// Lấy tất cả các hình ảnh đầu tiên tương ứng với tất cả
            //// các phòng trọ ở trên
            //List<string> firstHinhAnhs = _unitOfWork.HinhAnh.GetFirstHinhAnhListOfPhongTroList();
            //ViewBag.FirstHinhAnhs = firstHinhAnhs;

            //// Lấy các dịch vụ tương ứng với tất cả các phòng trọ ở trên
            //List<string> someDichVu = _unitOfWork.DichVu.GetSomeDichVuOfPhongTroList();
            //ViewBag.SomeDichVu = someDichVu;

            //return View(phongTroVMs);
            return View();
        }

        // Lấy các post đã được tạo, phân trang
        [HttpGet]
        public JsonResult GetAllPost(string txtSearch, int? page)
        {
            var data = _unitOfWork.PhongTro.GetAll();

            // Định dạng lại độ dài của các trường dữ liệu để 
            // qua view in ra được đồng bộ
            foreach (var item in data)
            {
                if (item.Ten.Length > 30)
                {
                    item.Ten = item.Ten.Substring(0, 30) + "...";
                }
            }

            if (!string.IsNullOrEmpty(txtSearch))
            {
                ViewBag.txtSearch = txtSearch;
                data = data.Where(x => x.Ten.Contains(txtSearch));
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
            var dataPost = data.OrderByDescending(x => x.Id)
                .Skip(start).Take(7);
            List<PhongTro> listPost = new List<PhongTro>();
            listPost = dataPost.ToList();
            List<PhongTroVM> listPostVM = _mapper
                .Map<List<PhongTro>, List<PhongTroVM>>(listPost);


            // Lấy tất cả các hình ảnh đầu tiên tương ứng với tất cả
            // các phòng trọ ở trên
            IEnumerable<string> firstHinhAnhs = _unitOfWork.HinhAnh
                .GetFirstHinhAnhListOfPhongTroList().Skip(start).Take(7);
            List<string> firstHinhAnhList = firstHinhAnhs.ToList();

            // Lấy các dịch vụ tương ứng với tất cả các phòng trọ ở trên
            IEnumerable<string> someDichVu = _unitOfWork.DichVu
                .GetSomeDichVuOfPhongTroList().Skip(start).Take(7);
            List<string> someDichVuList = someDichVu.ToList();

            // Kiểm tra xem các bài viết đã được quan tâm hay đã được người dùng lưu chưa
            // và truyền sang phía giao diện
            List<string> savedStatusList = new List<string>();
            foreach (var phongTro in listPostVM)
            {
                if (_unitOfWork.PhongTroLuuTru.IsSaved(
                    _unitOfWork.NguoiDung.GetUserId(), phongTro.Id))
                {
                    savedStatusList.Add("Đã lưu");
                } else
                {
                    savedStatusList.Add("Lưu");
                }
            }

            List<string> favStatusList = new List<string>();
            foreach (var phongTro in listPostVM)
            {
                if (_unitOfWork.PhongTroQuanTam.IsQuanTam(
                    _unitOfWork.NguoiDung.GetUserId(), phongTro.Id))
                {
                    favStatusList.Add("Đã quan tâm");
                }
                else
                {
                    favStatusList.Add("Quan tâm");
                }
            }

            List<int> numberOfQuanTamList = new List<int>();
            foreach (var phongTro in listPostVM)
            {
                int numOfQuanTam = _unitOfWork.PhongTroQuanTam.NumbersOfQuanTam(phongTro.Id);
                numberOfQuanTamList.Add(numOfQuanTam);
            }

            return Json(new { data = listPostVM, pageCurrent = page, numSize = numSize,
                firstHinhAnhs = firstHinhAnhList, someDichVu = someDichVuList,
                savedStatusList = savedStatusList, favStatusList = favStatusList,
                quanTams = numberOfQuanTamList });
        }

        public IActionResult Detail(int id)
        {
            PhongTro phongTro = _unitOfWork.PhongTro.GetById(id);
            PhongTroVM phongTroVM = _mapper
                .Map<PhongTroVM>(phongTro);

            List<HinhAnh> hinhAnhs = _unitOfWork.PhongTroHinhAnh
                .GetHinhAnhsByPhongTroId(id);

            List<string> hinhAnhFileNames = new List<string>();
            foreach (HinhAnh hinhAnh in hinhAnhs)
            {
                hinhAnhFileNames.Add(hinhAnh.Filename);
            }

            ViewBag.HinhAnhFileNames = hinhAnhFileNames;

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

        public JsonResult QuanTamPhongTro(int id)
        {
            string nguoiDungId = _unitOfWork.NguoiDung.GetUserId();
            PhongTroQuanTam phongTroQuanTam = new PhongTroQuanTam
            {
                NguoiDungId = nguoiDungId,
                PhongTroId = id
            };
            _unitOfWork.PhongTroQuanTam.Create(phongTroQuanTam);
            if (_unitOfWork.Save())
            {
                return new JsonResult(new { status = "ok" });
            }
            else
            {
                return new JsonResult(new { status = "err" });
            }
        }

        public JsonResult BoQuanTamPhongTro(int id)
        {
            string nguoiDungId = _unitOfWork.NguoiDung.GetUserId();
            PhongTroQuanTam phongTroQuanTam = _unitOfWork
                .PhongTroQuanTam.GetPhongTroQuanTamByNguoiDungIdAndPhongTroId(nguoiDungId, id);

            _unitOfWork.PhongTroQuanTam.Delete(phongTroQuanTam);
            if (_unitOfWork.Save())
            {
                return new JsonResult(new { status = "ok" });
            }
            else
            {
                return new JsonResult(new { status = "err" });
            }
        }

        [HttpPost]
        public JsonResult SaveCommentOnPost(int postId, string content)
        {
            // Lấy ra id của người dùng đã comment bài viết
            var ownedId = _unitOfWork.NguoiDung.GetUserId();
            BinhLuan newComment = new BinhLuan
            {
                //Id = _repo.GetNewCommentId(),
                PhongTroId = postId,
                NoiDung = content,
                NguoiDungId = ownedId,
                NgayDang = DateTime.Now,
                RootId = 0
            };
            _unitOfWork.BinhLuan.Create(newComment);

            if (_unitOfWork.Save())
            {
                // Lấy ra id của bình luận gần đây để có thể lấy ra bình luận gần đây
                // truyền qua view để hiển thị bình luận người dùng vừa viết
                var recentCommentId = Convert.ToInt32(_unitOfWork.BinhLuan.GetRecentCommentId());
                var loggedUserAvatar = _unitOfWork.NguoiDung.GetAvatarByName(User.Identity.Name);

                /* // Send email to author that new user comment on post
                 string emailOfAuthor = _userManagerRepo.GetEmailByUserName(User.Identity.Name);
                 Email email = new Email();

                 // Because the email i use to register account is fake, so 
                 // i use my email to test the mail alert when user comment
                 // on post
                 email.SendEmail(*//*emailOfAuthor*//*"hoanglongdev533@gmail.com",
                     _config.GetValue<string>("MailAccount:Email"),
                     _config.GetValue<string>("MailAccount:Password"),
                     $"{_userManagerRepo.GetNameByUserName(User.Identity.Name)} has commented on your post",
                     $"Content of comment: {content}");*/


                return Json(new
                {
                    status = "ok",
                    result = _mapper.Map<BinhLuanVM>(_unitOfWork.BinhLuan.GetById(recentCommentId)),
                    nameOfUserWriteThisComment = _unitOfWork.BinhLuan
                    .GetAuthorByCommentId(recentCommentId),
                    avatarOfUserWriteThisComment = _unitOfWork.BinhLuan
                    .GetAvatarOfAuthorByCommentId(recentCommentId),
                    loggedUserAvatar = loggedUserAvatar
                });
            }

            return Json(new { status = "err" });
        }

        public JsonResult LoadComments(int postId)
        {
            var listComment = _unitOfWork.BinhLuan.GetCommentsByPhongTroId(postId);
            var listAuthorName = _unitOfWork.BinhLuan.GetListOfAuthorNameByListComment(listComment);
            var listAvatar = _unitOfWork.BinhLuan.GetListOfAvatarByListComment(listComment);
            var loggedUserAvatar = _unitOfWork.NguoiDung.GetAvatarByName(User.Identity.Name);

            return Json(new
            {
                listComment = _mapper.Map<List<BinhLuanVM>>(listComment),
                listAuthorName = listAuthorName,
                listAvatar = listAvatar,
                loggedUserAvatar = loggedUserAvatar
            });
        }

        public JsonResult LoadSubCommentsByParentCommentId(int id)
        {
            var subComments = _unitOfWork.BinhLuan.GetSubCommentsById(id);
            // Get lish of author name
            var listAuthorName = _unitOfWork.BinhLuan.GetListOfAuthorNameByListComment(subComments);
            var listAvatar = _unitOfWork.BinhLuan.GetListOfAvatarByListComment(subComments);
            var loggedUserAvatar = _unitOfWork.NguoiDung.GetAvatarByName(User.Identity.Name);

            var listOfReplyUsers = new List<string>();

            foreach (var item in subComments)
            {
                var userReply = _unitOfWork.BinhLuan.GetReplyUserNameByCommentId(item.Id);
                listOfReplyUsers.Add(userReply);
            }

            return Json(new
            {
                result = _mapper.Map<List<BinhLuanVM>>(subComments),
                replyUser = listOfReplyUsers,
                listAuthorName = listAuthorName,
                listAvatar = listAvatar,
                loggedUserAvatar = loggedUserAvatar
            });
        }

        [HttpPost]
        public JsonResult SaveCommentOnComment(int postId, string content, int parentCmtId)
        {
            // Get the user that write this comment
            var ownedId = _unitOfWork.NguoiDung.GetUserId();
            var newComment = new BinhLuan
            {
                PhongTroId = postId,
                NoiDung = content,
                RootId = _unitOfWork.BinhLuan.GetRootIdByCommentId(parentCmtId),
                NguoiDungId = ownedId,
                ParentId = parentCmtId,
                NgayDang = DateTime.Now
            };
            _unitOfWork.BinhLuan.Create(newComment);
            if (_unitOfWork.Save())
            {
                // Convert long to int, this is terrable but will fix soon
                var recentCommentId = Convert.ToInt32(_unitOfWork.BinhLuan.GetRecentCommentId());
                var savedComment = _unitOfWork.BinhLuan.GetById(recentCommentId);
                var userNameReply = _unitOfWork.BinhLuan
                    .GetReplyUserNameByCommentId(savedComment.Id);
                var loggedUserAvatar = _unitOfWork.NguoiDung
                    .GetAvatarByName(User.Identity.Name);

                return Json(new
                {
                    status = "ok",
                    rootId = savedComment.RootId,
                    replyUserName = userNameReply,
                    result = _mapper.Map<BinhLuanVM>(savedComment),
                    nameOfUserWriteThisComment = _unitOfWork.BinhLuan
                    .GetAuthorByCommentId(recentCommentId),
                    avatarOfUserWriteThisComment = _unitOfWork.BinhLuan
                    .GetAvatarOfAuthorByCommentId(recentCommentId),
                    loggedUserAvatar = loggedUserAvatar
                });
            }

            return Json(new { status = "err" });
        }

        // Method to report a comment by their id
        [HttpPost]
        public JsonResult ReportCommentById(int id)
        {
            var comment = _unitOfWork.BinhLuan.GetById(id);
            comment.IsReported = true;
            _unitOfWork.BinhLuan.Update(comment);
            _unitOfWork.Save();

            return Json(comment);
        }

        // Tìm kiếm phòng trọ theo yêu cầu và trả về kết quả 
        // tương ứng
        public JsonResult SearchPhongTro(SearchPhongTroOptionsVM searchOptions, int? page)
        {
            var data = _unitOfWork.PhongTro.GetAll()
                .Where(x =>
                ((searchOptions.KhuVuc.Equals("Tất cả")) ? true : x.DiaChi.Contains(searchOptions.KhuVuc))
                && ((searchOptions.Gia == 0) ? true : x.Gia <= searchOptions.Gia)
                && ((searchOptions.SoNguoiO == 0) ? true : x.SucChua == searchOptions.SoNguoiO)
                && ((searchOptions.TienIchLanCan == null) ? true : x.MoTa.Contains(searchOptions.TienIchLanCan)));


            // Định dạng lại độ dài của các trường dữ liệu để 
            // qua view in ra được đồng bộ
            foreach (var item in data)
            {
                if (item.Ten.Length > 30)
                {
                    item.Ten = item.Ten.Substring(0, 30) + "...";
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
            int totalPage = data.Count();
            float totalNumsize = (totalPage / (float)7);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var dataPost = data.OrderByDescending(x => x.Id)
                .Skip(start).Take(7);
            List<PhongTro> listPost = new List<PhongTro>();
            listPost = dataPost.ToList();
            List<PhongTroVM> listPostVM = _mapper
                .Map<List<PhongTro>, List<PhongTroVM>>(listPost);


            // Lấy tất cả các hình ảnh đầu tiên tương ứng với tất cả
            // các phòng trọ ở trên
            IEnumerable<string> firstHinhAnhs = _unitOfWork.HinhAnh
                .GetFirstHinhAnhListOfPhongTroList(data).Skip(start).Take(7)
                .Reverse();
            List<string> firstHinhAnhList = firstHinhAnhs.ToList();

            // Lấy các dịch vụ tương ứng với tất cả các phòng trọ ở trên
            IEnumerable<string> someDichVu = _unitOfWork.DichVu
                .GetSomeDichVuOfPhongTroList(data).Skip(start).Take(7);
            List<string> someDichVuList = someDichVu.ToList();

            // Kiểm tra xem các bài viết đã được quan tâm hay đã được người dùng lưu chưa
            // và truyền sang phía giao diện
            List<string> savedStatusList = new List<string>();
            foreach (var phongTro in listPostVM)
            {
                if (_unitOfWork.PhongTroLuuTru.IsSaved(
                    _unitOfWork.NguoiDung.GetUserId(), phongTro.Id))
                {
                    savedStatusList.Add("Đã lưu");
                }
                else
                {
                    savedStatusList.Add("Lưu");
                }
            }

            List<string> favStatusList = new List<string>();
            foreach (var phongTro in listPostVM)
            {
                if (_unitOfWork.PhongTroQuanTam.IsQuanTam(
                    _unitOfWork.NguoiDung.GetUserId(), phongTro.Id))
                {
                    favStatusList.Add("Đã quan tâm");
                }
                else
                {
                    favStatusList.Add("Quan tâm");
                }
            }

            List<int> numberOfQuanTamList = new List<int>();
            foreach (var phongTro in listPostVM)
            {
                int numOfQuanTam = _unitOfWork.PhongTroQuanTam.NumbersOfQuanTam(phongTro.Id);
                numberOfQuanTamList.Add(numOfQuanTam);
            }

            return Json(new
            {
                data = listPostVM,
                pageCurrent = page,
                numSize = numSize,
                firstHinhAnhs = firstHinhAnhList,
                someDichVu = someDichVuList,
                savedStatusList = savedStatusList,
                favStatusList = favStatusList,
                quanTams = numberOfQuanTamList
            });
        }

        [HttpPost]
        public JsonResult GetAllPictures(int phongTroId)
        {
            List<string> hinhAnhFileName = new List<string>();
            var hinhAnhs = _unitOfWork.PhongTroHinhAnh
                .GetHinhAnhsByPhongTroId(phongTroId);

            foreach(var hinhAnh in hinhAnhs)
            {
                hinhAnhFileName.Add(hinhAnh.Filename);
            }
            return Json(new { status = "ok", data = hinhAnhFileName });
        }

        [HttpPost]
        public JsonResult GetAllServices(int phongTroId)
        {
            // Tìm các dịch vụ của phòng trọ và truyền đến view
            List<DichVu> dichVus = _unitOfWork.
                PhongTroDichVu.GetDichVusByPhongTroId(phongTroId);
            List<DichVuVM> dichVusVM = _mapper
                .Map<List<DichVu>, List<DichVuVM>>(dichVus);

            return Json(new { status = "ok", data = dichVusVM });
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
