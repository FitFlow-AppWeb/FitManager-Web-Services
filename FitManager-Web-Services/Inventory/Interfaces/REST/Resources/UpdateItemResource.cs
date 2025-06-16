using System;
using System.ComponentModel.DataAnnotations;

namespace FitManager_Web_Services.Inventory.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource used to update an existing inventory item via the REST API.
    /// This record acts as a Data Transfer Object (DTO) for incoming update requests.
    /// All properties are required to ensure the item remains consistent after update.
    /// </summary>
    /// <param name="LastMaintenanceDate">The date when the item was last maintained. (Required)</param>
    /// <param name="NextMaintenanceDate">The scheduled date for the next maintenance. (Required)</param>
    /// <param name="Status">The operational status of the item (e.g., "Operational", "OutOfService"). (Required)</param>
    /// <param name="ItemTypeId">The unique identifier for the item type associated with this item. (Required)</param>
    public record UpdateItemResource(
        [Required] DateTime LastMaintenanceDate,
        [Required] DateTime NextMaintenanceDate,
        [Required] string Status,
        // [Required] int EmployeeId, // TODO (Inventory): Awaiting Employee module support
        [Required] int ItemTypeId
    );
}