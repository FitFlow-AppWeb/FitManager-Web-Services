namespace FitManager_Web_Services.Classes.Interfaces.REST.Resources;

/// <summary>
/// Represents the resource for a fitness class, used for data transfer objects (DTOs) in RESTful APIs.
/// </summary>
/// <param name="Id">The unique identifier of the class.</param>
/// <param name="Name">The name of the class (e.g., "Morning Yoga", "Spin Class").</param>
/// <param name="Description">A detailed description of the class.</param>
/// <param name="Type">The type or category of the class (e.g., "Yoga", "Cardio", "Strength").</param>
/// <param name="Capacity">The maximum number of participants the class can accommodate.</param>
/// <param name="StartDate">The scheduled start date and time of the class.</param>
/// <param name="Duration">The duration of the class in minutes.</param>
/// <param name="Status">The current status of the class (e.g., "Scheduled", "Active", "Cancelled").</param>
/// <param name="EmployeeId">The unique identifier of the employee (instructor) assigned to this class.</param>
/// <param name="EnrolledMembers">An optional collection of <see cref="MemberResource"/> representing members currently enrolled in this class.</param>
public record ClassResource(
    int Id,
    string Name,
    string Description,
    string Type,
    int Capacity,
    DateTime StartDate,
    int Duration,
    string Status,
    int EmployeeId,
    IEnumerable<MemberResource>? EnrolledMembers
);