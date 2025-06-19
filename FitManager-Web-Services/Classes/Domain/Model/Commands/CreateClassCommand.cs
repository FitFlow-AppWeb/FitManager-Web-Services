using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using MediatR;

namespace FitManager_Web_Services.Classes.Domain.Commands;

public record CreateClassCommand(
    string Name,
    string Description,
    string Type,
    int Capacity,
    DateTime StartDate,
    int Duration,
    string Status,
    int EmployeeId
) : IRequest<Class>;