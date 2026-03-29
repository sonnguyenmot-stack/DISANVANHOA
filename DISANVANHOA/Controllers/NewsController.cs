using DISANVANHOA.Filters;
using DISANVANHOA.Models;
using DISANVANHOA.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Controllers
{
    
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: News
        [RequireLoginPopup]
        public ActionResult Index(int? page)//trang chủ tin tức
        {
           
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<News> items = db.News.OrderByDescending(x => x.CreatedDate);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }
        public ActionResult Detail(int id)//chi tiết tin tức
        {
            var item = db.News.Find(id);
            return View(item);
        }
        public ActionResult Partial_News_Home()//  Hiển thị new ngoài home
        {
            var items = db.News.Where(x => x.IsActive && x.IsHome).Take(3).ToList();
            return PartialView(items);
        }
    }
}