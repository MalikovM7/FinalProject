using FinalProjectMVC.Models;

namespace Services.Interfaces
{
    public interface IHomePreviewService
    {
        Task<List<HomePreview>> GetPreviewsAsync();
        Task<HomePreview> GetPreviewByIdAsync(int id);
        Task AddPreviewAsync(HomePreview homePreview);
        Task UpdatePreviewAsync(int id, HomePreview homePreview);
        Task DeletePreviewAsync(int id);

        Task MarkAsSelectedAsync(int id);
        Task UnmarkAsSelectedAsync(int id);
    }
}
