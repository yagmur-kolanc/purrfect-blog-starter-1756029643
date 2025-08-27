using PurrfectBlog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Microsoft.AspNet.Identity; // PasswordHasher için

namespace PurrfectBlog.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.CreateIfNotExists(); // Veritabanı yoksa oluştur

            if (!context.Authors.Any()) // Eğer Authors tablosu boşsa
            {
                var passwordHasher = new PasswordHasher();

                var authors = new List<Author>
                {
                    new Author { Username = "demo1", PasswordHash = passwordHasher.HashPassword("password1") },
                    new Author { Username = "demo2", PasswordHash = passwordHasher.HashPassword("password2") },
                    new Author { Username = "demo3", PasswordHash = passwordHasher.HashPassword("password3") }
                };

                context.Authors.AddRange(authors);
                context.SaveChanges();
            }
        }
    }
}
s