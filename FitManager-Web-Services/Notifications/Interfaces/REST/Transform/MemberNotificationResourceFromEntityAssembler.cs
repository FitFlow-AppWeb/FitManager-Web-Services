// Notifications/Interfaces/REST/Transform/MemberNotificationResourceFromEntityAssembler.cs

using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Notifications.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Notifications.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler responsible for transforming a <see cref="MemberNotification"/> entity
    /// into a <see cref="MemberNotificationResource"/>.
    /// </summary>
    public static class MemberNotificationResourceFromEntityAssembler
    {
        /// <summary>
        /// Assembles a <see cref="MemberNotificationResource"/> from a <see cref="MemberNotification"/> entity.
        /// </summary>
        /// <param name="entity">The entity to transform. Expects <see cref="Notification"/> and <see cref="Member"/> to be included.</param>
        /// <returns>A new <see cref="MemberNotificationResource"/> instance.</returns>
        public static MemberNotificationResource ToResourceFromEntity(MemberNotification entity)
        {

            return new MemberNotificationResource(
                entity.Id,
                entity.NotificationId,
                entity.MemberId,
                entity.Notification.Title,
                entity.Notification.Message,
                entity.Member.FirstName,
                entity.Member.LastName
            );
        }
    }
}