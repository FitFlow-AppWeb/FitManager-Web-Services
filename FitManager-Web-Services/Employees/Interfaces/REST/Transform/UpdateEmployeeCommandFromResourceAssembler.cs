using FitManager_Web_Services.Employees.Domain.Model.Commands;
using FitManager_Web_Services.Employees.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Employees.Interfaces.REST.Transform
{
    /// <summary>
    /// Provides static methods to assemble <see cref="UpdateEmployeeCommand"/> from <see cref="UpdateEmployeeResource"/>.
    /// </summary>
    /// <remarks>
    /// This assembler acts as a bridge between the API's data transfer objects (DTOs) and the domain's command objects.
    /// It translates the incoming <see cref="UpdateEmployeeResource"/> (from an HTTP request) along with the employee ID
    /// into an <see cref="UpdateEmployeeCommand"/> that can be processed by the domain layer to update an existing employee record.
    /// </remarks>
    public static class UpdateEmployeeCommandFromResourceAssembler
    {
        /// <summary>
        /// Converts an <see cref="UpdateEmployeeResource"/> DTO and an employee ID to an <see cref="UpdateEmployeeCommand"/> command.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee to be updated.</param>
        /// <param name="resource">The <see cref="UpdateEmployeeResource"/> containing the updated employee details.</param>
        /// <returns>A new <see cref="UpdateEmployeeCommand"/> representing the command to update an employee.</returns>
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