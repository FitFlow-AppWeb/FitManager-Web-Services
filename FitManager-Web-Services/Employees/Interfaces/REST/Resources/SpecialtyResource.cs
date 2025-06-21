namespace FitManager_Web_Services.Employees.Interfaces.REST.Resources
{
    /// <summary>
    /// Represents the resource for an employee specialty, used for data transfer objects (DTOs) in RESTful APIs.
    /// </summary>
    /// <param name="Id">The unique identifier of the specialty.</param>
    /// <param name="Name">The name of the specialty (e.g., "Strength Training", "Cardio", "Yoga Instructor").</param>
    /// <param name="Description">A brief description of the specialty.</param>
    /// <param name="EmployeeId">The unique identifier of the employee to whom this specialty belongs.</param>
    public record SpecialtyResource(
        int Id,
        string Name,
        string Description,
        int EmployeeId 
    );
}