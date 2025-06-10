using FitManager_Web_Services.Inventory.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Finances.Domain.Model.Aggregates
{
    public class PurchaseDetail
    {
        public int Id { get; private set; }

        public int SupplyPurchaseId { get; set; }
        public SupplyPurchase SupplyPurchase { get; set; }

        public int ItemTypeId { get; set; }
        public ItemType ItemType { get; set; }

        public float UnitPrice { get; set; }
        public int Quantity { get; set; }

        public PurchaseDetail(int supplyPurchaseId, int itemTypeId, float unitPrice, int quantity)
        {
            SupplyPurchaseId = supplyPurchaseId;
            ItemTypeId = itemTypeId;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        protected PurchaseDetail() { }
    }
}