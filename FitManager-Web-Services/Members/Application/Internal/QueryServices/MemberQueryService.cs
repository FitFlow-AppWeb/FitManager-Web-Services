using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Services; 

namespace FitManager_Web_Services.Members.Application.Internal.QueryServices
{
    public class MemberQueryService
    {
        private readonly IMemberService _memberService; 

        public MemberQueryService(IMemberService memberService) 
        {
            _memberService = memberService;
        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await _memberService.GetAllAsync(); 
        }

        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            return await _memberService.GetByIdAsync(id); 
        }

        public async Task<Member?> GetMemberByDniAsync(int dni)
        {
            return await _memberService.GetByDniAsync(dni); 
        }
    }
}