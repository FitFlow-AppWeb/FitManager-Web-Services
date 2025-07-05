using FitManager_Web_Services.Users.Domain.Model;
using FitManager_Web_Services.Shared.Domain.Repositories;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Users.Domain.Repositories;

/// <summary>
/// Defines the contract for a repository that manages <see cref="User"/> aggregates.
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Find a user by email (username).
    /// </summary>
    Task<User?> FindByEmailAsync(string email);
    
    /// <summary>
    /// Check if a user exists by email.
    /// </summary>
    bool ExistsByEmail(string email);
}