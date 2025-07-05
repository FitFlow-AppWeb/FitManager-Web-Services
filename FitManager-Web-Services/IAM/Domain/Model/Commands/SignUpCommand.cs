namespace FitManager_Web_Services.IAM.Domain.Model.Commands;

/// <summary>
/// Represents a command to sign up a new user.
/// </summary>
/// <param name="Email">The email address of the user.</param>
/// <param name="Password">The plain-text password to hash and store.</param>
public record SignUpCommand(string Email, string Password);
