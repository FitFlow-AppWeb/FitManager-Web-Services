using FitManager_Web_Services.Classes.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Inventory.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents a booking of an item for a specific class.
    /// </summary>
    public class ItemBooking
    {
        /// <summary>
        /// Gets the unique identifier of the booking.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the date and time when the item is reserved.
        /// </summary>
        public DateTime ReservationDate { get; private set; }

        /// <summary>
        /// Gets the duration (in minutes or hours, depending on business rules) of item usage.
        /// </summary>
        public int UsageTime { get; private set; }

        /// <summary>
        /// Gets the ID of the associated class.
        /// </summary>
        public int ClassId { get; private set; }

        /// <summary>
        /// Gets the class for which the item is booked.
        /// </summary>
        public Class Class { get; private set; }

        /// <summary>
        /// Gets the ID of the item being booked.
        /// </summary>
        public int ItemId { get; private set; }

        /// <summary>
        /// Gets the item being reserved.
        /// </summary>
        public Item Item { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemBooking"/> class.
        /// </summary>
        /// <param name="reservationDate">Date and time of the reservation.</param>
        /// <param name="usageTime">Duration of item usage (must be positive).</param>
        /// <param name="classId">ID of the class.</param>
        /// <param name="itemId">ID of the item.</param>
        public ItemBooking(DateTime reservationDate, int usageTime, int classId, int itemId)
        {
            if (usageTime <= 0) throw new ArgumentOutOfRangeException(nameof(usageTime), "Usage time must be a positive value.");
            if (classId <= 0) throw new ArgumentOutOfRangeException(nameof(classId), "Class ID must be positive.");
            if (itemId <= 0) throw new ArgumentOutOfRangeException(nameof(itemId), "Item ID must be positive.");

            ReservationDate = reservationDate;
            UsageTime = usageTime;
            ClassId = classId;
            ItemId = itemId;
        }

        /// <summary>
        /// Protected constructor for EF Core.
        /// </summary>
        protected ItemBooking() { }

        /// <summary>
        /// Updates the usage time and reservation date of the booking.
        /// </summary>
        /// <param name="reservationDate">New reservation date.</param>
        /// <param name="usageTime">New usage time (must be positive).</param>
        public void UpdateBooking(DateTime reservationDate, int usageTime)
        {
            if (usageTime <= 0) throw new ArgumentOutOfRangeException(nameof(usageTime), "Usage time must be a positive value.");

            ReservationDate = reservationDate;
            UsageTime = usageTime;
        }
    }
}
