using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Inventory.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages <see cref="ItemBooking"/> aggregates.
    /// This interface extends <see cref="IBaseRepository{TEntity}"/> to provide standard CRUD operations,
    /// and can be expanded to include booking-specific queries in the future.
    /// </summary>
    public interface IItemBookingRepository : IBaseRepository<ItemBooking>
    {
    }
}