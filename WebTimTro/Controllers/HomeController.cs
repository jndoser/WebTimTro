using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebTimTro.Data;
using WebTimTro.Interfaces;
using WebTimTro.Models;
using WebTimTro.Services.MoMo;

namespace WebTimTro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private static List<int> savedDichVus = new List<int>();

        // MoMo stuff
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public HomeController(ILogger<HomeController> logger,
            IUnitOfWork unitOfWork, IMapper mapper, 
            IWebHostEnvironment env)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public IActionResult Index()
        {
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
                && ((searchOptions.TienIchLanCan == null) ? true : x.MoTa.ToLower().Contains(searchOptions.TienIchLanCan.ToLower())));


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
        public JsonResult FilterPhongTro(FilterPhongTroOptionsVM filterOptions, 
            List<int> dichVus, int page, int filterStatus)
        {
            // filterStatus = 0 tức là lọc lại từ đầu
            // nếu != 0 thì là đang ấn next page trong pagination
            if(filterStatus == 0)
            {
                // Lưu lại list dịch vụ để khi bên UI người dùng
                // ấn qua trang khác thì còn có dữ liệu cũ để thực hiện lọc
                
                // Trước khi lưu thì xoá đi dữ liệu cũ nếu có
                if(savedDichVus.Count == 0)
                {
                    savedDichVus.AddRange(dichVus);
                } else
                {
                    savedDichVus.RemoveRange(0, dichVus.Count);
                    savedDichVus.AddRange(dichVus);
                }

                IEnumerable<PhongTro> data = null;
                if(filterOptions.SortStatus == true)
                {
                    data = _unitOfWork.PhongTro.GetAll()
                 .Where(x =>
                 ((filterOptions.KhuVuc.Equals("Tất cả")) ? true : x.District.Contains(filterOptions.KhuVuc))
                 && ((filterOptions.GiaFrom == 0) ? true : x.Gia >= filterOptions.GiaFrom)
                 && ((filterOptions.GiaTo == 0) ? true : x.Gia <= filterOptions.GiaTo)
                 && ((filterOptions.SucChua == 0) ? true : x.SucChua > filterOptions.SucChua))
                 .OrderBy(x => x.Gia);
                } else
                {
                    data = _unitOfWork.PhongTro.GetAll()
                 .Where(x =>
                 ((filterOptions.KhuVuc.Equals("Tất cả")) ? true : x.District.Contains(filterOptions.KhuVuc))
                 && ((filterOptions.GiaFrom == 0) ? true : x.Gia >= filterOptions.GiaFrom)
                 && ((filterOptions.GiaTo == 0) ? true : x.Gia <= filterOptions.GiaTo)
                 && ((filterOptions.SucChua == 0) ? true : x.SucChua > filterOptions.SucChua))
                 .OrderByDescending(x => x.Gia);
                }

                List<PhongTro> realData = new List<PhongTro>();

                if(savedDichVus.Count > 0)
                {
                    foreach (var item in data)
                    {
                        if (_unitOfWork.PhongTroDichVu.GetDichVusByPhongTroId(item.Id)
                            .Any(x => savedDichVus.Contains(x.Id)))
                        {
                            realData.Add(item);
                        }
                    }
                } else
                {
                    realData.AddRange(data);
                }
                


                // Định dạng lại độ dài của các trường dữ liệu để 
                // qua view in ra được đồng bộ
                foreach (var item in realData)
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
                var dataPost = realData.Skip(start).Take(7);
                List<PhongTro> listPost = new List<PhongTro>();
                listPost = dataPost.ToList();
                List<PhongTroVM> listPostVM = _mapper
                    .Map<List<PhongTro>, List<PhongTroVM>>(listPost);


                // Lấy tất cả các hình ảnh đầu tiên tương ứng với tất cả
                // các phòng trọ ở trên
                IEnumerable<string> firstHinhAnhs = _unitOfWork.HinhAnh
                    .GetFirstHinhAnhListOfPhongTroList(realData).Skip(start).Take(7);
                List<string> firstHinhAnhList = firstHinhAnhs.ToList();

                // Lấy các dịch vụ tương ứng với tất cả các phòng trọ ở trên
                IEnumerable<string> someDichVu = _unitOfWork.DichVu
                    .GetSomeDichVuOfPhongTroList(realData).Skip(start).Take(7);
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
            } else
            {
                IEnumerable<PhongTro> data = null;
                if (filterOptions.SortStatus == true)
                {
                    data = _unitOfWork.PhongTro.GetAll()
                 .Where(x =>
                 ((filterOptions.KhuVuc.Equals("Tất cả")) ? true : x.District.Contains(filterOptions.KhuVuc))
                 && ((filterOptions.GiaFrom == 0) ? true : x.Gia >= filterOptions.GiaFrom)
                 && ((filterOptions.GiaTo == 0) ? true : x.Gia <= filterOptions.GiaTo)
                 && ((filterOptions.SucChua == 0) ? true : x.SucChua > filterOptions.SucChua))
                 .OrderBy(x => x.Gia);
                }
                else
                {
                    data = _unitOfWork.PhongTro.GetAll()
                 .Where(x =>
                 ((filterOptions.KhuVuc.Equals("Tất cả")) ? true : x.District.Contains(filterOptions.KhuVuc))
                 && ((filterOptions.GiaFrom == 0) ? true : x.Gia >= filterOptions.GiaFrom)
                 && ((filterOptions.GiaTo == 0) ? true : x.Gia <= filterOptions.GiaTo)
                 && ((filterOptions.SucChua == 0) ? true : x.SucChua > filterOptions.SucChua))
                 .OrderByDescending(x => x.Gia);
                }

                List<PhongTro> realData = new List<PhongTro>();

                if (savedDichVus.Count > 0)
                {
                    foreach (var item in data)
                    {
                        if (_unitOfWork.PhongTroDichVu.GetDichVusByPhongTroId(item.Id)
                            .Any(x => savedDichVus.Contains(x.Id)))
                        {
                            realData.Add(item);
                        }
                    }
                }
                else
                {
                    realData.AddRange(data);
                }


                // Định dạng lại độ dài của các trường dữ liệu để 
                // qua view in ra được đồng bộ
                foreach (var item in realData)
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
                var dataPost = realData.Skip(start).Take(7);
                List<PhongTro> listPost = new List<PhongTro>();
                listPost = dataPost.ToList();
                List<PhongTroVM> listPostVM = _mapper
                    .Map<List<PhongTro>, List<PhongTroVM>>(listPost);


                // Lấy tất cả các hình ảnh đầu tiên tương ứng với tất cả
                // các phòng trọ ở trên
                IEnumerable<string> firstHinhAnhs = _unitOfWork.HinhAnh
                    .GetFirstHinhAnhListOfPhongTroList(realData).Skip(start).Take(7);
                List<string> firstHinhAnhList = firstHinhAnhs.ToList();

                // Lấy các dịch vụ tương ứng với tất cả các phòng trọ ở trên
                IEnumerable<string> someDichVu = _unitOfWork.DichVu
                    .GetSomeDichVuOfPhongTroList(realData).Skip(start).Take(7);
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

        [HttpGet]
        public JsonResult GetAllServices()
        {
            // Tìm các dịch vụ của phòng trọ và truyền đến view
            List<DichVu> dichVus = _unitOfWork.DichVu.GetAll().ToList();
            List<DichVuVM> dichVusVM = _mapper
                .Map<List<DichVu>, List<DichVuVM>>(dichVus);

            return Json(new { status = "ok", data = dichVusVM });
        }

        [HttpPost]
        public JsonResult CreatePhongTroBooking(int phongTroId)
        {
            string nguoiDungId = _unitOfWork.NguoiDung.GetUserId();
            _unitOfWork.PhongTroBooking.Create(new PhongTroBooking
            {
                NguoiDungId = nguoiDungId,
                PhongTroId = phongTroId,
                NgayDatTro = DateTime.Now
            });
            if (_unitOfWork.Save())
            {
                HttpContext.Session.SetInt32("PhongTroId", phongTroId);
                return Json(new { status = "ok" });
            }

            return Json(new { status = "err" });
        }

        [HttpGet]
        public IActionResult ThanhToanMoMo()
        {
            // get info of phong tro
            int id = HttpContext.Session.GetInt32("PhongTroId").Value;
            PhongTro phongTro = _unitOfWork.PhongTro.GetById(id);

            // Request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/v2/gateway/api/create";
            string partnerCode = "MOMO5RGX20191128";
            string accessKey = "M8brj9K6E22vXoDB";
            string serectkey = "nqQiVSgDMy809JoPF6OzP5OdBUB550Y4";
            string orderInfo = "Cọc trước 30% tiền trọ: " + phongTro.Ten;
            string redirectUrl = "https://localhost:44338/Home/PaymentResult";
            string ipnUrl = "https://localhost:44338/Home/NotifyUrl";
            string requestType = "captureWallet";


            double giaCoc = Math.Round(phongTro.Gia * 0.3, 0);
            string amount = giaCoc.ToString();
            string orderId = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "accessKey=" + accessKey +
                "&amount=" + amount +
                "&extraData=" + extraData +
                "&ipnUrl=" + ipnUrl +
                "&orderId=" + orderId +
                "&orderInfo=" + orderInfo +
                "&partnerCode=" + partnerCode +
                "&redirectUrl=" + redirectUrl +
                "&requestId=" + requestId +
                "&requestType=" + requestType
                ;

            log.Debug("rawHash = " + rawHash);

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);
            log.Debug("Signature = " + signature);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "partnerName", "Test" },
                { "storeId", "MomoTestStore" },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderId },
                { "orderInfo", orderInfo },
                { "redirectUrl", redirectUrl },
                { "ipnUrl", ipnUrl },
                { "lang", "en" },
                { "extraData", extraData },
                { "requestType", requestType },
                { "signature", signature }

            };
            log.Debug("Json request to MoMo: " + message.ToString());
            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);
            log.Debug("Return from MoMo: " + jmessage.ToString());

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        public ActionResult PaymentResult()
        {
            string param = Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);

            //Thành công

            int phongTroId = HttpContext.Session.GetInt32("PhongTroId").Value;

            if (HttpContext.Request.Query["message"].Equals("Successful."))
            {
                ViewBag.Message = "Đặt trọ thành công! Chủ trọ sẽ sớm liên hệ với bạn";
            }
            else
            {
                ViewBag.Message = "Thanh toán thất bại!";
                // Huỷ phiên booking người dùng vừa đặt
                PhongTroBooking phongTroBooking = _unitOfWork.PhongTroBooking
                    .GetPhongTroBookingByPhongTroAndNguoiDungId(phongTroId,
                    _unitOfWork.NguoiDung.GetUserId());
                _unitOfWork.PhongTroBooking.Delete(phongTroBooking);
                _unitOfWork.Save();
            }
            return View();
        }

        [HttpGet]
        public JsonResult NotifyUrl()
        {
            return Json(new { code = 200 });
        }

        public IActionResult EditUserProfile(string id)
        {
            var user = _unitOfWork.NguoiDung.GetUserById(id);
            var userModel = _mapper.Map<NguoiDung, NguoiDungVM>(user);

            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(NguoiDungVM newUser)
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

        public IActionResult DeleteUserProfile(string id)
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

        [HttpGet]
        public JsonResult RegisterChutrorRole()
        {
            if (User.IsInRole("Chutro") || User.IsInRole("Administrator"))
            {
                return Json(new { status = "no" });
            } else
            {
                string nguoiDungId = _unitOfWork.NguoiDung.GetUserId();
                NguoiDung nguoiDung = _unitOfWork.NguoiDung.GetUserById(nguoiDungId);
                NguoiDung newNguoiDung = nguoiDung;
                newNguoiDung.ChuTroRegisterStatus = true;

                _unitOfWork.NguoiDung.Update(newNguoiDung);


                if (_unitOfWork.Save())
                {
                    return Json(new { status = "ok" });
                }
                else
                {
                    return Json(new { status = "err" });
                }
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
