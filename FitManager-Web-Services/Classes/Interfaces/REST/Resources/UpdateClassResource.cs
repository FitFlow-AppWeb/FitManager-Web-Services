namespace FitManager_Web_Services.Classes.Interfaces.REST.Resources;

/// <summary>
/// Represents the resource for updating an existing fitness class, used for data transfer objects (DTOs) in RESTful API requests.
/// </summary>
/// <param name="Name">The updated name of the class (e.g., "Morning Yoga", "Spin Class").</param>
/// <param name="Description">The updated detailed description of the class.</param>
/// <param name="Type">The updated type or category of the class (e.g., "Yoga", "Cardio", "Strength").</param>
/// <param name="Capacity">The updated maximum number of participants the class can accommodate.</param>
/// <param name="StartDate">The updated scheduled start date and time of the class.</param>
/// <param name="Duration">The updated duration of the class in minutes.</param>
/// <param name="Status">The updated current status of the class (e.g., "Scheduled", "Active", "Cancelled").</param>
/// <param name="EmployeeId">The updated unique identifier of the employee (instructor) assigned to this class.</param>
public record UpdateClassResource(
    string Name,
    string Description,
    string Type,
    int Capacity,
    DateTime StartDate,
    int Duration,
    string Status,
    int EmployeeId);