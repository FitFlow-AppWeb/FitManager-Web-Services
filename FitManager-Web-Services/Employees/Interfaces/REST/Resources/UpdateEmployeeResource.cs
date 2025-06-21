using System.ComponentModel.DataAnnotations;

namespace FitManager_Web_Services.Employees.Interfaces.REST.Resources
{
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