// Members/Interfaces/REST/Transform/CreateMemberCommandFromResourceAssembler.cs

using FitManager_Web_Services.Members.Domain.Model.Commands;
using FitManager_Web_Services.Members.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Members.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler responsible for transforming a <see cref="CreateMemberResource"/> into a <see cref="CreateMemberCommand"/>.
    /// This static class encapsulates the mapping logic between the REST-specific input DTO
    /// and the domain-level command, adhering to the Clean Architecture principle of
    /// keeping external concerns separate from internal domain logic.
    /// </summary>
    public static class CreateMemberCommandFromResourceAssembler
    {
        /// <summary>
        /// Converts a <see cref="CreateMemberResource"/> to a <see cref="CreateMemberCommand"/>.
        /// </summary>
        /// <param name="resource">The <see cref="CreateMemberResource"/> to convert.</param>
        /// <returns>A new instance of <see cref="CreateMemberCommand"/> populated with data from the resource.</returns>
        public static CreateMemberCommand ToCommandFromResource(CreateMemberResource resource)
        {
            return new CreateMemberCommand(
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