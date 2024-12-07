using FinalProjectMVC.Models;

namespace Services.Interfaces
{
    public interface IVehicleService
    {
        Task<List<Car>> GetVehiclesAsync();

    }
}
