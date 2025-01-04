namespace FinalProjectMVC.ViewModels.Testimonials
{
    public class AddTestimonialViewModel
    {
        public string Comment { get; set; }
        public int Rating { get; set; }
        public string UserName { get; set; } // Optional if UserId is not needed here
        public string SuccessMessage { get; set; }
    }
}
