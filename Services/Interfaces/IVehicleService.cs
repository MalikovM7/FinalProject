using Domain.Models;
using Repositories.Repositories;

namespace Services.Interfaces
{
    public interface IVehicleService
    {
        Task<List<Car>> GetCarSAsync();
        Task<Car> GetCarByIdAsync(int id);
        Task AddCarAsync(Car Car);
        Task UpdateCarAsync(int id, Car Car);
        Task DeleteCarAsync(int id);

        Task<List<Category>> GetCategoriesAsync();
        Task<List<Car>> GetAvailableCarsAsync(DateTime startDate, DateTime endDate);



    }
}
