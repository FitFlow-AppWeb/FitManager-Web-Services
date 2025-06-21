// Employees/Domain/Model/Commands/CreateSpecialtyCommand.cs

namespace FitManager_Web_Services.Employees.Domain.Model.Commands
{
    /// <summary>
    /// Command to create a new Specialty.
    /// </summary>
    /// <param name="Name">The name of the specialty.</param>
    /// <param name="Description">The description of the specialty.</param>
    /// <param name="EmployeeId">The ID of the employee to whom the specialty belongs.</param>
    public record CreateSpecialtyCommand(string Name, string Description, int EmployeeId);
}