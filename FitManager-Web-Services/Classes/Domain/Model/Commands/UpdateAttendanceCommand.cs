using MediatR;

namespace FitManager_Web_Services.Classes.Domain.Commands;

public record UpdateAttendanceCommand(
    int Id,
    DateTime EntryTime,
    DateTime ExitTime) : IRequest;