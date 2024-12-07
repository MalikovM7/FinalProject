using FinalProjectMVC.Models;

namespace Services.Interfaces
{
    public interface IAboutUsService
    {
        Task<List<AboutUsViewModel>> GetAboutUsAsync();
        Task<AboutUsViewModel> GetAboutUsByIdAsync(int id);
        Task AddAboutUsAsync(AboutUsViewModel aboutUsmodel);
        Task UpdateAboutUsAsync(int id, AboutUsViewModel aboutUsmodel);
        Task DeleteAboutUsAsync(int id);


    }
}
