using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;
using FitManager_Web_Services.Members.Interfaces.REST.Transform;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Transform;

/// <summary>
/// Provides static methods to assemble <see cref="ClassResource"/> from <see cref="Class"/> entities.
/// </summary>
/// <remarks>
/// This assembler is responsible for transforming domain entities into API resources (DTOs),
/// ensuring that only necessary data is exposed through the RESTful interface.
/// It also handles the transformation of associated <see cref="ClassMember"/> entities into
/// a collection of <see cref="MemberResource"/> for enrolled members, if available.
/// </remarks>
public static class ClassResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a <see cref="Class"/> entity to a <see cref="ClassResource"/> DTO.
    /// </summary>
    /// <param name="entity">The <see cref="Class"/> entity to convert. This entity should ideally have its
    /// <see cref="Class.ClassMembers"/> navigation property loaded if enrolled members are to be included.</param>
    /// <returns>A new <see cref="ClassResource"/> representing the converted entity,
    /// potentially including a list of enrolled members.</returns>
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