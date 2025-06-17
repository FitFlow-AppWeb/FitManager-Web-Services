using System;
using FitManager_Web_Services.Employees.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Inventory.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource returned for an inventory item via the REST API.
    /// This record acts as a Data Transfer Object (DTO) for outgoing data,
    /// providing detailed information about an item, including maintenance info and item type.
    /// </summary>
    /// <param name="Id">The unique identifier of the item.</param>
    /// <param name="LastMaintenanceDate">The date when the item was last maintained.</param>
    /// <param name="NextMaintenanceDate">The date scheduled for the next maintenance.</param>
    /// <param name="Status">The current operational status of the item (e.g., "Operational", "Needs Maintenance").</param>
    /// <param name="ItemTypeId">The identifier of the item's type.</param>
    /// <param name="ItemType">The detailed resource associated with the item's type, if available.</param>
    public record ItemResource(
        int Id,
        DateTime LastMaintenanceDate,
        DateTime NextMaintenanceDate,
        string Status,

        int? EmployeeId,
        EmployeeResource? Employee,

        int ItemTypeId,
        ItemTypeResource? ItemType
    );
}