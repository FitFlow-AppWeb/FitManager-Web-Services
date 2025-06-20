using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Classes.Application.Internal.QueryServices
{
    /// <summary>
    /// Represents the query service for retrieving class information.
    /// This service handles queries related to classes and retrieves data from the repository.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling read-only use cases.
    /// </summary>
    public class ClassQueryService
    {
        private readonly IClassRepository _classRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassQueryService"/> class.
        /// </summary>
        /// <param name="classRepository">The class repository for data access operations.</param>
        public ClassQueryService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        /// <summary>
        /// Retrieves all class records.
        /// </summary>
        /// <remarks>
        /// This method delegates the retrieval of all <see cref="Class"/> aggregates to the <see cref="IClassRepository"/>.
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
        /// This method delegates the retrieval of a single <see cref="Class"/> aggregate by its ID to the <see cref="IClassRepository"/>.
        /// </remarks>
        /// <param name="id">The unique identifier of the class to retrieve.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the <see cref="Class"/> aggregate if found, otherwise null.</returns>
        public async Task<Class> GetClassByIdAsync(int id)
        {
            return await _classRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Retrieves class records by their type.
        /// </summary>
        /// <remarks>
        /// This method delegates the retrieval of <see cref="Class"/> aggregates filtered by type to the <see cref="IClassRepository"/>.
        /// </remarks>
        /// <param name="type">The type or category of classes to retrieve.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains an enumerable collection of <see cref="Class"/> aggregates matching the specified type.</returns>
        public async Task<IEnumerable<Class>> GetClassesByTypeAsync(string type)
        {
            return await _classRepository.FindByTypeAsync(type);
        }
    }
}