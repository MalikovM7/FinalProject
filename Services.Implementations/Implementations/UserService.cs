using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
