// Shared/Domain/Repositories/IUnitOfWork.cs



namespace FitManager_Web_Services.Shared.Domain.Repositories;

/// <summary>
/// Defines the contract for the Unit of Work pattern.
/// The Unit of Work ensures that all operations (e.g., adds, updates, deletes)
/// within a single business transaction are treated as one atomic operation.
/// It provides a mechanism to commit or rollback all changes together,
/// ensuring data consistency. This interface resides in the Shared Domain layer.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Asynchronously commits all pending changes to the underlying data store.
    /// This method signifies the successful completion of a business transaction,
    /// persisting all modifications made within the unit of work.
    /// </summary>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task CompleteAsync();
}