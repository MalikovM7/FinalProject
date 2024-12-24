using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public interface ITestimonialRepository : IBaseRepository<Testimonial>
    {
        Task AddTestimonial(Testimonial testimonial);
    }
}
