// Employees/Interfaces/REST/Controllers/SpecialtiesController.cs

using FitManager_Web_Services.Employees.Application.Internal.CommandServices;
using FitManager_Web_Services.Employees.Application.Internal.QueryServices;
using FitManager_Web_Services.Employees.Domain.Model.Commands; // Para CreateSpecialtyCommand, DeleteSpecialtyCommand
using FitManager_Web_Services.Employees.Domain.Model.Queries;   // Para GetAllSpecialtiesQuery
using FitManager_Web_Services.Employees.Interfaces.REST.Resources; // Para CreateSpecialtyResource, SpecialtyResource
using FitManager_Web_Services.Employees.Interfaces.REST.Transform; // Para los ensambladores
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations; // Para MediaTypeNames

namespace FitManager_Web_Services.Employees.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")] // Ruta base para el controlador: /api/v1/specialties
    [Produces(MediaTypeNames.Application.Json)]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtyCommandService _specialtyCommandService;
        private readonly ISpecialtyQueryService _specialtyQueryService;

        public SpecialtiesController(
            ISpecialtyCommandService specialtyCommandService,
            ISpecialtyQueryService specialtyQueryService)
        {
            _specialtyCommandService = specialtyCommandService;
            _specialtyQueryService = specialtyQueryService;
        }

        /// <summary>
        /// Creates a new Specialty.
        /// </summary>
        /// <param name="resource">The resource containing the new specialty data.</param>
        /// <returns>The created specialty resource if successful, otherwise BadRequest.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new Specialty")]
        [SwaggerResponse(201, "The specialty was created successfully", typeof(SpecialtyResource))]
        [SwaggerResponse(400, "Invalid input data")]
        public async Task<ActionResult<SpecialtyResource>> CreateSpecialty(CreateSpecialtyResource resource)
        {
            var createSpecialtyCommand = CreateSpecialtyCommandFromResourceAssembler.ToCommandFromResource(resource);
            var specialty = await _specialtyCommandService.Handle(createSpecialtyCommand);

            if (specialty is null)
            {
                return BadRequest("Unable to create specialty. Employee might not exist.");
            }

            var specialtyResource = SpecialtyResourceFromEntityAssembler.ToResourceFromEntity(specialty);
            return CreatedAtAction(nameof(GetSpecialties), new { id = specialtyResource.Id }, specialtyResource);
        }

        /// <summary>
        /// Gets all Specialties.
        /// </summary>
        /// <returns>A list of specialty resources.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Gets all Specialties")]
        [SwaggerResponse(200, "A list of specialties was retrieved successfully", typeof(IEnumerable<SpecialtyResource>))]
        public async Task<ActionResult<IEnumerable<SpecialtyResource>>> GetSpecialties()
        {
            var getAllSpecialtiesQuery = new GetAllSpecialtiesQuery();
            var specialties = await _specialtyQueryService.Handle(getAllSpecialtiesQuery);

            var specialtyResources = specialties.Select(SpecialtyResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(specialtyResources);
        }

        /// <summary>
        /// Deletes a Specialty by its ID.
        /// </summary>
        /// <param name="id">The ID of the specialty to delete.</param>
        /// <returns>NoContent if successful, otherwise NotFound.</returns>
        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Deletes a Specialty by ID")]
        [SwaggerResponse(204, "The specialty was deleted successfully")]
        [SwaggerResponse(404, "Specialty not found")]
        public async Task<IActionResult> DeleteSpecialty(int id)
        {
            var deleteSpecialtyCommand = new DeleteSpecialtyCommand(id);
            var result = await _specialtyCommandService.Handle(deleteSpecialtyCommand);

            if (!result)
            {
                return NotFound("Specialty not found or could not be deleted.");
            }

            return NoContent();
        }
    }
}