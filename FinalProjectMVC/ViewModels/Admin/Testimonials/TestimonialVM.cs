using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FinalProjectMVC.ViewModels.Admin.Testimonials
{
    public class TestimonialVM
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string PersonName { get; set; }

        [Required]
        public string PersonJob { get; set; }


        [Required]

        public string Picture { get; set; }

        [Required]

        public IFormFile Photo { get; set; } 
    }
}
