using MediatR;

namespace FitManager_Web_Services.Classes.Domain.Commands;

public record DeleteClassCommand(int Id) : IRequest;