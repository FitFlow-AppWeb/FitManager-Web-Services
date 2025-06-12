using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories; 


namespace FitManager_Web_Services.Members.Infrastructure.Repositories
{

    public class MembershipTypeRepository : BaseRepository<MembershipType>, IMembershipTypeRepository
    {
      

        public MembershipTypeRepository(AppDbContext context) : base(context) 
        {
          
        }

      
    }
}