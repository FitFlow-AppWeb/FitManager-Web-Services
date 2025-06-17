using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Inventory.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages <see cref="ItemType"/> aggregates.
    /// This interface extends <see cref="IBaseRepository{TEntity}"/> to provide common CRUD operations.
    /// Specific queries for item types can be added here if they are not covered by the base repository.
    /// </summary>
    public interface IItemTypeRepository : IBaseRepository<ItemType>
    {

    }
}