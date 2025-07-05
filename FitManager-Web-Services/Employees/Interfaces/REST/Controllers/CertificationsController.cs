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
using FitManager_Web_Services.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace FitManager_Web_Services.Employees.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")] // Ruta base para el controlador: /api/v1/certifications
    [Produces(MediaTypeNames.Application.Json)]
    public class CertificationsController : ControllerBase
    {
        private readonly ICertificationCommandService _certificationCommandService;
        private readonly ICertificationQueryService _certificationQueryService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CertificationsController(
            ICertificationCommandService certificationCommandService,
            ICertificationQueryService certificationQueryService,
            IStringLocalizer<SharedResource> localizer)
        {
            _certificationCommandService = certificationCommandService;
            _certificationQueryService = certificationQueryService;
            _localizer = localizer;
        }

        /// <summary>
        /// Creates a new Certification.
        /// </summary>
        /// <param name="resource">The resource containing the new certification data.</param>
        /// <returns>The created certification resource if successful, otherwise BadRequest.</returns>
        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = "Creates a new Certification")]
        [SwaggerResponse(201, "The certification was created successfully", typeof(CertificationResource))]
        [SwaggerResponse(400, "Invalid input data")]
        public async Task<IActionResult> CreateCertification(CreateCertificationResource resource)
        {
            var createCommand = CreateCertificationCommandFromResourceAssembler.ToCommandFromResource(resource);
            var certification = await _certificationCommandService.Handle(createCommand);

            if (certification is null)
            {
                var message = _localizer["CertificationCreateFailed"];
                return BadRequest(new
                {
                    message = new
                    {
                        name = "CertificationCreateFailed",
                        value = message.Value,
                        resourceNotFound = message.ResourceNotFound,
                        searchedLocation = message.SearchedLocation
                    }
                });
            }

            var certificationResource = CertificationResourceFromEntityAssembler.ToResourceFromEntity(certification);
            var msg = _localizer["CertificationCreated"];
            return CreatedAtAction(nameof(GetCertifications), new { id = certificationResource.Id }, new
            {
                message = new
                {
                    name = "CertificationCreated",
                    value = msg.Value,
                    resourceNotFound = msg.ResourceNotFound,
                    searchedLocation = msg.SearchedLocation
                },
                data = certificationResource
            });
        }

        /// <summary>
        /// Gets all Certifications.
        /// </summary>
        /// <returns>A list of certification resources.</returns>
        [HttpGet]
        [Authorize]
        [SwaggerOperation(Summary = "Gets all Certifications")]
        [SwaggerResponse(200, "A list of certifications was retrieved successfully", typeof(IEnumerable<CertificationResource>))]
        public async Task<IActionResult> GetCertifications()
        {
            var query = new GetAllCertificationsQuery();
            var certifications = await _certificationQueryService.Handle(query);
            var resources = certifications.Select(CertificationResourceFromEntityAssembler.ToResourceFromEntity);

            var msg = _localizer["CertificationsRetrieved"];
            return Ok(new
            {
                message = new
                {
                    name = "CertificationsRetrieved",
                    value = msg.Value,
                    resourceNotFound = msg.ResourceNotFound,
                    searchedLocation = msg.SearchedLocation
                },
                data = resources
            });
        }

        /// <summary>
        /// Deletes a Certification by its ID.
        /// </summary>
        /// <param name="id">The ID of the certification to delete.</param>
        /// <returns>NoContent if successful, otherwise NotFound.</returns>
        [HttpDelete("{id:int}")]
        [Authorize]
        [SwaggerOperation(Summary = "Deletes a Certification by ID")]
        [SwaggerResponse(204, "The certification was deleted successfully")]
        [SwaggerResponse(404, "Certification not found")]
        public async Task<IActionResult> DeleteCertification(int id)
        {
            var deleteCommand = new DeleteCertificationCommand(id);
            var result = await _certificationCommandService.Handle(deleteCommand);

            if (!result)
            {
                var message = _localizer["CertificationNotFound"];
                return NotFound(new
                {
                    message = new
                    {
                        name = "CertificationNotFound",
                        value = message.Value,
                        resourceNotFound = message.ResourceNotFound,
                        searchedLocation = message.SearchedLocation
                    }
                });
            }

            return NoContent();
        }
    }
}