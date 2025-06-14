using System;
using System.Collections.Generic;

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
        List<CertificationResource> Certifications,  // Lista de certificaciones
        List<SpecialtyResource> Specialties           // Lista de especialidades
    );
}