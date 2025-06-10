namespace FitManager_Web_Services.Inventory.Domain.Model.Aggregates
{
    public class Item
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int ItemTypeId { get; set; }
        public ItemType ItemType { get; set; }

        public ICollection<ItemBooking> ItemBookings { get; set; } = new List<ItemBooking>();

        public Item(string name, string description, int itemTypeId)
        {
            Name = name;
            Description = description;
            ItemTypeId = itemTypeId;
        }

        protected Item() { }
    }
}