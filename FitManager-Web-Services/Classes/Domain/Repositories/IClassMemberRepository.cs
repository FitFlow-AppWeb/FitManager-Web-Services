using FitManager_Web_Services.Classes.Domain.Model.Aggregates;

namespace FitManager_Web_Services.Classes.Domain.Repositories
{
    /// <summary>
    /// Defines the contract for a repository that manages <see cref="ClassMember"/> entities.
    /// </summary>
    /// <remarks>
    /// This interface provides methods specifically for managing the many-to-many relationship
    /// between members and classes through the <see cref="ClassMember"/> join entity.
    /// It focuses on adding, removing, checking existence, and finding specific associations.
    /// It resides in the Domain layer, acting as an abstraction over data persistence.
    /// </remarks>
    public interface IClassMemberRepository
    {
        /// <summary>
        /// Asynchronously adds a new <see cref="ClassMember"/> association to the repository.
        /// </summary>
        /// <param name="classMember">The <see cref="ClassMember"/> entity to add, representing the association between a member and a class.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task AddAsync(ClassMember classMember);

        /// <summary>
        /// Asynchronously removes a <see cref="ClassMember"/> association from the repository based on member and class IDs.
        /// </summary>
        /// <param name="memberId">The unique identifier of the member in the association to remove.</param>
        /// <param name="classId">The unique identifier of the class in the association to remove.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task RemoveAsync(int memberId, int classId);

        /// <summary>
        /// Asynchronously checks if a <see cref="ClassMember"/> association already exists for the given member and class IDs.
        /// </summary>
        /// <param name="memberId">The unique identifier of the member.</param>
        /// <param name="classId">The unique identifier of the class.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains <c>true</c> if the association exists, otherwise <c>false</c>.
        /// </returns>
        Task<bool> ExistsAsync(int memberId, int classId);

        /// <summary>
        /// Asynchronously retrieves a <see cref="ClassMember"/> association by its composite primary key (member ID and class ID).
        /// </summary>
        /// <param name="memberId">The unique identifier of the member.</param>
        /// <param name="classId">The unique identifier of the class.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous operation.
        /// The task result contains the <see cref="ClassMember"/> if found, otherwise null.
        /// </returns>
        Task<ClassMember?> FindByIdsAsync(int memberId, int classId);
    }
}