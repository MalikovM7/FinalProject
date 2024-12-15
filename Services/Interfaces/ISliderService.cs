using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
