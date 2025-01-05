using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectMVC.ViewModels.Reservation
{
    public class CarReservationViewModel
    {
        public int CarId { get; set; } // Car ID being reserved

        public string UserId { get; set; }
        public string Brand { get; set; } // Car Brand
        public string Model { get; set; } // Car Model
        public decimal PricePerDay { get; set; } // Price per day for the car
        public DateTime StartDate { get; set; } // Reservation start date
        public DateTime EndDate { get; set; } // Reservation end date
        public string? Location { get; set; } // Location of the car
        public decimal TotalPrice { get; set; } // Total reservation price

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Driving license upload is required.")]
        public IFormFile DrivingLicense { get; set; }

        public string Status { get; set; }
    }
}
