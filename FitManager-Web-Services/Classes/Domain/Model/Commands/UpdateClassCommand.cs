using MediatR;

namespace FitManager_Web_Services.Classes.Domain.Commands;

public record UpdateClassCommand(
    int Id,
    string Name,
    string Description,
    string Type,
    int Capacity,
    DateTime StartDate,
    int Duration,
    string Status,
    int EmployeeId
) : IRequest;