using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.IAM.Domain.Model;
using FitManager_Web_Services.IAM.Domain.Model.Queries;
using FitManager_Web_Services.IAM.Domain.Repositories;

namespace FitManager_Web_Services.IAM.Application.Internal.QueryServices
{
    /// <summary>
    /// Handles queries related to User aggregate, such as retrieval.
    /// </summary>
    public class UserQueryService
    {
        private readonly IUserRepository _userRepository;

        public UserQueryService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Handles the retrieval of all users.
        /// </summary>
        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
        {
            return await _userRepository.GetAllAsync();
        }

        /// <summary>
        /// Handles the retrieval of a user by ID.
        /// </summary>
        public async Task<User?> Handle(GetUserByIdQuery query)
        {
            return await _userRepository.GetByIdAsync(query.Id);
        }
    }
}
