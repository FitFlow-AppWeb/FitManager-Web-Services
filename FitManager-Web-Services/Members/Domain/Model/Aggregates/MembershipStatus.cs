// Members/Domain/Model/Aggregates/MembershipStatus.cs


namespace FitManager_Web_Services.Members.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents the membership status of a member.
    /// This class is modeled as a Value Object in DDD, meaning its identity is based on its attributes' values
    /// rather than a unique identifier. It encapsulates the period and current state of a member's subscription.
    /// It is part of the Member aggregate and its lifecycle is managed by the Member aggregate root.
    /// </summary>
    public class MembershipStatus
    {
        // Although this class has an 'Id', which often implies an Entity,
        // if its lifecycle and identity are strictly tied to the Member aggregate
        // and it's treated as an integral part of the Member's state (i.e., you don't query it independently,
        // or replace it entirely rather than updating it piece by piece), it can still function as a Value Object.
        // For simplicity and alignment with the current structure where it's part of Member's state,
        // it's commented as a Value Object. If it had its own lifecycle and needed to be queried
        // or referenced independently, it would typically be an Entity.

        /// <summary>
        /// Gets the unique identifier for the membership status (primarily for persistence).
        /// </summary>
        public int Id { get; private set; }
        
        /// <summary>
        /// Gets or sets the start date of the membership period.
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// Gets or sets the end date of the membership period.
        /// </summary>
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// Gets or sets the current status of the membership (e.g., "Active", "Expired", "Suspended").
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the foreign key to the associated Member.
        /// This creates a one-to-one or one-to-many relationship depending on context/configuration,
        /// but within the aggregate, it signifies its ownership by Member.
        /// </summary>
        public int MemberId { get; set; } 
        
        /// <summary>
        /// Gets or sets the foreign key to the associated MembershipType.
        /// </summary>
        public int MembershipTypeId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the associated Member.
        /// </summary>
        public Member Member { get; set; } 
        
        /// <summary>
        /// Gets or sets the navigation property to the associated MembershipType.
        /// </summary>
        public MembershipType MembershipType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipStatus"/> class.
        /// This constructor sets the initial state of the membership status.
        /// </summary>
        /// <param name="startDate">The start date of the membership.</param>
        /// <param name="endDate">The end date of the membership.</param>
        /// <param name="status">The status of the membership.</param>
        /// <param name="membershipTypeId">The ID of the associated membership type.</param>
        public MembershipStatus(DateTime startDate, DateTime endDate, string status, int membershipTypeId)
        {
            // Consider adding validations here (e.g., startDate < endDate, status not empty).
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            MembershipTypeId = membershipTypeId;
        }

        /// <summary>
        /// Protected constructor for EF Core.
        /// Used by the ORM to materialize entities from the database without invoking domain logic.
        /// </summary>
        protected MembershipStatus() { }

        /// <summary>
        /// Updates the status, end date, and optionally the start date and membership type of the membership.
        /// </summary>
        /// <param name="newStatus">The new status of the membership.</param>
        /// <param name="newEndDate">The new end date of the membership.</param>
        /// <param name="newMembershipTypeId">The new ID of the associated membership type.</param>
        /// <param name="newStartDate">Optional new start date of the membership.</param>
        public void UpdateStatus(string newStatus, DateTime newEndDate, int newMembershipTypeId, DateTime? newStartDate = null)
        {
            // Consider adding validations here as well (e.g., newStatus not empty, newStartDate < newEndDate).
            Status = newStatus;
            EndDate = newEndDate;
            MembershipTypeId = newMembershipTypeId;
            if (newStartDate.HasValue)
            {
                StartDate = newStartDate.Value;
            }
        }
    }
}