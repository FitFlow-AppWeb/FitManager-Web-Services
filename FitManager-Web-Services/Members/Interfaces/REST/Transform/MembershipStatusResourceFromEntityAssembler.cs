// Members/Interfaces/REST/Transform/MembershipStatusResourceFromEntityAssembler.cs

using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Members.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler responsible for transforming a <see cref="MembershipStatus"/> domain entity into a <see cref="MembershipStatusResource"/> DTO.
    /// This static class encapsulates the mapping logic from the internal domain representation
    /// to the external REST API representation, ensuring that related entities like <see cref="MembershipType"/>
    /// are also transformed and included in the resource.
    /// </summary>
    public static class MembershipStatusResourceFromEntityAssembler
    {
        /// <summary>
        /// Converts a single <see cref="MembershipStatus"/> entity to a <see cref="MembershipStatusResource"/>.
        /// </summary>
        /// <param name="entity">The <see cref="MembershipStatus"/> entity to convert.</param>
        /// <returns>A new instance of <see cref="MembershipStatusResource"/> populated with data from the entity.</returns>
        public static MembershipStatusResource ToResourceFromEntity(MembershipStatus entity)
        {
            MembershipTypeResource? membershipTypeResource = null;
            
            if (entity.MembershipType != null)
            {
                membershipTypeResource = MembershipTypeResourceFromEntityAssembler.ToResourceFromEntity(entity.MembershipType);
            }

            return new MembershipStatusResource(
                entity.Id, 
                entity.StartDate,
                entity.EndDate,
                entity.Status,
                entity.MembershipTypeId,
                membershipTypeResource 
            );
        }
    }
}