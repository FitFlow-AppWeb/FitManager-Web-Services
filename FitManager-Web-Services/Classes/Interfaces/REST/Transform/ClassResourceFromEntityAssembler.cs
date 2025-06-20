using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;
using FitManager_Web_Services.Members.Interfaces.REST.Transform;
using System.Linq;
using System.Collections.Generic;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Transform;

public static class ClassResourceFromEntityAssembler
{
    public static ClassResource ToResource(Class entity)
    {
        IEnumerable<MemberResource>? enrolledMembers = null;
        
        if (entity.ClassMembers != null && entity.ClassMembers.Any())
        {
            enrolledMembers = entity.ClassMembers
                .Select(cm => MemberResourceFromEntityAssembler.ToResourceFromEntity(cm.Member));
        }

        return new ClassResource(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.Type,       
            entity.Capacity,
            entity.StartDate,
            entity.Duration,
            entity.Status,
            entity.EmployeeId,
            enrolledMembers
        );
    }
}