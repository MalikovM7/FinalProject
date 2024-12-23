using Domain.Models;
using FinalProjectMVC.ViewModels.Admin.Testimonials;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.IO;

namespace FinalProjectMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class TestimonialsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TestimonialsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var testimonials = await _context.Testimomals.ToListAsync();
            return View(testimonials);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            var testimonial = await _context.Testimomals.FirstOrDefaultAsync(t => t.Id == id);
            if (testimonial == null) return NotFound();

            return View(testimonial);
        }

    
        [HttpGet]
        public IActionResult Create()
        {
            return View(new TestimonialVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestimonialVM model)
        {
            

            var testimonial = new Testimomal
            {
                Description = model.Description,
                PersonName = model.PersonName,
                PersonJob = model.PersonJob,
                Picture = model.Picture
                
            };

            if (model.Photo != null)
            {
                var uniqueFileName = $"{Guid.NewGuid()}_{model.Photo.FileName}";
                var uploadsFolder = Path.Combine(_env.WebRootPath, "assets", "img");
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }

                testimonial.Picture = uniqueFileName;
            }

            await _context.Testimomals.AddAsync(testimonial);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var testimonial = await _context.Testimomals.FirstOrDefaultAsync(t => t.Id == id);
            if (testimonial == null) return NotFound();

            return View(testimonial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Testimomal model, IFormFile? photo)
        {
            if (id != model.Id) return BadRequest();

            var testimonial = await _context.Testimomals.FindAsync(id);
            if (testimonial == null) return NotFound();

            testimonial.Description = model.Description;
            testimonial.PersonName = model.PersonName;
            testimonial.PersonJob = model.PersonJob;

            if (photo != null)
            {
                var uniqueFileName = $"{Guid.NewGuid()}_{photo.FileName}";
                var newFilePath = Path.Combine(_env.WebRootPath, "assets", "img", uniqueFileName);

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                testimonial.Picture = uniqueFileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var testimonial = await _context.Testimomals.FindAsync(id);
            if (testimonial == null) return NotFound();

            _context.Testimomals.Remove(testimonial);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}