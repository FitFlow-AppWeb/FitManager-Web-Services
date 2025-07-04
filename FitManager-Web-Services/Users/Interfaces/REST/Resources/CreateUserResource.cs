namespace FitManager_Web_Services.Users.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource for creating a new user via the REST API.
    /// This DTO carries the necessary data from the client to the application layer.
    /// </summary>
    /// <param name="Email">The email address of the user.</param>
    /// <param name="Password">The password for the user.</param>
    /// <param name="Icon">The URL or identifier for the user's icon.</param>
    /// <param name="Subscription">The initial subscription plan for the user.</param>
    public record CreateUserResource(
        string Email,
        string Password,
        string Icon,
        string Subscription
    );
}
