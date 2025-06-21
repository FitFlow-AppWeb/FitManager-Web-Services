using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace FitManager_Web_Services.Finances.Infrastructure.Repositories;

/// <summary>
/// Represents a concrete implementation of <see cref="IMembershipPaymentRepository"/>
/// using Entity Framework Core for data access.
/// </summary>
/// <remarks>
/// This repository extends <see cref="BaseRepository{TEntity}"/> to inherit common CRUD operations
/// and implements the specific queries defined in <see cref="IMembershipPaymentRepository"/>
/// for <see cref="MembershipPayment"/> entities. It interacts directly with the database context.
/// </remarks>
public class MembershipPaymentRepository : BaseRepository<MembershipPayment>, IMembershipPaymentRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MembershipPaymentRepository"/> class.
    /// </summary>
    /// <param name="context">The application's database context (<see cref="AppDbContext"/>).</param>
    public MembershipPaymentRepository(AppDbContext context) : base(context) { }
    
    /// <summary>
    /// Asynchronously retrieves a collection of membership payments for a specific month and year.
    /// </summary>
    /// <param name="month">The month (1-12) for which to retrieve payments.</param>
    /// <param name="year">The year for which to retrieve payments.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The task result contains an <see cref="IEnumerable{T}"/> of <see cref="MembershipPayment"/> objects
    /// matching the specified month and year.
    /// </returns>
    public async Task<IEnumerable<MembershipPayment>> GetByMonthYearAsync(int month, int year)
    {
        return await _context.MembershipPayments
            .Where(p => p.Date.Month == month && p.Date.Year == year)
            .ToListAsync();
    }
}