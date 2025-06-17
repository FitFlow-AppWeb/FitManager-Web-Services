using FitManager_Web_Services.Classes.Application.Internal.CommandServices;
using FitManager_Web_Services.Classes.Application.Internal.QueryServices;
using FitManager_Web_Services.Classes.Domain.Services;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;
using FitManager_Web_Services.Classes.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Members.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(
        Summary = "Crear una nueva Clase",
        Description = "Crea una nueva clase con los detalles proporcionados."
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
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Listar todas las Clases",
        Description = "Obtiene una lista de todas las clases disponibles."
    )]
    public async Task<IActionResult> GetAllClasses()
    {
        var results = await _classService.GetAllClassesAsync();
        var resources = results.Select(ClassResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Actualizar una Clase",
        Description = "Actualiza los detalles de una clase existente."
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

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Eliminar una Clase",
        Description = "Elimina una clase existente por su ID."
    )]
    public async Task<IActionResult> DeleteClass(int id)
    {
        await _classService.DeleteClassAsync(id);
        return NoContent();
    }
    
}