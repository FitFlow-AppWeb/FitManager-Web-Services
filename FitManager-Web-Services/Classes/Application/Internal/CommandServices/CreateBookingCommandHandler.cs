using FitManager_Web_Services.Classes.Domain.Commands;
using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories; // Asegúrate de que esta ruta sea correcta
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Classes.Application.Internal.CommandServices
{
    /// <summary>
    /// Handles the CreateBookingCommand to create a new booking and enroll the member in the class.
    /// </summary>
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Booking>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IClassRepository _classRepository; // Necesario para validar la capacidad de la clase
        private readonly IClassMemberRepository _classMemberRepository; // Para inscribir al miembro

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBookingCommandHandler"/> class.
        /// </summary>
        /// <param name="bookingRepository">The booking repository.</param>
        /// <param name="classRepository">The class repository.</param>
        /// <param name="classMemberRepository">The class member repository.</param>
        public CreateBookingCommandHandler(
            IBookingRepository bookingRepository,
            IClassRepository classRepository,
            IClassMemberRepository classMemberRepository)
        {
            _bookingRepository = bookingRepository;
            _classRepository = classRepository;
            _classMemberRepository = classMemberRepository;
        }

        /// <summary>
        /// Handles the CreateBookingCommand asynchronously.
        /// </summary>
        /// <param name="command">The command to create a new booking.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created <see cref="Booking"/> entity.</returns>
        public async Task<Booking> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            // 1. Validar la existencia de la clase y su capacidad
            var existingClass = await _classRepository.GetByIdAsync(command.ClassId);
            if (existingClass == null)
            {
                throw new System.ArgumentException($"Class with ID {command.ClassId} not found.");
            }

            // Opcional: Validar si la clase ya está llena
            // Puedes necesitar una forma de contar los miembros ya inscritos
            // if (existingClass.ClassMembers.Count >= existingClass.Capacity)
            // {
            //     throw new System.InvalidOperationException($"Class {existingClass.Name} is already full.");
            // }

            // Opcional: Validar si el miembro ya está inscrito en esta clase
            // var isMemberAlreadyEnrolled = existingClass.ClassMembers.Any(cm => cm.MemberId == command.MemberId);
            // if (isMemberAlreadyEnrolled)
            // {
            //     throw new System.InvalidOperationException($"Member with ID {command.MemberId} is already enrolled in Class {existingClass.Name}.");
            // }


            // 2. Crear la entidad Booking
            var booking = new Booking(command.MemberId, command.ClassId, command.Date);

            // 3. Crear la entidad ClassMember para inscribir al miembro en la clase
            // Asumo que la entidad ClassMember tiene un constructor ClassMember(int classId, int memberId)
            var classMember = new ClassMember(command.ClassId, command.MemberId);

            // 4. Persistir ambas entidades
            await _bookingRepository.AddAsync(booking); // Asumo que tienes un método AddAsync en IBookingRepository
            await _classMemberRepository.AddAsync(classMember); // Asumo que tienes un método AddAsync en IClassMemberRepository

            return booking; // Devolvemos la reserva creada
        }
    }
}