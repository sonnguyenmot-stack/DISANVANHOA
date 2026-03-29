using DISANVANHOA.Models;
using DISANVANHOA.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Areas.Admin.Controllers
{
    public class DocumentCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/DocumentCategory
        public ActionResult Index()
        {
            var items = db.DocumentCategories;
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(DocumentCategory model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = DISANVANHOA.Models.Common.Filter.FilterChar(model.Title);
                db.DocumentCategories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = db.DocumentCategories.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DocumentCategory model)
        {
            if (ModelState.IsValid)
            {
                var item = db.DocumentCategories.Find(model.Id);

                item.Title = model.Title;
                item.Description = model.Description;
                item.Alias = DISANVANHOA.Models.Common.Filter.FilterChar(model.Title);
                item.ModifiedDate = DateTime.Now;
                item.Modifiedby = model.Modifiedby;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var item = db.DocumentCategories.Find(id);
            if (item != null)
            {
                db.DocumentCategories.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}