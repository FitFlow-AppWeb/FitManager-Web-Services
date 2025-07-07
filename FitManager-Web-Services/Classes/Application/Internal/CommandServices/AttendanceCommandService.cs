using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration; // Necesario para AppDbContext y SaveChangesAsync
using MediatR; 


// IMPORTANTE: Si AttendanceCommandService no implementará IRequestHandler
// y será un simple servicio, entonces los SaveChangesAsync irían en el Handler
// que lo inyecte. Asumo que este será tu Handler principal.

namespace FitManager_Web_Services.Classes.Application.Internal.CommandServices
{
    // 1. Define el Command/Request DTO. Esto generalmente va en una carpeta separada (e.g., Domain/Model/Commands)
    // Pero lo ponemos aquí temporalmente para que veas la relación.
    public record RegisterAttendanceCommand(DateTime EntryTime, DateTime ExitTime, int MemberId, int ClassId) : IRequest<Attendance>;
    // O si quieres que sea más robusto para el caso de "ya existe":
    // public record RegisterAttendanceCommand(DateTime EntryTime, DateTime ExitTime, int MemberId, int ClassId) : IRequest<AttendanceResult>;
    // Donde AttendanceResult podría ser un record con el Attendance y un booleano IsNew o un enum Status.

    /// <summary>
    /// Represents the command handler for registering and updating attendance records.
    /// This class implements <see cref="IRequestHandler{TRequest, TResponse}"/> from MediatR,
    /// making it a command handler for the <see cref="RegisterAttendanceCommand"/>.
    /// It orchestrates the business logic and manages the unit of work.
    /// </summary>
    public class AttendanceCommandService : IRequestHandler<RegisterAttendanceCommand, Attendance> // <-- Implementa IRequestHandler
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly AppDbContext _dbContext; // <-- Inyectar el DbContext para SaveChangesAsync

        public AttendanceCommandService(IAttendanceRepository attendanceRepository, AppDbContext dbContext) // <-- Añadir DbContext al constructor
        {
            _attendanceRepository = attendanceRepository;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Handles the <see cref="RegisterAttendanceCommand"/> to register a new attendance record.
        /// </summary>
        /// <param name="command">The command containing attendance details.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>The newly created or existing <see cref="Attendance"/> aggregate.</returns>
        public async Task<Attendance> Handle(RegisterAttendanceCommand command, CancellationToken cancellationToken) // <-- Método Handle para MediatR
        {
            // 1. Verificar si la asistencia ya existe para este miembro y clase para la fecha específica
            var existingAttendance = await _attendanceRepository.GetByMemberClassAndDateAsync(command.MemberId, command.ClassId, command.EntryTime.Date);

            if (existingAttendance != null)
            {
                Console.WriteLine($"Attendance for MemberId {command.MemberId} in ClassId {command.ClassId} on {command.EntryTime.Date.ToShortDateString()} already exists. Returning existing record.");

                return existingAttendance;
            }

            // Si no existe, crear una nueva asistencia
            var attendance = new Attendance(command.EntryTime, command.ExitTime, command.MemberId, command.ClassId);
            await _attendanceRepository.AddAsync(attendance);
            
            // AHORA SÍ, guardamos los cambios en la base de datos para la transacción completa
            await _dbContext.SaveChangesAsync(cancellationToken); // <-- LLAMADA A SaveChangesAsync

            return attendance;
        }

    }
}