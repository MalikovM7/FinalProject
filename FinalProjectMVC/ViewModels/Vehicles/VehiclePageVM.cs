using Domain.Models;

namespace FinalProjectMVC.ViewModels.Vehicles
{
    public class VehiclePageVM
    {

        public IEnumerable<Car> Cars { get; set; }

        public IEnumerable<Testimonial> Testimonials { get; set; }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

    }
}
