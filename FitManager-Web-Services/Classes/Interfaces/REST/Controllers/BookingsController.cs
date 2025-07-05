using FitManager_Web_Services.Classes.Domain.Services;
using FitManager_Web_Services.Classes.Interfaces.REST.Resources;
using FitManager_Web_Services.Classes.Interfaces.REST.Transform;
using FitManager_Web_Services.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Localization;
    
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
    private readonly IStringLocalizer<SharedResource> _localizer;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookingsController"/> class.
    /// </summary>
    /// <param name="bookingService">The booking domain service.</param>
    public BookingsController(
        IBookingService bookingService,
        IStringLocalizer<SharedResource> localizer)
    {
        _bookingService = bookingService;
        _localizer = localizer;
    }
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create Booking",
        Description = "Registers a new booking for a class by a member."
    )]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var booking = await _bookingService.CreateBookingAsync(resource.MemberId, resource.ClassId, resource.Date);

        if (booking == null)
        {
            return NotFound(_localizer["BookingFailed"]);
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