
using FinalProjectMVC.Common;
using System.Drawing;

namespace FinalProjectMVC.Models
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

        public string Location { get; set; } // Represents the location where the car can be picked up or dropped off
        public DateTime AvailabilityStart { get; set; } // Start of the car's availability period
        public DateTime AvailabilityEnd { get; set; } // End of the car's availability period


    }
}
