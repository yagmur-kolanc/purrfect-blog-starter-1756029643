using System.Data.Entity;
using PurrfectBlog.Data;
using System.Web.Mvc;
using System.Web.Routing;

namespace PurrfectBlog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Veritabanı başlatıcısı (EF6: generic çağrı şart)
            Database.SetInitializer<ApplicationDbContext>(new DbInitializer());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Filter/Bundle dosyaların yoksa bu ikisini kullanma
            // FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            // BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
