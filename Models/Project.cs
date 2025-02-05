using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string LiveDemoUrl { get; set; }

        public string GitHubUrl { get; set; }

        [Required]
        public string Technologies { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Category { get; set; }

        public bool IsFeatured { get; set; }
    }
}
