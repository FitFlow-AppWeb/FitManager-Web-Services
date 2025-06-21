// Members/Interfaces/REST/Transform/CreateMembershipTypeCommandFromResourceAssembler.cs

using FitManager_Web_Services.Members.Domain.Model.Commands;
using FitManager_Web_Services.Members.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Members.Interfaces.REST.Transform
{
    /// <summary>
    /// Static class for transforming a CreateMembershipTypeResource into a CreateMembershipTypeCommand.
    /// </summary>
    public static class CreateMembershipTypeCommandFromResourceAssembler
    {
        /// <summary>
        /// Converts a <see cref="CreateMembershipTypeResource"/> to a <see cref="CreateMembershipTypeCommand"/>.
        /// </summary>
        /// <param name="resource">The resource to convert.</param>
        /// <returns>A new <see cref="CreateMembershipTypeCommand"/> instance.</returns>
        public static CreateMembershipTypeCommand ToCommandFromResource(CreateMembershipTypeResource resource)
        {
            return new CreateMembershipTypeCommand(resource.Name, resource.Description, resource.Price, resource.Duration, resource.Benefits);
        }
    }
}