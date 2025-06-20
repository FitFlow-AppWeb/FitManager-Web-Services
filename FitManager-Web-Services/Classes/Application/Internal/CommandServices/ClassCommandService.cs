using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;
using FitManager_Web_Services.Classes.Domain.Services;
using System.Collections.Generic; 
using System.Threading.Tasks; 
using System; 

namespace FitManager_Web_Services.Classes.Application.Internal.CommandServices
{
    /// <summary>
    /// Represents the command service for managing classes.
    /// This service orchestrates business logic related to creating, updating, and deleting classes,
    /// and also provides query operations for retrieving class information.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling use cases.
    /// This class implements <see cref="IClassService"/> to provide a concrete implementation of class-related operations.
    /// </summary>
    public class ClassCommandService : IClassService
    {
        private readonly IClassRepository _classRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassCommandService"/> class.
        /// </summary>
        /// <param name="classRepository">The class repository for data access operations.</param>
        public ClassCommandService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        /// <summary>
        /// Creates a new class based on the provided details.
        /// </summary>
        /// <remarks>
        /// This method instantiates a new <see cref="Class"/> aggregate with the given parameters
        /// and persists it to the database using the class repository.
        /// </remarks>
        /// <param name="name">The name of the class.</param>
        /// <param name="description">A detailed description of the class.</param>
        /// <param name="type">The type or category of the class (e.g., "Yoga", "Zumba").</param>
        /// <param name="capacity">The maximum number of participants the class can hold.</param>
        /// <param name="startDate">The date when the class officially starts.</param>
        /// <param name="duration">The duration of the class in a specified unit (e.g., minutes, hours).</param>
        /// <param name="status">The current status of the class (e.g., "Active", "Cancelled").</param>
        /// <param name="employeeId">The unique identifier of the employee assigned to teach the class.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the newly created <see cref="Class"/> aggregate.</returns>
        public async Task<Class> CreateClassAsync(string name, string description, string type, int capacity, DateTime startDate, int duration, string status, int employeeId)
        {
            var newClass = new Class(name, description, type, capacity, startDate, duration, status, employeeId);
            await _classRepository.AddAsync(newClass);
            return newClass;
        }

        /// <summary>
        /// Updates an existing class record.
        /// </summary>
        /// <remarks>
        /// This method retrieves an existing <see cref="Class"/> aggregate by its ID,
        /// applies updates to its properties, and then persists these changes.
        /// Throws an <see cref="Exception"/> if the class is not found.
        /// </remarks>
        /// <param name="id">The unique identifier of the class to update.</param>
        /// <param name="name">The new name of the class.</param>
        /// <param name="description">The new description of the class.</param>
        /// <param name="type">The new type or category of the class.</param>
        /// <param name="capacity">The new maximum capacity of the class.</param>
        /// <param name="startDate">The new start date for the class.</param>
        /// <param name="duration">The new duration of the class.</param>
        /// <param name="status">The new status of the class.</param>
        /// <param name="employeeId">The new unique identifier of the employee assigned to the class.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown if the class with the specified ID is not found.</exception>
        public async Task UpdateClassAsync(int id, string name, string description, string type, int capacity, DateTime startDate, int duration, string status, int employeeId)
        {
            var existingClass = await _classRepository.GetByIdAsync(id);
            if (existingClass == null) throw new Exception("Class not found");
            
            existingClass.Name = name;
            existingClass.Description = description;
            existingClass.Type = type;
            existingClass.Capacity = capacity;
            existingClass.StartDate = startDate;
            existingClass.Duration = duration;
            existingClass.Status = status;
            existingClass.EmployeeId = employeeId;
            
            await _classRepository.UpdateAsync(existingClass);
        }

        /// <summary>
        /// Deletes an existing class record.
        /// </summary>
        /// <remarks>
        /// This method retrieves a <see cref="Class"/> aggregate by its ID and
        /// then deletes it from the database using the class repository.
        /// Throws an <see cref="Exception"/> if the class is not found.
        /// </remarks>
        /// <param name="id">The unique identifier of the class to delete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown if the class with the specified ID is not found.</exception>
        public async Task DeleteClassAsync(int id)
        {
            var existingClass = await _classRepository.GetByIdAsync(id);
            if (existingClass == null) throw new Exception("Class not found");
            
            await _classRepository.DeleteAsync(existingClass);
        }
        
        /// <summary>
        /// Retrieves all class records.
        /// </summary>
        /// <remarks>
        /// This method fetches an enumerable collection of all <see cref="Class"/> aggregates from the repository.
        /// </remarks>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains an enumerable collection of all <see cref="Class"/> aggregates.</returns>
        public async Task<IEnumerable<Class>> GetAllClassesAsync()
        {
            return await _classRepository.ListAsync();
        }

        /// <summary>
        /// Retrieves a class record by its unique identifier.
        /// </summary>
        /// <remarks>
        /// This method fetches a single <see cref="Class"/> aggregate using its ID from the repository.
        /// </remarks>
        /// <param name="id">The unique identifier of the class to retrieve.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the <see cref="Class"/> aggregate if found, otherwise null.</returns>
        public async Task<Class?> GetClassByIdAsync(int id)
        {
            return await _classRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Retrieves class records by their type.
        /// </summary>
        /// <remarks>
        /// This method fetches an enumerable collection of <see cref="Class"/> aggregates
        /// that match the specified type from the repository.
        /// </remarks>
        /// <param name="type">The type or category of classes to retrieve.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains an enumerable collection of <see cref="Class"/> aggregates matching the type.</returns>
        public async Task<IEnumerable<Class>> GetClassesByTypeAsync(string type)
        {
            return await _classRepository.FindByTypeAsync(type);
        }

        /// <summary>
        /// Retrieves the members associated with a specific class.
        /// </summary>
        /// <remarks>
        /// This method fetches an enumerable collection of <see cref="ClassMember"/> objects
        /// related to the specified class ID from the repository.
        /// This assumes <see cref="ClassMember"/> is a representation of members within a class.
        /// </remarks>
        /// <param name="classId">The unique identifier of the class to retrieve members for.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains an enumerable collection of <see cref="ClassMember"/> objects for the class.</returns>
        public async Task<IEnumerable<ClassMember>> GetClassMembersAsync(int classId)
        {
            return await _classRepository.GetMembersByClassIdAsync(classId);
        }
    }
}