using Domain.Models;
using FinalProjectMVC.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;
using Persistence.Data;

using Services.Interfaces;

namespace FinalProjectMVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ITestimonialService _testimonialService;

        public BlogController(AppDbContext context, ITestimonialService testimonialService) 
        {
            _context = context; 
            _testimonialService = testimonialService;
        }

        public async Task<IActionResult> Index()
        {
           
            var testimonials = await _testimonialService.GetTestimonalAsync();

            
            var blogPosts = _context.BlogPosts
                .OrderByDescending(p => p.CreatedDate)
                .Select(p => new BlogPostVM
                {
                    Id = p.Id,
                    Title = p.Title,
                    Author = p.Author,
                    CreatedAt = p.CreatedDate,
                    ImageUrl = p.ImageUrl,
                    Content = p.Content,
                }).ToList();

         
            var model = new BlogIndexModel
            {
                BlogPosts = blogPosts,
                Testimonials = testimonials ?? Enumerable.Empty<Testimonial>()
            };

            return View(model); 
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
