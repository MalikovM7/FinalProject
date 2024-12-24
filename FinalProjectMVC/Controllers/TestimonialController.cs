using Domain.Models;
using FinalProjectMVC.ViewModels.Contact;
using FinalProjectMVC.ViewModels.Testimonials;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace FinalProjectMVC.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        [HttpGet]
        public IActionResult AddTestimonial()
        {
            return View(new AddTestimonialViewModel());
        }

        [HttpPost]
        public ActionResult AddTestimonial(AddTestimonialViewModel viewModel)
        {
            var testimonial = new Testimonial
            {
               Comment = viewModel.Comment,
               UserId = viewModel.UserId,
               Rating = viewModel.Rating,

             
            };

            _testimonialService.SaveTestimonial(testimonial);

            // Set success message
            viewModel.SuccessMessage = "Your message has been sent successfully!";
            ModelState.Clear(); // Clear the form
            return View(new AddTestimonialViewModel { SuccessMessage = viewModel.SuccessMessage });
        }
    }
}
