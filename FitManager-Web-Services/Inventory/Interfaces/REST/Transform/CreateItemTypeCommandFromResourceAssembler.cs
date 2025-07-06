using FitManager_Web_Services.Inventory.Domain.Model.Commands;
using FitManager_Web_Services.Inventory.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Inventory.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler for converting a <see cref="CreateItemTypeResource"/> to a <see cref="CreateItemTypeCommand"/>.
    /// This class bridges the gap between the REST API's data transfer objects and the domain's command objects.
    /// </summary>
    public static class CreateItemTypeCommandFromResourceAssembler
    {
        /// <summary>
        /// Converts a <see cref="CreateItemTypeResource"/> to a <see cref="CreateItemTypeCommand"/>.
        /// </summary>
        /// <param name="resource">The resource to convert.</param>
        /// <returns>A new <see cref="CreateItemTypeCommand"/> instance.</returns>
        public static CreateItemTypeCommand ToCommandFromResource(CreateItemTypeResource resource)
        {
            return new CreateItemTypeCommand(resource.Name, resource.Description);
        }
    }
}