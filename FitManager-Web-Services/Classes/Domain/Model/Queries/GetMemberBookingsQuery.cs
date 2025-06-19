using MediatR;
using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using System.Collections.Generic;

namespace FitManager_Web_Services.Classes.Domain.Queries;

public record GetMemberBookingsQuery(int MemberId) : IRequest<IEnumerable<Booking>>;