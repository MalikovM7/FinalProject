
using FinalProjectMVC.Exceptions;
using FinalProjectMVC.Models;
using Repositories.Repositories;
using Services.Interfaces;


namespace FinalProjectMVC.Services.Implementations
{
    public class HomePreviewService : IHomePreviewService
    {
        private readonly IHomePreviewRepository _previewrepository;

        public HomePreviewService(IHomePreviewRepository previewrepository)
        {
           _previewrepository = previewrepository;
        }

        public async Task AddPreviewAsync(HomePreview homePreview)
        {
             await _previewrepository.CreateAsync(homePreview);
        }

        public async Task DeletePreviewAsync(int id)
        {
            await _previewrepository.DeleteAsync(id);
        }

        public async Task UpdatePreviewAsync(int id, HomePreview homePreview)
        {
            var existingpreview = await _previewrepository.GetByIdAsync(id);
            if (existingpreview == null)
                throw new NotFoundException(ExceptionMessages.NotFoundMessage);

            existingpreview.Title= homePreview.Title;
            existingpreview.Description = homePreview.Description;
            existingpreview.ImagePath= homePreview.ImagePath;

            await _previewrepository.EditAsync(existingpreview);


        }
        public async Task<List<HomePreview>> GetPreviewsAsync()
        {
            return (await _previewrepository.GetAllAsync()).ToList();
        }

        public async Task<HomePreview> GetPreviewByIdAsync(int id)
        {
            return await _previewrepository.GetByIdAsync(id);
        }

        public async Task MarkAsSelectedAsync(int id)
        {
            var preview = await _previewrepository.GetByIdAsync(id);
            if (preview == null) throw new NotFoundException(ExceptionMessages.NotFoundMessage);

            preview.IsSelected = true;
            await _previewrepository.EditAsync(preview);
        }

        public async Task UnmarkAsSelectedAsync(int id)
        {
            var preview = await _previewrepository.GetByIdAsync(id);
            if (preview == null) throw new Exception("Preview not found.");

            preview.IsSelected = false;
            await _previewrepository.EditAsync(preview);
        }
    }
}
