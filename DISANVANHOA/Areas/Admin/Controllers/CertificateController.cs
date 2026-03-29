using DISANVANHOA.Models;
using DISANVANHOA.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Areas.Admin.Controllers
{
    public class CertificateController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Certificate
        public ActionResult Index()
        {
            var items = db.Certificates;
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Certificate model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = DISANVANHOA.Models.Common.Filter.FilterChar(model.Title);
                db.Certificates.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = db.Certificates.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Certificate model)
        {
            if (ModelState.IsValid)
            {
                var item = db.Certificates.Find(model.Id);

                item.Title = model.Title;
                item.Position = model.Position;
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
            var item = db.Certificates.Find(id);
            if (item != null)
            {
                db.Certificates.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}