using FitManager_Web_Services.Members.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Classes.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents an Attendance record entity within the Classes Bounded Context.
    /// This entity tracks a member's presence in a specific class, including entry and exit times.
    /// It links a <see cref="Member"/> to a <see cref="Class"/> at a specific point in time.
    /// </summary>
    public class Attendance
    {
        /// <summary>
        /// Gets the unique identifier for the attendance record.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the time when the member entered the class.
        /// </summary>
        public DateTime EntryTime { get; set; }

        /// <summary>
        /// Gets or sets the time when the member exited the class.
        /// </summary>
        public DateTime ExitTime { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the member associated with this attendance record.
        /// This is a foreign key to the <see cref="Member"/> aggregate.
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the <see cref="Member"/> aggregate
        /// whose attendance is being recorded.
        /// </summary>
        public Member Member { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the class for which this attendance is recorded.
        /// This is a foreign key to the <see cref="Class"/> aggregate.
        /// </summary>
        public int ClassId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the <see cref="Class"/> aggregate
        /// that this attendance record belongs to.
        /// </summary>
        public Class Class { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Attendance"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is used to create a new attendance record, ensuring that
        /// essential details like entry/exit times, and associations to a member and a class are captured.
        /// Consider adding validations to ensure <paramref name="entryTime"/> is before <paramref name="exitTime"/>.
        /// </remarks>
        /// <param name="entryTime">The time the member entered the class.</param>
        /// <param name="exitTime">The time the member exited the class.</param>
        /// <param name="memberId">The unique identifier of the member attending the class.</param>
        /// <param name="classId">The unique identifier of the class attended.</param>
        public Attendance(DateTime entryTime, DateTime exitTime, int memberId, int classId)
        {
            // Example of potential validation:
            // if (exitTime < entryTime) throw new ArgumentException("Exit time cannot be before entry time.", nameof(exitTime));
            // if (memberId <= 0) throw new ArgumentOutOfRangeException(nameof(memberId), "Member ID must be a positive number.");
            // if (classId <= 0) throw new ArgumentOutOfRangeException(nameof(classId), "Class ID must be a positive number.");

            EntryTime = entryTime;
            ExitTime = exitTime;
            MemberId = memberId;
            ClassId = classId;
        }

        /// <summary>
        /// Protected constructor for Entity Framework Core.
        /// </summary>
        /// <remarks>
        /// This parameterless constructor is typically used by ORMs like EF Core to materialize entities
        /// from the database. It should not be used for direct instantiation in application code
        /// to ensure that domain invariants (if any are enforced by the public constructor) are always met.
        /// </remarks>
        protected Attendance() { }
    }
}