namespace FitManager_Web_Services.IAM.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource returned after successful authentication.
    /// </summary>
    public record AuthenticatedUserResource(int Id, string Email, string Token);
}
