// Members/Application/Internal/CommandServices/IMembershipTypeCommandService.cs

using FitManager_Web_Services.Members.Domain.Model.Commands;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Members.Application.Internal.CommandServices
{
    /// <summary>
    /// Defines the contract for handling commands related to MembershipType entities.
    /// </summary>
    public interface IMembershipTypeCommandService
    {
        /// <summary>
        /// Handles the creation of a new membership type.
        /// </summary>
        /// <param name="command">The command containing the data for the new membership type.</param>
        /// <returns>The created MembershipType entity if successful, otherwise null.</returns>
        Task<MembershipType?> Handle(CreateMembershipTypeCommand command);

        /// <summary>
        /// Handles the update of an existing membership type.
        /// </summary>
        /// <param name="command">The command containing the ID and updated data for the membership type.</param>
        /// <returns>The updated MembershipType entity if successful, otherwise null.</returns>
        Task<MembershipType?> Handle(UpdateMembershipTypeCommand command);

        /// <summary>
        /// Handles the deletion of a membership type.
        /// </summary>
        /// <param name="command">The command containing the ID of the membership type to delete.</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        Task<bool> Handle(DeleteMembershipTypeCommand command);
    }
}