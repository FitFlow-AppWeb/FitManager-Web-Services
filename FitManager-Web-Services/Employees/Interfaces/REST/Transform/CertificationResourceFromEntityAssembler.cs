using FitManager_Web_Services.Employees.Domain.Model.Aggregates;
using FitManager_Web_Services.Employees.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Employees.Interfaces.REST.Transform
{
    public static class CertificationResourceFromEntityAssembler
    {
        public static CertificationResource ToResourceFromEntity(Certification entity)
        {
            return new CertificationResource(
                entity.Id,
                entity.Name,
                entity.Description
            );
        }
    }
}