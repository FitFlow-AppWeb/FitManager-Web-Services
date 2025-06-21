// Employees/Interfaces/REST/Transform/CertificationResourceFromEntityAssembler.cs

using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Employees.Interfaces.REST.Transform
{
    /// <summary>
    /// Assembler to transform a Certification entity into a CertificationResource.
    /// This ensures the separation of concerns between the domain layer entities and the API layer resources.
    /// </summary>
    public static class CertificationResourceFromEntityAssembler
    {
        /// <summary>
        /// Assembles a CertificationResource from a Certification entity.
        /// </summary>
        /// <param name="entity">The Certification entity from the domain layer.</param>
        /// <returns>A new CertificationResource instance.</returns>
        public static CertificationResource ToResourceFromEntity(Certification entity)
        {
            return new CertificationResource(
                entity.Id,
                entity.Name,
                entity.Description,
                entity.EmployeeId 
            );
        }
    }
}