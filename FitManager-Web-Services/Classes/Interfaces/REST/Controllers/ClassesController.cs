using FitManager_Web_Services.Classes.Application.Internal.CommandServices;
using FitManager_Web_Services.Classes.Application.Internal.QueryServices;
using FitManager_Web_Services.Classes.Domain.Services;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;
using FitManager_Web_Services.Classes.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Members.Interfaces.REST.Transform;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ClassesController : ControllerBase
{
    private readonly IClassService _classService;

    public ClassesController(IClassService classService)
    {
        _classService = classService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateClass([FromBody] CreateClassResource resource)
    {
        var command = CreateClassCommandFromResourceAssembler.ToCommand(resource);
        var result = await _classService.CreateClassAsync(
            command.Name,
            command.Description,
            command.Type,
            command.Capacity,
            command.StartDate,
            command.Duration,
            command.Status,
            command.EmployeeId);
        
        var classResource = ClassResourceFromEntityAssembler.ToResource(result);
        return CreatedAtAction(nameof(GetClassById), new { id = classResource.Id }, classResource);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClassById(int id)
    {
        var result = await _classService.GetClassByIdAsync(id);
        if (result == null) return NotFound();
        var resource = ClassResourceFromEntityAssembler.ToResource(result);
        return Ok(resource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClasses()
    {
        var results = await _classService.GetAllClassesAsync();
        var resources = results.Select(ClassResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    [HttpGet("type/{type}")]
    public async Task<IActionResult> GetClassesByType(string type)
    {
        var results = await _classService.GetClassesByTypeAsync(type);
        var resources = results.Select(ClassResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClass(int id, [FromBody] UpdateClassResource resource)
    {
        await _classService.UpdateClassAsync(
            id,
            resource.Name,
            resource.Description,
            resource.Type,
            resource.Capacity,
            resource.StartDate,
            resource.Duration,
            resource.Status,
            resource.EmployeeId);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClass(int id)
    {
        await _classService.DeleteClassAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/members")]
    public async Task<IActionResult> GetClassMembers(int id)
    {
        var classMembers = await _classService.GetClassMembersAsync(id);
        var memberResources = classMembers
            .Select(cm => MemberResourceFromEntityAssembler.ToResourceFromEntity(cm.Member))
            .ToList();
    
        return Ok(memberResources);
    }
}