using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace FitManager_Web_Services.Inventory.Infrastructure.Repositories
{
    /// <summary>
    /// Implements the <see cref="IItemRepository"/> interface, providing concrete data access operations for <see cref="Item"/> aggregates.
    /// This repository interacts with the database using Entity Framework Core and resides in the Infrastructure layer,
    /// encapsulating data persistence details.
    /// </summary>
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemRepository"/> class.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously retrieves all items from the database.
        /// </summary>
        /// <remarks>
        /// This method eagerly loads the associated <see cref="Item"/> and <see cref="Employee"/>
        /// entities to ensure complete item details are available.
        /// </remarks>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Item"/> objects.
        /// </returns>
        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _context.Items
                .Include(it => it.ItemType)
                //.Include(i => i.Employee)
                .ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves an <see cref="Item"/> by its unique identifier.
        /// </summary>
        /// <remarks>
        /// Includes the related <see cref="Item"/> and <see cref="Employee"/> data.
        /// </remarks>
        /// <param name="id">The unique identifier of the item.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// The task result contains the <see cref="Item"/> if found; otherwise, null.
        /// </returns>
        public async Task<Item?> GetByIdAsync(int id)
        {
            return await _context.Items
                .Include(it => it.ItemType)
                //.Include(i => i.Employee)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        /// <summary>
        /// Asynchronously adds a new item to the database.
        /// </summary>
        /// <param name="item">The <see cref="Item"/> to add.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddAsync(Item item)
        {
            await _context.Items.AddAsync(item);
        }

        /// <summary>
        /// Updates an existing item in the database.
        /// </summary>
        /// <param name="item">The <see cref="Item"/> to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task UpdateAsync(Item item)
        {
            _context.Items.Update(item);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Asynchronously deletes an item by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }
        }
    }
}