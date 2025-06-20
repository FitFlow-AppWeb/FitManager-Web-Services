// Members/Application/Internal/CommandServices/MemberCommandService.cs

using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Model.Commands;
using FitManager_Web_Services.Members.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FitManager_Web_Services.Members.Application.Internal.CommandServices
{
    /// <summary>
    /// Represents the command service for managing members.
    /// This service orchestrates business logic related to creating, updating, and deleting members.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling use cases.
    /// </summary>
    public class MemberCommandService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMembershipTypeRepository _membershipTypeRepository; 
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberCommandService"/> class.
        /// </summary>
        /// <param name="memberRepository">The member repository.</param>
        /// <param name="membershipTypeRepository">The membership type repository.</param>
        /// <param name="unitOfWork">The unit of work for managing transactions.</param>
        public MemberCommandService(IMemberRepository memberRepository, IMembershipTypeRepository membershipTypeRepository, IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _membershipTypeRepository = membershipTypeRepository; 
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the creation of a new member based on the provided command.
        /// </summary>
        /// <remarks>
        /// This method retrieves the associated membership type, creates a new Member aggregate
        /// with its MembershipStatus value object, persists it, and completes the unit of work.
        /// </remarks>
        /// <param name="command">The command containing the data for the new member.</param>
        /// <returns>The created Member aggregate if successful, otherwise null.</returns>
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

        /// <summary>
        /// Handles the update of an existing member based on the provided command.
        /// </summary>
        /// <remarks>
        /// This method retrieves the existing Member aggregate, applies updates to its properties
        /// and its MembershipStatus (if applicable), and persists the changes.
        /// </remarks>
        /// <param name="command">The command containing the data for the member update.</param>
        /// <returns>The updated Member aggregate if found and updated, otherwise null.</returns>
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

        /// <summary>
        /// Handles the deletion of an existing member based on the provided command.
        /// </summary>
        /// <remarks>
        /// This method retrieves the Member aggregate and deletes it using the repository.
        /// </remarks>
        /// <param name="command">The command containing the ID of the member to delete.</param>
        /// <returns>True if the member was deleted successfully, false otherwise.</returns>
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