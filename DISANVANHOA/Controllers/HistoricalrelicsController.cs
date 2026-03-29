using DISANVANHOA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Controllers
{
   
    public class HistoricalrelicsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Historicalrelics
        public ActionResult Index(string alias, int id)
        {
            var item = db.Historicalrelics.Find(id);
            if (item != null)
            {
                db.Historicalrelics.Attach(item);
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