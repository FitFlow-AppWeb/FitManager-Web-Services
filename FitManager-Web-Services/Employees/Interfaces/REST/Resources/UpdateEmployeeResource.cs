using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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
        List<int> CertificationIds,  // Lista de IDs de certificaciones
        List<int> SpecialtyIds,      // Lista de IDs de especialidades
        DateTime? StartDate, 
        DateTime? EndDate,
        string? Status,
        int? MembershipTypeId
    );
}