// Members/Domain/Model/Aggregates/Member.cs

using FitManager_Web_Services.Classes.Domain.Model.Aggregates; // Assuming ClassMember, Booking, Attendance are in this namespace

namespace FitManager_Web_Services.Members.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents the Member aggregate root.
    /// In Domain-Driven Design (DDD), an aggregate root is a cluster of domain objects
    /// that can be treated as a single unit for data changes. The Member is the root
    /// of this aggregate, meaning all changes to its associated entities (like MembershipStatus)
    /// should be done through the Member itself to ensure consistency.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Gets the unique identifier for the member.
        /// </summary>
        public int Id { get; private set; }
        
        /// <summary>
        /// Gets the first name of the member.
        /// </summary>
        public string FirstName { get; private set; }
        
        /// <summary>
        /// Gets the last name of the member.
        /// </summary>
        public string LastName { get; private set; }
        
        /// <summary>
        /// Gets the age of the member.
        /// </summary>
        public int Age { get; private set; }
        
        /// <summary>
        /// Gets the DNI (National Identity Document) of the member.
        /// </summary>
        public int Dni { get; private set; }
        
        /// <summary>
        /// Gets the phone number of the member.
        /// </summary>
        public int PhoneNumber { get; private set; }
        
        /// <summary>
        /// Gets the address of the member.
        /// </summary>
        public string Address { get; private set; }
        
        /// <summary>
        /// Gets the email address of the member.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the collection of classes the member is associated with.
        /// This represents a many-to-many relationship.
        /// </summary>
        public ICollection<ClassMember> ClassMembers { get; private set; } = new List<ClassMember>();
        
        /// <summary>
        /// Gets the collection of bookings made by the member.
        /// </summary>
        public ICollection<Booking> Bookings { get; private set; } = new List<Booking>();
        
        /// <summary>
        /// Gets the collection of attendance records for the member.
        /// </summary>
        public ICollection<Attendance> Attendances { get; private set; } = new List<Attendance>();
        
        /// <summary>
        /// Gets the membership status details for the member.
        /// This is a Value Object within the Member aggregate, encapsulating membership period and status.
        /// </summary>
        public MembershipStatus MembershipStatus { get; private set; }
        

        /// <summary>
        /// Initializes a new instance of the <see cref="Member"/> class.
        /// This constructor enforces business rules and invariants upon creation of a new Member.
        /// </summary>
        /// <param name="firstName">The first name of the member.</param>
        /// <param name="lastName">The last name of the member.</param>
        /// <param name="age">The age of the member. Must be a positive number.</param>
        /// <param name="dni">The DNI of the member. Must be a positive number.</param>
        /// <param name="phoneNumber">The phone number of the member. Must be a positive number.</param>
        /// <param name="address">The address of the member.</param>
        /// <param name="email">The email address of the member.</param>
        /// <exception cref="ArgumentException">Thrown if any string parameter is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if age, DNI, or phone number are not positive.</exception>
        public Member(string firstName, string lastName, int age, int dni, int phoneNumber, string address, string email)
        {
            // Invariant validations: Ensures the aggregate is always in a valid state upon creation.
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));
            if (age <= 0) throw new ArgumentOutOfRangeException(nameof(age), "Age must be a positive number.");
            if (dni <= 0) throw new ArgumentOutOfRangeException(nameof(dni), "DNI must be a positive number.");
            if (phoneNumber <= 0) throw new ArgumentOutOfRangeException(nameof(phoneNumber), "Phone number must be a positive number.");
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Address cannot be null or empty.", nameof(address));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Dni = dni;
            PhoneNumber = phoneNumber;
            Address = address;
            Email = email;

            // Initialize collections to prevent null reference exceptions.
            ClassMembers = new List<ClassMember>();
            Bookings = new List<Booking>();
            Attendances = new List<Attendance>();
        }

        /// <summary>
        /// Protected constructor for EF Core.
        /// Used by the ORM to materialize entities from the database without invoking domain logic validations.
        /// </summary>
        protected Member() { }

        /// <summary>
        /// Updates the core information of the member.
        /// This method encapsulates the business logic for modifying member attributes.
        /// </summary>
        /// <param name="firstName">The new first name.</param>
        /// <param name="lastName">The new last name.</param>
        /// <param name="age">The new age. Must be non-negative.</param>
        /// <param name="dni">The new DNI. Must be a positive number.</param>
        /// <param name="phoneNumber">The new phone number. Must be non-negative.</param>
        /// <param name="address">The new address.</param>
        /// <param name="email">The new email address.</param>
        /// <exception cref="ArgumentException">Thrown if any string parameter is null or empty during update.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if age, DNI, or phone number are invalid during update.</exception>
        public void Update(string firstName, string lastName, int age, int dni, int phoneNumber, string address, string email)
        {
            // Validations for update operations.
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name cannot be null or empty during update.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name cannot be null or empty during update.", nameof(lastName));
            // Age can be 0 if you allow it, but not negative.
            if (age < 0) throw new ArgumentOutOfRangeException(nameof(age), "Age cannot be negative.");
            if (dni <= 0) throw new ArgumentOutOfRangeException(nameof(dni), "DNI must be a positive number during update.");
            // Phone number can be 0 if you allow it, but not negative.
            if (phoneNumber < 0) throw new ArgumentOutOfRangeException(nameof(phoneNumber), "Phone number cannot be negative.");
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Address cannot be null or empty during update.", nameof(address));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be null or empty during update.", nameof(email));

            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Dni = dni;
            PhoneNumber = phoneNumber;
            Address = address;
            Email = email;
        }
        
        /// <summary>
        /// Updates the membership status details of the member.
        /// This method allows for partial updates of the MembershipStatus value object.
        /// </summary>
        /// <param name="startDate">Optional new start date for the membership.</param>
        /// <param name="endDate">Optional new end date for the membership.</param>
        /// <param name="status">Optional new status string for the membership.</param>
        /// <param name="membershipTypeId">Optional new ID of the associated membership type.</param>
        public void UpdateMembershipStatus(DateTime? startDate, DateTime? endDate, string? status, int? membershipTypeId)
        {
            // Ensure MembershipStatus object exists before attempting to update its properties.
            // This implicitly assumes MembershipStatus is already assigned when this method is called.
            if (MembershipStatus == null) 
            {
                // This scenario should ideally be prevented by ensuring a MembershipStatus is set upon Member creation.
                // Or by throwing an exception if update is attempted on a null status.
                throw new InvalidOperationException("MembershipStatus has not been assigned to this member.");
            }

            if (startDate.HasValue)
            {
                MembershipStatus.StartDate = startDate.Value;
            }
            if (endDate.HasValue)
            {
                MembershipStatus.EndDate = endDate.Value;
            }
            if (!string.IsNullOrEmpty(status)) 
            {
                MembershipStatus.Status = status;
            }
            if (membershipTypeId.HasValue)
            {
                MembershipStatus.MembershipTypeId = membershipTypeId.Value;
            }
        }

        /// <summary>
        /// Assigns a new MembershipStatus to the member.
        /// This method is intended for initial assignment or complete replacement of the status.
        /// </summary>
        /// <param name="status">The <see cref="MembershipStatus"/> value object to assign.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided status is null.</exception>
        public void AssignMembershipStatus(MembershipStatus status)
        {
            if (status == null) throw new ArgumentNullException(nameof(status), "Membership status cannot be null.");
            MembershipStatus = status;
        }
        
        /// <summary>
        /// Sets the membership status for the member.
        /// This is an alternative/redundant method for assigning membership status.
        /// Consider consolidating with <see cref="AssignMembershipStatus"/> if their purposes overlap.
        /// </summary>
        /// <param name="status">The <see cref="MembershipStatus"/> value object to set.</param>
        public void SetMembershipStatus(MembershipStatus status)
        {
            this.MembershipStatus = status;
        }
    }
}