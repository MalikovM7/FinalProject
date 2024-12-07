

using FinalProjectMVC.Common;

namespace FinalProjectMVC.Models
{
    public class Reservation : BaseEntity
    {
        


        // Reservation details
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }

        // Status of the reservation (Pending, Accepted, Rejected)
        public string Status { get; set; }
    }
}