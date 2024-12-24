using Domain.Models;
using Persistence.Data;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Implementations
{
    public class TestimonialRepository : BaseRepository<Testimonial> , ITestimonialRepository
    {
        public TestimonialRepository(AppDbContext context) : base(context)
        {

        }

        public async Task AddTestimonial(Testimonial testimonial)
        {
            _context.Testimonials.Add(testimonial);
            _context.SaveChanges();
        }
    }
}
