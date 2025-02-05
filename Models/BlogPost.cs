using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string? FeaturedImageUrl { get; set; }

        [Required]
        public string Slug { get; set; }

        public string? Excerpt { get; set; }

        public DateTime PublishedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public bool IsPublished { get; set; }

        public string? Category { get; set; }

        public string? Tags { get; set; }
    }
}
