// Members/Domain/Repositories/IMemberRepository.cs

using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories; // Required for IBaseRepository

namespace FitManager_Web_Services.Members.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages <see cref="Member"/> aggregates.
    /// This interface extends <see cref="IBaseRepository{TEntity}"/> to provide common CRUD operations,
    /// and adds specific methods relevant to the Member aggregate.
    /// It resides in the Domain layer, acting as an abstraction over data persistence.
    /// </summary>
    public interface IMemberRepository : IBaseRepository<Member>
    {
        /// <summary>
        /// Asynchronously retrieves a <see cref="Member"/> by their DNI (National Identity Document).
        /// </summary>
        /// <param name="dni">The DNI of the member to retrieve.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="Member"/> if found, otherwise null.
        /// </returns>
        Task<Member?> GetByDniAsync(int dni);
    }
}