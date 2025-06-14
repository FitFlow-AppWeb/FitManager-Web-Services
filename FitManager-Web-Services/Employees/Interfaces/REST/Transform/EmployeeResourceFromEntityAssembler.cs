using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Interfaces.REST.Resources;
using System.Collections.Generic;
using System.Linq;

namespace FitManager_Web_Services.Employees.Interfaces.REST.Transform
{
    public static class EmployeeResourceFromEntityAssembler
    {
        public static EmployeeResource ToResourceFromEntity(Employee entity)
        {
            var certifications = entity.Certifications.Select(cert => new CertificationResource(cert.Id, cert.Name, cert.Description)).ToList();
            var specialties = entity.Specialties.Select(spec => new SpecialtyResource(spec.Id, spec.Name, spec.Description)).ToList();

            return new EmployeeResource(
                entity.Id,
                entity.FirstName,
                entity.LastName,
                entity.Age,
                entity.Dni,
                entity.PhoneNumber,
                entity.Address,
                entity.Email,
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
