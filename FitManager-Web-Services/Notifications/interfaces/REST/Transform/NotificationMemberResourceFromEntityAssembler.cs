using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Notifications.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Notifications.Interfaces.REST.Transform
{
    public static class NotificationMemberResourceFromEntityAssembler
    {
        public static NotificationMemberResource ToResourceFromEntity(NotificationMember entity)
        {
            return new NotificationMemberResource
            {
                NotificationId = entity.NotificationId,
                MemberId = entity.MemberId
            };
        }
    }
}