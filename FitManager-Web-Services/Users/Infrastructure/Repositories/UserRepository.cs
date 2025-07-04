using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;
using FitManager_Web_Services.Users.Domain.Model;
using FitManager_Web_Services.Users.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using FitManager_Web_Services.Users.Domain.Model;

namespace FitManager_Web_Services.Users.Infrastructure.Repositories
{
    /// <summary>
    /// Implements the <see cref="IUserRepository"/>, providing data access for <see cref="User"/> aggregates.
    /// </summary>
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves a user by email.
        /// </summary>
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Set<User>()
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
