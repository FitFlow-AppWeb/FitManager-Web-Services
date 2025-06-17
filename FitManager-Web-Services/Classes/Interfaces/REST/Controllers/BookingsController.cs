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
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Crear una nueva Reserva",
        Description = "Crea una nueva reserva para un miembro en una clase específica."
    )]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingResource resource)
    {
        var result = await _bookingService.CreateBookingAsync(
            resource.MemberId,
            resource.ClassId,
            resource.Date);
        
        var bookingResource = BookingResourceFromEntityAssembler.ToResource(result);
        return CreatedAtAction(nameof(GetBookingById), new { id = bookingResource.Id }, bookingResource);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Obtener una Reserva por ID",
        Description = "Obtiene los detalles de una reserva específica por su ID."
    )]
    public async Task<IActionResult> GetBookingById(int id)
    {
        var result = await _bookingService.GetBookingByIdAsync(id);
        if (result == null) return NotFound();
        var resource = BookingResourceFromEntityAssembler.ToResource(result);
        return Ok(resource);
    }

    [HttpGet("class/{classId}")]
    [SwaggerOperation(
        Summary = "Listar Reservas por Clase",
        Description = "Obtiene una lista de todas las reservas realizadas para una clase específica."
    )]
    public async Task<IActionResult> GetBookingsByClass(int classId)
    {
        var results = await _bookingService.GetBookingsByClassAsync(classId);
        var resources = results.Select(BookingResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Cancelar una Reserva",
        Description = "Cancela una reserva existente por su ID."
    )]
    public async Task<IActionResult> CancelBooking(int id)
    {
        await _bookingService.CancelBookingAsync(id);
        return NoContent();
    }
}