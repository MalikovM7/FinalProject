using Persistence.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Repositories.Repositories;
using Persistence.Repositories.Implementations;
using Domain.Exceptions;

namespace FinalProjectMVC.Services.Implementations
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICategoryRepository _categoryRepository;

        public VehicleService(IVehicleRepository vehicleRepository, ICategoryRepository categoryRepository)
        {
            _vehicleRepository = vehicleRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task AddCarAsync(Car Car)
        {
            await _vehicleRepository.CreateAsync(Car);
        }

        public async Task DeleteCarAsync(int id)
        {
            await _vehicleRepository.DeleteAsync(id);
        }

        public async Task<List<Car>> GetAvailableCarsAsync(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date cannot be later than the end date.");
            }

            // Retrieve all cars from the repository
            var allCars = await _vehicleRepository.GetAllAsync();

            // Filter cars based on availability dates
            var availableCars = allCars
                .Where(car => car.IsAvailable &&
                              car.AvailabilityStart <= startDate &&
                              car.AvailabilityEnd >= endDate)
                .ToList();

            return availableCars;
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            return (await _vehicleRepository.GetByIdAsync(id));
        }

        public async Task<List<Car>> GetCarSAsync()
        {
            return (await _vehicleRepository.GetAllAsync()).ToList();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.ToList(); // Ensure it's converted to a list
        }

        public async Task UpdateCarAsync(int id, Car Car)
        {
            var existingCar = await _vehicleRepository.GetByIdAsync(id);
            if (existingCar == null)
                throw new NotFoundException(ExceptionMessages.NotFoundMessage);

            existingCar.Brand = Car.Brand;
            existingCar.Model = Car.Model;
            existingCar.Year = Car.Year;
            existingCar.Color = Car.Color;
            existingCar.Fueltype = Car.Fueltype;
            existingCar.LicensePlate = Car.LicensePlate;
            existingCar.PricePerDay = Car.PricePerDay;
            existingCar.IsAvailable = Car.IsAvailable;
            existingCar.ImagePath = Car.ImagePath;
            existingCar.Location = Car.Location;
            existingCar.AvailabilityStart = Car.AvailabilityStart;
            existingCar.AvailabilityEnd = Car.AvailabilityEnd;
            existingCar.CategoryId = Car.CategoryId;



            await _vehicleRepository.EditAsync(existingCar);
        }
    }
}
