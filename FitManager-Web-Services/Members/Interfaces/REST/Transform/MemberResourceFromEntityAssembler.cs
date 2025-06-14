using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Members.Interfaces.REST.Transform
{
    public static class MemberResourceFromEntityAssembler
    {
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

        public static IEnumerable<MemberResource> ToResourceListFromEntityList(IEnumerable<Member> entities)
        {
            return entities.Select(ToResourceFromEntity);
        }
    }
}