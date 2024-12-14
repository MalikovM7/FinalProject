using Domain.Exceptions;
using Domain.Models;
using Persistence.Repositories.Implementations;
using Repositories.Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Implementations
{
    public class SliderImageService : ISliderImageService
    {
        private readonly ISliderImageRepository _sliderimagerepository;

        public SliderImageService(ISliderImageRepository sliderimagerepository)
        {
            _sliderimagerepository = sliderimagerepository;
        }

        public async Task AddSliderImageAsync(SliderImage SliderImage)
        {
            await _sliderimagerepository.CreateAsync(SliderImage);
        }

        public async Task DeleteSliderImageAsync(int id)
        {
            await _sliderimagerepository.DeleteAsync(id);
        }

        public async Task<SliderImage> GetSliderImageByIdAsync(int id)
        {
           return (await _sliderimagerepository.GetByIdAsync(id));
        }

        public async Task<List<SliderImage>> GetSliderImageSAsync()
        {
           return (await _sliderimagerepository.GetAllAsync()).ToList();
        }

        public async Task UpdateSliderImageAsync(int id, SliderImage SliderImage)
        {
            var existingImage = await _sliderimagerepository.GetByIdAsync(id);
            if (existingImage == null)
                throw new NotFoundException(ExceptionMessages.NotFoundMessage);

               existingImage.Image= SliderImage.Image;
               existingImage.Photos = SliderImage.Photos;

            await _sliderimagerepository.EditAsync(existingImage);



        }
    }
}
