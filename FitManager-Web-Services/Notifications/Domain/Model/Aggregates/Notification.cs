namespace FitManager_Web_Services.Notifications.Domain.Model.Aggregates
{
    public class Notification
    {
        public int Id { get; private set; }

        public DateTime CreatedAt { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public Notification(DateTime createdAt, string title, string message)
        {
            CreatedAt = createdAt;
            Title = title;
            Message = message;
        }

        protected Notification() { }
    }
}