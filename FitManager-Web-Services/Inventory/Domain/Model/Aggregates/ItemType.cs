namespace FitManager_Web_Services.Inventory.Domain.Model.Aggregates
{
    public class ItemType
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();

        public ItemType(string name, string description)
        {
            Name = name;
            Description = description;
        }

        protected ItemType() { }
    }
}