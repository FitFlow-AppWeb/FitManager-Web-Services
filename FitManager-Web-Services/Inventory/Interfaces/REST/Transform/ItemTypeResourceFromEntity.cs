using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Inventory.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler responsible for transforming <see cref="ItemType"/> domain entities into <see cref="ItemTypeResource"/> DTOs.
    /// This static class abstracts the mapping logic between domain models and REST-facing data transfer objects.
    /// </summary>
    public static class ItemTypeResourceFromEntityAssembler
    {
        /// <summary>
        /// Converts a single <see cref="ItemType"/> entity into an <see cref="ItemTypeResource"/>.
        /// </summary>
        /// <param name="entity">The <see cref="ItemType"/> domain entity to convert.</param>
        /// <returns>An instance of <see cref="ItemTypeResource"/> populated with data from the entity.</returns>
        public static ItemTypeResource ToResourceFromEntity(ItemType entity)
        {
            return new ItemTypeResource(
                entity.Id,
                entity.Name,
                entity.Description
            );
        }

        /// <summary>
        /// Converts a collection of <see cref="ItemType"/> entities into a collection of <see cref="ItemTypeResource"/> DTOs.
        /// </summary>
        /// <param name="entities">An enumerable collection of <see cref="ItemType"/> entities.</param>
        /// <returns>An enumerable collection of <see cref="ItemTypeResource"/> objects.</returns>
        public static IEnumerable<ItemTypeResource> ToResourceListFromEntityList(IEnumerable<ItemType> entities)
        {
            return entities.Select(ToResourceFromEntity);
        }
    }
}