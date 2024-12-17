using System.ComponentModel.DataAnnotations;

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
        [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Fuel type is required.")]
        [Display(Name = "Fuel Type")]
        public string Fueltype { get; set; }

        [Required(ErrorMessage = "License plate is required.")]
        [Display(Name = "License Plate")]
        public string LicensePlate { get; set; }

        [Required(ErrorMessage = "Price per day is required.")]
        [Display(Name = "Price Per Day")]
        [DataType(DataType.Currency)]
        public decimal PricePerDay { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "Image path is required.")]
        [Display(Name = "Image Path")]
        public string ImagePath { get; set; }

        public string? Location { get; set; }

        [Display(Name = "Availability Start")]
        [DataType(DataType.Date)]
        public DateTime? AvailabilityStart { get; set; }

        [Display(Name = "Availability End")]
        [DataType(DataType.Date)]
        public DateTime? AvailabilityEnd { get; set; }

        public int CategoryId { get; set; } // Required for category binding
        public string CategoryName { get; set; } // Name of the category
    }
}
