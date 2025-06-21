// Employees/Infrastructure/Repositories/SpecialtyRepository.cs

using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;

using Microsoft.EntityFrameworkCore; 
namespace FitManager_Web_Services.Employees.Infrastructure.Repositories
{
    /// <summary>
    /// Concrete implementation of the Specialty repository.
    /// Inherits from BaseRepository, providing standard CRUD operations for Specialties.
    /// </summary>
    public class SpecialtyRepository : BaseRepository<Specialty>, ISpecialtyRepository
    {
        public SpecialtyRepository(AppDbContext context) : base(context)
        {
            
        }

        /// <summary>
        /// Asynchronously retrieves a collection of Specialties by their unique identifiers.
        /// </summary>
        /// <param name="ids">An enumerable collection of unique identifiers to retrieve.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Specialty"/> entities found,
        /// or an empty collection if no entities match the provided IDs.
        /// </returns>
        public async Task<IEnumerable<Specialty>> GetByIdsAsync(IEnumerable<int> ids) 
        {
            if (ids == null || !ids.Any())
            {
                return new List<Specialty>();
            }
           
            return await _entities.Where(s => ids.Contains(s.Id)).ToListAsync();
        }
    }
}