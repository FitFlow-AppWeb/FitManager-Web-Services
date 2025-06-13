using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;

namespace FitManager_Web_Services.Finances.Domain.Repositories;

public interface IMembershipPaymentRepository : IBaseRepository<MembershipPayment>
{
    Task<IEnumerable<MembershipPayment>> GetByMonthYearAsync(int month, int year);
}