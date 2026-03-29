using DISANVANHOA.Filters;
using DISANVANHOA.Models;
using DISANVANHOA.Models.EF;
using Microsoft.AspNet.SignalR;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Controllers
{
   
    public class CommunityController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [RequireLoginPopup]

        // Hiển thị CHỈ các bài đã được duyệt
        public ActionResult Index(int? page)
        {
           
            int pageSize = 10; // 10 bài / trang
            int pageNumber = (page ?? 1);

            // include MediaFiles để tránh lazy load
            var posts = db.Posts
                          .Where(p => p.IsApproved == true)
                          .OrderByDescending(p => p.CreatedAt)
                          .Include(p => p.Comments)
                          .Include(p => p.MediaFiles)
                          .ToPagedList(pageNumber, pageSize);

            return View(posts);
        }

        // Helper: lưu file và trả về path và type
        private (string filePath, string fileType) SaveUploadedFile(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength <= 0) return (null, null);

            var uploadsFolder = Server.MapPath("~/Uploads/");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            string fileName = Path.GetFileName(file.FileName);
            string unique = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString("n").Substring(0, 6);
            string safeName = unique + "_" + fileName;
            string virtualPath = "/Uploads/" + safeName;
            string physicalPath = Server.MapPath(virtualPath);
            file.SaveAs(physicalPath);

            var ext = Path.GetExtension(fileName).ToLower();
            string ft = "file";
            if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif" || ext == ".bmp" || ext == ".webp")
                ft = "image";
            else if (ext == ".mp4" || ext == ".webm" || ext == ".ogg" || ext == ".mov")
                ft = "video";
            else
                ft = "file";

            return (virtualPath, ft);
        }

        // Tạo bài: Student (hoặc anonymous) thì chờ duyệt
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(string author, string password, string content, IEnumerable<HttpPostedFileBase> media)
        {
            if (string.IsNullOrEmpty(author) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(content))
                return Json(new { success = false, message = "Vui lòng nhập đủ thông tin!" });

            bool isApproved = User.Identity.IsAuthenticated && User.IsInRole("Teacher");

            var post = new Post
            {
                Author = author,
                Password = password,
                Content = content,
                CreatedAt = DateTime.Now,
                IsApproved = isApproved
            };

            db.Posts.Add(post);
            db.SaveChanges();

            if (media != null)
            {
                foreach (var file in media)
                {
                    if (file == null || file.ContentLength <= 0) continue;
                    var (virtualPath, fileType) = SaveUploadedFile(file);
                    if (virtualPath == null) continue;

                    var pm = new PostMedia
                    {
                        PostId = post.Id,
                        FilePath = virtualPath,
                        FileType = fileType
                    };
                    db.PostMedias.Add(pm);

                    if (string.IsNullOrEmpty(post.MediaPath) && (fileType == "image" || fileType == "video"))
                        post.MediaPath = virtualPath;
                }
                db.SaveChanges();
            }

            // SignalR notify
            var context = GlobalHost.ConnectionManager.GetHubContext<Hubs.CommunityHub>();
            context.Clients.All.updatePosts();

            return Json(new { success = true, message = isApproved ? "Đăng bài thành công." : "Bài viết đã gửi. Chờ giáo viên duyệt." });
        }
        //bình luận
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(int postId, string author, string content)
        {
            if (string.IsNullOrEmpty(author) || string.IsNullOrEmpty(content))
                return RedirectToAction("Index");

            var comment = new Comment
            {
                Author = author,
                Content = content,
                PostId = postId,
                CreatedAt = DateTime.Now
            };
            db.Comments.Add(comment);
            db.SaveChanges();

            var context = GlobalHost.ConnectionManager.GetHubContext<Hubs.CommunityHub>();
            context.Clients.All.updateComments(postId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult LikePost(int postId)
        {
            var post = db.Posts.Find(postId);
            if (post != null)
            {
                post.Likes++;
                db.SaveChanges();
            }

            var context = GlobalHost.ConnectionManager.GetHubContext<Hubs.CommunityHub>();
            context.Clients.All.updatePosts();

            return Json(new { likes = post?.Likes ?? 0 });
        }
        // sửa bài đăng
        [HttpGet]
        public ActionResult EditPost(int id, string password)
        {
            var post = db.Posts.Include(p => p.MediaFiles).FirstOrDefault(p => p.Id == id);

            if (post == null || post.Password != password)
            {
                TempData["Error"] = "Mật khẩu sai hoặc bài viết không tồn tại!";
                return RedirectToAction("Index");
            }

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int id, string password, string content,
                                     IEnumerable<int> deleteOldMedia,
                                     IEnumerable<HttpPostedFileBase> media)
        {
            var post = db.Posts.Include(p => p.MediaFiles).FirstOrDefault(p => p.Id == id);

            if (post == null || post.Password != password)
            {
                TempData["Error"] = "Mật khẩu sai hoặc bài viết không tồn tại!";
                return RedirectToAction("Index");
            }

            // cập nhật nội dung
            post.Content = content;

            // XÓA MEDIA CŨ
            if (deleteOldMedia != null)
            {
                foreach (var mediaId in deleteOldMedia)
                {
                    var m = db.PostMedias.Find(mediaId);
                    if (m != null)
                    {
                        db.PostMedias.Remove(m);
                    }
                }
            }

            // THÊM MEDIA MỚI
            if (media != null)
            {
                foreach (var file in media)
                {
                    if (file == null || file.ContentLength == 0) continue;

                    var (path, type) = SaveUploadedFile(file);
                    if (path == null) continue;

                    db.PostMedias.Add(new PostMedia
                    {
                        PostId = post.Id,
                        FilePath = path,
                        FileType = type
                    });

                    // nếu bài chưa có thumbnail thì gán file đầu tiên
                    if (string.IsNullOrEmpty(post.MediaPath))
                        post.MediaPath = path;
                }
            }

            db.SaveChanges();

            // cập nhật real-time
            var context = GlobalHost.ConnectionManager.GetHubContext<Hubs.CommunityHub>();
            context.Clients.All.updatePosts();

            return RedirectToAction("Index");
        }



        // Xóa bài: yêu cầu mật khẩu. 
        [HttpPost]
        public ActionResult DeletePost(int id, string password)
        {
            var post = db.Posts.Include(p => p.MediaFiles).FirstOrDefault(p => p.Id == id);
            if (post != null && post.Password == password)
            {
                // Xóa file vật lý của media
                if (post.MediaFiles != null)
                {
                    foreach (var pm in post.MediaFiles.ToList())
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(pm.FilePath))
                            {
                                var phys = Server.MapPath(pm.FilePath);
                                if (System.IO.File.Exists(phys)) System.IO.File.Delete(phys);
                            }
                        }
                        catch { /* ignore file delete errors */ }

                        db.PostMedias.Remove(pm);
                    }
                }

                // xóa file MediaPath cũ nếu tồn tại
                if (!string.IsNullOrEmpty(post.MediaPath))
                {
                    try
                    {
                        var phys = Server.MapPath(post.MediaPath);
                        if (System.IO.File.Exists(phys)) System.IO.File.Delete(phys);
                    }
                    catch { }
                }

                db.Posts.Remove(post);
                db.SaveChanges();

                var context = GlobalHost.ConnectionManager.GetHubContext<Hubs.CommunityHub>();
                context.Clients.All.updatePosts();

                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        // ====== PHẦN DÀNH CHO GIÁO VIÊN ======
        [System.Web.Mvc.Authorize(Roles = "GiaoVien")]
        public ActionResult Pending()
        {
            // Lấy tất cả bài chưa duyệt và include MediaFiles
            var posts = db.Posts
                          .Where(p => p.IsApproved == false)
                          .OrderByDescending(p => p.CreatedAt)
                          .Include(p => p.MediaFiles)
                          .ToList();
            return View(posts);
        }

        // Duyệt 1 bài
        [HttpPost]
        [System.Web.Mvc.Authorize(Roles = "GiaoVien")]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(int id)
        {
            var post = db.Posts.Find(id);
            if (post == null) return HttpNotFound();

            post.IsApproved = true;
            db.SaveChanges();

            var context = GlobalHost.ConnectionManager.GetHubContext<Hubs.CommunityHub>();
            context.Clients.All.updatePosts();

            TempData["Success"] = "Đã duyệt bài.";
            return RedirectToAction("Pending");
        }

        // Từ chối/xóa bài
        [HttpPost]
        [System.Web.Mvc.Authorize(Roles = "GiaoVien")]
        [ValidateAntiForgeryToken]
        public ActionResult Reject(int id)
        {
            var post = db.Posts.Include(p => p.MediaFiles).FirstOrDefault(p => p.Id == id);
            if (post == null) return HttpNotFound();

            if (post.MediaFiles != null)
            {
                foreach (var pm in post.MediaFiles.ToList())
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(pm.FilePath))
                        {
                            var phys = Server.MapPath(pm.FilePath);
                            if (System.IO.File.Exists(phys)) System.IO.File.Delete(phys);
                        }
                    }
                    catch { }
                    db.PostMedias.Remove(pm);
                }
            }

            db.Posts.Remove(post);
            db.SaveChanges();

            var context = GlobalHost.ConnectionManager.GetHubContext<Hubs.CommunityHub>();
            context.Clients.All.updatePosts();

            TempData["Success"] = "Đã loại bỏ bài viết.";
            return RedirectToAction("Pending");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
