using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Data;

namespace FinalProjectMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class BlogPostController : Controller
    {
        private readonly AppDbContext _context;

        public BlogPostController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var blogPosts = _context.BlogPosts.ToList();
            return View(blogPosts);
        }

        // Create (GET)
        public IActionResult Create()
        {
            return View();
        }

        // Create (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                blogPost.CreatedDate = DateTime.Now;
                _context.BlogPosts.Add(blogPost);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        // Edit (GET)
        public IActionResult Edit(int id)
        {
            var blogPost = _context.BlogPosts.Find(id);
            if (blogPost == null) return NotFound();
            return View(blogPost);
        }

        // Edit (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                var existingPost = _context.BlogPosts.Find(blogPost.Id);
                if (existingPost == null) return NotFound();

                existingPost.Title = blogPost.Title;
                existingPost.Content = blogPost.Content;
                existingPost.Author = blogPost.Author;
                existingPost.ImageUrl = blogPost.ImageUrl;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        // Delete (GET)
        public IActionResult Delete(int id)
        {
            var blogPost = _context.BlogPosts.Find(id);
            if (blogPost == null) return NotFound();
            return View(blogPost);
        }

        // Delete (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var blogPost = _context.BlogPosts.Find(id);
            if (blogPost == null) return NotFound();

            _context.BlogPosts.Remove(blogPost);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
