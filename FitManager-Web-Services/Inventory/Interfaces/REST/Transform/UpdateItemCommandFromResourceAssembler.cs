using FitManager_Web_Services.Inventory.Domain.Model.Commands;
using FitManager_Web_Services.Inventory.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Inventory.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler responsible for transforming an <see cref="UpdateItemResource"/> and item ID
    /// into an <see cref="UpdateItemCommand"/>.
    /// This static class encapsulates the mapping logic between the REST input DTO
    /// and the corresponding domain-level command, adhering to Clean Architecture principles.
    /// </summary>
    public static class UpdateItemCommandFromResourceAssembler
    {
        /// <summary>
        /// Converts an <see cref="UpdateItemResource"/> and item ID into an <see cref="UpdateItemCommand"/>.
        /// </summary>
        /// <param name="id">The unique identifier of the item to update.</param>
        /// <param name="resource">The <see cref="UpdateItemResource"/> containing updated item data.</param>
        /// <returns>An <see cref="UpdateItemCommand"/> containing the mapped data from the resource.</returns>
        public static UpdateItemCommand ToCommandFromResource(int id, UpdateItemResource resource)
        {
            return new UpdateItemCommand(
                id,
                resource.LastMaintenanceDate,
                resource.NextMaintenanceDate,
                resource.Status,
                // resource.EmployeeId, // TODO (Inventory): Awaiting Employee module support
                resource.ItemTypeId
            );
        }
    }
}