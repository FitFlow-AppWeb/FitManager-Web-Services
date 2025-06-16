using FitManager_Web_Services.Inventory.Domain.Model.Commands;
using FitManager_Web_Services.Inventory.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Inventory.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler responsible for transforming a <see cref="CreateItemResource"/> into a <see cref="CreateItemCommand"/>.
    /// This static class encapsulates the mapping logic between the REST input DTO and the domain command,
    /// ensuring a clean separation of concerns between layers as per Clean Architecture principles.
    /// </summary>
    public static class CreateItemCommandFromResourceAssembler
    {
        /// <summary>
        /// Converts a <see cref="CreateItemResource"/> into a <see cref="CreateItemCommand"/>.
        /// </summary>
        /// <param name="resource">The resource containing the input data for creating an item.</param>
        /// <returns>A new instance of <see cref="CreateItemCommand"/> with data mapped from the resource.</returns>
        public static CreateItemCommand ToCommandFromResource(CreateItemResource resource)
        {
            return new CreateItemCommand(
                resource.LastMaintenanceDate,
                resource.NextMaintenanceDate,
                resource.Status,
                // resource.EmployeeId, // TODO (Inventory): Awaiting Employee module support
                resource.ItemTypeId
            );
        }
    }
}