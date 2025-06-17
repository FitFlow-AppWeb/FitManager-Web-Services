using System;

namespace FitManager_Web_Services.Inventory.Domain.Model.Commands
{
    /// <summary>
    /// Represents a command to update an existing inventory item.
    /// This record type is an immutable data transfer object (DTO) that carries the necessary
    /// information to update an item in the inventory system.
    /// It is processed by a command handler (e.g., <see cref="Application.Internal.CommandServices.ItemCommandService"/>).
    /// </summary>
    /// <param name="Id">The unique identifier of the item to be updated.</param>
    /// <param name="LastMaintenanceDate">The new last maintenance date of the item.</param>
    /// <param name="NextMaintenanceDate">The new next scheduled maintenance date of the item.</param>
    /// <param name="Status">The new status of the item (e.g., "Operational", "UnderRepair").</param>
    /// <param name="ItemTypeId">The ID of the updated item type associated with the item.</param>
    public record UpdateItemCommand(
        int Id,
        DateTime LastMaintenanceDate,
        DateTime NextMaintenanceDate,
        string Status,
        int ItemTypeId
        // int EmployeeId // TODO (Inventory): Awaiting Employee module support
    );
}