namespace FitManager_Web_Services.Employees.Domain.Model.Commands
{
    /// <summary>
    /// Represents a command to delete an existing employee.
    /// </summary>
    /// <remarks>
    /// This record type is an immutable data transfer object (DTO) that carries the unique identifier
    /// of the employee to be deleted from the system. It is processed by a command handler
    /// (e.g., <see cref="Application.Internal.CommandServices.EmployeeCommandService"/>)
    /// in the Domain/Application layer.
    /// </remarks>
    /// <param name="Id">The unique identifier of the employee to be deleted.</param>
    public record DeleteEmployeeCommand(int Id);
}