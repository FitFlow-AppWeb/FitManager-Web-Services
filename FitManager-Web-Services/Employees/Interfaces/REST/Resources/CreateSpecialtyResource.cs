// Members/Interfaces/REST/Resources/CreateSpecialtyResource.cs

using System.ComponentModel.DataAnnotations; 

namespace FitManager_Web_Services.Employees.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource for creating a new Specialty.
    /// </summary>
    /// <param name="Name">The name of the specialty.</param>
    /// <param name="Description">The description of the specialty.</param>
    /// <param name="EmployeeId">The ID of the employee to whom the specialty belongs.</param>
    public record CreateSpecialtyResource(
        [Required] [StringLength(100)] string Name,
        [StringLength(500)] string? Description, 
        [Required] [Range(1, int.MaxValue)] int EmployeeId
    );
}