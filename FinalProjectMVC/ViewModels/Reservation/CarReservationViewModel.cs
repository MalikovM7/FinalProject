using System.ComponentModel.DataAnnotations;

namespace FinalProjectMVC.ViewModels.Reservation
{
    public class CarReservationViewModel
    {
        public int CarId { get; set; } // Car ID being reserved
        public string Brand { get; set; } // Car Brand
        public string Model { get; set; } // Car Model
        public decimal PricePerDay { get; set; } // Price per day for the car
        public DateTime StartDate { get; set; } // Reservation start date
        public DateTime EndDate { get; set; } // Reservation end date
        public string? Location { get; set; } // Location of the car
        public decimal TotalPrice { get; set; } // Total reservation price
    }
}
