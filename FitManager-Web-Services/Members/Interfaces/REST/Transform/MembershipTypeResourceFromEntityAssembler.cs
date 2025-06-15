// Members/Interfaces/REST/Transform/MembershipTypeResourceFromEntityAssembler.cs

using FitManager_Web_Services.Members.Domain.Model.Aggregates; 
using FitManager_Web_Services.Members.Interfaces.REST.Resources; 

namespace FitManager_Web_Services.Members.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler responsible for transforming <see cref="MembershipType"/> domain entities into <see cref="MembershipTypeResource"/> DTOs.
    /// This static class encapsulates the mapping logic from the internal domain representation
    /// to the external REST API representation, ensuring a clean separation of concerns.
    /// </summary>
    public static class MembershipTypeResourceFromEntityAssembler
    {
        /// <summary>
        /// Converts a single <see cref="MembershipType"/> entity to a <see cref="MembershipTypeResource"/>.
        /// </summary>
        /// <param name="entity">The <see cref="MembershipType"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="MembershipTypeResource"/> populated with data from the entity.</returns>
        public static MembershipTypeResource ToResourceFromEntity(MembershipType entity)
        {
            return new MembershipTypeResource(
                entity.Id, 
                entity.Name,
                entity.Description, 
                entity.Price, 
                entity.Duration, 
                entity.Benefits);
        }

        /// <summary>
        /// Converts a collection of <see cref="MembershipType"/> entities to an enumerable collection of <see cref="MembershipTypeResource"/>.
        /// </summary>
        /// <param name="entityList">The enumerable collection of <see cref="MembershipType"/> entities to convert.</param>
        /// <returns>An enumerable collection of <see cref="MembershipTypeResource"/> objects.</returns>
        public static IEnumerable<MembershipTypeResource> ToResourceListFromEntityList(IEnumerable<MembershipType> entityList)
        {
            return entityList.Select(ToResourceFromEntity);
        }
    }
}