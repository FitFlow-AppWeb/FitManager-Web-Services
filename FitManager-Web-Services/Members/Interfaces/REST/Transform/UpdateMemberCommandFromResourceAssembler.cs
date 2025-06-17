// Members/Interfaces/REST/Transform/UpdateMemberCommandFromResourceAssembler.cs

using FitManager_Web_Services.Members.Domain.Model.Commands;
using FitManager_Web_Services.Members.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Members.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler responsible for transforming an <see cref="UpdateMemberResource"/> and a member ID
    /// into an <see cref="UpdateMemberCommand"/>.
    /// This static class encapsulates the mapping logic between the REST-specific input DTO
    /// and the domain-level command, adhering to the Clean Architecture principle of
    /// separating external concerns from internal domain logic.
    /// </summary>
    public static class UpdateMemberCommandFromResourceAssembler
    {
        /// <summary>
        /// Converts an <see cref="UpdateMemberResource"/> and a member ID to an <see cref="UpdateMemberCommand"/>.
        /// </summary>
        /// <param name="memberId">The unique identifier of the member to be updated.</param>
        /// <param name="resource">The <see cref="UpdateMemberResource"/> containing the updated data.</param>
        /// <returns>A new instance of <see cref="UpdateMemberCommand"/> populated with data from the resource and the provided ID.</returns>
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