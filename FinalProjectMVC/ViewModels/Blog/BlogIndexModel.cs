using Domain.Models;
namespace FinalProjectMVC.ViewModels.Blog
{
    public class BlogIndexModel
    {
        public IEnumerable<BlogPostVM> BlogPosts { get; set; }
        public IEnumerable<Testimonial> Testimonials { get; set; }
    }
}
