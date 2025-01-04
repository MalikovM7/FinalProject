using Domain.Models;
using FinalProjectMVC.ViewModels.Contact;
using FinalProjectMVC.ViewModels.Testimonials;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Services.Interfaces;

namespace FinalProjectMVC.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;
        private readonly IValidator<AddTestimonialViewModel> _testimonialvalidator;

        public TestimonialController(ITestimonialService testimonialService, IValidator<AddTestimonialViewModel> testimonialvalidator)
        {
            _testimonialService = testimonialService;
            _testimonialvalidator = testimonialvalidator;
        }

        [HttpGet]
        public IActionResult AddTestimonial()
        {
            return View(new AddTestimonialViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> AddTestimonial(AddTestimonialViewModel viewModel)
        {
            // Validate the incoming ViewModel
            var validation = await _testimonialvalidator.ValidateAsync(viewModel);

            if (!validation.IsValid)
            {
                // If validation fails, add errors to the ModelState
                foreach (var error in validation.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                // Return the view with the validation errors
                return View(viewModel);
            }

            // If validation succeeds, create and save the testimonial
            var testimonial = new Testimonial
            {
                Comment = viewModel.Comment,
                UserId = viewModel.UserName,
                Rating = viewModel.Rating,
            };

            // Save the testimonial
            await _testimonialService.SaveTestimonial(testimonial);

            // Show success message
            viewModel.SuccessMessage = "Your message has been sent successfully!";
            ModelState.Clear(); // Clear the form after a successful submission

            // Return a fresh view model with the success message
            return View(new AddTestimonialViewModel { SuccessMessage = viewModel.SuccessMessage });
        }
    }
}
