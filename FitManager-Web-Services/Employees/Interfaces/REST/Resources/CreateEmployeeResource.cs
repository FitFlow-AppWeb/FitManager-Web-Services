namespace FitManager_Web_Services.Employees.Interfaces.REST.Resources
{
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