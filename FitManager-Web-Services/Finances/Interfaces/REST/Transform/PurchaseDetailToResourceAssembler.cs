using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Transform;

/// <summary>
/// Provides static methods to assemble <see cref="PurchaseDetailResource"/> DTOs from <see cref="PurchaseDetail"/> entities.
/// </summary>
/// <remarks>
/// This assembler is responsible for transforming domain entities into API resources (DTOs),
/// ensuring that only necessary data is exposed through the RESTful interface.
/// </remarks>
public static class PurchaseDetailToResourceAssembler
{
    /// <summary>
    /// Converts a <see cref="PurchaseDetail"/> entity to a <see cref="PurchaseDetailResource"/> DTO.
    /// </summary>
    /// <param name="entity">The <see cref="PurchaseDetail"/> entity to convert.</param>
    /// <returns>A new <see cref="PurchaseDetailResource"/> representing the converted entity.</returns>
    public static PurchaseDetailResource ToResourceFromEntity(PurchaseDetail entity)
    {
        return new PurchaseDetailResource(
            entity.Id,
            entity.SupplyPurchaseId,
            entity.ItemTypeId,
            entity.UnitPrice,
            entity.Quantity
        );
    }

    /// <summary>
    /// Converts a collection of <see cref="PurchaseDetail"/> entities to an enumerable of <see cref="PurchaseDetailResource"/> DTOs.
    /// </summary>
    /// <param name="entities">The collection of <see cref="PurchaseDetail"/> entities to convert.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="PurchaseDetailResource"/> representing the converted entities.</returns>
    public static IEnumerable<PurchaseDetailResource> ToResourceListFromEntityList(IEnumerable<PurchaseDetail> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
}