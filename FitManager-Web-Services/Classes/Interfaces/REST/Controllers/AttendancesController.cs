using FitManager_Web_Services.Classes.Application.Internal.CommandServices;
using FitManager_Web_Services.Classes.Domain.Services;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;
using FitManager_Web_Services.Classes.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AttendancesController : ControllerBase
{
    private readonly IAttendanceService _attendanceService;

    public AttendancesController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    [HttpPost]
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
        return Ok(attendanceResource);
    }
    
    [HttpGet("class/{classId}")]
    [SwaggerOperation(
        Summary = "List Attendances by Class",
        Description = "Retrieves a list of all attendances registered for a specific class."
    )]
    public async Task<IActionResult> GetAttendancesByClass(int classId)
    {
        var results = await _attendanceService.GetAttendancesByClassAsync(classId);
        var resources = results.Select(AttendanceResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }
    
}