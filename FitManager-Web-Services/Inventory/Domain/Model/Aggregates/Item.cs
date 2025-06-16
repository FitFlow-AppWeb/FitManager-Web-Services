using FitManager_Web_Services.Employees.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Inventory.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents the Item aggregate root in the Inventory context.
    /// Responsible for managing maintenance information, booking history, and associations to employees and types.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Gets the unique identifier of the item.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the date of the last maintenance performed on this item.
        /// </summary>
        public DateTime LastMaintenanceDate { get; private set; }

        /// <summary>
        /// Gets the date of the next scheduled maintenance.
        /// </summary>
        public DateTime NextMaintenanceDate { get; private set; }

        /// <summary>
        /// Gets the current operational status of the item (e.g., Available, In Use, Under Maintenance).
        /// </summary>
        public string Status { get; private set; }

        /// <summary>
        /// Gets the ID of the employee responsible for the item (optional).
        /// </summary>
        public int? EmployeeId { get; private set; }

        /// <summary>
        /// Gets the employee assigned to this item.
        /// </summary>
        public Employee? Employee { get; private set; }

        /// <summary>
        /// Gets the ID of the associated item type.
        /// </summary>
        public int ItemTypeId { get; private set; }

        /// <summary>
        /// Gets the associated item type entity.
        /// </summary>
        public ItemType ItemType { get; private set; }

        /// <summary>
        /// Gets the collection of bookings that involve this item.
        /// </summary>
        public ICollection<ItemBooking> ItemBookings { get; private set; } = new List<ItemBooking>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class with required fields.
        /// </summary>
        public Item(DateTime lastMaintenanceDate, DateTime nextMaintenanceDate, string status, int itemTypeId, int? employeeId = null)
        {
            if (string.IsNullOrWhiteSpace(status)) throw new ArgumentException("Status cannot be null or empty.", nameof(status));
            if (nextMaintenanceDate <= lastMaintenanceDate)
                throw new ArgumentException("Next maintenance date must be after the last maintenance date.");

            LastMaintenanceDate = lastMaintenanceDate;
            NextMaintenanceDate = nextMaintenanceDate;
            Status = status;
            ItemTypeId = itemTypeId;
            EmployeeId = employeeId;
        }

        /// <summary>
        /// Protected constructor for EF Core.
        /// </summary>
        protected Item() { }

        /// <summary>
        /// Updates the maintenance information and status of the item.
        /// </summary>
        public void UpdateMaintenance(DateTime lastMaintenanceDate, DateTime nextMaintenanceDate, string status)
        {
            if (string.IsNullOrWhiteSpace(status)) throw new ArgumentException("Status cannot be null or empty.", nameof(status));
            if (nextMaintenanceDate <= lastMaintenanceDate)
                throw new ArgumentException("Next maintenance date must be after the last maintenance date.");

            LastMaintenanceDate = lastMaintenanceDate;
            NextMaintenanceDate = nextMaintenanceDate;
            Status = status;
        }

        /// <summary>
        /// Assigns or changes the responsible employee for this item.
        /// </summary>
        public void AssignEmployee(int employeeId)
        {
            if (employeeId <= 0) throw new ArgumentOutOfRangeException(nameof(employeeId), "Employee ID must be positive.");
            EmployeeId = employeeId;
        }

        /// <summary>
        /// Changes the type of the item.
        /// </summary>
        public void ChangeItemType(int itemTypeId)
        {
            if (itemTypeId <= 0) throw new ArgumentOutOfRangeException(nameof(itemTypeId), "ItemType ID must be positive.");
            ItemTypeId = itemTypeId;
        }
    }
}
