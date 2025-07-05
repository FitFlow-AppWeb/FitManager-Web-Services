using System.ComponentModel.DataAnnotations;

namespace FitManager_Web_Services.IAM.Domain.Model.Commands;

/// <summary>
/// Represents a command to sign up a new user.
/// </summary>
/// <param name="Email">The email address of the user.</param>
/// <param name="Password">The plain-text password to hash and store.</param>
/// <param name="Icon">The URL or identifier of the user's icon.</param>
/// <param name="Subscription">The initial subscription type (e.g., "basic", "premium").</param>
public record SignUpCommand(
    [Required]  string Email, 
    [Required] string Password, 
    [Required]
    [Url]
    string Icon, 
    [Required] string Subscription
    );