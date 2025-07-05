using FitManager_Web_Services.IAM.Domain.Model.Commands;
using FitManager_Web_Services.IAM.Interfaces.REST.Resources;

namespace FitManager_Web_Services.IAM.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembles a CreateUserCommand from a CreateUserResource.
    /// </summary>
    public static class CreateUserCommandFromResourceAssembler
    {
        /// <summary>
        /// Converts a CreateUserResource into a CreateUserCommand.
        /// </summary>
        public static CreateUserCommand ToCommandFromResource(CreateUserResource resource) =>
            new(
                resource.Email,
                resource.Password,
                resource.Icon,
                resource.Subscription
            );
    }
}
