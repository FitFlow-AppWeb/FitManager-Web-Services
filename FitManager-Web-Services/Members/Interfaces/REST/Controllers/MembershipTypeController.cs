using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Members.Application.Internal.QueryServices;
using FitManager_Web_Services.Members.Domain.Model.Queries; 
using FitManager_Web_Services.Members.Interfaces.REST.Resources; 
using FitManager_Web_Services.Members.Interfaces.REST.Transform; 

namespace FitManager_Web_Services.Members.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")] 
    public class MembershipTypeController : ControllerBase
    {
        private readonly MembershipTypeQueryService _membershipTypeQueryService;

        public MembershipTypeController(MembershipTypeQueryService membershipTypeQueryService)
        {
            _membershipTypeQueryService = membershipTypeQueryService;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembershipTypeResource>>> GetAllMembershipTypes()
        {
            var getAllQuery = new GetAllMembershipTypesQuery();
            var membershipTypes = await _membershipTypeQueryService.Handle(getAllQuery);
            var membershipTypeResources = MembershipTypeResourceFromEntityAssembler.ToResourceListFromEntityList(membershipTypes);
            return Ok(membershipTypeResources);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<MembershipTypeResource>> GetMembershipTypeById(int id)
        {
            var getByIdQuery = new GetMembershipTypeByIdQuery(id);
            var membershipType = await _membershipTypeQueryService.Handle(getByIdQuery);

            if (membershipType == null)
            {
                return NotFound(); 
            }

            var membershipTypeResource = MembershipTypeResourceFromEntityAssembler.ToResourceFromEntity(membershipType);
            return Ok(membershipTypeResource);
        }
    }
}