namespace FitManager_Web_Services.IAM.Domain.Model.Queries;

/// <summary>
/// Represents a query to retrieve a user by email.
/// </summary>
/// <param name="Email">The email to search.</param>
public record GetUserByUsernameQuery(string Email);
