// Members/Interfaces/REST/Controllers/MemberController.cs

using Microsoft.AspNetCore.Mvc;
using FitManager_Web_Services.Members.Application.Internal.CommandServices;
using FitManager_Web_Services.Members.Application.Internal.QueryServices;
using FitManager_Web_Services.Members.Interfaces.REST.Resources;
using FitManager_Web_Services.Members.Interfaces.REST.Transform;
using FitManager_Web_Services.Members.Domain.Model.Commands;
using FitManager_Web_Services.Members.Domain.Model.Queries;
using FitManager_Web_Services.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations; 

namespace FitManager_Web_Services.Members.Interfaces.REST.Controllers
{
    /// <summary>
    /// REST API controller for managing members.
    /// This controller exposes endpoints for creating, retrieving, updating, and deleting member resources.
    /// It acts as the entry point from the presentation layer (REST) to the application layer.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")] // Defines the base route for this controller
    public class MemberController : ControllerBase
    {
        private readonly MemberCommandService _memberCommandService;
        private readonly MemberQueryService _memberQueryService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberController"/> class.
        /// </summary>
        /// <param name="memberCommandService">The command service for member operations.</param>
        /// <param name="memberQueryService">The query service for member retrieval.</param>
        public MemberController(MemberCommandService memberCommandService, MemberQueryService memberQueryService, IStringLocalizer<SharedResource> localizer)
        {
            _memberCommandService = memberCommandService;
            _memberQueryService = memberQueryService;
            _localizer = localizer;
        }

        /// <summary>
        /// Creates a new member in the system.
        /// </summary>
        /// <param name="resource">The resource containing the data for the new member.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the result of the operation.
        /// Returns 201 Created if successful, 400 Bad Request if validation fails or member creation fails.
        /// </returns>
        [HttpPost]
        [Authorize]
        [SwaggerOperation(
            Summary = "Añadir Miembro",
            Description = "Crea un nuevo miembro en el sistema."
        )]
        public async Task<IActionResult> CreateMember([FromBody] CreateMemberResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createCommand = CreateMemberCommandFromResourceAssembler.ToCommandFromResource(resource);
            
            var member = await _memberCommandService.Handle(createCommand);

            if (member == null)
            {
                var errorMessage = _localizer["MemberCreationFailed"];
                return BadRequest(new { message = errorMessage });
            }

            var memberResource = MemberResourceFromEntityAssembler.ToResourceFromEntity(member);
            var successMessage = _localizer["MemberCreatedSuccessfully"];
            return Created(string.Empty, new { message = successMessage, data = memberResource });
        }

        /// <summary>
        /// Gets a list of all registered members in the system.
        /// </summary>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> containing an enumerable collection of <see cref="MemberResource"/>.
        /// Returns 200 OK with the list of members.
        /// </returns>
        [HttpGet]
        [Authorize]
        [SwaggerOperation(
            Summary = "Listar todos los Miembros",
            Description = "Obtiene una lista de todos los miembros registrados en el sistema."
        )]
        public async Task<ActionResult<IEnumerable<MemberResource>>> GetAllMembers()
        {
            var getAllQuery = new GetAllMembersQuery();
            
            var members = await _memberQueryService.Handle(getAllQuery);
            
            var memberResources = MemberResourceFromEntityAssembler.ToResourceListFromEntityList(members);
            var successMessage = _localizer["MembersRetrievedSuccessfully"];
            return Ok(new { message = successMessage, data = memberResources });
        }
        
        /// <summary>
        /// Updates the data of an existing member, including their membership status.
        /// </summary>
        /// <param name="id">The unique identifier of the member to update.</param>
        /// <param name="resource">The resource containing the updated data for the member.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the result of the operation.
        /// Returns 200 OK if successful, 400 Bad Request if validation fails, 404 Not Found if the member does not exist.
        /// </returns>
        [HttpPut("{id}")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Actualizar Miembro",
            Description = "Actualiza los datos de un miembro existente, incluyendo su estado de membresía."
        )]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] UpdateMemberResource resource) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateCommand = UpdateMemberCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            
            var updatedMember = await _memberCommandService.Handle(updateCommand); 

            if (updatedMember == null)
            {
                var errorMessage = _localizer["MemberNotFound"];
                return NotFound(new { message = errorMessage });
            }

            var memberResource = MemberResourceFromEntityAssembler.ToResourceFromEntity(updatedMember);
            var successMessage = _localizer["MemberUpdatedSuccessfully"];
            return Ok(new { message = successMessage, data = memberResource });

        }

        /// <summary>
        /// Deletes an existing member from the system by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the member to delete.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> representing the result of the operation.
        /// Returns 204 No Content if successful, 404 Not Found if the member does not exist.
        /// </returns>
        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Eliminar Miembro",
            Description = "Elimina un miembro existente del sistema por su ID."
        )]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var deleteCommand = new DeleteMemberCommand(id);
            
            var success = await _memberCommandService.Handle(deleteCommand);

            if (!success)
            {
                var errorMessage = _localizer["MemberNotFound"];
                return NotFound(new { message = errorMessage });
            }

            var successMessage = _localizer["MemberDeletedSuccessfully"];
            return NoContent(); // o: return Ok(new { message = successMessage });

        }
    }
}