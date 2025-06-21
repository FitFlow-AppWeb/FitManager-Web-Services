namespace FitManager_Web_Services.Finances.Domain.Model.Aggregates
{
  

    /// <summary>
    /// Represents the Supply Purchase aggregate root within the Finances Bounded Context.
    /// An aggregate root for tracking the acquisition of supplies, which can include multiple
    /// <see cref="PurchaseDetail"/> entities as its children, all managed as a single transactional unit.
    /// </summary>
    public class SupplyPurchase
    {
        /// <summary>
        /// Gets the unique identifier for the supply purchase.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the date when the supply purchase was made.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the supply purchase.
        /// </summary>
        public float Amount { get; set; }

        /// <summary>
        /// Gets or sets the payment method used for the purchase (e.g., "Cash", "Credit Card").
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the currency of the purchase amount (e.g., "USD", "PEN").
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the name of the vendor from whom the supplies were purchased.
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="PurchaseDetail"/> entities
        /// associated with this supply purchase. These details represent the individual
        /// items bought within this purchase. It is initialized to an empty list to prevent null reference exceptions.
        /// </summary>
        public ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplyPurchase"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is used to create a new main supply purchase record, ensuring that
        /// essential details are captured upon creation.
        /// </remarks>
        /// <param name="date">The date of the purchase.</param>
        /// <param name="amount">The total financial amount of the purchase.</param>
        /// <param name="method">The method used for the payment.</param>
        /// <param name="currency">The currency of the purchase.</param>
        /// <param name="vendorName">The name of the vendor.</param>
        public SupplyPurchase(DateTime date, float amount, string method, string currency, string vendorName)
        {
            // Consider adding validations here (e.g., amount must be positive, vendorName not empty).
            // Example: if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive.");
            // Example: if (string.IsNullOrWhiteSpace(vendorName)) throw new ArgumentException("Vendor name cannot be null or empty.", nameof(vendorName));

            Date = date;
            Amount = amount;
            Method = method;
            Currency = currency;
            VendorName = vendorName;
        }

        /// <summary>
        /// Protected constructor for Entity Framework Core.
        /// </summary>
        /// <remarks>
        /// This parameterless constructor is typically used by ORMs like EF Core to materialize entities
        /// from the database. It should not be used for direct instantiation in application code
        /// to ensure that domain invariants (if any are enforced by the public constructor) are always met.
        /// </remarks>
        protected SupplyPurchase() { }
    }
}