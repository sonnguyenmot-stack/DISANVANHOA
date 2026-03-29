using DISANVANHOA.Models;
using DISANVANHOA.Models.EF;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Areas.Admin.Controllers
{
    public class GeneralController : Controller
    {
        private ApplicationDbContext db=new ApplicationDbContext();
        // GET: Admin/General
        public ActionResult Index()
        {
            var items = db.Generals;
            return View(items);
        }
        public ActionResult Add()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(General model, HttpPostedFileBase file)
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

                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = DISANVANHOA.Models.Common.Filter.FilterChar(model.Title);
                db.Generals.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           
            return View();
        }
        public ActionResult Edit(int id)
        {
            var item = db.Generals.Find(id);
            
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(General model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var item = db.Generals.Find(model.Id);
                // Xử lý tải lên tệp (nếu có tệp mới)
                if (item == null) return HttpNotFound();

                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    file.SaveAs(path);
                    item.FilePath = "/Uploads/" + fileName;
                }

                item.ModifiedDate = DateTime.Now;
                item.Title=model.Title;
                item.Alias=DISANVANHOA.Models.Common.Filter.FilterChar(model.Title);
                item.Description=model.Description;
                item.Detail=model.Detail;   
                item.Icon=model.Icon;
                
             
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Generals.Find(id);
            if (item != null)
            {
                db.Generals.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}