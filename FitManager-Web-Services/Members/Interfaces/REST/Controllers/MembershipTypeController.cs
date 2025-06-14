using Microsoft.AspNetCore.Mvc;
using FitManager_Web_Services.Members.Application.Internal.QueryServices;
using FitManager_Web_Services.Members.Domain.Model.Queries; 
using FitManager_Web_Services.Members.Interfaces.REST.Resources; 
using FitManager_Web_Services.Members.Interfaces.REST.Transform; 
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(
            Summary = "Listar todos los Tipos de Membresía",
            Description = "Obtiene una lista de todos los tipos de membresía disponibles."
        )]
        public async Task<ActionResult<IEnumerable<MembershipTypeResource>>> GetAllMembershipTypes()
        {
            var getAllQuery = new GetAllMembershipTypesQuery();
            var membershipTypes = await _membershipTypeQueryService.Handle(getAllQuery);
            var membershipTypeResources = MembershipTypeResourceFromEntityAssembler.ToResourceListFromEntityList(membershipTypes);
            return Ok(membershipTypeResources);
        }

        
    }
}