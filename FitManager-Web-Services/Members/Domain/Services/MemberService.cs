using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Repositories;

namespace FitManager_Web_Services.Members.Domain.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public Task<IEnumerable<Member>> GetAllAsync()
        {
            return _memberRepository.GetAllAsync();
        }

        public Task<Member> GetByIdAsync(int id)
        {
            return _memberRepository.GetByIdAsync(id);
        }

        public Task<Member> GetByDniAsync(int dni)
        {
            return _memberRepository.GetByDniAsync(dni);
        }

        public Task AddAsync(Member member)
        {
            return _memberRepository.AddAsync(member);
        }

        public Task UpdateAsync(Member member)
        {
            return _memberRepository.UpdateAsync(member);
        }

        public Task DeleteAsync(int id)
        {
            return _memberRepository.DeleteAsync(id);
        }
    }
}