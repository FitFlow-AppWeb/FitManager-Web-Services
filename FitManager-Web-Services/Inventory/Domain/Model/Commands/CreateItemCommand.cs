namespace FitManager_Web_Services.Inventory.Domain.Model.Commands
{
    /// <summary>
    /// Represents a command to create a new inventory item, with optional updates to the related ItemType.
    /// </summary>
    /// <param name="LastMaintenanceDate">The date when the item was last maintained.</param>
    /// <param name="NextMaintenanceDate">The scheduled date for the next maintenance.</param>
    /// <param name="Status">The current operational status of the item (e.g., "Operational", "Out of Service").</param>
    /// <param name="ItemTypeId">The ID representing the type/category of the item.</param>
    /// <param name="EmployeeId">The ID of the employee responsible for this item.</param>
    public record CreateItemCommand(
        DateTime LastMaintenanceDate,
        DateTime NextMaintenanceDate,
        string Status,
        int ItemTypeId,
        int EmployeeId
    );
}