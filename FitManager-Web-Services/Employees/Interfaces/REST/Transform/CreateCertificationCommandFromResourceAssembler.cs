// Employees/Interfaces/REST/Transform/CreateCertificationCommandFromResourceAssembler.cs

using FitManager_Web_Services.Employees.Domain.Model.Commands;
using FitManager_Web_Services.Employees.Interfaces.REST.Resources; // Para CreateCertificationResource

namespace FitManager_Web_Services.Employees.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler to transform a CreateCertificationResource into a CreateCertificationCommand.
    /// This ensures the separation of concerns between the API layer and the domain layer commands.
    /// </summary>
    public static class CreateCertificationCommandFromResourceAssembler
    {
        /// <summary>
        /// Assembles a CreateCertificationCommand from a CreateCertificationResource.
        /// </summary>
        /// <param name="resource">The CreateCertificationResource received from the API.</param>
        /// <returns>A new CreateCertificationCommand instance.</returns>
        public static CreateCertificationCommand ToCommandFromResource(CreateCertificationResource resource)
        {
            return new CreateCertificationCommand(resource.Name, resource.Description, resource.EmployeeId);
        }
    }
}