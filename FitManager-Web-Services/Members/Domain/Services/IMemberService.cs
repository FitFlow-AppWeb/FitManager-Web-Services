// Members/Domain/Services/IMemberService.cs

using FitManager_Web_Services.Members.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Members.Domain.Services
{
    /// <summary>
    /// Defines the contract for the domain service responsible for member-related operations.
    /// In Domain-Driven Design (DDD), domain services encapsulate business logic that
    /// doesn't naturally fit within a single aggregate, or orchestrates operations across multiple aggregates.
    /// However, given the current structure, these operations often mirror repository operations
    /// and might be seen as part of the Application layer's command/query services.
    /// This interface provides a facade for member management.
    /// </summary>
    public interface IMemberService
    {
        /// <summary>
        /// Asynchronously retrieves all members.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains an enumerable collection of <see cref="Member"/> objects.</returns>
        Task<IEnumerable<Member>> GetAllAsync();
        
        /// <summary>
        /// Asynchronously retrieves a member by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the member.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the <see cref="Member"/> if found.</returns>
        Task<Member> GetByIdAsync(int id);
        
        /// <summary>
        /// Asynchronously retrieves a member by their DNI (National Identity Document).
        /// </summary>
        /// <param name="dni">The DNI of the member.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result
        /// contains the <see cref="Member"/> if found.</returns>
        Task<Member> GetByDniAsync(int dni);
        
        /// <summary>
        /// Asynchronously adds a new member.
        /// </summary>
        /// <param name="member">The <see cref="Member"/> object to add.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task AddAsync(Member member);
        
        /// <summary>
        /// Asynchronously updates an existing member.
        /// </summary>
        /// <param name="member">The <see cref="Member"/> object to update.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task UpdateAsync(Member member);
        
        /// <summary>
        /// Asynchronously deletes a member by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the member to delete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task DeleteAsync(int id);
    }
}