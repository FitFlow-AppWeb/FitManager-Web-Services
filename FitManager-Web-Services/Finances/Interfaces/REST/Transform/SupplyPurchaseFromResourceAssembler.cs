using FitManager_Web_Services.Finances.Domain.Model.Aggregates;
using FitManager_Web_Services.Finances.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Finances.Interfaces.REST.Transform;

public static class SupplyPurchaseFromResourceAssembler
{
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