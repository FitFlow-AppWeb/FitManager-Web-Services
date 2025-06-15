// Members/Application/Internal/QueryServices/MemberQueryService.cs

using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Repositories; 
using FitManager_Web_Services.Members.Domain.Model.Queries; 

namespace FitManager_Web_Services.Members.Application.Internal.QueryServices
{
    /// <summary>
    /// Represents the query service for retrieving member information.
    /// This service handles queries related to members and retrieves data from the repository.
    /// It belongs to the Application layer in Clean Architecture, responsible for handling read-only use cases.
    /// </summary>
    public class MemberQueryService
    {
        private readonly IMemberRepository _memberRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberQueryService"/> class.
        /// </summary>
        /// <param name="memberRepository">The member repository.</param>
        public MemberQueryService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        /// <summary>
        /// Handles the query to retrieve all members.
        /// </summary>
        /// <remarks>
        /// This method delegates the retrieval of all members to the <see cref="IMemberRepository"/>.
        /// </remarks>
        /// <param name="query">The query object for retrieving all members.</param>
        /// <returns>An asynchronous operation that returns an enumerable collection of <see cref="Member"/> objects.</returns>
        public async Task<IEnumerable<Member>> Handle(GetAllMembersQuery query)
        {
            return await _memberRepository.GetAllAsync();
        }
        
    }
}