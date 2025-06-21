using FitManager_Web_Services.Employees.Domain.Model.Commands;
using FitManager_Web_Services.Employees.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Employees.Interfaces.REST.Transform
{
    /// <summary>
    /// Provides static methods to assemble <see cref="CreateEmployeeCommand"/> from <see cref="CreateEmployeeResource"/>.
    /// </summary>
    /// <remarks>
    /// This assembler acts as a bridge between the API's data transfer objects (DTOs) and the domain's command objects.
    /// It translates the incoming <see cref="CreateEmployeeResource"/> (from an HTTP request) into a
    /// <see cref="CreateEmployeeCommand"/> that can be processed by the domain layer to create a new employee.
    /// </remarks>
    public static class CreateEmployeeCommandFromResourceAssembler
    {
        /// <summary>
        /// Converts a <see cref="CreateEmployeeResource"/> DTO to a <see cref="CreateEmployeeCommand"/> command.
        /// </summary>
        /// <param name="resource">The <see cref="CreateEmployeeResource"/> to convert.</param>
        /// <returns>A new <see cref="CreateEmployeeCommand"/> representing the command to create an employee.</returns>
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
                resource.WorkHours
                
            );
        }
    }
}