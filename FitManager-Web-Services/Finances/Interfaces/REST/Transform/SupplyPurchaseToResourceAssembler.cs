using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;
using System.Collections.Generic; 
using System.Linq;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Transform;

public static class SupplyPurchaseToResourceAssembler
{
    public static SupplyPurchaseResource ToResourceFromEntity(SupplyPurchase entity)
    {
        return new SupplyPurchaseResource(
            entity.Id,
            entity.Date,
            entity.Amount,
            entity.Method,
            entity.Currency,
            entity.VendorName,
            PurchaseDetailToResourceAssembler.ToResourceListFromEntityList(entity.PurchaseDetails ?? new List<PurchaseDetail>()).ToList()
        );
    }

    public static IEnumerable<SupplyPurchaseResource> ToResourceListFromEntityList(IEnumerable<SupplyPurchase> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
}