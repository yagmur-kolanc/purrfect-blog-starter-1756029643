using System;
using System.ComponentModel.DataAnnotations;

namespace PurrfectBlog.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(120, ErrorMessage = "Title cannot exceed 120 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        public string Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
