namespace FitManager_Web_Services.Employees.Domain.Model.Commands
{
    /// <summary>
    /// Represents a command to create a new employee.
    /// </summary>
    /// <remarks>
    /// This record type is an immutable data transfer object (DTO) that carries all the necessary
    /// information from the application's external boundary (e.g., REST API) into the Domain/Application layer
    /// for processing by a command handler (e.g., <see cref="Application.Internal.CommandServices.EmployeeCommandService"/>).
    /// It includes core personal details, employment terms, and identifiers for associated certifications and specialties.
    /// </remarks>
    /// <param name="FirstName">The first name of the employee.</param>
    /// <param name="LastName">The last name of the employee.</param>
    /// <param name="Age">The age of the employee.</param>
    /// <param name="Dni">The DNI (National Identity Document) of the employee.</param>
    /// <param name="PhoneNumber">The phone number of the employee.</param>
    /// <param name="Address">The address of the employee.</param>
    /// <param name="Email">The email address of the employee.</param>
    /// <param name="Password">The password for the employee's account.</param>
    /// <param name="Wage">The wage (salary) of the employee.</param>
    /// <param name="Role">The role of the employee (e.g., "Trainer", "Administrator").</param>
    /// <param name="WorkHours">The standard work hours per week for the employee.</param>
    /// <param name="CertificationIds">An array of unique identifiers for the certifications to be associated with this employee.</param>
    /// <param name="SpecialtyIds">An array of unique identifiers for the specialties to be associated with this employee.</param>
    public record CreateEmployeeCommand(
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
        int WorkHours,
        int[] CertificationIds,
        int[] SpecialtyIds
    );
}