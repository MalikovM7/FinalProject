using Domain.Exceptions;
using Domain.Models;
using Repositories.Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Implementations
{
    public class SliderService : ISliderService
    {

        public readonly ISliderRepository _sliderrepository;

        public SliderService(ISliderRepository sliderrepository)
        {
            _sliderrepository = sliderrepository;
        }


        public async Task AddSliderAsync(Slider Slider)
        {
            await _sliderrepository.CreateAsync(Slider);
        }

        public async Task DeleteSliderAsync(int id)
        {
            await _sliderrepository.DeleteAsync(id);
        }

        public async Task<Slider> GetSliderByIdAsync(int id)
        {
            return (await _sliderrepository.GetByIdAsync(id));
        }

        public async Task<List<Slider>> GetSliderSAsync()
        {
            return (await _sliderrepository.GetAllAsync()).ToList();
        }

        public async Task UpdateSliderAsync(int id, Slider Slider)
        {
            var existingSlider = await _sliderrepository.GetByIdAsync(id);
            if (existingSlider == null)
                throw new NotFoundException(ExceptionMessages.NotFoundMessage);

            existingSlider.Title = Slider.Title;
            existingSlider.Subtitle = Slider.Subtitle;
            existingSlider.IsMain = Slider.IsMain;

            await _sliderrepository.EditAsync(existingSlider);
        }
    }
}
