using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Transform;

/// <summary>
/// Provides static methods to assemble <see cref="PurchaseDetail"/> entities from <see cref="CreatePurchaseDetailResource"/> DTOs.
/// </summary>
/// <remarks>
/// This assembler acts as a bridge between the API's data transfer objects (DTOs) and the domain's entities.
/// It translates the incoming <see cref="CreatePurchaseDetailResource"/> (from an HTTP request) into a
/// <see cref="PurchaseDetail"/> entity that can be used by the domain layer.
/// </remarks>
public static class PurchaseDetailFromResourceAssembler
{
    /// <summary>
    /// Converts a <see cref="CreatePurchaseDetailResource"/> DTO to a <see cref="PurchaseDetail"/> entity.
    /// </summary>
    /// <param name="resource">The <see cref="CreatePurchaseDetailResource"/> to convert.</param>
    /// <param name="supplyPurchaseId">The ID of the parent supply purchase to which this detail belongs.</param>
    /// <returns>A new <see cref="PurchaseDetail"/> entity representing the converted resource.</returns>
    public static PurchaseDetail ToEntityFromResource(
        CreatePurchaseDetailResource resource,
        int supplyPurchaseId 
    )
    {
        return new PurchaseDetail(
            supplyPurchaseId,
            resource.ItemTypeId,
            resource.UnitPrice,
            resource.Quantity
        );
    }
}