﻿namespace FitManager_Web_Services.Employees.Domain.Model.Commands
{
    public record CreateEmployeeCommand(
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
        int[] CertificationIds, // Asumiendo que un empleado puede tener varias certificaciones
        int[] SpecialtyIds // Asumiendo que un empleado puede tener varias especialidades
    );
}