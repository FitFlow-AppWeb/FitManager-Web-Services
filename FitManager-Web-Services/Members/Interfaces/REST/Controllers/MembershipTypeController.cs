// Members/Interfaces/REST/Controllers/MembershipTypeController.cs

using Microsoft.AspNetCore.Mvc;
using FitManager_Web_Services.Members.Application.Internal.QueryServices;
using FitManager_Web_Services.Members.Application.Internal.CommandServices;
using FitManager_Web_Services.Members.Domain.Model.Commands; 
using FitManager_Web_Services.Members.Domain.Model.Queries; 
using FitManager_Web_Services.Members.Interfaces.REST.Resources;
using FitManager_Web_Services.Members.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net.Mime;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using FitManager_Web_Services.Resources;

namespace FitManager_Web_Services.Members.Interfaces.REST.Controllers
{
    /// <summary>
    /// REST API controller for managing membership types.
    /// This controller exposes endpoints for retrieving, creating, updating, and deleting membership type resources.
    /// It acts as the entry point from the presentation layer (REST) to the application layer for all CRUD operations.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class MembershipTypeController : ControllerBase
    {
        private readonly MembershipTypeQueryService _membershipTypeQueryService;
        private readonly IMembershipTypeCommandService _membershipTypeCommandService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipTypeController"/> class.
        /// </summary>
        /// <param name="membershipTypeQueryService">The query service for membership type retrieval.</param>
        /// <param name="membershipTypeCommandService">The command service for membership type operations.</param>
        public MembershipTypeController(
            MembershipTypeQueryService membershipTypeQueryService,
            IMembershipTypeCommandService membershipTypeCommandService,
            IStringLocalizer<SharedResource> localizer)
        {
            _membershipTypeQueryService = membershipTypeQueryService;
            _membershipTypeCommandService = membershipTypeCommandService;
            _localizer = localizer;
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
            Summary = "List All Membership Types",
            Description = "Retrieves a list of all available membership types."
        )]
        [ProducesResponseType(typeof(IEnumerable<MembershipTypeResource>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult<IEnumerable<MembershipTypeResource>>> GetAllMembershipTypes()
        {
            try
            {
                var getAllQuery = new GetAllMembershipTypesQuery(); 
                var membershipTypes = await _membershipTypeQueryService.Handle(getAllQuery);
    
                var membershipTypeResources = MembershipTypeResourceFromEntityAssembler.ToResourceFromEntities(membershipTypes);
                var message = _localizer["MembershipTypesRetrieved"];
                return Ok(new { message, data = membershipTypeResources });
            }
            catch (System.Exception ex)
            {
                var error = _localizer["ErrorRetrievingMembershipTypes"];
                return StatusCode(500, new { message = $"{error}: {ex.Message}" });
            }
        }



        /// <summary>
        /// Creates a new membership type.
        /// </summary>
        /// <param name="resource">The resource containing membership type details (Name, Description, Price, Duration, Benefits).</param>
        /// <returns>The created membership type resource with its ID and a 201 Created status.</returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create Membership Type",
            Description = "Creates a new membership type in the system with its details."
        )]
        [ProducesResponseType(typeof(MembershipTypeResource), 201)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [HttpPost]
        public async Task<IActionResult> CreateMembershipType([FromBody] CreateMembershipTypeResource resource)
        {
            if (!ModelState.IsValid)
            {
                var error = _localizer["InvalidData"];
                return BadRequest(new { message = error });
            }

            try
            {
                var command = CreateMembershipTypeCommandFromResourceAssembler.ToCommandFromResource(resource);
                var created = await _membershipTypeCommandService.Handle(command);

                if (created == null)
                {
                    var error = _localizer["MembershipTypeCreationFailed"];
                    return BadRequest(new { message = error });
                }

                var membershipTypeResource = MembershipTypeResourceFromEntityAssembler.ToResourceFromEntity(created);
                var message = _localizer["MembershipTypeCreated"];
                return StatusCode(201, new { message, data = membershipTypeResource });
            }
            catch (System.Exception ex)
            {
                var error = _localizer["ErrorCreatingMembershipType"];
                return StatusCode(500, new { message = $"{error}: {ex.Message}" });
            }
        }


    /// <summary>
        /// Updates an existing membership type.
        /// </summary>
        /// <param name="id">The ID of the membership type to update.</param>
        /// <param name="resource">The resource containing updated membership type details.</param>
        /// <returns>The updated membership type resource with a 200 OK status, or 404 if not found.</returns>
        [HttpPut("{id:int}")]
        [SwaggerOperation(
            Summary = "Update Membership Type",
            Description = "Updates the details of an existing membership type by its ID."
        )]
        [ProducesResponseType(typeof(MembershipTypeResource), 200)]
        [ProducesResponseType(typeof(void), 404)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> UpdateMembershipType(int id, [FromBody] UpdateMembershipTypeResource resource)
        {
            if (!ModelState.IsValid)
            {
                var error = _localizer["InvalidData"];
                return BadRequest(new { message = error });
            }

            try
            {
                var command = UpdateMembershipTypeCommandFromResourceAssembler.ToCommandFromResource(id, resource);
                var updated = await _membershipTypeCommandService.Handle(command);

                if (updated == null)
                {
                    return NotFound(new { message = _localizer["MembershipTypeNotFound"] });
                }

                var membershipTypeResource = MembershipTypeResourceFromEntityAssembler.ToResourceFromEntity(updated);
                var message = _localizer["MembershipTypeUpdated"];
                return Ok(new { message, data = membershipTypeResource });
            }
            catch (System.Exception ex)
            {
                var error = _localizer["ErrorUpdatingMembershipType"];
                return StatusCode(500, new { message = $"{error}: {ex.Message}" });
            }
        }

        /// <summary>
        /// Deletes a membership type by its ID.
        /// </summary>
        /// <param name="id">The ID of the membership type to delete.</param>
        /// <returns>A 204 No Content status if successful, or 404 if not found.</returns>
        [HttpDelete("{id:int}")]
        [SwaggerOperation(
            Summary = "Delete Membership Type",
            Description = "Deletes an existing membership type from the system by its ID."
        )]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> DeleteMembershipType(int id)
        {
            try
            {
                var command = new DeleteMembershipTypeCommand(id);
                var result = await _membershipTypeCommandService.Handle(command);

                if (!result)
                {
                    return NotFound(new { message = _localizer["MembershipTypeNotFound"] });
                }

                return NoContent();
            }
            catch (System.Exception ex)
            {
                var error = _localizer["ErrorDeletingMembershipType"];
                return StatusCode(500, new { message = $"{error}: {ex.Message}" });
            }
        }
    }
}