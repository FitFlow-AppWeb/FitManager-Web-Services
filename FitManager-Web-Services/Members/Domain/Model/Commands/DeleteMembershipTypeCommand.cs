// Members/Domain/Model/Commands/DeleteMembershipTypeCommand.cs

namespace FitManager_Web_Services.Members.Domain.Model.Commands
{
    /// <summary>
    /// Command to delete a membership type by its ID.
    /// </summary>
    /// <param name="Id">The unique identifier of the membership type to delete.</param>
    public record DeleteMembershipTypeCommand(int Id);
}