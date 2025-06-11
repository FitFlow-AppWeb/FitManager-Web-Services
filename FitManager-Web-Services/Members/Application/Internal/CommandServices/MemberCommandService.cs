using System.Threading.Tasks;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Repositories;
using FitManager_Web_Services.Members.Domain.Services;
using FitManager_Web_Services.Members.Domain.Model.Commands;
using FitManager_Web_Services.Shared.Domain.Repositories;
using System;

namespace FitManager_Web_Services.Members.Application.Internal.CommandServices
{
    public class MemberCommandService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMemberService _memberService;
        private readonly IUnitOfWork _unitOfWork;

        public MemberCommandService(IMemberRepository memberRepository, IMemberService memberService, IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _memberService = memberService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Member?> Handle(CreateMemberCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            var newMember = new Member(
                command.FirstName,
                command.LastName,
                command.Age,
                command.Dni,
                command.PhoneNumber,
                command.Address,
                command.Email
            );

            await _memberRepository.AddAsync(newMember);
            await _unitOfWork.CompleteAsync();

            return newMember;
        }

        public async Task<Member?> Handle(UpdateMemberCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            var existingMember = await _memberRepository.GetByIdAsync(command.Id);
            if (existingMember == null)
            {
                return null;
            }

            existingMember.Update(
                command.FirstName,
                command.LastName,
                command.Age,
                command.Dni,
                command.PhoneNumber,
                command.Address,
                command.Email
            );

            await _memberRepository.UpdateAsync(existingMember);
            await _unitOfWork.CompleteAsync();

            return existingMember;
        }

        public async Task<bool> Handle(DeleteMemberCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            var memberToDelete = await _memberRepository.GetByIdAsync(command.Id);
            if (memberToDelete == null)
            {
                return false;
            }

            await _memberRepository.DeleteAsync(command.Id);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}