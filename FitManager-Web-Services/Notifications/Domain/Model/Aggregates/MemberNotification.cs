using FitManager_Web_Services.Members.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Notifications.Domain.Model.Aggregates
{
    public class MemberNotification
    {
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }

        public MemberNotification(int notificationId, int memberId)
        {
            NotificationId = notificationId;
            MemberId = memberId;
        }

        protected MemberNotification() { }
    }
}