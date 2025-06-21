using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace FitManager_Web_Services.Finances.Infrastructure.Repositories;

/// <summary>
/// Represents a concrete implementation of <see cref="ISalaryPaymentRepository"/>
/// using Entity Framework Core for data access.
/// </summary>
/// <remarks>
/// This repository extends <see cref="BaseRepository{TEntity}"/> to inherit common CRUD operations
/// and implements the specific queries defined in <see cref="ISalaryPaymentRepository"/>
/// for <see cref="SalaryPayment"/> entities. It interacts directly with the database context.
/// </remarks>
public class SalaryPaymentRepository : BaseRepository<SalaryPayment>, ISalaryPaymentRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SalaryPaymentRepository"/> class.
    /// </summary>
    /// <param name="context">The application's database context (<see cref="AppDbContext"/>).</param>
    public SalaryPaymentRepository(AppDbContext context) : base(context) { }
    
    /// <summary>
    /// Asynchronously retrieves a collection of salary payments for a specific month and year.
    /// </summary>
    /// <param name="month">The month (1-12) for which to retrieve payments.</param>
    /// <param name="year">The year for which to retrieve payments.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="SalaryPayment"/> objects
    /// matching the specified month and year.
    /// </returns>
    public async Task<IEnumerable<SalaryPayment>> GetByMonthYearAsync(int month, int year)
    {
        return await _context.SalaryPayments
            .Where(p => p.Date.Month == month && p.Date.Year == year)
            .ToListAsync();
    }

    /// <summary>
    /// Asynchronously retrieves a collection of salary payments made to a specific employee.
    /// </summary>
    /// <param name="employeeId">The unique identifier of the employee whose salary payments are to be retrieved.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="SalaryPayment"/> objects
    /// associated with the given <paramref name="employeeId"/>.
    /// </returns>
    public async Task<IEnumerable<SalaryPayment>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _context.SalaryPayments
            .Where(sp => sp.EmployeeId == employeeId)
            .ToListAsync();
    }
}