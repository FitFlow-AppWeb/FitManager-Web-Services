using FitManager_Web_Services.Classes.Domain.Services; 
using FitManager_Web_Services.Classes.Interfaces.REST.Resources; 
using FitManager_Web_Services.Classes.Interfaces.REST.Transform; 
using FitManager_Web_Services.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using FitManager_Web_Services.Classes.Application.Internal.QueryServices;
using FitManager_Web_Services.Classes.Domain.Queries;

using FitManager_Web_Services.Classes.Application.Internal.CommandServices;
using MediatR; 

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
    private readonly AttendanceQueryService _attendanceQueryService;
    private readonly RawAttendanceResourceFromEntityAssembler _rawAttendanceResourceAssembler;
    private readonly IMediator _mediator; 

    /// <summary>
    /// Initializes a new instance of the <see cref="AttendancesController"/> class.
    /// </summary>
    /// <param name="attendanceService">The attendance domain service.</param>
    /// <param name="localizer">The string localizer.</param>
    /// <param name="attendanceQueryService">The attendance query service.</param>
    /// <param name="mediator">The MediatR mediator for sending commands and queries.</param> 
    public AttendancesController(
        IAttendanceService attendanceService,
        IStringLocalizer<SharedResource> localizer,
        AttendanceQueryService attendanceQueryService,
        IMediator mediator) // <-- Añadir IMediator al constructor
    {
        _attendanceService = attendanceService;
        _localizer = localizer;
        _attendanceQueryService = attendanceQueryService;
        _rawAttendanceResourceAssembler = new RawAttendanceResourceFromEntityAssembler();
        _mediator = mediator; // <-- Asignar IMediator
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
    [ProducesResponseType(typeof(AttendanceResource), 200)] 
    [ProducesResponseType(400)] 
    [ProducesResponseType(401)] 
    public async Task<IActionResult> RegisterAttendance([FromBody] CreateAttendanceResource resource)
    {
        
        var command = new RegisterAttendanceCommand(
            resource.EntryTime,
            resource.ExitTime,
            resource.MemberId,
            resource.ClassId);

        
        var result = await _mediator.Send(command); 

        
        var attendanceResource = AttendanceResourceFromEntityAssembler.ToResource(result);

 
        var message = _localizer["AttendanceRetrievedSuccessfully"]; 

        return Ok(new
        {
            message = message.Value, // Acceder directamente al valor localizado
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
    
    /// <summary>
    /// Retrieves a list of all attendance records.
    /// </summary>
    /// <returns>
    /// An <see cref="IActionResult"/> containing a list of <see cref="AttendanceResource"/> objects.
    /// Returns 200 OK with the list of all attendances.
    /// </returns>
    [HttpGet] 
    [Authorize]
    [SwaggerOperation(
        Summary = "List All Attendances",
        Description = "Retrieves a list of all attendance records."
    )]
    [ProducesResponseType(typeof(IEnumerable<AttendanceResource>), 200)]
    [ProducesResponseType(401)]
    public async Task<IActionResult> GetAllAttendances()
    {
        var query = new GetAllAttendancesQuery();
        var attendances = await _attendanceQueryService.Handle(query);

        var resources = attendances.Select(AttendanceResourceFromEntityAssembler.ToResource).ToList();

        var message = resources.Any()
            ? _localizer["AttendancesRetrievedSuccessfully"]
            : _localizer["AttendanceNotFound"];

        return Ok(new
        {
            message = message.Value,
            data = resources
        });
    }

    
    /// <summary>
    /// Gets all raw attendance records (with entry and exit times) that occurred today.
    /// This endpoint is used for dashboard occupancy visualization.
    /// </summary>
    /// <returns>A list of raw attendance records for today.</returns>
    [HttpGet("raw-today")] 
    [Authorize] 
    [ProducesResponseType(typeof(IEnumerable<RawAttendanceResource>), 200)]
    [ProducesResponseType(401)] 
    [ProducesResponseType(500)] 
    [SwaggerOperation(
        Summary = "Get Today's Raw Attendances",
        Description = "Retrieves all raw attendance records (including entry/exit times) for the current day for occupancy calculations."
    )]
    public async Task<IActionResult> GetRawAttendancesForToday()
    {
        var query = new GetAllAttendancesQuery(); 
        var allAttendances = await _attendanceQueryService.Handle(query); 

        
        var today = DateTime.Today; 
        var todayAttendances = allAttendances.Where(a =>
                a.EntryTime.Date == today || a.ExitTime.Date == today 
        ).ToList();

        var resources = _rawAttendanceResourceAssembler.ToResourcesFromEntities(todayAttendances);

        
        var message = todayAttendances.Any()
            ? _localizer["AttendancesRetrievedSuccessfully"] 
            : _localizer["NoAttendancesToday"]; 


        return Ok(new
        {
            message = message.Value, 
            data = resources
        });
    }

    /// <summary>
    /// Checks if an attendance record already exists for a specific member, class, and date.
    /// </summary>
    /// <param name="memberId">The unique identifier of the member.</param>
    /// <param name="classId">The unique identifier of the class.</param>
    /// <param name="date">The specific date to check for attendance (format YYYY-MM-DD).</param>
    /// <returns>A JSON object with a boolean 'exists' indicating if the attendance was found.</returns>
    [HttpGet("exists")] 
    [Authorize]
    [SwaggerOperation(
        Summary = "Check if Attendance Exists",
        Description = "Checks if an attendance record already exists for a specific member, class, and date."
    )]
    [ProducesResponseType(typeof(object), 200)] 
    [ProducesResponseType(400)]  
    [ProducesResponseType(401)] 
    public async Task<IActionResult> CheckAttendanceExists([FromQuery] int memberId, [FromQuery] int classId, [FromQuery] DateTime date)
    {
        var exists = await _attendanceService.DoesAttendanceExistForMemberClassAndDateAsync(memberId, classId, date.Date);
        return Ok(new { exists = exists });
    }
}