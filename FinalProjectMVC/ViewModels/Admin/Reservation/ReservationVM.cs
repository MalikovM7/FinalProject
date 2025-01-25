namespace FinalProjectMVC.ViewModels.Admin.Reservation
{
    public class ReservationVM
    {
        public int Id { get; set; } 

        public int CarId { get; set; } 

        public string UserId { get; set; } 

        public string UserName { get; set; }

        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 

        public decimal TotalPrice { get; set; } 

        public string Status { get; set; }

        public string PhoneNumber { get; set; }
        public string DrivingLicensePath { get; set; } 
    }
}
