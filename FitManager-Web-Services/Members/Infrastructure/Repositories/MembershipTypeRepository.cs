// Members/Infrastructure/Repositories/MembershipTypeRepository.cs

using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration; // Required for AppDbContext
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories; // Required for BaseRepository
using System.Threading.Tasks; // Required for Task

namespace FitManager_Web_Services.Members.Infrastructure.Repositories
{
    /// <summary>
    /// Implements the <see cref="IMembershipTypeRepository"/> interface, providing concrete data access operations for <see cref="MembershipType"/> aggregates.
    /// This repository leverages Entity Framework Core and extends <see cref="BaseRepository{TEntity}"/> for common CRUD functionalities.
    /// It resides in the Infrastructure layer, handling the details of data persistence.
    /// </summary>
    public class MembershipTypeRepository : BaseRepository<MembershipType>, IMembershipTypeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipTypeRepository"/> class.
        /// </summary>
        /// <param name="context">The application's database context (AppDbContext).</param>
        public MembershipTypeRepository(AppDbContext context) : base(context) 
        {
    
        }

    }
}