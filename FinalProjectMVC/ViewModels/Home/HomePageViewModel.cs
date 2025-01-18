using Domain.Models;
using FinalProjectMVC.ViewModels.Reservation;

namespace FinalProjectMVC.ViewModels.Home
{
    public class HomePageViewModel
    {
        public IEnumerable<HomePreview> Previews { get; set; }
        public IEnumerable<AboutUsViewModel> AboutUs { get; set; }
        public IEnumerable<FAQ> FAQs { get; set; }

        public IEnumerable<News> News { get; set; }

        public IEnumerable<Slider> Sliders { get; set; }

        public IEnumerable<SliderImage> SliderImages { get; set; }

        public IEnumerable<Car> Cars { get; set; }


        

        //public IEnumerable<ReservePageViewModel> AvailableCars { get; set; }
        public int TotalCars { get; set; }
        public int TotalUsers { get; set; }
        public int TotalReservations { get; set; }
    }
}
