using System;
using System.ComponentModel.DataAnnotations;

namespace FitManager_Web_Services.Inventory.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource for creating a new item in the inventory via the REST API.
    /// Serves as a Data Transfer Object (DTO) for client requests.
    /// </summary>
    /// <param name="LastMaintenanceDate">The date the item was last maintained.</param>
    /// <param name="NextMaintenanceDate">The scheduled date for the next maintenance.</param>
    /// <param name="Status">The current operational status of the item (e.g., "Operational", "Under Maintenance").</param>
    /// <param name="ItemTypeId">The identifier of the item type to which this item belongs.</param>
    public record CreateItemResource(
        [Required] DateTime LastMaintenanceDate,
        [Required] DateTime NextMaintenanceDate,
        [Required] string Status,

        // TODO (Inventory): Add [Required] int EmployeeId when Employee module is integrated
        [Required] int ItemTypeId
    );
}