// Members/Domain/Model/Commands/DeleteMemberCommand.cs

namespace FitManager_Web_Services.Members.Domain.Model.Commands
{
    /// <summary>
    /// Represents a command to delete an existing member.
    /// This record type is an immutable data transfer object (DTO) that carries the unique identifier
    /// of the member to be deleted. It is processed by a command handler
    /// (e.g., <see cref="Application.Internal.CommandServices.MemberCommandService"/>).
    /// </summary>
    /// <param name="Id">The unique identifier of the member to be deleted.</param>
    public record DeleteMemberCommand(int Id);
}