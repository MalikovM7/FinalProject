﻿using FinalProjectMVC.ViewModels.Testimonials;
using FluentValidation;



namespace FinalProjectMVC.FluentValidation.TestimonialsValidation
{
    public class TestimonialAddValidation : AbstractValidator<AddTestimonialViewModel>
    {

       public TestimonialAddValidation() {

            RuleFor(x => x.UserName)
                    .NotEmpty()
                    .NotNull()
                    .MaximumLength(30);
            RuleFor(x=>x.Comment)
                 .NotEmpty()
                    .NotNull()
                    .MaximumLength(300);
            RuleFor(x => x.Rating)
                    .NotEmpty()
                    .NotNull()
                    .InclusiveBetween(1, 5)
                    .WithMessage("Rating must be between 1 and 5.");


        }


    }
}
