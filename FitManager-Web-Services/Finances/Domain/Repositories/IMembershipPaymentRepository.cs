using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Finances.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages <see cref="MembershipPayment"/> aggregates.
    /// </summary>
    /// <remarks>
    /// This interface extends <see cref="IBaseRepository{TEntity}"/> to provide common CRUD (Create, Read, Update, Delete)
    /// operations for <see cref="MembershipPayment"/> entities. It also includes specific methods to query
    /// membership payments based on financial criteria, such as by month and year.
    /// It resides in the Domain layer, acting as an abstraction over data persistence.
    /// </remarks>
    public interface IMembershipPaymentRepository : IBaseRepository<MembershipPayment>
    {
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
        Task<IEnumerable<MembershipPayment>> GetByMonthYearAsync(int month, int year);
    }
}