using System;
using System.Collections.Generic;

namespace FitManager_Web_Services.Employees.Domain.Model.Commands
{
    public record CreateEmployeeCommand(
        string FirstName,
        string LastName,
        int Age,
        int Dni,
        int PhoneNumber,
        string Address,
        string Email,
        List<int> CertificationIds,  // Lista de IDs de certificaciones
        List<int> SpecialtyIds       // Lista de IDs de especialidades
    );
}
