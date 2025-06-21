// Employees/Domain/Repositories/ISpecialtyRepository.cs


using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;


namespace FitManager_Web_Services.Employees.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository managing Specialty aggregate roots.
    /// It extends the generic base repository with specific operations for Specialties,
    /// if any are needed beyond the standard CRUD provided by IBaseRepository.
    /// </summary>
    public interface ISpecialtyRepository : IBaseRepository<Specialty>
    {
        /// <summary>
        /// Asynchronously retrieves a collection of Specialties by their unique identifiers.
        /// </summary>
        /// <param name="ids">An enumerable collection of unique identifiers to retrieve.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Specialty"/> entities found,
        /// or an empty collection if no entities match the provided IDs.
        /// </returns>
        Task<IEnumerable<Specialty>> GetByIdsAsync(IEnumerable<int> ids); 
    }
}