using FitManager_Web_Services.Classes.Domain.Commands; 
using FitManager_Web_Services.Classes.Domain.Model.Aggregates;
using FitManager_Web_Services.Classes.Domain.Services; 
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;
using FitManager_Web_Services.Classes.Interfaces.REST.Transform;
using FitManager_Web_Services.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Localization;
using MediatR; 
    
namespace FitManager_Web_Services.Classes.Interfaces.REST.Controllers;

/// <summary>
/// API controller for managing booking records.
/// </summary>
/// <remarks>
/// Provides RESTful endpoints for retrieving booking lists.
/// </remarks>
[ApiController]
[Route("api/v1/[controller]")]
public class BookingsController : ControllerBase
{
    // NO NECESITARÁS IBookingService para CreateBooking, solo para GetBookingsByClass
    // Si tu IBookingService solo se usaba para crear bookings, podrías eliminarlo.
    // Si lo usas para otras cosas (como GetBookingsByClass), lo mantendremos.
    private readonly IMediator _mediator; 
    private readonly IBookingService _bookingService; 
    private readonly IStringLocalizer<SharedResource> _localizer;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookingsController"/> class.
    /// </summary>
    /// <param name="bookingService">The booking domain service (if still needed for queries).</param>
    /// <param name="localizer">The string localizer for messages.</param>
    public BookingsController(
        IMediator mediator, // Inyectar IMediator
        IBookingService bookingService, // Mantener si GetBookingsByClassAsync sigue en él
        IStringLocalizer<SharedResource> localizer)
    {
        _mediator = mediator; // Asignar el mediador
        _bookingService = bookingService; // Asignar el servicio si se mantiene
        _localizer = localizer;
    }

    [HttpPost]
    [Authorize]
    [SwaggerOperation(
        Summary = "Create Booking",
        Description = "Registers a new booking for a class by a member."
    )]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Crear el comando a partir del recurso DTO
        // Podrías usar un assembler para esto si CreateBookingCommand tuviera más lógica de mapeo
        var command = new CreateBookingCommand(resource.MemberId, resource.ClassId, resource.Date);

        // Enviar el comando a través de MediatR
        Booking booking;
        try
        {
            booking = await _mediator.Send(command); 
        }
        catch (System.ArgumentException ex) 
        {
            return BadRequest(ex.Message); 
        }
        catch (System.InvalidOperationException ex)  
        {
            return Conflict(ex.Message); 
        }
        catch (System.Exception ex) 
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
        


        var bookingResource = BookingResourceFromEntityAssembler.ToResource(booking);

        return CreatedAtAction(nameof(GetBookingsByClass), new { classId = resource.ClassId }, bookingResource);
    }

    /// <summary>
    /// Retrieves a list of all bookings registered for a specific class.
    /// </summary>
    /// <param name="classId">The unique identifier of the class.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing a list of <see cref="BookingResource"/> objects.
    /// Returns 200 OK with the list of bookings.
    /// </returns>
    [HttpGet("class/{classId}")]
    [Authorize]
    [SwaggerOperation(
        Summary = "List Bookings by Class",
        Description = "Retrieves a list of all bookings registered for a specific class."
    )]
    public async Task<IActionResult> GetBookingsByClass(int classId)
    {
        
        var results = await _bookingService.GetBookingsByClassAsync(classId); 
        var resources = results.Select(BookingResourceFromEntityAssembler.ToResource);

        var message = _localizer["BookingsRetrieved"];

        return Ok(new
        {
            message = new
            {
                name = "BookingsRetrieved",
                value = message.Value,
                resourceNotFound = message.ResourceNotFound,
                searchedLocation = message.SearchedLocation
            },
            data = resources
        }); 
    }
}