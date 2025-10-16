
using System.Data.Entity;
using PurrfectBlog.Models;

namespace PurrfectBlog.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Web.config’teki connection string ismi ile eşleşmeli
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        // Veritabanına eklenecek tabloyu temsil eder
        public DbSet<BlogPost> BlogPosts { get; set; }

    }
}
