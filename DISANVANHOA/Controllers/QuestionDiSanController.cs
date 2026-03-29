using DISANVANHOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Controllers
{

    public class QuestionDiSanController : Controller
    {
        private ApplicationDbContext db= new ApplicationDbContext();
        // GET: QuestionDiSan
        public ActionResult Index()
        {
            

            return View();
        }
        public ActionResult GameImg()
        {
            var items = db.QuestionDiSans
                          .OrderBy(x => Guid.NewGuid())
                          .ToList(); // FIX
            return PartialView("GameImg", items);
        }
       
        public ActionResult Gameflip()
        {
            var items = db.QuestionDiSans
                          .OrderBy(x => Guid.NewGuid())
                          .ToList(); // FIX
            return PartialView("Gameflip", items);
        }
        [HttpPost]
        public JsonResult SubmitAnswer(int questionId, string selected)
        {
            var question = db.QuestionDiSans.Find(questionId);
            bool isCorrect = question != null && selected == question.result;
            return Json(new { correct = isCorrect, correctAnswer = question?.result });
        }
    }

    }
