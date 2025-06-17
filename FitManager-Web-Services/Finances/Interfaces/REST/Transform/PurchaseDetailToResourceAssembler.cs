using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Transform;

public static class PurchaseDetailToResourceAssembler
{
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

    public static IEnumerable<PurchaseDetailResource> ToResourceListFromEntityList(IEnumerable<PurchaseDetail> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
}