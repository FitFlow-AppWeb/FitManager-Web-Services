namespace FitManager_Web_Services.Users.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the data returned by the Users API for a user.
    /// </summary>
    public record UserResource(
        int Id,
        string Email,
        string Icon,
        string Subscription
    );
}
