namespace Portfolio.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Project> FeaturedProjects { get; set; } = new();
        public List<Skill> TopSkills { get; set; } = new();
        public List<Testimonial> Testimonials { get; set; } = new();
        public List<BlogPost> RecentBlogPosts { get; set; } = new();
    }
}
