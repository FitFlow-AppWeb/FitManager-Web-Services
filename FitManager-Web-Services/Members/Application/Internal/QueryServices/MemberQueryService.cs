using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Repositories;

namespace FitManager_Web_Services.Members.Application.Internal.QueryServices
{
    public class MemberQueryService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberQueryService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await _memberRepository.GetAllAsync();
        }

        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            return await _memberRepository.GetByIdAsync(id);
        }

        public async Task<Member?> GetMemberByDniAsync(int dni)
        {
            return await _memberRepository.GetByDniAsync(dni);
        }
    }
}