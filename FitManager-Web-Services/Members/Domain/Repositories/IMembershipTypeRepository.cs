// Members/Domain/Repositories/IMembershipTypeRepository.cs

using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories; 
namespace FitManager_Web_Services.Members.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for repository operations specific to the MembershipType aggregate root.
    /// Inherits common CRUD operations from <see cref="IBaseRepository{TEntity}"/>.
    /// </summary>
    public interface IMembershipTypeRepository : IBaseRepository<MembershipType>
    {
        // No necesitas agregar métodos específicos aquí a menos que tengas necesidades adicionales
        // que no estén cubiertas por IBaseRepository.
    }
}