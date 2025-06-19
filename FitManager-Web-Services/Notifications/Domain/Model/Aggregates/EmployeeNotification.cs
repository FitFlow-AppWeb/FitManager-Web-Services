// Notifications/Domain/Model/Aggregates/EmployeeNotification.cs

using FitManager_Web_Services.Employees.Domain.Model.Aggregates; // Required for Employee aggregate

namespace FitManager_Web_Services.Notifications.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents a specific instance of a notification sent to an employee.
    /// This is an entity representing the many-to-many relationship between
    /// <see cref="Notification"/> and <see cref="Employee"/>, acting as a join table with implicit composite key.
    /// </summary>
    public class EmployeeNotification
    {
        
        public int Id { get; private set; }
        /// <summary>
        /// Gets or sets the foreign key to the associated <see cref="Notification"/>.
        /// This property, along with <see cref="EmployeeId"/>, forms the composite primary key.
        /// </summary>
        public int NotificationId { get; set; }
        
        /// <summary>
        /// Gets or sets the navigation property to the associated <see cref="Notification"/>.
        /// </summary>
        public Notification Notification { get; set; } // Navigation property

        /// <summary>
        /// Gets or sets the foreign key to the associated <see cref="Employee"/>.
        /// This property, along with <see cref="NotificationId"/>, forms the composite primary key.
        /// </summary>
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// Gets or sets the navigation property to the associated <see cref="Employee"/>.
        /// </summary>
        public Employee Employee { get; set; } // Navigation property

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeNotification"/> class
        /// with the specified notification and employee identifiers.
        /// </summary>
        /// <param name="notificationId">The ID of the notification.</param>
        /// <param name="employeeId">The ID of the employee.</param>
        public EmployeeNotification(int notificationId, int employeeId)
        {
            NotificationId = notificationId;
            EmployeeId = employeeId;
        }

        /// <summary>
        /// Protected constructor for ORM (e.g., Entity Framework Core) materialization.
        /// </summary>
        protected EmployeeNotification() { }
    }
}