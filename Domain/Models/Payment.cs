using FinalProjectMVC.Common;

namespace FinalProjectMVC.Models
{
    public class Payment : BaseEntity
    {
       
        public int ReservationId { get; set; } // Foreign Key
        public Reservation Reservation { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; } // e.g., Paid, Pending, Failed
    }
}
