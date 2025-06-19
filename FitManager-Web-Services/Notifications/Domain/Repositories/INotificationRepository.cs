using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Shared.Domain.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FitManager_Web_Services.Notifications.Domain.Repositories
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetAllNotificationsAsync();
        Task<Notification?> GetNotificationByIdAsync(int id);
    }
}