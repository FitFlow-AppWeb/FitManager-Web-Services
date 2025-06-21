using System.ComponentModel.DataAnnotations;

namespace FitManager_Web_Services.Employees.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource for updating an existing employee, used for data transfer objects (DTOs) in RESTful API requests.
    /// </summary>
    /// <param name="FirstName">The updated first name of the employee. Required.</param>
    /// <param name="LastName">The updated last name of the employee. Required.</param>
    /// <param name="Age">The updated age of the employee. Required.</param>
    /// <param name="Dni">The updated DNI (National Identity Document) of the employee. Required.</param>
    /// <param name="PhoneNumber">The updated phone number of the employee. Required.</param>
    /// <param name="Address">The updated address of the employee. Required.</param>
    /// <param name="Email">The updated email address of the employee. Required.</param>
    /// <param name="Password">The updated password for the employee's account. This field is optional for updates, allowing partial updates without changing the password. (Note: In a real-world scenario, password updates should typically be handled via a separate, secure endpoint.)</param>
    /// <param name="Wage">The updated hourly wage or salary of the employee.</param>
    /// <param name="Role">The updated role or position of the employee within the organization.</param>
    /// <param name="WorkHours">The updated standard weekly work hours for the employee.</param>
    public record UpdateEmployeeResource(
        [Required] string FirstName,
        [Required] string LastName,
        [Required] int Age,
        [Required] int Dni,
        [Required] int PhoneNumber, 
        [Required] string Address,
        [Required] string Email,
        string Password,
        float Wage,
        string Role,
        int WorkHours
    );
}