namespace FitManager_Web_Services.Employees.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource for an employee certification, used for data transfer objects (DTOs) in RESTful APIs.
    /// </summary>
    /// <param name="Id">The unique identifier of the certification.</param>
    /// <param name="Name">The name of the certification (e.g., "Certified Personal Trainer", "Nutrition Specialist").</param>
    /// <param name="Description">A brief description of the certification.</param>
    /// <param name="EmployeeId">The unique identifier of the employee to whom this certification belongs.</param>
    public record CertificationResource(
        int Id,
        string Name,
        string Description,
        int EmployeeId 
    );
}