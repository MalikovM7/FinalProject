
using Domain.Common;

using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Models
{
    public class Car : BaseEntity
    {

        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string Fueltype { get; set; }
        public string LicensePlate { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public string ImagePath { get; set; }
        public string? Location { get; set; }
        public DateTime? AvailabilityStart { get; set; }
        public DateTime? AvailabilityEnd { get; set; }

        
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

       
        public Category Category { get; set; }

        [NotMapped] 
        public string CategoryName => Category?.Name;



    }
}
