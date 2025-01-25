
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using Domain.Identity;
using Domain.Common;

namespace Domain.Models
{
    public class Reservation : BaseEntity 
    {

       

        [ForeignKey("Car")]
        public int CarId { get; set; }

        [Required]
        public string UserId { get; set; }

        
        [Required]
        public string UserName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public string? PhoneNumber { get; set; }
        public string? DrivingLicensePath { get; set; }
        public string? Status { get; set; } 



        
        public Car Car { get; set; }
        public AppUser AppUser { get; set; }
    }
}
