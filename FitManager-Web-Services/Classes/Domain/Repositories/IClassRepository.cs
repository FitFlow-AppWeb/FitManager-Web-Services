using FitManager_Web_Services.Classes.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Classes.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages <see cref="Class"/> aggregates.
    /// </summary>
    /// <remarks>
    /// This interface provides methods for common data access operations related to fitness classes.
    /// It includes specific queries to find classes by type, and to retrieve associated
    /// members and bookings, ensuring the domain layer can interact with persistence in an abstract manner.
    /// It resides in the Domain layer, acting as an abstraction over data persistence.
    /// </remarks>
    public interface IClassRepository
    {
        /// <summary>
        /// Asynchronously retrieves a single class by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the class.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="Class"/> if found, otherwise null.
        /// </returns>
        Task<Class?> GetByIdAsync(int id);

        /// <summary>
        /// Asynchronously retrieves all classes from the repository.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an <see cref="IEnumerable{T}"/> of all <see cref="Class"/> objects.
        /// </returns>
        Task<IEnumerable<Class>> ListAsync();

        /// <summary>
        /// Asynchronously retrieves a collection of classes filtered by their type.
        /// </summary>
        /// <param name="type">The type of the classes to retrieve (e.g., "Yoga", "Cardio").</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="Class"/> objects
        /// matching the specified <paramref name="type"/>.
        /// </returns>
        Task<IEnumerable<Class>> FindByTypeAsync(string type);

        /// <summary>
        /// Asynchronously adds a new class to the repository.
        /// </summary>
        /// <param name="fitnessClass">The <see cref="Class"/> entity to add.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task AddAsync(Class fitnessClass);

        /// <summary>
        /// Asynchronously updates an existing class in the repository.
        /// </summary>
        /// <param name="fitnessClass">The <see cref="Class"/> entity with updated information.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task UpdateAsync(Class fitnessClass);

        /// <summary>
        /// Asynchronously deletes a class from the repository.
        /// </summary>
        /// <param name="fitnessClass">The <see cref="Class"/> entity to delete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task DeleteAsync(Class fitnessClass);
        
        /// <summary>
        /// Asynchronously retrieves the <see cref="ClassMember"/> associations for a specific class.
        /// </summary>
        /// <param name="classId">The unique identifier of the class.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="ClassMember"/> objects
        /// associated with the specified class.
        /// </returns>
        Task<IEnumerable<ClassMember>> GetMembersByClassIdAsync(int classId);

        /// <summary>
        /// Asynchronously retrieves the <see cref="Booking"/> records for a specific class.
        /// </summary>
        /// <param name="classId">The unique identifier of the class.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="Booking"/> objects
        /// associated with the specified class.
        /// </returns>
        Task<IEnumerable<Booking>> GetBookingsByClassIdAsync(int classId);
    }
}