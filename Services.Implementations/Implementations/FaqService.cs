using FinalProjectMVC.Exceptions;
using FinalProjectMVC.Models;
using Repositories.Repositories;
using Services.Interfaces;

namespace FinalProjectMVC.Services.Implementations
{
    public class FaqService : IfaqService
    {
        private readonly IFaqRepository _faqRepository;

        public FaqService(IFaqRepository faqRepository)
        {
            _faqRepository = faqRepository;
        }

        public async Task<List<FAQ>> GetFAQSAsync()
        {
            return (await _faqRepository.GetAllAsync()).ToList();
        }

        public async Task<FAQ> GetFAQByIdAsync(int id)
        {
            return await _faqRepository.GetByIdAsync(id);
        }

        public async Task AddFAQAsync(FAQ faq)
        {
            await _faqRepository.CreateAsync(faq);
        }

        public async Task UpdateFAQAsync(int id, FAQ faq)
        {
            var existingFaq = await _faqRepository.GetByIdAsync(id);
            if (existingFaq == null)
                throw new NotFoundException(ExceptionMessages.NotFoundMessage);

            existingFaq.Question = faq.Question;
            existingFaq.Answer = faq.Answer;

            await _faqRepository.EditAsync(existingFaq);
        }

        public async Task DeleteFAQAsync(int id)
        {
            await _faqRepository.DeleteAsync(id);
        }
    }
}