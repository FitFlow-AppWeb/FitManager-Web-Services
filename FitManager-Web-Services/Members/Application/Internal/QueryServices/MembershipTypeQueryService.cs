using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Domain.Model.Queries; 
using FitManager_Web_Services.Members.Domain.Repositories; 

namespace FitManager_Web_Services.Members.Application.Internal.QueryServices
{
    public class MembershipTypeQueryService
    {
        private readonly IMembershipTypeRepository _membershipTypeRepository;

        public MembershipTypeQueryService(IMembershipTypeRepository membershipTypeRepository)
        {
            _membershipTypeRepository = membershipTypeRepository;
        }

        public async Task<IEnumerable<MembershipType>> Handle(GetAllMembershipTypesQuery query)
        {
            return await _membershipTypeRepository.GetAllAsync();
        }

        public async Task<MembershipType?> Handle(GetMembershipTypeByIdQuery query)
        {
            return await _membershipTypeRepository.GetByIdAsync(query.Id);
        }
    }
}