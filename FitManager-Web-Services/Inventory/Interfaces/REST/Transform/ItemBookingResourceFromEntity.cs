using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;
using FitManager_Web_Services.Inventory.Interfaces.REST.Resources;
using System.Collections.Generic;
using System.Linq;

namespace FitManager_Web_Services.Inventory.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler responsible for transforming <see cref="ItemBooking"/> domain entities into <see cref="ItemBookingResource"/> DTOs.
    /// This class abstracts the transformation logic from the domain to the presentation layer,
    /// ensuring encapsulation of internal models.
    /// </summary>
    public static class ItemBookingResourceFromEntityAssembler
    {
        /// <summary>
        /// Converts a single <see cref="ItemBooking"/> entity to an <see cref="ItemBookingResource"/>.
        /// If the related <see cref="Item"/> is present, it is also transformed and embedded.
        /// </summary>
        /// <param name="entity">The <see cref="ItemBooking"/> entity to transform.</param>
        /// <returns>An instance of <see cref="ItemBookingResource"/> representing the transformed data.</returns>
        public static ItemBookingResource ToResourceFromEntity(ItemBooking entity)
        {
            ItemResource? itemResource = null;

            if (entity.Item != null)
            {
                itemResource = ItemResourceFromEntityAssembler.ToResourceFromEntity(entity.Item);
            }

            return new ItemBookingResource(
                entity.Id,
                entity.ReservationDate,
                entity.UsageTime,
                entity.ClassId,
                entity.ItemId,
                itemResource
            );
        }

        /// <summary>
        /// Converts a collection of <see cref="ItemBooking"/> entities into a collection of <see cref="ItemBookingResource"/> DTOs.
        /// </summary>
        /// <param name="entities">The collection of <see cref="ItemBooking"/> entities to transform.</param>
        /// <returns>A collection of <see cref="ItemBookingResource"/> DTOs.</returns>
        public static IEnumerable<ItemBookingResource> ToResourceListFromEntityList(IEnumerable<ItemBooking> entities)
        {
            return entities.Select(ToResourceFromEntity);
        }
    }
}
