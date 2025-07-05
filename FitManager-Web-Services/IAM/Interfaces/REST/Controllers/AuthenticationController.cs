using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FitManager_Web_Services.IAM.Application.Internal.CommandServices;
using FitManager_Web_Services.IAM.Domain.Model;
using FitManager_Web_Services.IAM.Domain.Model.Commands;
using FitManager_Web_Services.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using FitManager_Web_Services.IAM.Interfaces.REST.Resources;
using FitManager_Web_Services.IAM.Interfaces.REST.Transform;
using FitManager_Web_Services.Resources;
using Microsoft.Extensions.Localization;

namespace FitManager_Web_Services.IAM.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly UserCommandService _commandService;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public AuthenticationController(UserCommandService commandService, IStringLocalizer<SharedResource> localizer)
    {
        _commandService = commandService;
        _localizer = localizer;
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
    {
        var command = SignUpCommandFromResourceAssembler.ToCommand(resource);
        await _commandService.Handle(command);

        return Created(string.Empty, new
        {
            message = _localizer["UserSignedUp"]
        });
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
    {
        var command = SignInCommandFromResourceAssembler.ToCommand(resource);
        var (user, token) = await _commandService.Handle(command);

        if (user == null)
        {
            return Unauthorized(new
            {
                message = _localizer["InvalidCredentials"]
            });
        }

        var result = AuthenticatedUserResourceFromEntityAssembler.ToResource(user, token);
        return Ok(new
        {
            message = _localizer["UserSignedIn"],
            data = result
        });
    }
}