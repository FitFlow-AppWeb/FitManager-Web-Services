// Members/Interfaces/REST/Transform/MembershipTypeResourceFromEntityAssembler.cs

using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Interfaces.REST.Resources;
using System.Collections.Generic;
using System.Linq; // Necesario para el método Select

namespace FitManager_Web_Services.Members.Interfaces.REST.Transform
{
    /// <summary>
    /// Static class for transforming MembershipType entities into MembershipTypeResource DTOs.
    /// </summary>
    public static class MembershipTypeResourceFromEntityAssembler
    {
        /// <summary>
        /// Converts a single <see cref="MembershipType"/> entity to a <see cref="MembershipTypeResource"/>.
        /// </summary>
        /// <param name="entity">The MembershipType entity to convert.</param>
        /// <returns>A new <see cref="MembershipTypeResource"/> instance.</returns>
        public static MembershipTypeResource ToResourceFromEntity(MembershipType entity)
        {
            return new MembershipTypeResource(entity.Id, entity.Name, entity.Description, entity.Price, entity.Duration, entity.Benefits);
        }

        /// <summary>
        /// Converts a collection of <see cref="MembershipType"/> entities to an enumerable collection of <see cref="MembershipTypeResource"/>.
        /// </summary>
        /// <param name="entities">The collection of MembershipType entities to convert.</param>
        /// <returns>An enumerable collection of <see cref="MembershipTypeResource"/> instances.</returns>
        public static IEnumerable<MembershipTypeResource> ToResourceFromEntities(IEnumerable<MembershipType> entities) // <-- NUEVO MÉTODO
        {
            return entities.Select(ToResourceFromEntity);
        }
    }
}