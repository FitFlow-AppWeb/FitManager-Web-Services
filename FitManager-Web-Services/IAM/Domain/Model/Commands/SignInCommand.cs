namespace FitManager_Web_Services.Users.Domain.Model.Commands;

/// <summary>
/// Represents a command to sign in an existing user.
/// </summary>
/// <param name="Email">The email address of the user.</param>
/// <param name="Password">The plain-text password to verify.</param>
public record SignInCommand(string Email, string Password);
