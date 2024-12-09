using Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectMVC.ViewModels.Admin.Car
{
    public class VehicleVM
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public string Fueltype { get; set; }
        public string LicensePlate { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public string ImagePath { get; set; }
        public string? Location { get; set; }
        public DateTime? AvailabilityStart { get; set; }
        public DateTime? AvailabilityEnd { get; set; }
        public int CategoryId { get; set; } // Required for category binding
        public string CategoryName { get; set; } // Name of the category
    }
}
