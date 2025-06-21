// Employees/Interfaces/REST/Transform/CreateSpecialtyCommandFromResourceAssembler.cs

using FitManager_Web_Services.Employees.Domain.Model.Commands;
using FitManager_Web_Services.Employees.Interfaces.REST.Resources; // Para CreateSpecialtyResource

namespace FitManager_Web_Services.Employees.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler to transform a CreateSpecialtyResource into a CreateSpecialtyCommand.
    /// This ensures the separation of concerns between the API layer and the domain layer commands.
    /// </summary>
    public static class CreateSpecialtyCommandFromResourceAssembler
    {
        /// <summary>
        /// Assembles a CreateSpecialtyCommand from a CreateSpecialtyResource.
        /// </summary>
        /// <param name="resource">The CreateSpecialtyResource received from the API.</param>
        /// <returns>A new CreateSpecialtyCommand instance.</returns>
        public static CreateSpecialtyCommand ToCommandFromResource(CreateSpecialtyResource resource)
        {
            return new CreateSpecialtyCommand(resource.Name, resource.Description, resource.EmployeeId);
        }
    }
}