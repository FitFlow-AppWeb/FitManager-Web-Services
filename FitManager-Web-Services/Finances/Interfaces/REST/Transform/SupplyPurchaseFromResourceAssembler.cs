using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Transform;

/// <summary>
/// Provides static methods to assemble <see cref="SupplyPurchase"/> entities from <see cref="CreateSupplyPurchaseResource"/> DTOs.
/// </summary>
/// <remarks>
/// This assembler acts as a bridge between the API's data transfer objects (DTOs) and the domain's entities.
/// It translates the incoming <see cref="CreateSupplyPurchaseResource"/> (from an HTTP request) into a
/// <see cref="SupplyPurchase"/> entity that can be used by the domain layer.
/// </remarks>
public static class SupplyPurchaseFromResourceAssembler
{
    /// <summary>
    /// Converts a <see cref="CreateSupplyPurchaseResource"/> DTO to a <see cref="SupplyPurchase"/> entity.
    /// </summary>
    /// <param name="resource">The <see cref="CreateSupplyPurchaseResource"/> to convert.</param>
    /// <returns>A new <see cref="SupplyPurchase"/> entity representing the converted resource.</returns>
    public static SupplyPurchase ToEntityFromResource(CreateSupplyPurchaseResource resource)
    {
        return new SupplyPurchase(
            resource.Date,
            resource.Amount,
            resource.Method,
            resource.Currency,
            resource.VendorName
        );
    }
}