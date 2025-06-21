namespace FitManager_Web_Services.Employees.Domain.Model.Commands
{
    /// <summary>
    /// Represents a command to update an existing employee's information.
    /// </summary>
    /// <remarks>
    /// This record type is an immutable data transfer object (DTO) that carries all the updated
    /// information for an existing employee. It's sent from the application's external boundary
    /// (e.g., REST API) to the Domain/Application layer for processing by a command handler
    /// (e.g., <see cref="Application.Internal.CommandServices.EmployeeCommandService"/>).
    /// It includes core personal details, employment terms, and the complete set of associated
    /// certification and specialty IDs that should replace any existing associations.
    /// </remarks>
    /// <param name="Id">The unique identifier of the employee to be updated.</param>
    /// <param name="FirstName">The updated first name of the employee.</param>
    /// <param name="LastName">The updated last name of the employee.</param>
    /// <param name="Age">The updated age of the employee.</param>
    /// <param name="Dni">The updated DNI (National Identity Document) of the employee.</param>
    /// <param name="PhoneNumber">The updated phone number of the employee.</param>
    /// <param name="Address">The updated address of the employee.</param>
    /// <param name="Email">The updated email address of the employee.</param>
    /// <param name="Password">The updated password for the employee's account.</param>
    /// <param name="Wage">The updated wage (salary) of the employee.</param>
    /// <param name="Role">The updated role of the employee.</param>
    /// <param name="WorkHours">The updated standard work hours per week for the employee.</param>
    public record UpdateEmployeeCommand(
        int Id,
        string FirstName,
        string LastName,
        int Age,
        int Dni,
        int PhoneNumber,
        string Address,
        string Email,
        string Password,
        float Wage,
        string Role,
        int WorkHours
    );
}