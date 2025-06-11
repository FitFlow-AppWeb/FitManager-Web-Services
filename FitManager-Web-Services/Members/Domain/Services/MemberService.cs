using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories; 

namespace FitManager_Web_Services.Members.Domain.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork; 

        public MemberService(IMemberRepository memberRepository, IUnitOfWork unitOfWork) 
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork; // <-- ¡Asignar!
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

        public async Task AddAsync(Member member)
        {
            await _memberRepository.AddAsync(member);
            await _unitOfWork.CompleteAsync(); 
        }

        public async Task UpdateAsync(Member member)
        {
            await _memberRepository.UpdateAsync(member);
            await _unitOfWork.CompleteAsync(); 
        }

        public async Task DeleteAsync(int id)
        {
            await _memberRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync(); 
        }
    }
}