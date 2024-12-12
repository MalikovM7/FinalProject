using Domain.Models;

namespace FinalProjectMVC.ViewModels.Home
{
    public class HomePageViewModel
    {
        public IEnumerable<HomePreview> Previews { get; set; }
        public IEnumerable<AboutUsViewModel> AboutUs { get; set; }
        public IEnumerable<FAQ> FAQs { get; set; }

        public IEnumerable<News> News { get; set; }
    }
}
