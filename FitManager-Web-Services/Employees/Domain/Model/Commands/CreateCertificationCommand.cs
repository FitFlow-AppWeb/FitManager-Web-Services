// Employees/Domain/Model/Commands/CreateCertificationCommand.cs

namespace FitManager_Web_Services.Employees.Domain.Model.Commands
{
    /// <summary>
    /// Command to create a new Certification.
    /// </summary>
    /// <param name="Name">The name of the certification.</param>
    /// <param name="Description">The description of the certification.</param>
    /// <param name="EmployeeId">The ID of the employee to whom the certification belongs.</param>
    public record CreateCertificationCommand(string Name, string Description, int EmployeeId);
}