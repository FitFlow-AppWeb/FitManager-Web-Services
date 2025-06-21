// Employees/Domain/Model/Commands/DeleteCertificationCommand.cs

namespace FitManager_Web_Services.Employees.Domain.Model.Commands
{
    /// <summary>
    /// Command to delete an existing Certification by its ID.
    /// </summary>
    /// <param name="Id">The unique identifier of the certification to delete.</param>
    public record DeleteCertificationCommand(int Id);
}