using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Employees.Interfaces.REST.Transform
{
    public static class EmployeeResourceFromEntityAssembler
    {
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
                entity.Password,  // Agregar el campo Password
                entity.Wage,      // Agregar el campo Wage
                entity.Role,      // Agregar el campo Role
                entity.WorkHours, // Agregar el campo WorkHours
                certifications,
                specialties
            );
        }

        public static IEnumerable<EmployeeResource> ToResourceListFromEntityList(IEnumerable<Employee> entities)
        {
            return entities.Select(ToResourceFromEntity);
        }
    }
}