using FitManager_Web_Services.Members.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Finances.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents the Membership Payment entity within the Finances Bounded Context.
    /// This entity tracks individual payments made by members for their memberships.
    /// It is part of the Finances aggregate, linked to a specific <see cref="Member"/>.
    /// </summary>
    public class MembershipPayment
    {
        /// <summary>
        /// Gets the unique identifier for the membership payment.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the date and time when the payment was made.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the amount of the payment.
        /// </summary>
        public float Amount { get; set; }

        /// <summary>
        /// Gets or sets the payment method used (e.g., "Cash", "Credit Card", "Bank Transfer").
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the currency of the payment (e.g., "USD", "EUR", "PEN").
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the member associated with this payment.
        /// This is a foreign key to the <see cref="Member"/> aggregate.
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property to the <see cref="Member"/> aggregate
        /// who made this payment. This represents the association between the payment and the member.
        /// </summary>
        public Member Member { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipPayment"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is used to create a new membership payment record, ensuring that
        /// essential details are captured upon creation.
        /// </remarks>
        /// <param name="date">The date and time of the payment.</param>
        /// <param name="amount">The financial amount of the payment.</param>
        /// <param name="method">The method by which the payment was made (e.g., "Cash", "Card").</param>
        /// <param name="currency">The currency of the payment.</param>
        /// <param name="memberId">The unique identifier of the member making the payment.</param>
        public MembershipPayment(DateTime date, float amount, string method, string currency, int memberId)
        {
            Date = date;
            Amount = amount;
            Method = method;
            Currency = currency;
            MemberId = memberId;
        }

        /// <summary>
        /// Protected constructor for Entity Framework Core.
        /// </summary>
        /// <remarks>
        /// This parameterless constructor is typically used by ORMs like EF Core to materialize entities
        /// from the database. It should not be used for direct instantiation in application code.
        /// </remarks>
        protected MembershipPayment() { }
    }
}