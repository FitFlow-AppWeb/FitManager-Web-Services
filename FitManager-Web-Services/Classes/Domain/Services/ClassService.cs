using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;

namespace FitManager_Web_Services.Classes.Domain.Services
{
    /// <summary>
    /// Implements the <see cref="IClassService"/> contract, providing concrete business logic
    /// for managing <see cref="Class"/> aggregates.
    /// </summary>
    /// <remarks>
    /// This service acts as a central point for class-related operations, handling the creation,
    /// updating, deletion, and various retrievals of class records. It interacts with <see cref="IClassRepository"/>
    /// for data persistence, encapsulating domain rules and orchestrating data access.
    /// </remarks>
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassService"/> class.
        /// </summary>
        /// <param name="classRepository">The repository for <see cref="Class"/> aggregates.</param>
        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        /// <summary>
        /// Asynchronously creates a new fitness class and adds it to the repository.
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
        public async Task<Class> CreateClassAsync(string name, string description, string type, int capacity, DateTime startDate, int duration, string status, int employeeId)
        {
            var fitnessClass = new Class(name, description, type, capacity, startDate, duration, status, employeeId);
            await _classRepository.AddAsync(fitnessClass);
            return fitnessClass;
        }

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
        /// <exception cref="Exception">Thrown if the class with the specified ID is not found.</exception>
        public async Task UpdateClassAsync(int id, string name, string description, string type, int capacity, DateTime startDate, int duration, string status, int employeeId)
        {
            var existingClass = await _classRepository.GetByIdAsync(id);
            if (existingClass == null)
            {
                throw new Exception("Class not found");
            }
            
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
        /// Asynchronously deletes a class from the repository by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the class to delete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown if the class with the specified ID is not found.</exception>
        public async Task DeleteClassAsync(int id)
        {
            var existingClass = await _classRepository.GetByIdAsync(id);
            if (existingClass == null)
            {
                throw new Exception("Class not found");
            }
            
            await _classRepository.DeleteAsync(existingClass);
        }

        /// <summary>
        /// Asynchronously retrieves a single class by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the class.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="Class"/> if found, otherwise <c>null</c>.
        /// </returns>
        public async Task<Class?> GetClassByIdAsync(int id) => await _classRepository.GetByIdAsync(id);

        /// <summary>
        /// Asynchronously retrieves all fitness classes.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of all <see cref="Class"/> objects.
        /// </returns>
        public async Task<IEnumerable<Class>> GetAllClassesAsync() => await _classRepository.ListAsync();

        /// <summary>
        /// Asynchronously retrieves a collection of classes filtered by their type.
        /// </summary>
        /// <param name="type">The type of the classes to retrieve (e.g., "Yoga", "Cardio").</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Class"/> objects
        /// matching the specified <paramref name="type"/>.
        /// </returns>
        public async Task<IEnumerable<Class>> GetClassesByTypeAsync(string type) => await _classRepository.FindByTypeAsync(type);

        /// <summary>
        /// Asynchronously retrieves the <see cref="ClassMember"/> associations for a specific class.
        /// </summary>
        /// <param name="classId">The unique identifier of the class.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="ClassMember"/> objects
        /// associated with the specified class.
        /// </returns>
        public async Task<IEnumerable<ClassMember>> GetClassMembersAsync(int classId) => await _classRepository.GetMembersByClassIdAsync(classId);
    }
}