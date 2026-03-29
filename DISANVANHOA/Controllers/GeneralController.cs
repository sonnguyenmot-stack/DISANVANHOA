using DISANVANHOA.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Controllers
{
    
    public class GeneralController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();       
        // GET: General
        public ActionResult Index(string alias, int id)
        {
            var item = db.Generals.Find(id);
            if (item != null)
            {
                db.Generals.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                db.Entry(item).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }
            var countReview = db.Reviews.Where(x => x.ProductId == id).Count();
            ViewBag.CountReview = countReview;
            return View(item);
            
        }
    }
}