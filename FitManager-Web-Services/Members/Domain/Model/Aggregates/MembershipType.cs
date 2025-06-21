// Members/Domain/Model/Aggregates/MembershipType.cs

using System.Collections.Generic;

namespace FitManager_Web_Services.Members.Domain.Model.Aggregates
{
    /// <summary>
    /// Represents a type of membership offered by the fitness manager.
    /// This is an aggregate root within the Domain, encapsulating the details of a specific
    /// membership plan, such as its name, price, duration, and benefits.
    /// </summary>
    public class MembershipType
    {
        /// <summary>
        /// Gets the unique identifier for the membership type.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the name of the membership type (e.g., "Premium", "Basic", "Annual").
        /// </summary>
        public string Name { get; private set; } // Cambiado a private set

        /// <summary>
        /// Gets a detailed description of the membership type.
        /// </summary>
        public string Description { get; private set; } // Cambiado a private set

        /// <summary>
        /// Gets the price of the membership type.
        /// </summary>
        public int Price { get; private set; } // Cambiado a private set

        /// <summary>
        /// Gets the duration of the membership (e.g., "1 Month", "3 Months", "1 Year").
        /// </summary>
        public string Duration { get; private set; } // Cambiado a private set

        /// <summary>
        /// Gets the benefits included with this membership type.
        /// </summary>
        public string Benefits { get; private set; } // Cambiado a private set

        /// <summary>
        /// Gets or sets the collection of membership statuses associated with this membership type.
        /// This represents a one-to-many relationship where one MembershipType can have many MembershipStatuses.
        /// </summary>
        public ICollection<MembershipStatus> MembershipStatuses { get; set; } = new List<MembershipStatus>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipType"/> class.
        /// </summary>
        /// <param name="name">The name of the membership type.</param>
        /// <param name="description">The description of the membership type.</param>
        /// <param name="price">The price of the membership type.</param>
        /// <param name="duration">The duration of the membership.</param>
        /// <param name="benefits">The benefits included with the membership.</param>
        public MembershipType(string name, string description, int price, string duration, string benefits)
        {
            Name = name;
            Description = description;
            Price = price;
            Duration = duration;
            Benefits = benefits;
        }

        /// <summary>
        /// Protected constructor for EF Core.
        /// Used by the ORM to materialize entities from the database.
        /// </summary>
        protected MembershipType() { }

        /// <summary>
        /// Updates the details of the membership type.
        /// </summary>
        /// <param name="name">The new name for the membership type.</param>
        /// <param name="description">The new description for the membership type.</param>
        /// <param name="price">The new price for the membership type.</param>
        /// <param name="duration">The new duration for the membership type.</param>
        /// <param name="benefits">The new benefits for the membership type.</param>
        public void Update(string name, string description, int price, string duration, string benefits)
        {
            Name = name;
            Description = description;
            Price = price;
            Duration = duration;
            Benefits = benefits;
        }
    }
}