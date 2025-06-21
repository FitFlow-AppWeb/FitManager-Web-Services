using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Transform;

/// <summary>
/// Provides static methods to assemble <see cref="BookingResource"/> from <see cref="Booking"/> entities.
/// </summary>
/// <remarks>
/// This assembler is responsible for transforming domain entities into API resources (DTOs),
/// ensuring that only necessary data is exposed through the RESTful interface.
/// </remarks>
public static class BookingResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a <see cref="Booking"/> entity to a <see cref="BookingResource"/> DTO.
    /// </summary>
    /// <param name="entity">The <see cref="Booking"/> entity to convert.</param>
    /// <returns>A new <see cref="BookingResource"/> representing the converted entity.</returns>
    public static BookingResource ToResource(Booking entity)
    {
        return new BookingResource(
            entity.Id,
            entity.MemberId,
            entity.ClassId,
            entity.Date);
    }
}