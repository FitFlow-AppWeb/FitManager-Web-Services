using FitManager_Web_Services.IAM.Domain.Model.Commands;
using FitManager_Web_Services.IAM.Interfaces.REST.Resources;

namespace FitManager_Web_Services.IAM.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembles a SignUpCommand from a SignUpResource.
    /// </summary>
    public static class SignUpCommandFromResourceAssembler
    {
        public static SignUpCommand ToCommand(SignUpResource resource)
        {
            return new SignUpCommand(resource.Email, resource.Password);
        }
    }
}
