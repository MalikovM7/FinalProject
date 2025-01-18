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
            
            var validation = await _testimonialvalidator.ValidateAsync(viewModel);

            if (!validation.IsValid)
            {
                
                foreach (var error in validation.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return View(viewModel);
            }

           
            var testimonial = new Testimonial
            {
                Comment = viewModel.Comment,
                UserId = viewModel.UserName,
                Rating = viewModel.Rating,
            };

           
            await _testimonialService.SaveTestimonial(testimonial);

            
            viewModel.SuccessMessage = "Your message has been sent successfully!";
            ModelState.Clear(); 

            
            return View(new AddTestimonialViewModel { SuccessMessage = viewModel.SuccessMessage });
        }
    }
}
