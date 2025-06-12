using FitManager_Web_Services.Members.Domain.Model.Commands;
using FitManager_Web_Services.Members.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Members.Interfaces.REST.Transform
{
    public static class UpdateMemberCommandFromResourceAssembler
    {
        public static UpdateMemberCommand ToCommandFromResource(int memberId, UpdateMemberResource resource)
        {
            return new UpdateMemberCommand(
                memberId,
                resource.FirstName,
                resource.LastName,
                resource.Age,
                resource.Dni,
                resource.PhoneNumber,
                resource.Address,
                resource.Email,
                resource.StartDate,
                resource.EndDate,
                resource.Status,
                resource.MembershipTypeId
            );
        }
    }
}