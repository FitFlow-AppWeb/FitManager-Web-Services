// Members/Domain/Model/Commands/UpdateMembershipTypeCommand.cs

namespace FitManager_Web_Services.Members.Domain.Model.Commands
{
    /// <summary>
    /// Command to update an existing membership type.
    /// </summary>
    /// <param name="Id">The unique identifier of the membership type to update.</param>
    /// <param name="Name">The new name of the membership type.</param>
    /// <param name="Description">The new detailed description of the membership type.</param>
    /// <param name="Price">The new price of the membership type.</param>
    /// <param name="Duration">The new duration of the membership.</param>
    /// <param name="Benefits">The new benefits included with this membership type.</param>
    public record UpdateMembershipTypeCommand(int Id, string Name, string Description, int Price, string Duration, string Benefits);
}