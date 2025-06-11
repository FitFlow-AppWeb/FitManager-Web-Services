using FitManager_Web_Services.Members.Domain.Model.Commands;
using FitManager_Web_Services.Members.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Members.Interfaces.REST.Transform
{
    public static class CreateMemberCommandFromResourceAssembler
    {
        public static CreateMemberCommand ToCommandFromResource(CreateMemberResource resource)
        {
            return new CreateMemberCommand(
                resource.FirstName,
                resource.LastName,
                resource.Age,
                resource.Dni,
                resource.PhoneNumber,
                resource.Address,
                resource.Email
            );
        }
    }
}