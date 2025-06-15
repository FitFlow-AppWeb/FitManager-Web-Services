// Members/Domain/Services/MemberService.cs

using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Repositories;
using FitManager_Web_Services.Shared.Domain.Repositories; 

namespace FitManager_Web_Services.Members.Domain.Services
{
    /// <summary>
    /// Implements the <see cref="IMemberService"/> interface, providing concrete implementations
    /// for member-related domain operations.
    /// While named a "Domain Service," its current implementation primarily acts as a direct facade
    /// to the <see cref="IMemberRepository"/> and orchestrates the Unit of Work.
    /// In a more complex DDD scenario, this service might contain business logic that spans multiple aggregates.
    /// </summary>
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork; 

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberService"/> class.
        /// </summary>
        /// <param name="memberRepository">The member repository.</param>
        /// <param name="unitOfWork">The unit of work for managing transactions.</param>
        public MemberService(IMemberRepository memberRepository, IUnitOfWork unitOfWork) 
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork; 
        }

        /// <summary>
        /// Asynchronously retrieves all members from the repository.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains an enumerable collection of <see cref="Member"/> objects.</returns>
        public Task<IEnumerable<Member>> GetAllAsync()
        {
            return _memberRepository.GetAllAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a member by their unique identifier from the repository.
        /// </summary>
        /// <param name="id">The unique identifier of the member.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the <see cref="Member"/> if found, otherwise null.</returns>
        public Task<Member> GetByIdAsync(int id)
        {
            return _memberRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Asynchronously retrieves a member by their DNI (National Identity Document) from the repository.
        /// </summary>
        /// <param name="dni">The DNI of the member.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the <see cref="Member"/> if found, otherwise null.</returns>
        public Task<Member> GetByDniAsync(int dni)
        {
            return _memberRepository.GetByDniAsync(dni);
        }

        /// <summary>
        /// Asynchronously adds a new member to the repository and commits the changes.
        /// </summary>
        /// <param name="member">The <see cref="Member"/> object to add.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task AddAsync(Member member)
        {
            await _memberRepository.AddAsync(member);
            await _unitOfWork.CompleteAsync(); 
        }

        /// <summary>
        /// Asynchronously updates an existing member in the repository and commits the changes.
        /// </summary>
        /// <param name="member">The <see cref="Member"/> object to update.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task UpdateAsync(Member member)
        {
            await _memberRepository.UpdateAsync(member);
            await _unitOfWork.CompleteAsync(); 
        }

        /// <summary>
        /// Asynchronously deletes a member by their unique identifier from the repository and commits the changes.
        /// </summary>
        /// <param name="id">The unique identifier of the member to delete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task DeleteAsync(int id)
        {
            await _memberRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync(); 
        }
    }
}