namespace FitManager_Web_Services.Users.Domain.Model.Commands
{
    /// <summary>
    /// Command to delete an existing user.
    /// </summary>
    /// <param name="Id">The unique identifier of the user to delete.</param>
    public record DeleteUserCommand(int Id);
}
