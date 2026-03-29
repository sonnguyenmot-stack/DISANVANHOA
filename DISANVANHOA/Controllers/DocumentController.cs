using DISANVANHOA.Filters;
using DISANVANHOA.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Controllers
{
    
    public class DocumentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Document
        [RequireLoginPopup]

        public ActionResult Index(string Searchtext, int? page)
        {
            int pageSize = 12;
            int pageIndex = page ?? 1;

            var query = db.Documents
                          .Include("DocumentCategory")
                          .OrderByDescending(x => x.Id)
                          .AsQueryable();

            if (!string.IsNullOrWhiteSpace(Searchtext))
            {
                query = query.Where(x =>
                    (x.Alias != null && x.Alias.Contains(Searchtext)) ||
                    (x.Title != null && x.Title.Contains(Searchtext)) ||
                    (x.DocumentCategory != null &&
                     x.DocumentCategory.Title != null &&
                     x.DocumentCategory.Title.Contains(Searchtext))
                );
            }

            ViewBag.Searchtext = Searchtext;
            return View(query.ToPagedList(pageIndex, pageSize));
        }
        public ActionResult Partial_ItemsByCateId()// tài liệu theo hình thức
        {
            var items = db.Documents.Where(x => x.IsHome && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }
        public ActionResult Culturaltype(string alias, int id, int? page)
        {
            int pageSize = 12;
            int pageIndex = page ?? 1;

            var query = db.Documents
                          .Where(x => x.DocTypeId == id)
                          .OrderByDescending(x => x.Id)
                          .AsQueryable();
            var cate = db.DocTypes.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }
            ViewBag.CateId = id;
            return View(query.ToPagedList(pageIndex, pageSize));
        }
        // chi tiết tài liệu di sản 
        public ActionResult Detail(string alias, int? id)
        {
            var item = db.Documents.Find(id);
            if (item != null)
            {
                db.Documents.Attach(item);
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