namespace FitManager_Web_Services.Classes.Interfaces.REST.Resources;

public record CreateClassResource(
    string Name,
    string Description,
    string Type,
    int Capacity,
    DateTime StartDate,
    int Duration,
    string Status,
    int EmployeeId);