namespace FitManager_Web_Services.Classes.Interfaces.REST.Resources;

/// <summary>
/// Represents the resource for creating a new fitness class, used for data transfer objects (DTOs) in RESTful API requests.
/// </summary>
/// <param name="Name">The name of the class (e.g., "Morning Yoga", "Spin Class").</param>
/// <param name="Description">A detailed description of the class.</param>
/// <param name="Type">The type or category of the class (e.g., "Yoga", "Cardio", "Strength").</param>
/// <param name="Capacity">The maximum number of participants the class can accommodate.</param>
/// <param name="StartDate">The scheduled start date and time of the class.</param>
/// <param name="Duration">The duration of the class in minutes.</param>
/// <param name="Status">The initial status of the class (e.g., "Scheduled", "Active").</param>
/// <param name="EmployeeId">The unique identifier of the employee (instructor) assigned to this class.</param>
public record CreateClassResource(
    string Name,
    string Description,
    string Type,
    int Capacity,
    DateTime StartDate,
    int Duration,
    string Status,
    int EmployeeId);