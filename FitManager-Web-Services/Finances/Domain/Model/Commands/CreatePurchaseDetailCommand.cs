namespace FitManager_Web_Services.Finances.Domain.Model.Commands
{
    public record CreatePurchaseDetailCommand(
        float UnitPrice,
        int Quantity,
        int SupplyPurchaseId,
        int ItemTypeId
    );
}