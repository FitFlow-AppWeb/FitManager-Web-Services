namespace FitManager_Web_Services.Users.Domain.Model.Commands;

/// <summary>
/// Represents a command to create a new user.
/// Carries the necessary data from the API layer into the application for user creation.
/// </summary>
/// <param name="Email">The email address of the user.</param>
/// <param name="Password">The password for the user (hashed).</param>
/// <param name="Icon">The URL or identifier for the user's icon.</param>
/// <param name="Subscription">The initial subscription plan for the user.</param>
public record CreateUserCommand(
    string Email,
    string Password,
    string Icon,
    string Subscription
);