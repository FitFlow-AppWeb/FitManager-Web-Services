using FitManager_Web_Services.Members.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Classes.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents a Booking entity within the Classes Bounded Context.
    /// This entity records a reservation made by a member for a specific class on a particular date.
    /// It establishes a link between a <see cref="Member"/> and a <see cref="Class"/>.
    /// </summary>
    public class Booking
    {
        /// <summary>
        /// Gets the unique identifier for the booking.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the unique identifier of the member who made the booking.
        /// This is a foreign key to the <see cref="Member"/> aggregate.
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the class that was booked.
        /// This is a foreign key to the <see cref="Class"/> aggregate.
        /// </summary>
        public int ClassId { get; set; }

        /// <summary>
        /// Gets or sets the date and time for which the class was booked.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the <see cref="Member"/> aggregate
        /// who made this booking.
        /// </summary>
        public Member Member { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the <see cref="Class"/> aggregate
        /// that was booked.
        /// </summary>
        public Class Class { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Booking"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is used to create a new booking record, capturing essential details
        /// such as the associated member, class, and the date of the booked session.
        /// </remarks>
        /// <param name="memberId">The unique identifier of the member making the booking.</param>
        /// <param name="classId">The unique identifier of the class being booked.</param>
        /// <param name="date">The date and time for which the class is booked.</param>
        public Booking(int memberId, int classId, DateTime date)
        {
            // You might consider adding validations here, e.g., to ensure memberId and classId are positive.
            // Example: if (memberId <= 0) throw new ArgumentOutOfRangeException(nameof(memberId), "Member ID must be positive.");
            // Example: if (classId <= 0) throw new ArgumentOutOfRangeException(nameof(classId), "Class ID must be positive.");

            MemberId = memberId;
            ClassId = classId;
            Date = date;
        }

        /// <summary>
        /// Protected constructor for Entity Framework Core.
        /// </summary>
        /// <remarks>
        /// This parameterless constructor is typically used by ORMs like EF Core to materialize entities
        /// from the database. It should not be used for direct instantiation in application code
        /// to ensure that domain invariants (if any are enforced by the public constructor) are always met.
        /// </remarks>
        protected Booking() { }
    }
}