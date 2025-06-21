// Employees/Interfaces/REST/Resources/SpecialtyResource.cs

namespace FitManager_Web_Services.Employees.Interfaces.REST.Resources
{
    public record SpecialtyResource(
        int Id,
        string Name,
        string Description,
        int EmployeeId 
    );
}