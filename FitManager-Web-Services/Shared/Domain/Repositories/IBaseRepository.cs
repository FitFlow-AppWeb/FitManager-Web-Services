// Shared/Domain/Repositories/IBaseRepository.cs


namespace FitManager_Web_Services.Shared.Domain.Repositories;

/// <summary>
/// Defines the base contract for a generic repository.
/// This interface provides common asynchronous CRUD (Create, Read, Update, Delete) operations
/// for any aggregate root or entity type. It resides in the Shared Domain layer,
/// making it accessible across different bounded contexts.
/// </summary>
/// <typeparam name="T">The type of the aggregate root or entity that this repository manages.
/// Must be a reference type.</typeparam>
public interface IBaseRepository<T> where T : class
{
    /// <summary>
    /// Asynchronously retrieves all entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains an enumerable collection of <typeparamref name="T"/> entities.</returns>
    Task<IEnumerable<T>> GetAllAsync();
    
    /// <summary>
    /// Asynchronously retrieves an entity of type <typeparamref name="T"/> by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains the <typeparamref name="T"/> entity if found, otherwise null.
    /// </returns>
    Task<T?> GetByIdAsync(int id); 
    
    /// <summary>
    /// Asynchronously adds a new entity of type <typeparamref name="T"/> to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task AddAsync(T entity);
    
    /// <summary>
    /// Asynchronously updates an existing entity of type <typeparamref name="T"/> in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task UpdateAsync(T entity); 
    
    /// <summary>
    /// Asynchronously deletes an entity of type <typeparamref name="T"/> by its unique identifier from the repository.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task DeleteAsync(int id); 
}