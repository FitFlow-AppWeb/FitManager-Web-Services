using FitManager_Web_Services.Classes.Domain.Services;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;
using FitManager_Web_Services.Classes.Interfaces.REST.Transform;
using FitManager_Web_Services.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Controllers;

/// <summary>
/// API controller for managing attendance records.
/// </summary>
/// <remarks>
/// Provides RESTful endpoints for registering new attendances and retrieving attendance lists.
/// </remarks>
[ApiController]
[Route("api/v1/[controller]")]
public class AttendancesController : ControllerBase
{
    private readonly IAttendanceService _attendanceService;
    private readonly IStringLocalizer<SharedResource> _localizer;

    /// <summary>
    /// Initializes a new instance of the <see cref="AttendancesController"/> class.
    /// </summary>
    /// <param name="attendanceService">The attendance domain service.</param>
    public AttendancesController(
        IAttendanceService attendanceService,
        IStringLocalizer<SharedResource> localizer)
    {
        _attendanceService = attendanceService;
        _localizer = localizer;
    }

    /// <summary>
    /// Registers a member's attendance for a specific class.
    /// </summary>
    /// <param name="resource">The resource containing details for the new attendance.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> representing the result of the registration operation.
    /// Returns 200 OK with the created attendance resource on success.
    /// </returns>
    [HttpPost]
    [Authorize]
    [SwaggerOperation(
        Summary = "Register Attendance",
        Description = "Registers a member's attendance for a specific class."
    )]
    public async Task<IActionResult> RegisterAttendance([FromBody] CreateAttendanceResource resource)
    {
        var result = await _attendanceService.RegisterAttendanceAsync(
            resource.EntryTime,
            resource.ExitTime,
            resource.MemberId,
            resource.ClassId);

        var attendanceResource = AttendanceResourceFromEntityAssembler.ToResource(result);

        return Ok(new
        {
            message = _localizer["AttendanceCreated"],
            data = attendanceResource
        });
    }
    
    /// <summary>
    /// Retrieves a list of all attendances registered for a specific class.
    /// </summary>
    /// <param name="classId">The unique identifier of the class.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing a list of <see cref="AttendanceResource"/> objects.
    /// Returns 200 OK with the list of attendances.
    /// </returns>
    [HttpGet("class/{classId}")]
    [Authorize]
    [SwaggerOperation(
        Summary = "List Attendances by Class",
        Description = "Retrieves a list of all attendances registered for a specific class."
    )]
    public async Task<IActionResult> GetAttendancesByClass(int classId)
    {
        var results = await _attendanceService.GetAttendancesByClassAsync(classId);
        var resources = results.Select(AttendanceResourceFromEntityAssembler.ToResource).ToList();

        var message = resources.Any()
            ? _localizer["AttendancesRetrieved"]
            : _localizer["AttendanceNotFound"];

        return Ok(new
        {
            message = new
            {
                name = message.Name,
                value = message.Value,
                resourceNotFound = message.ResourceNotFound,
                searchedLocation = message.SearchedLocation
            },
            data = resources
        });
    }
}