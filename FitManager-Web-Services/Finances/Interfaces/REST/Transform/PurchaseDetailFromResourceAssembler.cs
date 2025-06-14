using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Transform;

public static class PurchaseDetailFromResourceAssembler
{
    public static PurchaseDetail ToEntityFromResource(CreatePurchaseDetailResource resource)
    {
        return new PurchaseDetail(
            resource.SupplyPurchaseId,
            resource.ItemTypeId,
            resource.UnitPrice,
            resource.Quantity
        );
    }
}