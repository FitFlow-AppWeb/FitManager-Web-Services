using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using MediatR;

namespace FitManager_Web_Services.Classes.Domain.Commands;

public record RegisterAttendanceCommand(
    DateTime EntryTime,
    DateTime ExitTime,
    int MemberId,
    int ClassId
) : IRequest<Attendance>;