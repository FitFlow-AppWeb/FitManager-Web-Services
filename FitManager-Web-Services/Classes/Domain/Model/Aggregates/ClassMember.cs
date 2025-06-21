using FitManager_Web_Services.Members.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Classes.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents a Class Member entity within the Classes Bounded Context.
    /// This is typically a **join entity** (or junction table) used to manage the many-to-many relationship
    /// between a <see cref="Member"/> and a <see cref="Class"/>. It indicates that a specific member is
    /// enrolled in or associated with a particular class.
    /// </summary>
    public class ClassMember
    {
        /// <summary>
        /// Gets or sets the unique identifier of the member associated with this class.
        /// This property forms part of the composite primary key for this join entity.
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the class associated with this member.
        /// This property forms part of the composite primary key for this join entity.
        /// </summary>
        public int ClassId { get; set; }
        
        /// <summary>
        /// Gets or sets the navigation property to the <see cref="Member"/> aggregate
        /// associated with this class membership.
        /// </summary>
        public Member Member { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the <see cref="Class"/> aggregate
        /// associated with this member's enrollment.
        /// </summary>
        public Class Class { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassMember"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is used to create a new association between a member and a class.
        /// Since this is a join entity, its primary purpose is to link existing <see cref="Member"/>
        /// and <see cref="Class"/> instances.
        /// </remarks>
        /// <param name="memberId">The unique identifier of the member being associated.</param>
        /// <param name="classId">The unique identifier of the class being associated.</param>
        public ClassMember(int memberId, int classId)
        {
            // You might add basic validations here, e.g., that IDs are positive.
            // Example: if (memberId <= 0) throw new ArgumentOutOfRangeException(nameof(memberId), "Member ID must be a positive number.");
            // Example: if (classId <= 0) throw new ArgumentOutOfRangeException(nameof(classId), "Class ID must be a positive number.");

            MemberId = memberId;
            ClassId = classId;
        }

        /// <summary>
        /// Protected constructor for Entity Framework Core.
        /// </summary>
        /// <remarks>
        /// This parameterless constructor is typically used by ORMs like EF Core to materialize entities
        /// from the database. It should not be used for direct instantiation in application code.
        /// </remarks>
        protected ClassMember() { }
    }
}