// Employees/Domain/Repositories/ICertificationRepository.cs

using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;


namespace FitManager_Web_Services.Employees.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository managing Certification aggregate roots.
    /// It extends the generic base repository with specific operations for Certifications,
    /// if any are needed beyond the standard CRUD provided by IBaseRepository.
    /// </summary>
    public interface ICertificationRepository : IBaseRepository<Certification>
    {
        /// <summary>
        /// Asynchronously retrieves a collection of Certifications by their unique identifiers.
        /// </summary>
        /// <param name="ids">An enumerable collection of unique identifiers to retrieve.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Certification"/> entities found,
        /// or an empty collection if no entities match the provided IDs.
        /// </returns>
        Task<IEnumerable<Certification>> GetByIdsAsync(IEnumerable<int> ids); 
    }
}