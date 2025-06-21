namespace FitManager_Web_Services.Employees.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents a Specialty entity within the Employees Bounded Context.
    /// This entity details a specific area of expertise or skill set possessed by an employee.
    /// It is owned by the <see cref="Employee"/> aggregate, as its lifecycle is dependent on the employee.
    /// </summary>
    public class Specialty
    {
        /// <summary>
        /// Gets the unique identifier for the specialty.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the name of the specialty (e.g., "Strength Training", "Yoga Instructor").
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a detailed description of the specialty.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the unique identifier of the employee who has this specialty.
        /// This is a foreign key to the <see cref="Employee"/> aggregate.
        /// </summary>
        public int EmployeeId { get; private set; }

        /// <summary>
        /// Gets the navigation property to the <see cref="Employee"/> aggregate
        /// who possesses this specialty.
        /// </summary>
        public Employee Employee { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Specialty"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor ensures that a <see cref="Specialty"/> is created with essential, valid data.
        /// It enforces invariants at the time of creation.
        /// </remarks>
        /// <param name="name">The name of the specialty.</param>
        /// <param name="description">A description of the specialty.</param>
        /// <param name="employeeId">The unique identifier of the employee possessing this specialty.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="name"/> or <paramref name="description"/>
        /// are null, empty, or consist only of white-space characters.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="employeeId"/> is not a positive number.</exception>
        public Specialty(string name, string description, int employeeId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description cannot be null or empty.", nameof(description));
            if (employeeId <= 0) throw new ArgumentOutOfRangeException(nameof(employeeId), "EmployeeId must be a positive number.");

            Name = name;
            Description = description;
            EmployeeId = employeeId;
        }

        /// <summary>
        /// Protected constructor for Entity Framework Core.
        /// </summary>
        /// <remarks>
        /// This parameterless constructor is typically used by ORMs like EF Core to materialize entities
        /// from the database. It should not be used for direct instantiation in application code
        /// to ensure that domain invariants (enforced by the public constructor) are always met.
        /// </remarks>
        protected Specialty() { }
    }
}