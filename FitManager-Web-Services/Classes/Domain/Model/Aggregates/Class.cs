using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Classes.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents the Class aggregate root within the Classes Bounded Context.
    /// This aggregate represents a scheduled fitness class, encompassing its details, capacity,
    /// and associations with employees, members, and necessary equipment.
    /// It serves as a central point for managing class-related business logic.
    /// </summary>
    public class Class
    {
        /// <summary>
        /// Gets the unique identifier for the class.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the name of the class (e.g., "Morning Yoga", "HIIT").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a detailed description of the class.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type or category of the class (e.g., "Yoga", "Cardio", "Strength").
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the maximum capacity of attendees for the class.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Gets or sets the scheduled start date and time of the class.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the duration of the class in minutes.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the current status of the class (e.g., "Scheduled", "Active", "Cancelled", "Completed").
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the employee (trainer/instructor) assigned to this class.
        /// This is a foreign key to the <see cref="Employee"/> aggregate.
        /// </summary>
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// Gets or sets the navigation property to the <see cref="Employee"/> aggregate
        /// who is assigned to teach or manage this class.
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="ClassMember"/> entities
        /// linking members to this class. This represents the many-to-many relationship
        /// between classes and members. It is initialized to an empty list.
        /// </summary>
        public ICollection<ClassMember> ClassMembers { get; set; } = new List<ClassMember>();

        /// <summary>
        /// Gets or sets the collection of <see cref="Booking"/> entities
        /// representing reservations made by members for this class. It is initialized to an empty list.
        /// </summary>
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        /// <summary>
        /// Gets or sets the collection of <see cref="Attendance"/> entities
        /// tracking member attendance for this class. It is initialized to an empty list.
        /// </summary>
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        
        /// <summary>
        /// Gets or sets the collection of <see cref="ItemBooking"/> entities
        /// representing equipment booked for this class. It is initialized to an empty list.
        /// </summary>
        public ICollection<ItemBooking> ItemBookings { get; set; } = new List<ItemBooking>();
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Class"/> aggregate.
        /// </summary>
        /// <remarks>
        /// This constructor is used to create a new class, ensuring all essential scheduling
        /// and descriptive details are provided upon creation.
        /// Consider adding invariant validations here (e.g., capacity > 0, duration > 0,
        /// non-empty strings, valid employeeId) to enforce domain rules immediately.
        /// </remarks>
        /// <param name="name">The name of the class.</param>
        /// <param name="description">A description of the class.</param>
        /// <param name="type">The type or category of the class.</param>
        /// <param name="capacity">The maximum number of participants for the class.</param>
        /// <param name="startDate">The scheduled start date and time of the class.</param>
        /// <param name="duration">The duration of the class in minutes.</param>
        /// <param name="status">The initial status of the class.</param>
        /// <param name="employeeId">The unique identifier of the employee assigned to the class.</param>
        public Class(string name, string description, string type, int capacity, DateTime startDate, int duration, string status, int employeeId)
        {
            // Example of potential validations to add for robust aggregate creation:
            // if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            // if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be a positive number.");
            // if (duration <= 0) throw new ArgumentOutOfRangeException(nameof(duration), "Duration must be a positive number.");
            // if (employeeId <= 0) throw new ArgumentOutOfRangeException(nameof(employeeId), "Employee ID must be a positive number.");

            Name = name;
            Description = description;
            Type = type;
            Capacity = capacity;
            StartDate = startDate;
            Duration = duration;
            Status = status;
            EmployeeId = employeeId;
        }

        /// <summary>
        /// Protected constructor for Entity Framework Core.
        /// </summary>
        /// <remarks>
        /// This parameterless constructor is typically used by ORMs like EF Core to materialize entities
        /// from the database. It should not be used for direct instantiation in application code
        /// to ensure that domain invariants (if any are enforced by the public constructor) are always met.
        /// </remarks>
        protected Class() { }
    }
}