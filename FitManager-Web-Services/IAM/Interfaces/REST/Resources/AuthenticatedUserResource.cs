namespace FitManager_Web_Services.Users.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource returned after successful authentication.
    /// </summary>
    public record AuthenticatedUserResource(int Id, string Email, string Token);
}
