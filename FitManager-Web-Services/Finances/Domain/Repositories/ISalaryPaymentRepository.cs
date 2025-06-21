using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Finances.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages <see cref="SalaryPayment"/> aggregates.
    /// </summary>
    /// <remarks>
    /// This interface extends <see cref="IBaseRepository{TEntity}"/> to provide common CRUD operations
    /// for <see cref="SalaryPayment"/> entities. It also includes specific methods to query
    /// salary payments based on financial periods, such as by month and year.
    /// It resides in the Domain layer, acting as an abstraction over data persistence.
    /// </remarks>
    public interface ISalaryPaymentRepository : IBaseRepository<SalaryPayment>
    {
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
        Task<IEnumerable<SalaryPayment>> GetByMonthYearAsync(int month, int year);
    }
}