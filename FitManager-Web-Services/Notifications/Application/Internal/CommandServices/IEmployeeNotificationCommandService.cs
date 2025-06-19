// Notifications/Application/Internal/CommandServices/IEmployeeNotificationCommandService.cs

using FitManager_Web_Services.Notifications.Domain.Model.Commands;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Notifications.Application.Internal.CommandServices
{
    /// <summary>
    /// Defines the contract for the command service responsible for processing
    /// commands related to sending notifications to employees.
    /// This service orchestrates the creation of <see cref="Domain.Model.Aggregates.Notification"/>
    /// and <see cref="Domain.Model.Aggregates.EmployeeNotification"/> entities.
    /// </summary>
    public interface IEmployeeNotificationCommandService
    {
        /// <summary>
        /// Handles the command to create a new notification and send it to specified employees.
        /// </summary>
        /// <param name="command">The <see cref="CreateEmployeeNotificationCommand"/> containing notification details and employee IDs.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Handle(CreateEmployeeNotificationCommand command);
    }
}