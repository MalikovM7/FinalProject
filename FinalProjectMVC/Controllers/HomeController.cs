using FinalProjectMVC.Models;
using FinalProjectMVC.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomePreviewService _homePreviewService;
        private readonly IAboutUsService _aboutUsService;
        private readonly IfaqService _faqService;

        public HomeController(ILogger<HomeController> logger, IHomePreviewService homePreviewService, IAboutUsService aboutUsService, IfaqService faqService)
        {
            _logger = logger;
            _homePreviewService = homePreviewService;
            _aboutUsService = aboutUsService;
            _faqService = faqService;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch only selected previews
            var previews = await _homePreviewService.GetPreviewsAsync();
            var selectedPreviews = previews.Where(p => p.IsSelected).ToList();

            var aboutUsContent = await _aboutUsService.GetAboutUsAsync();
            var faqs = await _faqService.GetFAQSAsync();

            // Logging if data is missing
            if (!selectedPreviews.Any())
            {
                _logger.LogWarning("No selected home preview content found in the database.");
            }

            if (aboutUsContent == null || !aboutUsContent.Any())
            {
                _logger.LogWarning("No About Us content found in the database.");
            }

            if (faqs == null || !faqs.Any())
            {
                _logger.LogWarning("No FAQs found in the database.");
            }

            // Populate ViewModel
            var model = new HomePageViewModel
            {
                Previews = selectedPreviews ?? Enumerable.Empty<HomePreview>(),
                AboutUs = aboutUsContent ?? Enumerable.Empty<AboutUsViewModel>(),
                FAQs = faqs ?? Enumerable.Empty<FAQ>()
            };

            return View(model);
        }
    }
}