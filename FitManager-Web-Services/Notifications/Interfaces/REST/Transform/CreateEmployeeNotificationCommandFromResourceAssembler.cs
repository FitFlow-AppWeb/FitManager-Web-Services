// Notifications/Interfaces/REST/Transform/CreateEmployeeNotificationCommandFromResourceAssembler.cs

using FitManager_Web_Services.Notifications.Domain.Model.Commands;
using FitManager_Web_Services.Notifications.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Notifications.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler responsible for transforming a <see cref="CreateEmployeeNotificationResource"/>
    /// into a <see cref="CreateEmployeeNotificationCommand"/>.
    /// </summary>
    public static class CreateEmployeeNotificationCommandFromResourceAssembler
    {
        /// <summary>
        /// Assembles a <see cref="CreateEmployeeNotificationCommand"/> from a <see cref="CreateEmployeeNotificationResource"/>.
        /// </summary>
        /// <param name="resource">The resource to transform.</param>
        /// <returns>A new <see cref="CreateEmployeeNotificationCommand"/> instance.</returns>
        public static CreateEmployeeNotificationCommand ToCommandFromResource(CreateEmployeeNotificationResource resource)
        {
            return new CreateEmployeeNotificationCommand(
                resource.Title,
                resource.Message,
                resource.EmployeeIds
            );
        }
    }
}