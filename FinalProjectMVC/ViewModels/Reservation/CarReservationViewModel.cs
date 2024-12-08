namespace FinalProjectMVC.ViewModels.Reservation
{
    public class CarReservationViewModel
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public DateTime AvailabilityStart { get; set; }
        public DateTime AvailabilityEnd { get; set; }
        public string Location { get; set; }
    }
}
