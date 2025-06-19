using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Notifications.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Notifications.Infrastructure.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            return await _context.Notifications
                .Include(n => n.NotificationMembers)
                .Include(n => n.NotificationPersonals)
                .ToListAsync();
        }

        public async Task<Notification?> GetNotificationByIdAsync(int id)
        {
            return await _context.Notifications
                .Include(n => n.NotificationMembers)
                .Include(n => n.NotificationPersonals)
                .FirstOrDefaultAsync(n => n.Id == id);
        }
    }
}