using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Members.Interfaces.REST.Transform
{
    public static class MembershipTypeResourceFromEntityAssembler
    {
        public static MembershipTypeResource ToResourceFromEntity(MembershipType entity)
        {
            return new MembershipTypeResource(
                entity.Id,
                entity.Name,
                entity.Description,
                entity.Price,
                entity.Duration,
                entity.Benefits
            );
        }
    }
}