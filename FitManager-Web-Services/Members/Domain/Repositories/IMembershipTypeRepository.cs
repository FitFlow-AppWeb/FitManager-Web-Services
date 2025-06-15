// Members/Domain/Repositories/IMembershipTypeRepository.cs

using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories; // Required for IBaseRepository

namespace FitManager_Web_Services.Members.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages <see cref="MembershipType"/> aggregates.
    /// This interface extends <see cref="IBaseRepository{TEntity}"/> to provide common CRUD operations.
    /// It resides in the Domain layer, acting as an abstraction over data persistence.
    /// No specific methods beyond the base repository are currently defined here, as all necessary
    /// operations for MembershipType are handled by the generic base implementation.
    /// </summary>
    public interface IMembershipTypeRepository : IBaseRepository<MembershipType>
    {
       
    }
}