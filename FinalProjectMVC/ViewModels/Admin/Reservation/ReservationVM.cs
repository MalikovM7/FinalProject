﻿namespace FinalProjectMVC.ViewModels.Admin.Reservation
{
    public class ReservationVM
    {
        public int Id { get; set; } // Unique identifier for the reservation

        public int CarId { get; set; } // Car associated with the reservation

        public string UserId { get; set; } // User making the reservation

        public DateTime StartDate { get; set; } // Reservation start date
        public DateTime EndDate { get; set; } // Reservation end date

        public decimal TotalPrice { get; set; } // Total price for the reservation
    }
}
