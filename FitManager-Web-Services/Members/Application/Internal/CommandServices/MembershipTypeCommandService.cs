// Members/Application/Internal/CommandServices/MembershipTypeCommandService.cs

using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Model.Commands;
using FitManager_Web_Services.Members.Domain.Repositories; // Para IMembershipTypeRepository
using FitManager_Web_Services.Shared.Domain.Repositories; // Para IUnitOfWork

namespace FitManager_Web_Services.Members.Application.Internal.CommandServices
{
    /// <summary>
    /// Implements the command service for MembershipType entities, handling creation, update, and deletion operations.
    /// </summary>
    public class MembershipTypeCommandService : IMembershipTypeCommandService
    {
        private readonly IMembershipTypeRepository _membershipTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MembershipTypeCommandService(IMembershipTypeRepository membershipTypeRepository, IUnitOfWork unitOfWork)
        {
            _membershipTypeRepository = membershipTypeRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the creation of a new membership type.
        /// </summary>
        /// <param name="command">The command containing the data for the new membership type.</param>
        /// <returns>The created MembershipType entity if successful, otherwise null.</returns>
        public async Task<MembershipType?> Handle(CreateMembershipTypeCommand command)
        {
            var membershipType = new MembershipType(command.Name, command.Description, command.Price, command.Duration, command.Benefits);
            await _membershipTypeRepository.AddAsync(membershipType);
            await _unitOfWork.CompleteAsync();
            return membershipType;
        }

        /// <summary>
        /// Handles the update of an existing membership type.
        /// </summary>
        /// <param name="command">The command containing the ID and updated data for the membership type.</param>
        /// <returns>The updated MembershipType entity if successful, otherwise null.</returns>
        public async Task<MembershipType?> Handle(UpdateMembershipTypeCommand command)
        {
            // N: Usar GetByIdAsync
            var existingMembershipType = await _membershipTypeRepository.GetByIdAsync(command.Id);
            if (existingMembershipType == null) return null;

            existingMembershipType.Update(command.Name, command.Description, command.Price, command.Duration, command.Benefits);
            // N: Usar UpdateAsync
            await _membershipTypeRepository.UpdateAsync(existingMembershipType);
            await _unitOfWork.CompleteAsync();
            return existingMembershipType;
        }

        /// <summary>
        /// Handles the deletion of a membership type.
        /// </summary>
        /// <param name="command">The command containing the ID of the membership type to delete.</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        public async Task<bool> Handle(DeleteMembershipTypeCommand command)
        {
            // N: Usar GetByIdAsync y DeleteAsync
            var membershipType = await _membershipTypeRepository.GetByIdAsync(command.Id);
            if (membershipType == null) return false;

            await _membershipTypeRepository.DeleteAsync(command.Id); // DeleteAsync toma el ID
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}