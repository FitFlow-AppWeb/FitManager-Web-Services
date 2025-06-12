using System.Threading.Tasks;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Model.Commands;
using FitManager_Web_Services.Members.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;


namespace FitManager_Web_Services.Members.Application.Internal.CommandServices
{
    public class MemberCommandService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMembershipTypeRepository _membershipTypeRepository; 
        private readonly IUnitOfWork _unitOfWork;

        public MemberCommandService(IMemberRepository memberRepository, IMembershipTypeRepository membershipTypeRepository, IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _membershipTypeRepository = membershipTypeRepository; // <-- Asignación
            _unitOfWork = unitOfWork;
        }

        public async Task<Member?> Handle(CreateMemberCommand command)
        {
            var membershipType = await _membershipTypeRepository.GetByIdAsync(command.MembershipTypeId);
            if (membershipType == null)
            {
                return null; 
            }

  
            var membershipStatus = new MembershipStatus(
                command.StartDate,
                command.EndDate,
                command.Status,
                membershipType.Id 
            );

            var member = new Member(
                command.FirstName,
                command.LastName,
                command.Age,
                command.Dni,
                command.PhoneNumber,
                command.Address,
                command.Email
            );

            member.SetMembershipStatus(membershipStatus); 

            await _memberRepository.AddAsync(member);
            await _unitOfWork.CompleteAsync();

            return member;
        }

        public async Task<Member?> Handle(UpdateMemberCommand command)
        {
            var existingMember = await _memberRepository.GetByIdAsync(command.Id);
            if (existingMember == null) return null;

            existingMember.Update(
                command.FirstName,
                command.LastName,
                command.Age,
                command.Dni,
                command.PhoneNumber,
                command.Address,
                command.Email
            );

            
            if (command.StartDate.HasValue || command.EndDate.HasValue || command.Status != null || command.MembershipTypeId.HasValue)
            {
                
                if (command.MembershipTypeId.HasValue && command.MembershipTypeId.Value != existingMember.MembershipStatus.MembershipTypeId)
                {
                    var newMembershipType = await _membershipTypeRepository.GetByIdAsync(command.MembershipTypeId.Value);
                    if (newMembershipType == null)
                    {
                       
                        return null; 
                    }
                }

                existingMember.UpdateMembershipStatus(
                    command.StartDate,
                    command.EndDate,
                    command.Status,
                    command.MembershipTypeId
                );
            }

            await _memberRepository.UpdateAsync(existingMember);
            await _unitOfWork.CompleteAsync();

            return existingMember;
        }

        public async Task<bool> Handle(DeleteMemberCommand command)
        {
            var member = await _memberRepository.GetByIdAsync(command.Id);
            if (member == null) return false;

            await _memberRepository.DeleteAsync(command.Id);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}