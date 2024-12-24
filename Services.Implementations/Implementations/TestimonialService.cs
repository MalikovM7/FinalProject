using Domain.Exceptions;
using Domain.Models;
using Persistence.Migrations;
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
    public class TestimonialService : ITestimonialService
    {
        private readonly ITestimonialRepository _testimonialRepository;

        public TestimonialService(ITestimonialRepository testimonialRepository)
        {
            _testimonialRepository = testimonialRepository;
        }


        public async Task SaveTestimonial(Domain.Models.Testimonial testimonial)
        {
            
            await _testimonialRepository.AddTestimonial(testimonial); 
        }

        public async Task DeleteTestimonialAsync(int id)
        {
            var testimonial = await _testimonialRepository.GetByIdAsync(id);
            if (testimonial == null)
                throw new KeyNotFoundException($"Testimonial with id {id} not found.");

            await _testimonialRepository.DeleteAsync(id);
        }

        public async Task<Domain.Models.Testimonial> GetTestimonialByIdAsync(int id)
        {
            return await _testimonialRepository.GetByIdAsync(id);
        }

        public async Task<List<Domain.Models.Testimonial>> GetTestimonalAsync()
        {
            return (await _testimonialRepository.GetAllAsync()).ToList();
        }

        public async Task UpdateTestimonialAsync(int id, Domain.Models.Testimonial testimonial)
        {
            var existingtest = await _testimonialRepository.GetByIdAsync(id);
            if (existingtest == null)
                throw new NotFoundException(ExceptionMessages.NotFoundMessage);
            existingtest.Comment = testimonial.Comment;
            existingtest.Rating = testimonial.Rating;
            existingtest.UserId= testimonial.UserId;

            await _testimonialRepository.EditAsync(existingtest);
        }

    }
}
