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
    public class NewsService : INewsService
    {

        public readonly INewsRepository _newsrepository;

        public NewsService(INewsRepository newsrepository)
        {
            _newsrepository = newsrepository;
        }
        public async Task AddNewsAsync(News news)
        {
            await _newsrepository.CreateAsync(news);
        }

        public async Task DeleteNewsAsync(int id)
        {
           await _newsrepository.DeleteAsync(id);
        }

        public async Task<List<News>> GetNewsAsync()
        {
            return (await _newsrepository.GetAllAsync()).ToList();
        }

        public async Task<News> GetNewsByIdAsync(int id)
        {
           return await _newsrepository.GetByIdAsync(id);
        }

        public async Task MarkAsSelectedAsync(int id)
        {
            var selectedNewsCount = await _newsrepository.CountAsync(n => n.IsSelected);

            if (selectedNewsCount >= 2)
            {
                throw new InvalidOperationException("Only two items can be selected at a time.");
            }

            var news = await _newsrepository.GetByIdAsync(id);
            if (news == null)
                throw new NotFoundException(ExceptionMessages.NotFoundMessage);

            news.IsSelected = true;
            await _newsrepository.EditAsync(news);
        }

        public async Task UnmarkAsSelectedAsync(int id)
        {
            var news = await _newsrepository.GetByIdAsync(id);

            if (news == null)
                throw new NotFoundException(ExceptionMessages.NotFoundMessage);

            news.IsSelected = false;

            await _newsrepository.EditAsync(news);
        }

        public async Task UpdateNewsAsync(int id, News news)
        {
           var existingnews = await _newsrepository.GetByIdAsync(id);
            if (existingnews == null)
                throw new NotFoundException(ExceptionMessages.NotFoundMessage);
            existingnews.Title = news.Title;
            existingnews.Description = news.Description;
            existingnews.AuthorName = news.AuthorName;
            existingnews.ImagePath = news.ImagePath;
            existingnews.NewsDate = news.NewsDate;
            existingnews.NewsCategory = news.NewsCategory;

            await _newsrepository.EditAsync(existingnews);




        }
    }
}
