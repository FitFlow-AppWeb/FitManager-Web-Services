using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Notifications.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Notifications.Interfaces.REST.Transform
{
    public static class NotificationPersonalResourceFromEntityAssembler
    {
        public static NotificationPersonalResource ToResourceFromEntity(NotificationPersonal entity)
        {
            return new NotificationPersonalResource
            {
                NotificationId = entity.NotificationId,
                PersonalId = entity.EmployeeId
            };
        }
    }
}