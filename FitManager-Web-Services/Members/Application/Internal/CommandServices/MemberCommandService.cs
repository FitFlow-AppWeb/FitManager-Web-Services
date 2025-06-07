using System.Threading.Tasks;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Repositories;

namespace FitManager_Web_Services.Members.Application.Internal.CommandServices
{
    public class MemberCommandService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberCommandService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task AddMemberAsync(Member member)
        {
            await _memberRepository.AddAsync(member);
        }

        public async Task UpdateMemberAsync(Member member)
        {
            await _memberRepository.UpdateAsync(member);
        }

        public async Task DeleteMemberAsync(int id)
        {
            await _memberRepository.DeleteAsync(id);
        }
    }
}