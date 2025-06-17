namespace FitManager_Web_Services.Finances.Domain.Model.Aggregates
{
    public class SupplyPurchase
    {
        public int Id { get; private set; }

        public DateTime Date { get; set; }
        public float Amount { get; set; }
        public string Method { get; set; }
        public string Currency { get; set; }
        public string VendorName { get; set; }

        public ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

        public SupplyPurchase(DateTime date, float amount, string method, string currency, string vendorName)
        {
            Date = date;
            Amount = amount;
            Method = method;
            Currency = currency;
            VendorName = vendorName;
        }

        protected SupplyPurchase() { }
    }
}
