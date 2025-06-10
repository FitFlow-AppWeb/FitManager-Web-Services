using FitManager_Web_Services.Employees.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Notifications.Domain.Model.Aggregates
{
    public class EmployeeNotification
    {
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public EmployeeNotification(int notificationId, int employeeId)
        {
            NotificationId = notificationId;
            EmployeeId = employeeId;
        }

        protected EmployeeNotification() { }
    }
}