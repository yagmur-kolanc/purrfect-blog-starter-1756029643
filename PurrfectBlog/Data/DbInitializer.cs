using PurrfectBlog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Microsoft.AspNet.Identity; // PasswordHasher için

namespace PurrfectBlog.Data
{
    // EF6: IDatabaseInitializer<ApplicationDbContext> uygular
    public class DbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext db)
        {
            db.BlogPosts.Add(new BlogPost
            {
                Title = "The Rise of Artificial Intelligence",
                Content = "AI is transforming industries from healthcare to finance.",
                Category = "AI & Innovation"
            });
            db.BlogPosts.Add(new BlogPost
            {
                Title = "How Quantum Computing Works",
                Content = "Quantum computing uses qubits and enables new possibilities.",
                Category = "Quantum Technology"
            });
            db.BlogPosts.Add(new BlogPost
            {
                Title = "Top 5 Programming Languages to Learn in 2025",
                Content = "Python, Rust, Go, JavaScript and Kotlin stand out.",
                Category = "Programming"
            });

            db.SaveChanges();
            base.Seed(db);
        }
    }
}