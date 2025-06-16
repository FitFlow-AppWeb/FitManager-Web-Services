namespace FitManager_Web_Services.Inventory.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents a type or category of item (e.g., treadmill, dumbbell, mat).
    /// </summary>
    public class ItemType
    {
        /// <summary>
        /// Gets the unique identifier of the item type.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the name of the item type.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description of the item type.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the collection of items associated with this type.
        /// </summary>
        public ICollection<Item> Items { get; private set; } = new List<Item>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemType"/> class.
        /// </summary>
        /// <param name="name">Name of the item type (required).</param>
        /// <param name="description">Description of the item type.</param>
        public ItemType(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be null or empty.", nameof(description));

            Name = name;
            Description = description;
        }

        /// <summary>
        /// Protected constructor for EF Core.
        /// </summary>
        protected ItemType() { }

        /// <summary>
        /// Updates the name and description of the item type.
        /// </summary>
        /// <param name="name">New name (required).</param>
        /// <param name="description">New description (required).</param>
        public void Update(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be null or empty.", nameof(description));

            Name = name;
            Description = description;
        }
    }
}
