uusing System.Linq;
using System.Web.Mvc;
using PurrfectBlog.Models;
using System.Security.Cryptography;
using System.Text;

namespace PurrfectBlog.Controllers
{
    public class AuthorController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Author/Dashboard
        public ActionResult Dashboard()
        {
            if (Session["AuthorId"] == null)
                return RedirectToAction("Login"); // Oturum yoksa Login sayfasına yönlendir

            return View(); // Oturum varsa Dashboard sayfasını göster
        }

        // POST: Author/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Username and password are required.";
                return View();
            }

            if (_context.Authors.Any(a => a.Username == username))
            {
                ViewBag.Error = "Username already exists.";
                return View();
            }

            var author = new Author
            {
                Username = username,
                PasswordHash = ComputeHash(password)
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            // Oturum aç
            Session["AuthorId"] = author.Id;
            return RedirectToAction("Dashboard");
        }

        // GET: Author/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Author/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Username == username);
            if (author == null || author.PasswordHash != ComputeHash(password))
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            Session["AuthorId"] = author.Id;
            return RedirectToAction("Dashboard");
        }

        // GET: Author/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // GET: Author/Dashboard
        public ActionResult Dashboard()
        {
            if (Session["AuthorId"] == null)
                return RedirectToAction("Login");

            return View();
        }

        // Basit SHA256 hash fonksiyonu
        private string ComputeHash(string input)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                return string.Join("", bytes.Select(b => b.ToString("X2")));
            }
        }
    }
}
