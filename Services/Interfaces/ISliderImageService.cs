using Domain.Models;

namespace Services.Interfaces
{
    public interface ISliderImageService
    {
        Task<List<SliderImage>> GetSliderImageSAsync();
        Task<SliderImage> GetSliderImageByIdAsync(int id);
        Task AddSliderImageAsync(SliderImage SliderImage);
        Task UpdateSliderImageAsync(int id, SliderImage SliderImage);
        Task DeleteSliderImageAsync(int id);

    }
}
