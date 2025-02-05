using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Models.ViewModels;
using Portfolio.Data;
using System.Diagnostics;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                FeaturedProjects = _context.Projects.Where(p => p.IsFeatured).ToList(),
                TopSkills = _context.Skills.OrderByDescending(s => s.Proficiency).Take(6).ToList(),
                Testimonials = _context.Testimonials.Where(t => t.IsVisible).Take(3).ToList(),
                RecentBlogPosts = _context.BlogPosts.Where(b => b.IsPublished)
                    .OrderByDescending(b => b.PublishedDate)
                    .Take(3)
                    .ToList()
            };

            return View(viewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View(new ContactViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Här kan du lägga till logik för att hantera kontaktformuläret
                // Till exempel skicka e-post eller spara i databasen
                
                TempData["Message"] = "Tack för ditt meddelande! Vi återkommer så snart som möjligt.";
                return RedirectToAction(nameof(Contact));
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
