using FitManager_Web_Services.Classes.Domain.Services;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;
using FitManager_Web_Services.Classes.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FitManager_Web_Services.Classes.Interfaces.REST.Controllers;

/// <summary>
/// API controller for managing fitness classes.
/// </summary>
/// <remarks>
/// Provides RESTful endpoints for creating, retrieving, updating, and deleting class records.
/// </remarks>
[ApiController]
[Route("api/v1/[controller]")]
public class ClassesController : ControllerBase
{
    private readonly IClassService _classService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClassesController"/> class.
    /// </summary>
    /// <param name="classService">The class domain service.</param>
    public ClassesController(IClassService classService)
    {
        _classService = classService;
    }

    /// <summary>
    /// Creates a new class with the provided details.
    /// </summary>
    /// <param name="resource">The resource containing details for the new class.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> representing the result of the creation operation.
    /// Returns 200 OK with the created class resource on success.
    /// </returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a New Class",
        Description = "Creates a new class with the provided details."
    )]
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
        return  Ok(classResource);
    }
    
    /// <summary>
    /// Retrieves a list of all available classes.
    /// </summary>
    /// <returns>
    /// An <see cref="IActionResult"/> containing a list of <see cref="ClassResource"/> objects.
    /// Returns 200 OK with the list of classes.
    /// </returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "List All Classes",
        Description = "Retrieves a list of all available classes."
    )]
    public async Task<IActionResult> GetAllClasses()
    {
        var results = await _classService.GetAllClassesAsync();
        var resources = results.Select(ClassResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    /// <summary>
    /// Updates the details of an existing class.
    /// </summary>
    /// <param name="id">The unique identifier of the class to update.</param>
    /// <param name="resource">The resource containing the updated details for the class.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> representing the result of the update operation.
    /// Returns 204 No Content on successful update.
    /// </returns>
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a Class",
        Description = "Updates the details of an existing class."
    )]
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

    /// <summary>
    /// Deletes an existing class by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the class to delete.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> representing the result of the deletion operation.
    /// Returns 204 No Content on successful deletion.
    /// </returns>
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a Class",
        Description = "Deletes an existing class by its ID."
    )]
    public async Task<IActionResult> DeleteClass(int id)
    {
        await _classService.DeleteClassAsync(id);
        return NoContent();
    }
    
}