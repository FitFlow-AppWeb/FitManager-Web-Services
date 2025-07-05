using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FitManager_Web_Services.Users.Application.Internal.CommandServices;
using FitManager_Web_Services.Users.Domain.Model.Commands;
using FitManager_Web_Services.Users.Interfaces.REST.Resources;
using FitManager_Web_Services.Users.Interfaces.REST.Transform;

namespace FitManager_Web_Services.Users.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserCommandService _commandService;

        public AuthenticationController(UserCommandService commandService)
        {
            _commandService = commandService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
        {
            var command = SignUpCommandFromResourceAssembler.ToCommand(resource);
            await _commandService.Handle(command);
            return Created(string.Empty, null);
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
        {
            var command = SignInCommandFromResourceAssembler.ToCommand(resource);
            var (user, token) = await _commandService.Handle(command);
            if (user == null)
                return Unauthorized();
            var result = AuthenticatedUserResourceFromEntityAssembler.ToResource(user, token);
            return Ok(result);
        }
    }
}
