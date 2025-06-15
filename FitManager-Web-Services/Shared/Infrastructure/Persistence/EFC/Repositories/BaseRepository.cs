// Shared/Infrastructure/Persistence/EFC/Repositories/BaseRepository.cs

using Microsoft.EntityFrameworkCore; // Required for DbContext, DbSet, ToListAsync, FindAsync
using FitManager_Web_Services.Shared.Domain.Repositories; // Required for IBaseRepository
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration; // Required for AppDbContext
using System.Collections.Generic; // Required for IEnumerable
using System.Threading.Tasks; // Required for Task

namespace FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Provides a generic base implementation for repositories using Entity Framework Core.
/// This class implements <see cref="IBaseRepository{T}"/> and offers common CRUD (Create, Read, Update, Delete) operations.
/// It resides in the Infrastructure layer, encapsulating the persistence details.
/// </summary>
/// <typeparam name="T">The type of the entity that this repository manages. Must be a class.</typeparam>
public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    /// <summary>
    /// The application's database context.
    /// </summary>
    protected readonly AppDbContext _context;
    
    /// <summary>
    /// The DbSet representing the collection of entities in the database.
    /// </summary>
    protected readonly DbSet<T> _entities;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
    /// </summary>
    /// <param name="context">The application's database context.</param>
    public BaseRepository(AppDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    /// <summary>
    /// Asynchronously retrieves all entities of type <typeparamref name="T"/>.
    /// This method can be overridden by derived repositories to include eager loading
    /// of related entities if needed.
    /// </summary>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains an enumerable collection of <typeparamref name="T"/> entities.</returns>
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entities.ToListAsync();
    }

    /// <summary>
    /// Asynchronously retrieves an entity of type <typeparamref name="T"/> by its unique identifier.
    /// This method can be overridden by derived repositories to include eager loading
    /// of related entities if needed.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains the <typeparamref name="T"/> entity if found, otherwise null.
    /// </returns>
    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _entities.FindAsync(id);
    }

    /// <summary>
    /// Asynchronously adds a new entity of type <typeparamref name="T"/> to the database context.
    /// The changes are not persisted until <see cref="IUnitOfWork.CompleteAsync"/> is called.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
    }

    /// <summary>
    /// Updates an existing entity of type <typeparamref name="T"/> in the database context.
    /// The changes are not persisted until <see cref="IUnitOfWork.CompleteAsync"/> is called.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task UpdateAsync(T entity)
    {
        _entities.Update(entity);
        return Task.CompletedTask; 
    }

    /// <summary>
    /// Asynchronously deletes an entity of type <typeparamref name="T"/> by its unique identifier from the database context.
    /// The changes are not persisted until <see cref="IUnitOfWork.CompleteAsync"/> is called.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public async Task DeleteAsync(int id)
    {
        var entityToDelete = await _entities.FindAsync(id);
        if (entityToDelete != null)
        {
            _entities.Remove(entityToDelete);
        }
   
    }
}