using Domain.Models;


namespace Services.Interfaces
{
    public interface ISliderService
    {

        Task<List<Slider>> GetSliderSAsync();
        Task<Slider> GetSliderByIdAsync(int id);
        Task AddSliderAsync(Slider Slider);
        Task UpdateSliderAsync(int id, Slider Slider);
        Task DeleteSliderAsync(int id);


    }
}
