using MediatR;
using FitManager_Web_Services.Classes.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Classes.Domain.Queries;

public record GetClassByIdQuery(int Id) : IRequest<Class?>;