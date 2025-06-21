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
                entity.Password,  
                entity.Wage,      
                entity.Role,      
                entity.WorkHours, 
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