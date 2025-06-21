namespace FitManager_Web_Services.Employees.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource for creating a new employee, used for data transfer objects (DTOs) in RESTful API requests.
    /// </summary>
    /// <param name="FirstName">The first name of the employee.</param>
    /// <param name="LastName">The last name of the employee.</param>
    /// <param name="Age">The age of the employee.</param>
    /// <param name="Dni">The DNI (National Identity Document) of the employee.</param>
    /// <param name="PhoneNumber">The phone number of the employee.</param>
    /// <param name="Address">The address of the employee.</param>
    /// <param name="Email">The email address of the employee.</param>
    /// <param name="Password">The password for the employee's account.</param>
    /// <param name="Wage">The hourly wage or salary of the employee.</param>
    /// <param name="Role">The role or position of the employee within the organization (e.g., "Trainer", "Administrator").</param>
    /// <param name="WorkHours">The standard weekly work hours for the employee.</param>
    public record CreateEmployeeResource(
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