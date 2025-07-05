using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;
using FitManager_Web_Services.Users.Domain.Model;
using FitManager_Web_Services.Users.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Users.Infrastructure.Repositories
{
    /// <summary>
    /// Implements the <see cref="IUserRepository"/>, providing data access for <see cref="User"/> aggregates.
    /// </summary>
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// Find a user by email.
        /// </summary>
        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _context.Set<User>()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// Check if a user exists by email.
        /// </summary>
        public bool ExistsByEmail(string email)
        {
            return _context.Set<User>().Any(u => u.Email == email);
        }
    }
}
