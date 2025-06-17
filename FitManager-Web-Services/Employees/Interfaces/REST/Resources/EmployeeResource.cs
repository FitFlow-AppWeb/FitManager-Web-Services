namespace FitManager_Web_Services.Employees.Interfaces.REST.Resources
{
    public record EmployeeResource(
        int Id,
        string FirstName,
        string LastName,
        int Age,
        int Dni,
        int PhoneNumber,
        string Address,
        string Email,
        string Password,  // Agregar el campo Password
        float Wage,       // Agregar el campo Wage
        string Role,      // Agregar el campo Role
        int WorkHours,    // Agregar el campo WorkHours
        IEnumerable<CertificationResource>? Certifications,  // Certificaciones asociadas
        IEnumerable<SpecialtyResource>? Specialties         // Especialidades asociadas
    );
}