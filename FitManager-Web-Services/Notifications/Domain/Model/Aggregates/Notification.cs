// Notifications/Domain/Model/Aggregates/Notification.cs

using System;
using System.Collections.Generic; // Required for ICollection

namespace FitManager_Web_Services.Notifications.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents a generic notification aggregate root in the domain model.
    /// This entity encapsulates the core details of a notification, such as
    /// its content and creation timestamp, and can be related to specific
    /// recipients via intermediate entities like <see cref="EmployeeNotification"/>
    /// and <see cref="MemberNotification"/>.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Gets the unique identifier for the notification.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the timestamp when the notification was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the title of the notification.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the main message content of the notification.
        /// </summary>
        public string Message { get; set; }


        /// <summary>
        /// Gets the collection of <see cref="MemberNotification"/> entities associated with this notification.
        /// Represents the many-to-many relationship with members.
        /// </summary>
        public ICollection<MemberNotification> MemberNotifications { get; set; } 

        /// <summary>
        /// Gets the collection of <see cref="EmployeeNotification"/> entities associated with this notification.
        /// Represents the many-to-many relationship with employees.
        /// </summary>
        public ICollection<EmployeeNotification> EmployeeNotifications { get; set; } 


        /// <summary>
        /// Initializes a new instance of the <see cref="Notification"/> class with specified details.
        /// </summary>
        /// <param name="createdAt">The date and time the notification was created.</param>
        /// <param name="title">The title of the notification.</param>
        /// <param name="message">The main message content of the notification.</param>
        public Notification(DateTime createdAt, string title, string message)
        {
            CreatedAt = createdAt;
            Title = title;
            Message = message;
            MemberNotifications = new List<MemberNotification>();
            EmployeeNotifications = new List<EmployeeNotification>();
        }

        /// <summary>
        /// Protected constructor for ORM (e.g., Entity Framework Core) materialization.
        /// </summary>
        protected Notification()
        {
            MemberNotifications = new List<MemberNotification>();
            EmployeeNotifications = new List<EmployeeNotification>();
        }
    }
}