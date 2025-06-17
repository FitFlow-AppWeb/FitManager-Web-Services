// Members/Interfaces/REST/Controllers/MembershipTypeController.cs

using Microsoft.AspNetCore.Mvc;
using FitManager_Web_Services.Members.Application.Internal.QueryServices;
using FitManager_Web_Services.Members.Domain.Model.Queries; 
using FitManager_Web_Services.Members.Interfaces.REST.Resources; 
using FitManager_Web_Services.Members.Interfaces.REST.Transform; 
using Swashbuckle.AspNetCore.Annotations; 
namespace FitManager_Web_Services.Members.Interfaces.REST.Controllers
{
    /// <summary>
    /// REST API controller for managing membership types.
    /// This controller exposes endpoints for retrieving membership type resources.
    /// It acts as the entry point from the presentation layer (REST) to the application layer for read operations.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")] // Defines the base route for this controller
    public class MembershipTypeController : ControllerBase
    {
        private readonly MembershipTypeQueryService _membershipTypeQueryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipTypeController"/> class.
        /// </summary>
        /// <param name="membershipTypeQueryService">The query service for membership type retrieval.</param>
        public MembershipTypeController(MembershipTypeQueryService membershipTypeQueryService)
        {
            _membershipTypeQueryService = membershipTypeQueryService;
        }

        /// <summary>
        /// Gets a list of all available membership types.
        /// </summary>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> containing an enumerable collection of <see cref="MembershipTypeResource"/>.
        /// Returns 200 OK with the list of membership types.
        /// </returns>
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