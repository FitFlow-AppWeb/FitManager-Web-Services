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

    [HttpGet("class/{classId}")]
    [SwaggerOperation(
        Summary = "List Attendances by Class",
        Description = "Retrieves a list of all attendances registered for a specific class."
    )]
    public async Task<IActionResult> GetBookingsByClass(int classId)
    {
        var results = await _bookingService.GetBookingsByClassAsync(classId);
        var resources = results.Select(BookingResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }
    
}