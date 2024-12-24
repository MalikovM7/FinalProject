using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FinalProjectMVC.ViewModels.Admin.Testimonials
{
    public class AdminTestimonialViewModel
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

        public string UserId { get; set; }
        public bool IsApproved { get; set; }
    }
}
