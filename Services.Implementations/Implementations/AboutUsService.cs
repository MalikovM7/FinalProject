using FinalProjectMVC.Data;
using FinalProjectMVC.Exceptions;
using FinalProjectMVC.Models;

using Repositories.Repositories;
using Services.Interfaces;

namespace FinalProjectMVC.Services.Implementations
{
    public class AboutUsService : IAboutUsService
    {
        private readonly IAboutUsRepository _aboutUsRepository;

        public AboutUsService(IAboutUsRepository aboutUsRepository)
        {
            _aboutUsRepository = aboutUsRepository;
        }

        public async Task AddAboutUsAsync(AboutUsViewModel aboutUsmodel)
        {
            await _aboutUsRepository.CreateAsync(aboutUsmodel);
        }

        public async Task DeleteAboutUsAsync(int id)
        {
            await _aboutUsRepository.DeleteAsync(id);
        }

        public async Task UpdateAboutUsAsync(int id, AboutUsViewModel aboutUsmodel)
        {
            var existingaboutus = await _aboutUsRepository.GetByIdAsync(id);
            if (existingaboutus == null)
                throw new NotFoundException(ExceptionMessages.NotFoundMessage);

            existingaboutus.Title = aboutUsmodel.Title;
            existingaboutus.Subtitle = aboutUsmodel.Subtitle;
            existingaboutus.Description = aboutUsmodel.Description;
            existingaboutus.Points = aboutUsmodel.Points;
            existingaboutus.ImageUrls = aboutUsmodel.ImageUrls;
          
            

            await _aboutUsRepository.EditAsync(existingaboutus);


        }
        public async Task<List<AboutUsViewModel>> GetAboutUsAsync()
        {
            return (await _aboutUsRepository.GetAllAsync()).ToList();
        }

        public async Task<AboutUsViewModel> GetAboutUsByIdAsync(int id)
        {
            return await _aboutUsRepository.GetByIdAsync(id);
        }

   
    }
}
