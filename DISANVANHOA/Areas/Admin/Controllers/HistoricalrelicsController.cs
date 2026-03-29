using DISANVANHOA.Models;
using DISANVANHOA.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Areas.Admin.Controllers
{
    public class HistoricalrelicsController : Controller
    {
        // GET: Admin/Historicalrelics
        private ApplicationDbContext db= new ApplicationDbContext();
        public ActionResult Index(string Searchtext, int? page)
        {
            int pageSize = 10;
            int pageIndex = page ?? 1;

            var items = db.Historicalrelics.OrderByDescending(x => x.Id).AsEnumerable();
            if (!string.IsNullOrEmpty(Searchtext))
            {
                string searchLower = Searchtext.ToLower();

                items = items.Where(x =>
                    (x.Alias != null && x.Alias.ToLower().Contains(searchLower)) ||
                    (x.Title != null && x.Title.ToLower().Contains(searchLower)) 
                    
                );
            }
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageIndex;

            return View(items.ToPagedList(pageIndex, pageSize));
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Historicalrelics model, HttpPostedFileBase file, HttpPostedFileBase videoFile)
        {
            if (ModelState.IsValid)
            {
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
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;

                model.Alias = DISANVANHOA.Models.Common.Filter.FilterChar(model.Title);
                db.Historicalrelics.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var item = db.Historicalrelics.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Historicalrelics model, HttpPostedFileBase file, HttpPostedFileBase videoFile)
        {
            if (ModelState.IsValid)
            {
                var item = db.Historicalrelics.Find(model.Id);
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
                item.Image = model.Image;
                item.Author = model.Author;


                item.ModifiedDate = DateTime.Now;
                item.Modifiedby = model.Modifiedby;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Historicalrelics.Find(id);
            if (item != null)
            {
                db.Historicalrelics.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.Historicalrelics.Find(id);
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
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                foreach (var idStr in ids.Split(','))
                {
                    int id = Convert.ToInt32(idStr);
                    var doc = db.Historicalrelics.Find(id);
                    if (doc != null) db.Historicalrelics.Remove(doc);
                }
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

    }
}