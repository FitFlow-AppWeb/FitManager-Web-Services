using FitManager_Web_Services.IAM.Domain.Model;
using FitManager_Web_Services.IAM.Interfaces.REST.Resources;

namespace FitManager_Web_Services.IAM.Interfaces.REST.Transform
{
    /// <summary>
    /// Transforms User + token into AuthenticatedUserResource.
    /// </summary>
    public static class AuthenticatedUserResourceFromEntityAssembler
    {
        public static AuthenticatedUserResource ToResource(User user, string token)
        {
            return new AuthenticatedUserResource(user.Id, user.Email, token);
        }
    }
}
