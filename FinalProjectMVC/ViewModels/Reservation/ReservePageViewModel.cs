using FinalProjectMVC.ViewModels.Reservation;
using FinalProjectMVC.ViewModels.Admin.Car;

namespace FinalProjectMVC.ViewModels.Reservation
{
    public class ReservePageViewModel
    {
        public List<VehicleVM> AvailableCars { get; set; } = new List<VehicleVM>();

        public CarReservationViewModel Reservation { get; set; } = new CarReservationViewModel();

        public List<CarReservationViewModel> ReservationList { get; set; } = new List<CarReservationViewModel>();

    }
}
