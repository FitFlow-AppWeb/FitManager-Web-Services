// Members/Interfaces/REST/Transform/MemberResourceFromEntityAssembler.cs

using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Interfaces.REST.Resources;


namespace FitManager_Web_Services.Members.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler responsible for transforming <see cref="Member"/> domain entities into <see cref="MemberResource"/> DTOs.
    /// This static class encapsulates the mapping logic from the internal domain representation
    /// to the external REST API representation, ensuring that sensitive domain logic or internal structure
    /// is not directly exposed. It also handles the transformation of nested aggregate parts like <see cref="MembershipStatus"/>.
    /// </summary>
    public static class MemberResourceFromEntityAssembler
    {
        /// <summary>
        /// Converts a single <see cref="Member"/> entity to a <see cref="MemberResource"/>.
        /// </summary>
        /// <param name="entity">The <see cref="Member"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="MemberResource"/> populated with data from the entity.</returns>
        public static MemberResource ToResourceFromEntity(Member entity)
        {
            MembershipStatusResource? membershipStatusResource = null;
            
            if (entity.MembershipStatus != null)
            {
                membershipStatusResource = MembershipStatusResourceFromEntityAssembler.ToResourceFromEntity(entity.MembershipStatus);
            }

            return new MemberResource(
                entity.Id,
                entity.FirstName,
                entity.LastName,
                entity.Age,
                entity.Dni,
                entity.PhoneNumber,
                entity.Address,
                entity.Email,
                membershipStatusResource 
            );
        }

        /// <summary>
        /// Converts a collection of <see cref="Member"/> entities to an enumerable collection of <see cref="MemberResource"/>.
        /// </summary>
        /// <param name="entities">The enumerable collection of <see cref="Member"/> entities to convert.</param>
        /// <returns>An enumerable collection of <see cref="MemberResource"/> objects.</returns>
        public static IEnumerable<MemberResource> ToResourceListFromEntityList(IEnumerable<Member> entities)
        {
            return entities.Select(ToResourceFromEntity);
        }
    }
}