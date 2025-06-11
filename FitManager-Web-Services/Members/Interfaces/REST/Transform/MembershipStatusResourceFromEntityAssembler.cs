using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Members.Interfaces.REST.Transform
{
    public static class MembershipStatusResourceFromEntityAssembler
    {
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