﻿using FitManager_Web_Services.Employees.Domain.Model.Commands;
using FitManager_Web_Services.Employees.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Employees.Interfaces.REST.Transform
{
    public static class CreateEmployeeCommandFromResourceAssembler
    {
        public static CreateEmployeeCommand ToCommandFromResource(CreateEmployeeResource resource)
        {
            return new CreateEmployeeCommand(
                resource.FirstName,
                resource.LastName,
                resource.Age,
                resource.Dni,
                resource.PhoneNumber,
                resource.Address,
                resource.Email,
                resource.Password,
                resource.Wage,
                resource.Role,
                resource.WorkHours,
                resource.CertificationIds, // Certificaciones asociadas
                resource.SpecialtyIds // Especialidades asociadas
                
            );
        }
    }
}