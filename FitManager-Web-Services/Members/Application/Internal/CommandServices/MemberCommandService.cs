using System.Threading.Tasks;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Services; 


namespace FitManager_Web_Services.Members.Application.Internal.CommandServices
{
    public class MemberCommandService
    {
        private readonly IMemberService _memberService; 

        public MemberCommandService(IMemberService memberService) 
        {
            _memberService = memberService;
        }

        public async Task AddMemberAsync(Member member)
        {
            
            await _memberService.AddAsync(member);
            
        }

        public async Task UpdateMemberAsync(Member member)
        {
            
            await _memberService.UpdateAsync(member); 
        }

        public async Task DeleteMemberAsync(int id)
        {
            
            await _memberService.DeleteAsync(id); 
        }
    }
}