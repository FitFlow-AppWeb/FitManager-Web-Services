using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Repositories; 
using FitManager_Web_Services.Members.Domain.Model.Queries; 

namespace FitManager_Web_Services.Members.Application.Internal.QueryServices
{
    public class MemberQueryService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberQueryService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<IEnumerable<Member>> Handle(GetAllMembersQuery query)
        {
            return await _memberRepository.GetAllAsync();
        }

    }
}