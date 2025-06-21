using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;

/// <summary>
/// Provides static methods to assemble <see cref="AttendanceWithMemberResource"/> from <see cref="Attendance"/> entities.
/// </summary>
/// <remarks>
/// This assembler is responsible for transforming domain entities into enriched API resources (DTOs)
/// that include related member and class information, ensuring a comprehensive view for the client.
/// It handles potential null references for navigation properties gracefully.
/// </remarks>
public static class AttendanceWithMemberResourceFromEntityAssembler
{
    /// <summary>
    /// Converts an <see cref="Attendance"/> entity to an <see cref="AttendanceWithMemberResource"/> DTO.
    /// </summary>
    /// <param name="entity">The <see cref="Attendance"/> entity to convert, which should ideally have its <see cref="Attendance.Member"/> and <see cref="Attendance.Class"/> navigation properties loaded.</param>
    /// <returns>A new <see cref="AttendanceWithMemberResource"/> representing the converted entity with member and class details.</returns>
    public static AttendanceWithMemberResource ToResource(Attendance entity)
    {
        return new AttendanceWithMemberResource(
            entity.Id,
            entity.EntryTime,
            entity.ExitTime,
            entity.MemberId,
            // Concatenate first and last name, handling potential null Member gracefully
            $"{entity.Member?.FirstName} {entity.Member?.LastName}".Trim(), 
            entity.ClassId,
            // Use null-coalescing operator to provide "N/A" if Class or Class.Name is null
            entity.Class?.Name ?? "N/A"); 
    }
}