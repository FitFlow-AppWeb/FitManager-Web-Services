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

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipTypeController"/> class.
        /// </summary>
        /// <param name="membershipTypeQueryService">The query service for membership type retrieval.</param>
        /// <param name="membershipTypeCommandService">The command service for membership type operations.</param>
        public MembershipTypeController(
            MembershipTypeQueryService membershipTypeQueryService,
            IMembershipTypeCommandService membershipTypeCommandService)
        {
            _membershipTypeQueryService = membershipTypeQueryService;
            _membershipTypeCommandService = membershipTypeCommandService;
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
                return Ok(membershipTypeResources);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving membership types: {ex.Message}");
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
        public async Task<IActionResult> CreateMembershipType([FromBody] CreateMembershipTypeResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // N: Tipado explícito de la variable command para resolver ambigüedad
                CreateMembershipTypeCommand command = CreateMembershipTypeCommandFromResourceAssembler.ToCommandFromResource(resource);
                var createdMembershipType = await _membershipTypeCommandService.Handle(command);

                if (createdMembershipType == null)
                {
                    return BadRequest("Unable to create membership type. Please check the provided data.");
                }

                var membershipTypeResource = MembershipTypeResourceFromEntityAssembler.ToResourceFromEntity(createdMembershipType);
                // NOTA: Si eliminamos GetMembershipTypeById, CreatedAtAction ya no es válido para apuntar a ese método.
                // Podríamos usar CreatedAtRoute si tenemos una ruta nombrada, o simplemente Ok(resource) con status 201.
                // Para simplificar, si no hay un endpoint GetById, podemos retornar un 201 OK con el recurso.
                return StatusCode(201, membershipTypeResource); // Retorna 201 Created con el recurso en el cuerpo
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating membership type: {ex.Message}");
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
                return BadRequest(ModelState);
            }

            try
            {
                
                UpdateMembershipTypeCommand command = UpdateMembershipTypeCommandFromResourceAssembler.ToCommandFromResource(id, resource);
                var updatedMembershipType = await _membershipTypeCommandService.Handle(command);

                if (updatedMembershipType == null)
                {
                    return NotFound();
                }

                var membershipTypeResource = MembershipTypeResourceFromEntityAssembler.ToResourceFromEntity(updatedMembershipType);
                return Ok(membershipTypeResource);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating membership type: {ex.Message}");
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
                DeleteMembershipTypeCommand command = new DeleteMembershipTypeCommand(id);
                var result = await _membershipTypeCommandService.Handle(command);

                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting membership type: {ex.Message}");
            }
        }
    }
}