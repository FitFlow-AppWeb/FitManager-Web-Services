using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using MediatR;

namespace FitManager_Web_Services.Classes.Domain.Commands;

public record CreateBookingCommand(
    int MemberId,
    int ClassId,
    DateTime Date
) : IRequest<Booking>;