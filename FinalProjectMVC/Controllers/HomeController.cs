using Domain.Models;
using FinalProjectMVC.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Migrations;
using Services.Interfaces;


namespace FinalProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IHomePreviewService _homePreviewService;
        private readonly IAboutUsService _aboutUsService;
        private readonly IfaqService _faqService;
        private readonly INewsService _newsService;
        private readonly ISliderService _sliderService;
        private readonly IVehicleService _vehicleService;
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IHomePreviewService homePreviewService, IAboutUsService aboutUsService, IfaqService faqService, INewsService newsService, ISliderService sliderService, IVehicleService vehicleService, IReservationService reservationService, IUserService userService)
        {
            _logger = logger;
            _context = context;
            _homePreviewService = homePreviewService;
            _aboutUsService = aboutUsService;
            _faqService = faqService;
            _newsService = newsService;
            _sliderService = sliderService;
            _vehicleService = vehicleService;
            _reservationService = reservationService;
            _userService = userService;
           
        }

        public async Task<IActionResult> Index()
        {
           
            var previews = await _homePreviewService.GetPreviewsAsync();
            var selectedPreviews = previews.Where(p => p.IsSelected).ToList();
            var aboutUsContent = await _aboutUsService.GetAboutUsAsync();
            var faqs = await _faqService.GetFAQSAsync();
            var news = await _newsService.GetNewsAsync();
            var sliders = await _sliderService.GetSliderSAsync();
            var sortedSliders = sliders.OrderByDescending(s => s.IsMain).ToList();
            var cars = await _vehicleService.GetCarSAsync();
            var reservations = await _reservationService.GetReservationsAsync();
            var users = await _userService.GetUsersAsync();
            var sliderimages = await _context.SliderImages.ToListAsync();



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

            if (sliders == null || !sliders.Any())
            {
                _logger.LogWarning("No sliders found in the database");
            }
            if (sliderimages == null || !sliderimages.Any())
            {
                _logger.LogWarning("No slider images found in the database");
            }
            if (cars == null || !cars.Any())
            {
                _logger.LogWarning("No cars found in the database");
            }

            if (reservations == null || !reservations.Any())
            {
                _logger.LogWarning("No reservations found in the database");
            }
            if (users == null || !users.Any())
            {
                _logger.LogWarning("No users found in the database");
            }



            // Populate ViewModel
            var model = new HomePageViewModel
            {
                Previews = selectedPreviews ?? Enumerable.Empty<HomePreview>(),
                AboutUs = aboutUsContent ?? Enumerable.Empty<AboutUsViewModel>(),
                FAQs = faqs ?? Enumerable.Empty<FAQ>(),
                News = news ?? Enumerable.Empty<News>(),
                Sliders = sortedSliders ?? Enumerable.Empty<Slider>(),
                SliderImages = sliderimages ?? Enumerable.Empty<SliderImage>(),
                Cars = cars ?? Enumerable.Empty<Car>(),
                TotalCars = cars.Count(),
                TotalUsers = users.Count(),
                TotalReservations = reservations.Count()
            };

            return View(model);
        }

      
    }
}