using DISANVANHOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Controllers
{
    public class QuizController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var items = db.Questions
                          .OrderBy(x => Guid.NewGuid())
                          .ToList(); // FIX
            return View(items);
        }

        public ActionResult Game()
        {
            var items = db.Questions
                          .OrderBy(x => Guid.NewGuid())
                          .ToList(); // FIX
            return PartialView("Game", items);
        }

        [HttpPost]
        public JsonResult SubmitAnswer(int questionId, string selected)
        {
            var question = db.Questions.Find(questionId);
            bool isCorrect = question != null && selected == question.CorrectAnswer;
            return Json(new { correct = isCorrect, correctAnswer = question?.CorrectAnswer });
        }
    }
}