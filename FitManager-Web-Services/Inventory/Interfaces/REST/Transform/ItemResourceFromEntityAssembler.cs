using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Interfaces.REST.Resources;
using FitManager_Web_Services.Employees.Interfaces.REST.Resources;
using FitManager_Web_Services.Employees.Interfaces.REST.Transform;

namespace FitManager_Web_Services.Inventory.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler responsible for transforming <see cref="Item"/> domain entities into <see cref="ItemResource"/> DTOs.
    /// This static class ensures that domain logic or internal structure is not directly exposed to the REST API.
    /// It also maps nested relationships such as <see cref="ItemType"/>.
    /// </summary>
    public static class ItemResourceFromEntityAssembler
    {
        /// <summary>
        /// Converts a single <see cref="Item"/> entity to a <see cref="ItemResource"/>.
        /// </summary>
        /// <param name="entity">The <see cref="Item"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="ItemResource"/> populated with data from the entity.</returns>
        public static ItemResource ToResourceFromEntity(Item entity)
        {
            ItemTypeResource? itemTypeResource = null;

            if (entity.ItemType != null)
            {
                itemTypeResource = ItemTypeResourceFromEntityAssembler.ToResourceFromEntity(entity.ItemType);
            }
            EmployeeResource? employeeResource = null;
            if (entity.Employee != null)
            {
                 employeeResource = EmployeeResourceFromEntityAssembler.ToResourceFromEntity(entity.Employee);
            }

            return new ItemResource(
                entity.Id,
                entity.LastMaintenanceDate,
                entity.NextMaintenanceDate,
                entity.Status,
                entity.EmployeeId,
                employeeResource,
                entity.ItemTypeId,
                itemTypeResource
            );
        }

        /// <summary>
        /// Converts a collection of <see cref="Item"/> entities into a collection of <see cref="ItemResource"/> DTOs.
        /// </summary>
        /// <param name="entities">The enumerable collection of <see cref="Item"/> entities.</param>
        /// <returns>An enumerable collection of <see cref="ItemResource"/> objects.</returns>
        public static IEnumerable<ItemResource> ToResourceListFromEntityList(IEnumerable<Item> entities)
        {
            return entities.Select(ToResourceFromEntity);
        }
    }
}
