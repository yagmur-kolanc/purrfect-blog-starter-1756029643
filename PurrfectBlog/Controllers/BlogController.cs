using System;
using System.Linq;
using System.Web.Mvc;
using PurrfectBlog.Data;
using PurrfectBlog.Models;

namespace PurrfectBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: /Posts  (RouteConfig'te PostsList rotasına bağlı)
        public ActionResult Posts()
        {
            var posts = _db.BlogPosts
                           .OrderByDescending(p => p.CreatedAt)
                           .ToList();
            return View(posts);
        }

        // GET: /Posts/{id}  (RouteConfig'te PostDetails rotasına bağlı)
        public ActionResult Post(int id)
        {
            var post = _db.BlogPosts.Find(id);
            if (post == null) return HttpNotFound();

            return View("PostDetails", post); // Views/Blog/PostDetails.cshtml
        }

        // GET: /Blog/CreatePost
        [HttpGet]
        public ActionResult CreatePost()
        {
            return View();
        }

        // POST: /Blog/CreatePost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(BlogPost post)
        {
            if (!ModelState.IsValid)
                return View(post);

            post.CreatedAt = DateTime.Now;
            _db.BlogPosts.Add(post);
            _db.SaveChanges();

            TempData["Message"] = "Post saved successfully.";
            return RedirectToAction("Posts");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
