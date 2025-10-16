using System.Linq;
using System.Web.Mvc;
using PurrfectBlog.Data;

namespace PurrfectBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var latest = _db.BlogPosts
                            .OrderByDescending(p => p.CreatedAt)
                            .Take(3)
                            .ToList();
            return View(latest);
        }
    }
}