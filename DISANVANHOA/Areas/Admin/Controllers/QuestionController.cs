using DISANVANHOA.Models;
using DISANVANHOA.Models.EF;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Areas.Admin.Controllers
{
    public class QuestionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Question
        public ActionResult Index()
        {
            var items = db.Questions.ToList();
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Question model)
        {
            if (ModelState.IsValid)
            {

                db.Questions.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = db.Questions.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Question model)
        {
            if (ModelState.IsValid)
            {
                var item = db.Questions.Find(model.Id);
                if (item == null) return HttpNotFound();

                // Cập nhật từng trường
                item.Text = model.Text;
                item.OptionA = model.OptionA;
                item.OptionB = model.OptionB;
                item.OptionC = model.OptionC;
                item.OptionD = model.OptionD;
                item.CorrectAnswer = model.CorrectAnswer;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var item = db.Questions.Find(id);
            if (item != null)
            {
                db.Questions.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
