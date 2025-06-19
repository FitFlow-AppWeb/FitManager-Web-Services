using FitManager_Web_Services.Notifications.Domain.Model.Aggregates;
using FitManager_Web_Services.Notifications.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Notifications.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler responsible for transforming <see cref="Notification"/> domain entities into <see cref="NotificationResource"/> DTOs.
    /// </summary>
    public static class NotificationResourceFromEntityAssembler
    {
        /// <summary>
        /// Converts a single <see cref="Notification"/> entity to a <see cref="NotificationResource"/>.
        /// </summary>
        /// <param name="entity">The <see cref="Notification"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="NotificationResource"/> populated with data from the entity.</returns>
        public static NotificationResource ToResourceFromEntity(Notification entity)
        {
            var notificationMembers = entity.NotificationMembers.Select(nm => NotificationMemberResourceFromEntityAssembler.ToResourceFromEntity(nm));
            var notificationPersonals = entity.NotificationPersonals.Select(np => NotificationPersonalResourceFromEntityAssembler.ToResourceFromEntity(np));

            return new NotificationResource(
                entity.Id,
                entity.CreatedAt,
                entity.Title,
                entity.Message,
                notificationMembers,
                notificationPersonals
            );
        }

        /// <summary>
        /// Converts a collection of <see cref="Notification"/> entities to an enumerable collection of <see cref="NotificationResource"/>.
        /// </summary>
        /// <param name="entities">The enumerable collection of <see cref="Notification"/> entities to convert.</param>
        /// <returns>An enumerable collection of <see cref="NotificationResource"/> objects.</returns>
        public static IEnumerable<NotificationResource> ToResourceListFromEntityList(IEnumerable<Notification> entities)
        {
            return entities.Select(ToResourceFromEntity);
        }
    }
}
