using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectMVC.ViewModels.Admin.Car
{
    public class VehicleVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Brand is required.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Color is required.")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "FuelType is required.")]
        public string Fueltype { get; set; }

        [Required(ErrorMessage = "LicensePlate is required.")]
        public string LicensePlate { get; set; }

        [Required(ErrorMessage = "PricePerDay is required.")]
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "ImagePath is required.")]
        public string ImagePath { get; set; }
        public string? Location { get; set; }
        public DateTime? AvailabilityStart { get; set; }
        public DateTime? AvailabilityEnd { get; set; }
        public int CategoryId { get; set; } // Required for category binding
        public string CategoryName { get; set; } // Name of the category

       
    }
}
