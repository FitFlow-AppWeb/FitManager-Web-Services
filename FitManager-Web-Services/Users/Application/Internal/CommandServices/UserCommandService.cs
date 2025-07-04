using FitManager_Web_Services.Users.Domain.Model;
using FitManager_Web_Services.Users.Domain.Model.Commands;
using FitManager_Web_Services.Users.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories;
using System.Threading.Tasks;

namespace FitManager_Web_Services.Users.Application.Internal.CommandServices
{
    /// <summary>
    /// Handles commands related to User aggregate, such as creation.
    /// </summary>
    public class UserCommandService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the creation of a new User based on the provided command.
        /// </summary>
        public async Task<User?> Handle(CreateUserCommand command)
        {
            // Ensure email is unique
            var existing = await _userRepository.GetByEmailAsync(command.Email);
            if (existing != null) return null;

            var user = new User(0, command.Email, command.Password, command.Icon, command.Subscription);
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return user;
        }

        /// <summary>
        /// Handles the update of an existing User.
        /// </summary>
        public async Task<User?> Handle(UpdateUserCommand command)
        {
            var existing = await _userRepository.GetByIdAsync(command.Id);
            if (existing == null) return null;
            existing.ChangeEmail(command.Email);
            existing.ChangePassword(command.Password);
            existing.UpdateIcon(command.Icon);
            existing.ActivateSubscription(command.Subscription);
            await _userRepository.UpdateAsync(existing);
            await _unitOfWork.CompleteAsync();
            return existing;
        }

        /// <summary>
        /// Handles the deletion of an existing User.
        /// </summary>
        public async Task<bool> Handle(DeleteUserCommand command)
        {
            var existing = await _userRepository.GetByIdAsync(command.Id);
            if (existing == null) return false;
            await _userRepository.DeleteAsync(command.Id);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
