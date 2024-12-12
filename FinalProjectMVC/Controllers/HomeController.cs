using Domain.Models;
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
        private readonly INewsService _newsService;

        public HomeController(ILogger<HomeController> logger, IHomePreviewService homePreviewService, IAboutUsService aboutUsService, IfaqService faqService, INewsService newsService)
        {
            _logger = logger;
            _homePreviewService = homePreviewService;
            _aboutUsService = aboutUsService;
            _faqService = faqService;
            _newsService = newsService;
           
        }

        public async Task<IActionResult> Index()
        {
           
            var previews = await _homePreviewService.GetPreviewsAsync();
            var selectedPreviews = previews.Where(p => p.IsSelected).ToList();

            var aboutUsContent = await _aboutUsService.GetAboutUsAsync();
            var faqs = await _faqService.GetFAQSAsync();
            var news = await _newsService.GetNewsAsync();

         
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

            if (news == null || !news.Any())
            {
                _logger.LogWarning("No News found in the database.");
            }

            // Populate ViewModel
            var model = new HomePageViewModel
            {
                Previews = selectedPreviews ?? Enumerable.Empty<HomePreview>(),
                AboutUs = aboutUsContent ?? Enumerable.Empty<AboutUsViewModel>(),
                FAQs = faqs ?? Enumerable.Empty<FAQ>(),
                News = news ?? Enumerable.Empty<News>()
            };

            return View(model);
        }
    }
}