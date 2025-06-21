using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Finances.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents a Purchase Detail entity within the Finances Bounded Context.
    /// This entity represents a single line item within a <see cref="SupplyPurchase"/>,
    /// detailing the quantity and unit price of a specific <see cref="ItemType"/> purchased.
    /// It is part of the Supply Purchase aggregate.
    /// </summary>
    public class PurchaseDetail
    {
        /// <summary>
        /// Gets the unique identifier for the purchase detail.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets or sets the unique identifier of the associated supply purchase.
        /// This is a foreign key to the <see cref="SupplyPurchase"/> aggregate.
        /// </summary>
        public int SupplyPurchaseId { get; set; }
        
        /// <summary>
        /// Gets or sets the navigation property to the associated <see cref="SupplyPurchase"/> aggregate.
        /// This represents the parent purchase to which this detail belongs.
        /// </summary>
        public SupplyPurchase SupplyPurchase { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the item type being purchased.
        /// This is a foreign key to the <see cref="ItemType"/> aggregate from the Inventory context.
        /// </summary>
        public int ItemTypeId { get; set; }
        
        /// <summary>
        /// Gets or sets the navigation property to the associated <see cref="ItemType"/> aggregate.
        /// This represents the specific type of item bought in this detail.
        /// </summary>
        public ItemType ItemType { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the item at the time of purchase.
        /// </summary>
        public float UnitPrice { get; set; }
        
        /// <summary>
        /// Gets or sets the quantity of the item purchased in this detail.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseDetail"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is used to create a new line item for a supply purchase, ensuring that
        /// essential details like the associated purchase, item type, unit price, and quantity are captured upon creation.
        /// </remarks>
        /// <param name="supplyPurchaseId">The unique identifier of the parent supply purchase.</param>
        /// <param name="itemTypeId">The unique identifier of the type of item being detailed.</param>
        /// <param name="unitPrice">The price of a single unit of the item.</param>
        /// <param name="quantity">The number of units purchased.</param>
        public PurchaseDetail(int supplyPurchaseId, int itemTypeId, float unitPrice, int quantity)
        {
            // Consider adding validations here if unitPrice or quantity must be positive, etc.
            // Example: if (unitPrice <= 0) throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price must be positive.");
            // Example: if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");

            SupplyPurchaseId = supplyPurchaseId;
            ItemTypeId = itemTypeId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        /// <summary>
        /// Protected constructor for Entity Framework Core.
        /// </summary>
        /// <remarks>
        /// This parameterless constructor is typically used by ORMs like EF Core to materialize entities
        /// from the database. It should not be used for direct instantiation in application code
        /// to ensure that domain invariants (if any are enforced by the public constructor) are always met.
        /// </remarks>
        protected PurchaseDetail() { }
    }
}