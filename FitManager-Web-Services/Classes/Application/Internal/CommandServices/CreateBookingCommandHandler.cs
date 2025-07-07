using FitManager_Web_Services.Classes.Domain.Commands;
using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Repositories;
using FitManager_Web_Services.Members.Domain.Repositories;
using MediatR;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace FitManager_Web_Services.Classes.Application.Internal.CommandServices
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Booking>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IClassRepository _classRepository;
        private readonly IClassMemberRepository _classMemberRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly AppDbContext _dbContext;

        public CreateBookingCommandHandler(
            IBookingRepository bookingRepository,
            IClassRepository classRepository,
            IClassMemberRepository classMemberRepository,
            IMemberRepository memberRepository,
            AppDbContext dbContext)
        {
            _bookingRepository = bookingRepository;
            _classRepository = classRepository;
            _classMemberRepository = classMemberRepository;
            _memberRepository = memberRepository;
            _dbContext = dbContext;
        }

        public async Task<Booking> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            var existingClass = await _classRepository.GetByIdAsync(command.ClassId);
            if (existingClass == null)
            {
                throw new System.ArgumentException($"Class with ID {command.ClassId} not found.");
            }

            var existingMember = await _memberRepository.GetByIdAsync(command.MemberId);
            if (existingMember == null)
            {
                throw new System.ArgumentException($"Member with ID {command.MemberId} not found.");
            }

            // Usamos el método ExistsAsync que ya tienes!
            var classMemberExists = await _classMemberRepository.ExistsAsync(command.MemberId, command.ClassId);

            var booking = new Booking(command.MemberId, command.ClassId, command.Date);
            await _bookingRepository.AddAsync(booking); // Asumiendo que AddAsync de BookingRepository tampoco hace SaveChangesAsync

            if (!classMemberExists)
            {
                var classMember = new ClassMember(command.ClassId, command.MemberId);
                await _classMemberRepository.AddAsync(classMember); // Este AddAsync ya NO llamará a SaveChangesAsync
            }

            // ESTA ES LA ÚNICA LLAMADA A SaveChangesAsync para toda la operación de negocio
            await _dbContext.SaveChangesAsync(cancellationToken);

            return booking;
        }
    }
}