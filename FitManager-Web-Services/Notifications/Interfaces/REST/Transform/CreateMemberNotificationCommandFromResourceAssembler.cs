// Notifications/Interfaces/REST/Transform/CreateMemberNotificationCommandFromResourceAssembler.cs

using FitManager_Web_Services.Notifications.Domain.Model.Commands;
using FitManager_Web_Services.Notifications.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Notifications.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler responsible for transforming a <see cref="CreateMemberNotificationResource"/>
    /// into a <see cref="CreateMemberNotificationCommand"/>.
    /// </summary>
    public static class CreateMemberNotificationCommandFromResourceAssembler
    {
        /// <summary>
        /// Assembles a <see cref="CreateMemberNotificationCommand"/> from a <see cref="CreateMemberNotificationResource"/>.
        /// </summary>
        /// <param name="resource">The resource to transform.</param>
        /// <returns>A new <see cref="CreateMemberNotificationCommand"/> instance.</returns>
        public static CreateMemberNotificationCommand ToCommandFromResource(CreateMemberNotificationResource resource)
        {
            return new CreateMemberNotificationCommand(
                resource.Title,
                resource.Message,
                resource.MemberIds
            );
        }
    }
}