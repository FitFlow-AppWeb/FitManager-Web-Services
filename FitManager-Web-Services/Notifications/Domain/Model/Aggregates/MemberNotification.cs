// Notifications/Domain/Model/Aggregates/MemberNotification.cs

using FitManager_Web_Services.Members.Domain.Model.Aggregates; // Required for Member aggregate

namespace FitManager_Web_Services.Notifications.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents a specific instance of a notification sent to a member.
    /// This is an entity representing the many-to-many relationship between
    /// <see cref="Notification"/> and <see cref="Member"/>, acting as a join table with implicit composite key.
    /// </summary>
    public class MemberNotification
    {
        
        public int Id { get; private set; } 
        /// <summary>
        /// Gets or sets the foreign key to the associated <see cref="Notification"/>.
        /// This property, along with <see cref="MemberId"/>, forms the composite primary key.
        /// </summary>
        public int NotificationId { get; set; }
        
        /// <summary>
        /// Gets or sets the navigation property to the associated <see cref="Notification"/>.
        /// </summary>
        public Notification Notification { get; set; } // Navigation property

        /// <summary>
        /// Gets or sets the foreign key to the associated <see cref="Member"/>.
        /// This property, along with <see cref="NotificationId"/>, forms the composite primary key.
        /// </summary>
        public int MemberId { get; set; }
        
        /// <summary>
        /// Gets or sets the navigation property to the associated <see cref="Member"/>.
        /// </summary>
        public Member Member { get; set; } // Navigation property

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberNotification"/> class
        /// with the specified notification and member identifiers.
        /// </summary>
        /// <param name="notificationId">The ID of the notification.</param>
        /// <param name="memberId">The ID of the member.</param>
        public MemberNotification(int notificationId, int memberId)
        {
            NotificationId = notificationId;
            MemberId = memberId;
        }

        /// <summary>
        /// Protected constructor for ORM (e.g., Entity Framework Core) materialization.
        /// </summary>
        protected MemberNotification() { }
    }
}