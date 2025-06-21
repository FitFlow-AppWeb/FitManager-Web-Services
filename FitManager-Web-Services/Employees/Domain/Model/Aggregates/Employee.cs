using FitManager_Web_Services.Classes.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Employees.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents the Employee aggregate root.
    /// In Domain-Driven Design (DDD), an aggregate root is a cluster of domain objects
    /// that can be treated as a single unit for data changes. The <see cref="Employee"/> is the root
    /// of this aggregate, meaning all changes to its associated entities (like <see cref="Certification"/>
    /// and <see cref="Specialty"/>) should be done through the Employee itself to ensure consistency.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets the unique identifier for the employee.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the first name of the employee.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the employee.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the age of the employee.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the DNI (National Identity Document) of the employee.
        /// </summary>
        public int Dni { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the employee.
        /// </summary>
        public int PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the address of the employee.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the email address of the employee.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password for the employee's account.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the wage (salary) of the employee.
        /// </summary>
        public float Wage { get; set; }

        /// <summary>
        /// Gets or sets the role of the employee (e.g., "Trainer", "Administrator").
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the standard work hours per week for the employee.
        /// </summary>
        public int WorkHours { get; set; }

        /// <summary>
        /// Gets or sets the collection of classes taught or managed by this employee.
        /// This represents a potential relationship where an employee can be associated with multiple classes.
        /// It is initialized to an empty list to prevent null reference exceptions.
        /// </summary>
        public ICollection<Class> Classes { get; set; } = new List<Class>();

        /// <summary>
        /// Gets or sets the collection of <see cref="Specialty"/> entities
        /// held by this employee. It is initialized to an empty list.
        /// </summary>
        public ICollection<Specialty> Specialties { get; set; } = new List<Specialty>();

        /// <summary>
        /// Gets or sets the collection of <see cref="Certification"/> entities
        /// held by this employee. It is initialized to an empty list.
        /// </summary>
        public ICollection<Certification> Certifications { get; set; } = new List<Certification>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class with core personal and work details.
        /// </summary>
        /// <remarks>
        /// This constructor ensures that an <see cref="Employee"/> aggregate is created with all essential
        /// information, including details pertinent to their employment such as password, wage, role, and work hours.
        /// </remarks>
        /// <param name="firstName">The first name of the employee.</param>
        /// <param name="lastName">The last name of the employee.</param>
        /// <param name="age">The age of the employee.</param>
        /// <param name="dni">The DNI (National Identity Document) of the employee.</param>
        /// <param name="phoneNumber">The phone number of the employee.</param>
        /// <param name="address">The address of the employee.</param>
        /// <param name="email">The email address of the employee.</param>
        /// <param name="password">The password for the employee's account.</param>
        /// <param name="wage">The wage (salary) of the employee.</param>
        /// <param name="role">The role of the employee.</param>
        /// <param name="workHours">The standard work hours per week.</param>
        public Employee(string firstName, string lastName, int age, int dni, int phoneNumber, string address, string email, string password, float wage, string role, int workHours)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Dni = dni;
            PhoneNumber = phoneNumber;
            Address = address;
            Email = email;
            Password = password;
            Wage = wage;
            Role = role;
            WorkHours = workHours;
        }

        /// <summary>
        /// Assigns a collection of <see cref="Certification"/> entities to the employee.
        /// </summary>
        /// <remarks>
        /// This method encapsulates the business logic for associating certifications with an employee.
        /// It iterates through the provided collection and adds each certification to the employee's list of certifications.
        /// Additional business rules (e.g., preventing duplicate certifications, validating certification content)
        /// could be implemented here.
        /// </remarks>
        /// <param name="certifications">An enumerable collection of <see cref="Certification"/> objects to assign.</param>
        public void AssignCertifications(IEnumerable<Certification> certifications)
        {
            if (certifications != null)
            {
                foreach (var certification in certifications)
                {
                    // Consider adding validation logic here, e.g., to prevent adding duplicates
                    // or to ensure the certification is valid before adding.
                    Certifications.Add(certification);
                }
            }
        }

        /// <summary>
        /// Assigns a collection of <see cref="Specialty"/> entities to the employee.
        /// </summary>
        /// <remarks>
        /// This method provides the means to associate various specialties with an employee.
        /// It adds each specialty from the provided collection to the employee's list of specialties.
        /// Similar to certifications, further validation or business rules can be applied here.
        /// </remarks>
        /// <param name="specialties">An enumerable collection of <see cref="Specialty"/> objects to assign.</param>
        public void AssignSpecialties(IEnumerable<Specialty> specialties)
        {
            if (specialties != null)
            {
                foreach (var specialty in specialties)
                {
                    // Consider adding validation logic here, e.g., to prevent adding duplicates.
                    Specialties.Add(specialty);
                }
            }
        }

        /// <summary>
        /// Updates the core information of the employee.
        /// </summary>
        /// <remarks>
        /// This method encapsulates the business logic for modifying an employee's attributes,
        /// including personal details, contact information, account password, and employment terms.
        /// It ensures that updates are applied in a consistent manner.
        /// Consider adding input validations (e.g., for null/empty strings, negative numbers)
        /// to maintain the aggregate's integrity during updates.
        /// </remarks>
        /// <param name="firstName">The new first name.</param>
        /// <param name="lastName">The new last name.</param>
        /// <param name="age">The new age.</param>
        /// <param name="dni">The new DNI.</param>
        /// <param name="phoneNumber">The new phone number.</param>
        /// <param name="address">The new address.</param>
        /// <param name="email">The new email address.</param>
        /// <param name="password">The new password.</param>
        /// <param name="wage">The new wage.</param>
        /// <param name="role">The new role.</param>
        /// <param name="workHours">The new work hours.</param>
        public void Update(
            string firstName,
            string lastName,
            int age,
            int dni,
            int phoneNumber,
            string address,
            string email,
            string password,
            float wage,
            string role,
            int workHours)
        {
            // Recommended: Add input validation here to ensure updated values are valid.
            // Example: if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));
            // Example: if (age < 0) throw new ArgumentOutOfRangeException(nameof(age), "Age cannot be negative.");

            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Dni = dni;
            PhoneNumber = phoneNumber;
            Address = address;
            Email = email;
            Password = password;
            Wage = wage;
            Role = role;
            WorkHours = workHours;
        }

        /// <summary>
        /// Protected constructor for Entity Framework Core.
        /// </summary>
        /// <remarks>
        /// This parameterless constructor is typically used by ORMs like EF Core to materialize entities
        /// from the database. It should not be used for direct instantiation in application code
        /// to ensure that domain invariants (enforced by the public constructor) are always met.
        /// </remarks>
        protected Employee() { }
    }
}