// Members/Interfaces/REST/Transform/UpdateMembershipTypeCommandFromResourceAssembler.cs

using FitManager_Web_Services.Members.Domain.Model.Commands;
using FitManager_Web_Services.Members.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Members.Interfaces.REST.Transform
{
    /// <summary>
    /// Static class for transforming an UpdateMembershipTypeResource into an UpdateMembershipTypeCommand.
    /// </summary>
    public static class UpdateMembershipTypeCommandFromResourceAssembler
    {
        /// <summary>
        /// Converts an <see cref="UpdateMembershipTypeResource"/> to an <see cref="UpdateMembershipTypeCommand"/>, including the entity ID.
        /// </summary>
        /// <param name="id">The ID of the membership type to update.</param>
        /// <param name="resource">The resource containing the updated data.</param>
        /// <returns>A new <see cref="UpdateMembershipTypeCommand"/> instance.</returns>
        public static UpdateMembershipTypeCommand ToCommandFromResource(int id, UpdateMembershipTypeResource resource)
        {
            return new UpdateMembershipTypeCommand(id, resource.Name, resource.Description, resource.Price, resource.Duration, resource.Benefits);
        }
    }
}