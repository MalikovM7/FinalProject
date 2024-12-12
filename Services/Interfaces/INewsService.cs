using Domain.Models;


namespace Services.Interfaces
{
    public interface INewsService
    {
        Task<List<News>> GetNewsAsync();
        Task<News> GetNewsByIdAsync(int id);
        Task AddNewsAsync(News news);
        Task UpdateNewsAsync(int id, News news);
        Task DeleteNewsAsync(int id);

        Task MarkAsSelectedAsync(int id);
        Task UnmarkAsSelectedAsync(int id);

    }
}
