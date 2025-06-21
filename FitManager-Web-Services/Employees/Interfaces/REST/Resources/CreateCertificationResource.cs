// Members/Interfaces/REST/Resources/CreateCertificationResource.cs

using System.ComponentModel.DataAnnotations; 

namespace FitManager_Web_Services.Employees.Interfaces.REST.Resources
{
    /// <summary>
    /// Resource for creating a new Certification.
    /// </summary>
    /// <param name="Name">The name of the certification.</param>
    /// <param name="Description">The description of the certification.</param>
    /// <param name="EmployeeId">The ID of the employee to whom the certification belongs.</param>
    public record CreateCertificationResource(
        [Required] [StringLength(100)] string Name,
        [StringLength(500)] string? Description, 
        [Required] [Range(1, int.MaxValue)] int EmployeeId
    );
}