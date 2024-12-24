using FinalProjectMVC.ViewModels.Admin.Testimonials;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace FinalProjectMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var testimonials = await _testimonialService.GetTestimonalAsync();
            var viewModel = testimonials.Select(t => new AdminTestimonialViewModel
            {
                Id = t.Id,
                UserId = t.UserId,
                Comment = t.Comment,
                Rating = t.Rating,
                IsApproved = t.IsApproved
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                var testimonial = await _testimonialService.GetTestimonialByIdAsync(id);
                if (testimonial == null)
                {
                    TempData["Error"] = "Testimonial not found.";
                    return RedirectToAction("Index");
                }

                var viewModel = new AdminTestimonialViewModel
                {
                    Id = testimonial.Id,
                    UserId = testimonial.UserId,
                    Comment = testimonial.Comment,
                    Rating = testimonial.Rating,
                    IsApproved = true
                };

                testimonial.IsApproved = true;
                await _testimonialService.UpdateTestimonialAsync(id, testimonial);
                TempData["Message"] = "Testimonial approved successfully.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _testimonialService.DeleteTestimonialAsync(id);
                TempData["Message"] = "Testimonial deleted successfully.";
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}