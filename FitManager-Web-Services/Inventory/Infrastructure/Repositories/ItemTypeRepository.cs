using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration; // For AppDbContext
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories; // For BaseRepository

namespace FitManager_Web_Services.Inventory.Infrastructure.Repositories
{
    /// <summary>
    /// Implements the <see cref="IItemTypeRepository"/> interface, providing concrete data access operations for <see cref="ItemType"/> aggregates.
    /// This repository leverages Entity Framework Core and extends <see cref="BaseRepository{TEntity}"/> for common CRUD functionalities.
    /// It resides in the Infrastructure layer, handling the details of data persistence.
    /// </summary>
    public class ItemTypeRepository : BaseRepository<ItemType>, IItemTypeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTypeRepository"/> class.
        /// </summary>
        /// <param name="context">The application's database context (AppDbContext).</param>
        public ItemTypeRepository(AppDbContext context) : base(context)
        {
            // The base constructor (BaseRepository) handles setting up the DbContext
            // and often provides DbSet access. Any ItemType-specific initialization
            // can go here, but usually, it's not needed for a simple repository.
        }
    }
}