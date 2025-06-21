// Employees/Domain/Model/Commands/DeleteSpecialtyCommand.cs

namespace FitManager_Web_Services.Employees.Domain.Model.Commands
{
    /// <summary>
    /// Command to delete an existing Specialty by its ID.
    /// </summary>
    /// <param name="Id">The unique identifier of the specialty to delete.</param>
    public record DeleteSpecialtyCommand(int Id);
}