using FitManager_Web_Services.Classes.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Classes.Domain.Services
{
    /// <summary>
    /// Defines the contract for the domain service responsible for managing <see cref="Class"/> aggregates.
    /// </summary>
    /// <remarks>
    /// This interface provides a facade for class-related business operations,
    /// including creating, updating, deleting, and various retrieval methods for fitness classes.
    /// It also includes methods to retrieve related data such as class members.
    /// </remarks>
    public interface IClassService
    {
        /// <summary>
        /// Asynchronously creates a new fitness class.
        /// </summary>
        /// <param name="name">The name of the class (e.g., "Morning Yoga", "HIIT").</param>
        /// <param name="description">A detailed description of the class.</param>
        /// <param name="type">The type or category of the class (e.g., "Yoga", "Cardio").</param>
        /// <param name="capacity">The maximum capacity of attendees for the class.</param>
        /// <param name="startDate">The scheduled start date and time of the class.</param>
        /// <param name="duration">The duration of the class in minutes.</param>
        /// <param name="status">The initial status of the class (e.g., "Scheduled", "Active").</param>
        /// <param name="employeeId">The unique identifier of the employee (trainer/instructor) assigned to this class.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the newly created <see cref="Class"/> object.
        /// </returns>
        Task<Class> CreateClassAsync(string name, string description, string type, int capacity, DateTime startDate, int duration, string status, int employeeId);

        /// <summary>
        /// Asynchronously updates an existing fitness class's information.
        /// </summary>
        /// <param name="id">The unique identifier of the class to update.</param>
        /// <param name="name">The updated name of the class.</param>
        /// <param name="description">The updated description of the class.</param>
        /// <param name="type">The updated type or category of the class.</param>
        /// <param name="capacity">The updated maximum capacity of attendees for the class.</param>
        /// <param name="startDate">The updated scheduled start date and time of the class.</param>
        /// <param name="duration">The updated duration of the class in minutes.</param>
        /// <param name="status">The updated status of the class.</param>
        /// <param name="employeeId">The updated unique identifier of the employee (trainer/instructor) assigned to this class.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task UpdateClassAsync(int id, string name, string description, string type, int capacity, DateTime startDate, int duration, string status, int employeeId);

        /// <summary>
        /// Asynchronously deletes a class by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the class to delete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task DeleteClassAsync(int id);

        /// <summary>
        /// Asynchronously retrieves a single class by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the class.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="Class"/> if found, otherwise <c>null</c>.
        /// </returns>
        Task<Class?> GetClassByIdAsync(int id);

        /// <summary>
        /// Asynchronously retrieves all fitness classes.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of all <see cref="Class"/> objects.
        /// </returns>
        Task<IEnumerable<Class>> GetAllClassesAsync();

        /// <summary>
        /// Asynchronously retrieves a collection of classes filtered by their type.
        /// </summary>
        /// <param name="type">The type of the classes to retrieve (e.g., "Yoga", "Cardio").</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Class"/> objects
        /// matching the specified <paramref name="type"/>.
        /// </returns>
        Task<IEnumerable<Class>> GetClassesByTypeAsync(string type);

        /// <summary>
        /// Asynchronously retrieves the <see cref="ClassMember"/> associations for a specific class.
        /// </summary>
        /// <param name="classId">The unique identifier of the class.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="ClassMember"/> objects
        /// associated with the specified class.
        /// </returns>
        Task<IEnumerable<ClassMember>> GetClassMembersAsync(int classId);
    }
}