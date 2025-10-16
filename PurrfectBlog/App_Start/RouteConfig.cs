using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PurrfectBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // OPTIONAL: If you also want to use [Route(...)] on actions:
            // routes.MapMvcAttributeRoutes();

            // ---- Custom routes for Task 3 ----
            // /Posts  → BlogController.Posts()
            routes.MapRoute(
                name: "PostsList",
                url: "Posts",
                defaults: new { controller = "Blog", action = "Posts" }
            );

            // /Posts/{id}  → BlogController.Post(int id)
            routes.MapRoute(
                name: "PostDetails",
                url: "Posts/{id}",
                defaults: new { controller = "Blog", action = "Post" },
                constraints: new { id = @"\d+" } // only digits
            );

            // ---- Default fallback ----
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}