using DISANVANHOA.Models;
using DISANVANHOA.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Areas.Admin.Controllers
{
    public class QuestionDiSanController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/QuestionDiSan
        public ActionResult Index()
        {
            var items = db.QuestionDiSans.ToList();
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(QuestionDiSan model)
        {
            if (ModelState.IsValid)
            {

                db.QuestionDiSans.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = db.QuestionDiSans.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionDiSan model)
        {
            if (ModelState.IsValid)
            {
                var item = db.QuestionDiSans.Find(model.Id);
                if (item == null) return HttpNotFound();

                // Cập nhật từng trường
                item.Name = model.Name;
                item.Img1 = model.Img1;
                item.Img2 = model.Img2;
                item.Img3 = model.Img3;
                item.Img4 = model.Img4;
                item.result = model.result;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var item = db.QuestionDiSans.Find(id);
            if (item != null)
            {
                db.QuestionDiSans.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}