using System;
using System.Threading.Tasks;
using FitManager_Web_Services.Shared.Domain.Repositories;
using FitManager_Web_Services.IAM.Application.Internal.OutboundServices;
using FitManager_Web_Services.IAM.Domain.Model;
using FitManager_Web_Services.IAM.Domain.Model.Commands;
using FitManager_Web_Services.IAM.Domain.Repositories;

namespace FitManager_Web_Services.IAM.Application.Internal.CommandServices
{
    /// <summary>
    /// Handles user commands (authentication + CRUD).
    /// </summary>
    public class UserCommandService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashingService _hashingService;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public UserCommandService(
            IUserRepository userRepository,
            ITokenService tokenService,
            IHashingService hashingService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _hashingService = hashingService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles user sign-up: hashes password and creates user.
        /// </summary>
        public async Task Handle(SignUpCommand command)
        {
            if (_userRepository.ExistsByEmail(command.Email))
                throw new InvalidOperationException("Email already in use.");
            var hash = _hashingService.HashPassword(command.Password);
            var user = new User(0, command.Email, hash, command.Icon, command.Subscription);
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }

        /// <summary>
        /// Handles user sign-in: verifies password and returns token.
        /// </summary>
        public async Task<(User? user, string token)> Handle(SignInCommand command)
        {
            var user = await _userRepository.FindByEmailAsync(command.Email);
            if (user == null || !_hashingService.VerifyPassword(command.Password, user.PasswordHash))
                return (null, string.Empty);
            var token = _tokenService.GenerateToken(user);
            return (user, token);
        }

        /// <summary>
        /// Handles the creation of a new User based on the provided command.
        /// </summary>
        public async Task<User?> Handle(CreateUserCommand command)
        {
            // Ensure email is unique
            var existing = await _userRepository.FindByEmailAsync(command.Email);
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
            existing.UpdatePasswordHash(command.Password);
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
