using DISANVANHOA.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Refresh()
        {
            ThongKeModel model = new ThongKeModel();

            ViewBag.Visitors_online = HttpContext.Application["visitors_online"];

            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ThongKe", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    model.HomNay = dr["HomNay"].ToString();
                    model.HomQua = dr["HomQua"].ToString();
                    model.TuanNay = dr["TuanNay"].ToString();
                    model.TuanTruoc = dr["TuanTruoc"].ToString();
                    model.ThangNay = dr["ThangNay"].ToString();
                    model.ThangTruoc = dr["ThangTruoc"].ToString();
                    model.TatCa = dr["TatCa"].ToString();
                }
            }

            return PartialView(model);
        }

    }
}