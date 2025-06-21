using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Transform;

/// <summary>
/// Provides static methods to assemble <see cref="SupplyPurchaseResource"/> DTOs from <see cref="SupplyPurchase"/> entities.
/// </summary>
/// <remarks>
/// This assembler is responsible for transforming domain entities into API resources (DTOs),
/// ensuring that only necessary data is exposed through the RESTful interface.
/// It also handles the transformation of associated <see cref="SupplyPurchase.PurchaseDetails"/> into their respective resource DTOs.
/// </remarks>
public static class SupplyPurchaseToResourceAssembler
{
    /// <summary>
    /// Converts a <see cref="SupplyPurchase"/> entity to a <see cref="SupplyPurchaseResource"/> DTO.
    /// </summary>
    /// <param name="entity">The <see cref="SupplyPurchase"/> entity to convert. This entity should ideally have its
    /// <see cref="SupplyPurchase.PurchaseDetails"/> navigation property loaded if they are to be included in the resource.</param>
    /// <returns>A new <see cref="SupplyPurchaseResource"/> representing the converted entity,
    /// potentially including its purchase details.</returns>
    public static SupplyPurchaseResource ToResourceFromEntity(SupplyPurchase entity)
    {
        return new SupplyPurchaseResource(
            entity.Id,
            entity.Date,
            entity.Amount,
            entity.Method,
            entity.Currency,
            entity.VendorName,
            // Transform the collection of PurchaseDetail entities into PurchaseDetailResource DTOs.
            // If PurchaseDetails is null, it defaults to an empty list to prevent NullReferenceException.
            PurchaseDetailToResourceAssembler.ToResourceListFromEntityList(entity.PurchaseDetails ?? new List<PurchaseDetail>()).ToList()
        );
    }

    /// <summary>
    /// Converts a collection of <see cref="SupplyPurchase"/> entities to an enumerable of <see cref="SupplyPurchaseResource"/> DTOs.
    /// </summary>
    /// <param name="entities">The collection of <see cref="SupplyPurchase"/> entities to convert.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="SupplyPurchaseResource"/> representing the converted entities.</returns>
    public static IEnumerable<SupplyPurchaseResource> ToResourceListFromEntityList(IEnumerable<SupplyPurchase> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
}