// Employees/Interfaces/REST/Controllers/CertificationsController.cs

using FitManager_Web_Services.Employees.Application.Internal.CommandServices;
using FitManager_Web_Services.Employees.Application.Internal.QueryServices;
using FitManager_Web_Services.Employees.Domain.Model.Commands; // Para CreateCertificationCommand, DeleteCertificationCommand
using FitManager_Web_Services.Employees.Domain.Model.Queries;   // Para GetAllCertificationsQuery
using FitManager_Web_Services.Employees.Interfaces.REST.Resources; // Para CreateCertificationResource, CertificationResource
using FitManager_Web_Services.Employees.Interfaces.REST.Transform; 
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;

namespace FitManager_Web_Services.Employees.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")] // Ruta base para el controlador: /api/v1/certifications
    [Produces(MediaTypeNames.Application.Json)]
    public class CertificationsController : ControllerBase
    {
        private readonly ICertificationCommandService _certificationCommandService;
        private readonly ICertificationQueryService _certificationQueryService;

        public CertificationsController(
            ICertificationCommandService certificationCommandService,
            ICertificationQueryService certificationQueryService)
        {
            _certificationCommandService = certificationCommandService;
            _certificationQueryService = certificationQueryService;
        }

        /// <summary>
        /// Creates a new Certification.
        /// </summary>
        /// <param name="resource">The resource containing the new certification data.</param>
        /// <returns>The created certification resource if successful, otherwise BadRequest.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new Certification")]
        [SwaggerResponse(201, "The certification was created successfully", typeof(CertificationResource))]
        [SwaggerResponse(400, "Invalid input data")]
        public async Task<ActionResult<CertificationResource>> CreateCertification(CreateCertificationResource resource)
        {
            var createCertificationCommand = CreateCertificationCommandFromResourceAssembler.ToCommandFromResource(resource);
            var certification = await _certificationCommandService.Handle(createCertificationCommand);

            if (certification is null)
            {
                return BadRequest("Unable to create certification. Employee might not exist.");
            }

            var certificationResource = CertificationResourceFromEntityAssembler.ToResourceFromEntity(certification);
            return CreatedAtAction(nameof(GetCertifications), new { id = certificationResource.Id }, certificationResource);
        }

        /// <summary>
        /// Gets all Certifications.
        /// </summary>
        /// <returns>A list of certification resources.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Gets all Certifications")]
        [SwaggerResponse(200, "A list of certifications was retrieved successfully", typeof(IEnumerable<CertificationResource>))]
        public async Task<ActionResult<IEnumerable<CertificationResource>>> GetCertifications()
        {
            var getAllCertificationsQuery = new GetAllCertificationsQuery();
            var certifications = await _certificationQueryService.Handle(getAllCertificationsQuery);

            var certificationResources = certifications.Select(CertificationResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(certificationResources);
        }

        /// <summary>
        /// Deletes a Certification by its ID.
        /// </summary>
        /// <param name="id">The ID of the certification to delete.</param>
        /// <returns>NoContent if successful, otherwise NotFound.</returns>
        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Deletes a Certification by ID")]
        [SwaggerResponse(204, "The certification was deleted successfully")]
        [SwaggerResponse(404, "Certification not found")]
        public async Task<IActionResult> DeleteCertification(int id)
        {
            var deleteCertificationCommand = new DeleteCertificationCommand(id);
            var result = await _certificationCommandService.Handle(deleteCertificationCommand);

            if (!result)
            {
                return NotFound("Certification not found or could not be deleted.");
            }

            return NoContent();
        }
    }
}