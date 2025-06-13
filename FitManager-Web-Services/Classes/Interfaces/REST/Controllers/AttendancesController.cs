using FitManager_Web_Services.Classes.Application.Internal.CommandServices;
using FitManager_Web_Services.Classes.Domain.Services;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;
using FitManager_Web_Services.Classes.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    public async Task<IActionResult> RegisterAttendance([FromBody] CreateAttendanceResource resource)
    {
        var result = await _attendanceService.RegisterAttendanceAsync(
            resource.EntryTime,
            resource.ExitTime,
            resource.MemberId,
            resource.ClassId);
        
        var attendanceResource = AttendanceResourceFromEntityAssembler.ToResource(result);
        return CreatedAtAction(nameof(GetAttendanceById), new { id = attendanceResource.Id }, attendanceResource);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAttendanceById(int id)
    {
        var result = await _attendanceService.GetAttendanceByIdAsync(id);
        if (result == null) return NotFound();
        var resource = AttendanceResourceFromEntityAssembler.ToResource(result);
        return Ok(resource);
    }

    [HttpGet("class/{classId}")]
    public async Task<IActionResult> GetAttendancesByClass(int classId)
    {
        var results = await _attendanceService.GetAttendancesByClassAsync(classId);
        var resources = results.Select(AttendanceResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    [HttpGet("member/{memberId}")]
    public async Task<IActionResult> GetAttendancesByMember(int memberId)
    {
        var results = await _attendanceService.GetAttendancesByMemberAsync(memberId);
        var resources = results.Select(AttendanceResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttendance(int id, [FromBody] UpdateAttendanceResource resource)
    {
        await _attendanceService.UpdateAttendanceAsync(
            id,
            resource.EntryTime,
            resource.ExitTime);
        
        return NoContent();
    }
}