using FitManager_Web_Services.Users.Domain.Model.Commands;
using FitManager_Web_Services.Users.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Users.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembles an UpdateUserCommand from a UpdateUserResource.
    /// </summary>
    public static class UpdateUserCommandFromResourceAssembler
    {
        /// <summary>
        /// Converts an UpdateUserResource and an ID into an UpdateUserCommand.
        /// </summary>
        public static UpdateUserCommand ToCommandFromResource(int id, UpdateUserResource resource) =>
            new(
                id,
                resource.Email,
                resource.Password,
                resource.Icon,
                resource.Subscription
            );
    }
}
