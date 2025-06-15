// Members/Application/Internal/QueryServices/MembershipTypeQueryService.cs

using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Model.Queries;
using FitManager_Web_Services.Members.Domain.Repositories;

namespace FitManager_Web_Services.Members.Application.Internal.QueryServices
{
    /// <summary>
    /// Represents the query service for retrieving membership type information.
    /// This service handles queries related to membership types and retrieves data from the repository.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling read-only use cases.
    /// </summary>
    public class MembershipTypeQueryService
    {
        private readonly IMembershipTypeRepository _membershipTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipTypeQueryService"/> class.
        /// </summary>
        /// <param name="membershipTypeRepository">The membership type repository.</param>
        public MembershipTypeQueryService(IMembershipTypeRepository membershipTypeRepository)
        {
            _membershipTypeRepository = membershipTypeRepository;
        }

        /// <summary>
        /// Handles the query to retrieve all membership types.
        /// </summary>
        /// <remarks>
        /// This method delegates the retrieval of all membership types to the <see cref="IMembershipTypeRepository"/>.
        /// </remarks>
        /// <param name="query">The query object for retrieving all membership types.</param>
        /// <returns>An asynchronous operation that returns an enumerable collection of <see cref="MembershipType"/> objects.</returns>
        public async Task<IEnumerable<MembershipType>> Handle(GetAllMembershipTypesQuery query)
        {
            return await _membershipTypeRepository.GetAllAsync();
        }
        
    }
}