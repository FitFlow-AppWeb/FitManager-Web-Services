namespace FitManager_Web_Services.IAM.Domain.Model.Commands
{
    /// <summary>
    /// Command to update an existing user.
    /// </summary>
    /// <param name="Id">The unique identifier of the user to update.</param>
    /// <param name="Email">The new email address.</param>
    /// <param name="Password">The new password.</param>
    /// <param name="Icon">The new icon URL or identifier.</param>
    /// <param name="Subscription">The new subscription plan.</param>
    public record UpdateUserCommand(
        int Id,
        string Email,
        string Password,
        string Icon,
        string Subscription
    );
}
