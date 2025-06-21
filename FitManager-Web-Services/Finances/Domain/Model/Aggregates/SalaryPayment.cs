using FitManager_Web_Services.Employees.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Finances.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents the Salary Payment entity within the Finances Bounded Context.
    /// This entity tracks individual payments made to employees as their salary.
    /// It is part of the Finances aggregate, linked to a specific <see cref="Employee"/>.
    /// </summary>
    public class SalaryPayment
    {
        /// <summary>
        /// Gets the unique identifier for the salary payment.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the date and time when the salary payment was issued.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the amount of the salary payment.
        /// </summary>
        public float Amount { get; set; }

        /// <summary>
        /// Gets or sets the method used for the salary payment (e.g., "Bank Transfer", "Cash", "Check").
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the currency of the salary payment (e.g., "USD", "PEN", "EUR").
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the employee who received this payment.
        /// This is a foreign key to the <see cref="Employee"/> aggregate from the Employees context.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the <see cref="Employee"/> aggregate
        /// who received this salary payment. This represents the association between the payment and the employee.
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SalaryPayment"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is used to create a new salary payment record, ensuring that
        /// essential details are captured upon creation.
        /// </remarks>
        /// <param name="date">The date and time of the salary payment.</param>
        /// <param name="amount">The financial amount of the salary payment.</param>
        /// <param name="method">The method by which the payment was made.</param>
        /// <param name="currency">The currency of the payment.</param>
        /// <param name="employeeId">The unique identifier of the employee receiving the payment.</param>
        public SalaryPayment(DateTime date, float amount, string method, string currency, int employeeId)
        {
            // Consider adding validations here (e.g., amount must be positive).
            // Example: if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive.");

            Date = date;
            Amount = amount;
            Method = method;
            Currency = currency;
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
        protected SalaryPayment() { }
    }
}