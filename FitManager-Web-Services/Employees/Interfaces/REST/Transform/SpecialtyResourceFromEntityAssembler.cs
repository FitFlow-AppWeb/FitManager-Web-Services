// Employees/Interfaces/REST/Transform/SpecialtyResourceFromEntityAssembler.cs

using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Employees.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler to transform a Specialty entity into a SpecialtyResource.
    /// This ensures the separation of concerns between the domain layer entities and the API layer resources.
    /// </summary>
    public static class SpecialtyResourceFromEntityAssembler
    {
        /// <summary>
        /// Assembles a SpecialtyResource from a Specialty entity.
        /// </summary>
        /// <param name="entity">The Specialty entity from the domain layer.</param>
        /// <returns>A new SpecialtyResource instance.</returns>
        public static SpecialtyResource ToResourceFromEntity(Specialty entity)
        {
            return new SpecialtyResource(
                entity.Id,
                entity.Name,
                entity.Description,
                entity.EmployeeId 
            );
        }
    }
}