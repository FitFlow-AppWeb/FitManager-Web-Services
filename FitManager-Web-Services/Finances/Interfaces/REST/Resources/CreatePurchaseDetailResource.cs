using System.ComponentModel.DataAnnotations;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource for creating a new purchase detail, used for data transfer objects (DTOs) in RESTful API requests.
    /// </summary>
    /// <param name="ItemTypeId">The unique identifier of the type of item purchased. Required.</param>
    /// <param name="UnitPrice">The price per unit of the item. Required.</param>
    /// <param name="Quantity">The number of units of the item purchased. Required.</param>
    /// <param name="LastMaintenanceDate">The date of the last maintenance performed on the item. Required.</param>
    /// <param name="NextMaintenanceDate">The date of the next scheduled maintenance for the item. Required.</param>
    /// <param name="Status">The current status of the item (e.g., "New", "In Use", "Maintenance Required"). Required.</param>
    /// <param name="EmployeeId">The unique identifier of the employee associated with this item's record or maintenance. Required.</param>
    public record CreatePurchaseDetailResource(
        [Required] int ItemTypeId,
        [Required] float UnitPrice,
        [Required] int Quantity,
        [Required] DateTime LastMaintenanceDate,
        [Required] DateTime NextMaintenanceDate,
        [Required] string Status,
        [Required] int EmployeeId
    );
}