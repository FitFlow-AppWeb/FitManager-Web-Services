using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace FitManager_Web_Services.Employees.Infrastructure.Repositories
{
    /// <summary>
    /// Represents a concrete implementation of <see cref="IEmployeeRepository"/>
    /// using Entity Framework Core for data access.
    /// </summary>
    /// <remarks>
    /// This repository extends <see cref="BaseRepository{TEntity}"/> to inherit common CRUD operations
    /// and implements the specific queries defined in <see cref="IEmployeeRepository"/>
    /// for <see cref="Employee"/> aggregates. It ensures that related <see cref="Certification"/>
    /// and <see cref="Specialty"/> entities are eagerly loaded when retrieving employees.
    /// </remarks>
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRepository"/> class.
        /// </summary>
        /// <param name="context">The application's database context (<see cref="AppDbContext"/>).</param>
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Asynchronously retrieves an employee by their DNI (National Identity Document),
        /// including their associated certifications and specialties.
        /// </summary>
        /// <param name="dni">The DNI of the employee to retrieve.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="Employee"/> if found (with related data included),
        /// otherwise <c>null</c>.
        /// </returns>
        public async Task<Employee?> GetByDniAsync(int dni)
        {
            return await _context.Employees
                .Include(e => e.Certifications)
                .Include(e => e.Specialties)  
                .FirstOrDefaultAsync(e => e.Dni == dni);
        }

        /// <summary>
        /// Asynchronously retrieves all employee records, including their associated certifications and specialties.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an <see cref="IEnumerable{T}"/> of all <see cref="Employee"/> objects,
        /// with their <see cref="Employee.Certifications"/> and <see cref="Employee.Specialties"/> included.
        /// </returns>
        public override async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Certifications)
                .Include(e => e.Specialties)  
                .ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a single employee by their unique identifier,
        /// including their associated certifications and specialties.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="Employee"/> if found (with related data included),
        /// otherwise <c>null</c>.
        /// </returns>
        public override async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Certifications)
                .Include(e => e.Specialties)    
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Asynchronously retrieves all employees, ensuring their certifications and specialties are loaded.
        /// This method is functionally identical to <see cref="GetAllAsync()"/> with eager loading.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains an <see cref="IEnumerable{T}"/> of all <see cref="Employee"/> objects,
        /// with their <see cref="Employee.Certifications"/> and <see cref="Employee.Specialties"/> included.
        /// </returns>
        public async Task<IEnumerable<Employee>> GetAllWithCertificationsAndSpecialtiesAsync()
        {
            return await _context.Employees
                .Include(e => e.Certifications)
                .Include(e => e.Specialties)
                .ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a single employee by their unique identifier,
        /// ensuring their certifications and specialties are loaded.
        /// This method is functionally identical to <see cref="GetByIdAsync(int)"/> with eager loading.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="Employee"/> if found (with related data included),
        /// otherwise <c>null</c>.
        /// </returns>
        public async Task<Employee?> GetByIdWithCertificationsAndSpecialtiesAsync(int employeeId)
        {
            return await _context.Employees
                .Where(e => e.Id == employeeId)
                .Include(e => e.Certifications)
                .Include(e => e.Specialties)
                .FirstOrDefaultAsync();
        }
    }
}