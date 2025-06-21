using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Employees.Interfaces.REST.Transform
{
    /// <summary>
    /// Provides static methods to assemble <see cref="EmployeeResource"/> from <see cref="Employee"/> entities.
    /// </summary>
    /// <remarks>
    /// This assembler is responsible for transforming domain entities into API resources (DTOs),
    /// ensuring that only necessary data is exposed through the RESTful interface.
    /// It also handles the transformation of associated <see cref="Employee.Certifications"/> and
    /// <see cref="Employee.Specialties"/> into their respective resource DTOs.
    /// </remarks>
    public static class EmployeeResourceFromEntityAssembler
    {
        /// <summary>
        /// Converts an <see cref="Employee"/> entity to an <see cref="EmployeeResource"/> DTO.
        /// </summary>
        /// <param name="entity">The <see cref="Employee"/> entity to convert. This entity should ideally have its
        /// <see cref="Employee.Certifications"/> and <see cref="Employee.Specialties"/> navigation properties loaded
        /// if they are to be included in the resource.</param>
        /// <returns>A new <see cref="EmployeeResource"/> representing the converted entity,
        /// potentially including its certifications and specialties.</returns>
        public static EmployeeResource ToResourceFromEntity(Employee entity)
        {
            var certifications = entity.Certifications.Select(c => CertificationResourceFromEntityAssembler.ToResourceFromEntity(c));
            
            var specialties = entity.Specialties.Select(s => SpecialtyResourceFromEntityAssembler.ToResourceFromEntity(s));

            return new EmployeeResource(
                entity.Id,
                entity.FirstName,
                entity.LastName,
                entity.Age,
                entity.Dni,
                entity.PhoneNumber,
                entity.Address,
                entity.Email,
                entity.Password, 
                entity.Wage,      
                entity.Role,      
                entity.WorkHours, 
                certifications, 
                specialties      
            );
        }

        /// <summary>
        /// Converts a collection of <see cref="Employee"/> entities to an enumerable of <see cref="EmployeeResource"/> DTOs.
        /// </summary>
        /// <param name="entities">The collection of <see cref="Employee"/> entities to convert.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="EmployeeResource"/> representing the converted entities.</returns>
        public static IEnumerable<EmployeeResource> ToResourceListFromEntityList(IEnumerable<Employee> entities)
        { 
            return entities.Select(ToResourceFromEntity);
        }
    }
}