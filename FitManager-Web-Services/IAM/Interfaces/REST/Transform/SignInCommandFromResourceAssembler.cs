using FitManager_Web_Services.IAM.Domain.Model.Commands;
using FitManager_Web_Services.IAM.Interfaces.REST.Resources;

namespace FitManager_Web_Services.IAM.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembles a SignInCommand from a SignInResource.
    /// </summary>
    public static class SignInCommandFromResourceAssembler
    {
        public static SignInCommand ToCommand(SignInResource resource)
        {
            return new SignInCommand(resource.Email, resource.Password);
        }
    }
}
