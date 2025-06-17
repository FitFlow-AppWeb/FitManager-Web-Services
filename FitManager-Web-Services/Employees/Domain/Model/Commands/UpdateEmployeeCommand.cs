namespace FitManager_Web_Services.Employees.Domain.Model.Commands
{
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
        int WorkHours,
        int[] CertificationIds, // Nuevos IDs de certificación a actualizar
        int[] SpecialtyIds // Nuevos IDs de especialidad a actualizar
    );
}