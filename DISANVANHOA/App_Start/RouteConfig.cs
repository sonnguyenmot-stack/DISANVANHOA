using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DISANVANHOA
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
           name: "AI",
           url: "tro-ly-ai",
           defaults: new { controller = "chat", action = "Index", alias = UrlParameter.Optional },
           namespaces: new[] { "DISANVANHOA.Controllers" }
       );

            routes.MapRoute(
          name: "detailditichichsu",
          url: "chi-tiet-lich-su/{alias}-d{id}",
          defaults: new { controller = "Historicalrelics", action = "Index", alias = UrlParameter.Optional },
          namespaces: new[] { "DISANVANHOA.Controllers" }
      );

            routes.MapRoute(
           name: "detailGeneral",
           url: "chi-tiet-giao-trinh/{alias}-k{id}",
           defaults: new { controller = "General", action = "Index", alias = UrlParameter.Optional },
           namespaces: new[] { "DISANVANHOA.Controllers" }
       );

            routes.MapRoute(
            name: "TinTuc",
            url: "tin-tuc",
            defaults: new { controller = "News", action = "Index", alias = UrlParameter.Optional },
            namespaces: new[] { "DISANVANHOA.Controllers" }
        );

            routes.MapRoute(
        name: "DetailNew",
        url: "{alias}-n{id}",
        defaults: new { controller = "News", action = "Detail", id = UrlParameter.Optional },
        namespaces: new[] { "DISANVANHOA.Controllers" }
    );
            routes.MapRoute(
             name: "Congdong",
             url: "cong-dong",
             defaults: new { controller = "Community", action = "Index", alias = UrlParameter.Optional },
             namespaces: new[] { "DISANVANHOA.Controllers" }
         );

            routes.MapRoute(
             name: "Contact",
             url: "ban-do",
             defaults: new { controller = "Contact", action = "Index", alias = UrlParameter.Optional },
             namespaces: new[] { "DISANVANHOA.Controllers" }
         );
            routes.MapRoute(
             name: "detailDocument",
             url: "chi-tiet/{alias}-p{id}",
             defaults: new { controller = "Document", action = "Detail", alias = UrlParameter.Optional },
             namespaces: new[] { "DISANVANHOA.Controllers" }
         );
            routes.MapRoute(
             name: "Culturaltype",
             url: "loai-hinh-di-san/{alias}-{id}",
             defaults: new { controller = "Document", action = "Culturaltype", id = UrlParameter.Optional },
             namespaces: new[] { "DISANVANHOA.Controllers" }
         );

            routes.MapRoute(
              name: "Thu vien di san",
              url: "thu-vien-di-san",
              defaults: new { controller = "Document", action = "Index", alias = UrlParameter.Optional },
              namespaces: new[] { "DISANVANHOA.Controllers" }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "DISANVANHOA.Controllers" }
            );
        }
    }
}
