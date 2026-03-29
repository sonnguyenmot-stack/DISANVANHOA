
using DISANVANHOA.Models;
using DISANVANHOA.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace DISANVANHOA.Areas.Admin.Controllers
{
    public class DocumentController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index(string Searchtext, int? page)
        {
            int pageSize = 10;
            int pageIndex = page ?? 1;

            var items = db.Documents
                          .Include(x => x.DocumentCategory) // QUAN TRỌNG
                          .OrderByDescending(x => x.Id)
                          .AsQueryable();

            if (!string.IsNullOrEmpty(Searchtext))
            {
                string searchLower = Searchtext.ToLower();

                items = items.Where(x =>
                    (x.Alias != null && x.Alias.ToLower().Contains(searchLower)) ||
                    (x.Title != null && x.Title.ToLower().Contains(searchLower)) ||
                    (x.DocumentCategory != null &&
                     x.DocumentCategory.Title != null &&
                     x.DocumentCategory.Title.ToLower().Contains(searchLower))
                );
            }

            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageIndex;

            return View(items.ToPagedList(pageIndex, pageSize));
        }


        public ActionResult Add()
        {
            LoadDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Document model, List<string> Images, List<int> rDefault, HttpPostedFileBase file, HttpPostedFileBase videoFile)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return View(model);
            }

            // Upload file PDF/DOC
            if (file != null && file.ContentLength > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                file.SaveAs(path);
                model.FilePath = "/Uploads/" + fileName;
            }

            // Upload Video
            if (videoFile != null && videoFile.ContentLength > 0)
            {
                string videoName = Path.GetFileName(videoFile.FileName);
                string videoPath = Path.Combine(Server.MapPath("~/Uploads/Videos/"), videoName);

                // Tạo thư mục nếu chưa có
                if (!Directory.Exists(Server.MapPath("~/Uploads/Videos/")))
                    Directory.CreateDirectory(Server.MapPath("~/Uploads/Videos/"));

                videoFile.SaveAs(videoPath);
                model.VideoPath = "/Uploads/Videos/" + videoName;
            }

            // Upload nhiều ảnh
            if (Images != null && Images.Count > 0)
            {
                model.DocumentImages = new List<DocumentImage>();
                for (int i = 0; i < Images.Count; i++)
                {
                    bool isDefault = (rDefault != null && rDefault.Count > 0 && i + 1 == rDefault[0]);
                    if (isDefault) model.Img = Images[i];

                    model.DocumentImages.Add(new DocumentImage
                    {
                        DocumentId = model.Id,
                        Image = Images[i],
                        IsDefault = isDefault
                    });
                }
            }

            model.CreatedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;

            if (string.IsNullOrEmpty(model.Alias))
                model.Alias = DISANVANHOA.Models.Common.Filter.FilterChar(model.Title);

            db.Documents.Add(model);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var item = db.Documents.Find(id);
            if (item == null) return HttpNotFound();
            LoadDropdowns();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Document model, HttpPostedFileBase file, HttpPostedFileBase videoFile)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return View(model);
            }

            var item = db.Documents.Find(model.Id);
            if (item == null) return HttpNotFound();

            if (file != null && file.ContentLength > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                file.SaveAs(path);
                item.FilePath = "/Uploads/" + fileName;
            }

            if (videoFile != null && videoFile.ContentLength > 0)
            {
                string videoName = Path.GetFileName(videoFile.FileName);
                string videoPath = Path.Combine(Server.MapPath("~/Uploads/Videos/"), videoName);

                if (!Directory.Exists(Server.MapPath("~/Uploads/Videos/")))
                    Directory.CreateDirectory(Server.MapPath("~/Uploads/Videos/"));

                videoFile.SaveAs(videoPath);
                item.VideoPath = "/Uploads/Videos/" + videoName;
            }

            item.Title = model.Title;
            item.Description = model.Description;
            item.Alias = DISANVANHOA.Models.Common.Filter.FilterChar(model.Title);
            item.DocumentCategoryId = model.DocumentCategoryId;
            item.DocTypeId = model.DocTypeId;
            item.CertificateId = model.CertificateId;
            item.Img = model.Img;
            item.ModifiedDate = DateTime.Now;
            item.Author= model.Author;
            item.Detail = model.Detail;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var item = db.Documents.Find(id);
            if (item != null)
            {
                db.Documents.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                foreach (var idStr in ids.Split(','))
                {
                    int id = Convert.ToInt32(idStr);
                    var doc = db.Documents.Find(id);
                    if (doc != null) db.Documents.Remove(doc);
                }
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        
        // ============================================
        // 4) Load Dropdowns cho view
        // ============================================
        private void LoadDropdowns()
        {
            ViewBag.DoccumentCategory = new SelectList(db.DocumentCategories, "Id", "Title");
            ViewBag.Certificate = new SelectList(db.Certificates, "Id", "Title");
            ViewBag.DocType = new SelectList(db.DocTypes, "Id", "Title");
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.Documents.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isAcive = item.IsActive });
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = db.Documents.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsHome = item.IsHome });
            }

            return Json(new { success = false });
        }
    }
}
