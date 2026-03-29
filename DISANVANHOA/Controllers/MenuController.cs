using DISANVANHOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Controllers
{
    
    public class MenuController : Controller
    {
        // GET: Menu
        private ApplicationDbContext db= new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var items = db.Categories.OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop", items);
        }
        public ActionResult MenuTop2()
        {
            var items = db.Categories.OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop2", items);
        }
        public ActionResult Heritage()// loại hình văn hóa
        {
            var items = db.DocTypes.ToList();
            return PartialView("_Heritage", items);
        }
        public ActionResult MenuIcon()//Tài liệu chung
        {
            var items = db.Generals.ToList();
            return PartialView("_MenuIcon", items);
        }
       
        public ActionResult Partial_Historicalrelics()//di tích ;lịch sử
        {
            var items = db.Historicalrelics.Where(x => x.IsActive).Take(12).ToList();
            return PartialView("_Partial_Historicalrelics", items);
        }
        public ActionResult MenuLeft(int? id)
        {
            if (id != null)
            {
                ViewBag.CateId = id;
            }
            var items = db.DocTypes.ToList();
            return PartialView("_MenuLeft", items);
        }

    }
}