// Notifications/Interfaces/REST/Transform/EmployeeNotificationResourceFromEntityAssembler.cs

using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Notifications.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Notifications.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler responsible for transforming an <see cref="EmployeeNotification"/> entity
    /// into an <see cref="EmployeeNotificationResource"/>.
    /// </summary>
    public static class EmployeeNotificationResourceFromEntityAssembler
    {
        /// <summary>
        /// Assembles an <see cref="EmployeeNotificationResource"/> from an <see cref="EmployeeNotification"/> entity.
        /// </summary>
        /// <param name="entity">The entity to transform. Expects <see cref="Notification"/> and <see cref="Employee"/> to be included.</param>
        /// <returns>A new <see cref="EmployeeNotificationResource"/> instance.</returns>
        public static EmployeeNotificationResource ToResourceFromEntity(EmployeeNotification entity)
        {

            
            return new EmployeeNotificationResource(
                entity.Id,
                entity.NotificationId,
                entity.EmployeeId,
                entity.Notification.Title,
                entity.Notification.Message,
                entity.Employee.FirstName,
                entity.Employee.LastName
            );
        }
    }
}