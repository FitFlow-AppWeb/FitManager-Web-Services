using FitManager_Web_Services.Employees.Domain.Model.Commands;
using FitManager_Web_Services.Employees.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Employees.Interfaces.REST.Transform
{
    public static class UpdateEmployeeCommandFromResourceAssembler
    {
        public static UpdateEmployeeCommand ToCommandFromResource(int employeeId, UpdateEmployeeResource resource)
        {
            return new UpdateEmployeeCommand(
                employeeId,
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
                resource.WorkHours
            );
        }
    }
}