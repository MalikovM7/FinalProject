using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectMVC.ViewModels.Reservation
{
    public class CarReservationViewModel
    {
        public int CarId { get; set; } 

        public string UserId { get; set; }
        public string Brand { get; set; } 
        public string Model { get; set; } 
        public decimal PricePerDay { get; set; } 
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 
        public string? Location { get; set; } 
        public decimal TotalPrice { get; set; } 

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Driving license upload is required.")]
        public IFormFile DrivingLicense { get; set; }

        public string Status { get; set; }
    }
}
