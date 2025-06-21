using FitManager_Web_Services.Classes.Domain.Services;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;
using FitManager_Web_Services.Classes.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    private readonly IBookingService _bookingService;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookingsController"/> class.
    /// </summary>
    /// <param name="bookingService">The booking domain service.</param>
    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
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
    [SwaggerOperation(
        Summary = "List Bookings by Class",
        Description = "Retrieves a list of all bookings registered for a specific class."
    )]
    public async Task<IActionResult> GetBookingsByClass(int classId)
    {
        var results = await _bookingService.GetBookingsByClassAsync(classId);
        var resources = results.Select(BookingResourceFromEntityAssembler.ToResource); 
        return Ok(resources);
    }
    
}