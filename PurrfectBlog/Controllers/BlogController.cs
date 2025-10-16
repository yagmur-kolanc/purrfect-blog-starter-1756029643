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

        // GET: /Posts  (mapped in RouteConfig as PostsList)
        public ActionResult Posts()
        {
            var posts = _db.BlogPosts
                           .OrderByDescending(p => p.CreatedAt)
                           .ToList();
            return View(posts);
        }

        // GET: /Posts/{id}  (mapped in RouteConfig as PostDetails)
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

        // GET: /Blog/EditPost/{id}
        [HttpGet]
        public ActionResult EditPost(int id)
        {
            var post = _db.BlogPosts.Find(id);
            if (post == null) return HttpNotFound();
            return View(post); // Views/Blog/EditPost.cshtml
        }

        // POST: /Blog/EditPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(BlogPost updatedPost)
        {
            // Server-side validation
            if (!ModelState.IsValid)
            {
                return View(updatedPost);
            }

            var post = _db.BlogPosts.Find(updatedPost.Id);
            if (post == null) return HttpNotFound();

            // Update only allowed fields (overposting-safe)
            post.Title = updatedPost.Title;
            post.Content = updatedPost.Content;
            post.Category = updatedPost.Category;

            _db.SaveChanges();

            TempData["ToastSuccess"] = "Post updated successfully.";
            return RedirectToAction("Post", new { id = post.Id });
        }

        // POST: /Blog/DeletePost/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var post = _db.BlogPosts.Find(id);
            if (post == null)
            {
                TempData["ToastInfo"] = "Post not found or already deleted.";
                return RedirectToAction("Posts");
            }

            _db.BlogPosts.Remove(post);
            _db.SaveChanges();

            TempData["ToastSuccess"] = "Post deleted successfully.";
            return RedirectToAction("Posts");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
