using FinalProjectMVC.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;
using Persistence.Data;

namespace FinalProjectMVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context) 
        {
            _context = context; 
        }

        public IActionResult Index()
        {
            // Fetch blog posts from the database
            var blogPosts = _context.BlogPosts
                .OrderByDescending(p => p.CreatedDate) // Order by most recent
                .Select(p => new BlogPostVM
                {
                    Id = p.Id,
                    Title = p.Title,
                    Author = p.Author,
                    CreatedAt = p.CreatedDate,
                    ImageUrl = p.ImageUrl,
                    Content = p.Content,
                }).ToList();

            return View(blogPosts);
        }

        public IActionResult Details(int id)
        {
            var post = _context.BlogPosts
                .Where(p => p.Id == id)
                .Select(p => new BlogPostVM
                {
                    Id = p.Id,
                    Title = p.Title,
                    Author = p.Author,
                    CreatedAt = p.CreatedDate,
                    ImageUrl = p.ImageUrl,
                    Content = p.Content,
                })
                .FirstOrDefault();

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }


    }
}
