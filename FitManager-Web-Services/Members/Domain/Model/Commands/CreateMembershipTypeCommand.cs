// Members/Domain/Model/Commands/CreateMembershipTypeCommand.cs

namespace FitManager_Web_Services.Members.Domain.Model.Commands
{
    /// <summary>
    /// Command to create a new membership type.
    /// </summary>
    /// <param name="Name">The name of the membership type.</param>
    /// <param name="Description">A detailed description of the membership type.</param>
    /// <param name="Price">The price of the membership type.</param>
    /// <param name="Duration">The duration of the membership (e.g., "1 Month", "1 Year").</param>
    /// <param name="Benefits">The benefits included with this membership type.</param>
    public record CreateMembershipTypeCommand(string Name, string Description, int Price, string Duration, string Benefits);
}