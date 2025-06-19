using FitManager_Web_Services.Notifications.Domain.Model.Commands;
using FitManager_Web_Services.Notifications.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Notifications.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler responsible for transforming a <see cref="CreateNotificationResource"/> into a <see cref="CreateNotificationCommand"/>.
    /// </summary>
    public static class CreateNotificationCommandFromResourceAssembler
    {
        /// <summary>
        /// Converts a <see cref="CreateNotificationResource"/> to a <see cref="CreateNotificationCommand"/>.
        /// </summary>
        /// <param name="resource">The <see cref="CreateNotificationResource"/> to convert.</param>
        /// <returns>A new instance of <see cref="CreateNotificationCommand"/> populated with data from the resource.</returns>
        public static CreateNotificationCommand ToCommandFromResource(CreateNotificationResource resource)
        {
            return new CreateNotificationCommand(
                resource.CreatedAt,
                resource.Title,
                resource.Message,
                resource.MemberIds,
                resource.EmployeeIds
            );
        }
    }
}