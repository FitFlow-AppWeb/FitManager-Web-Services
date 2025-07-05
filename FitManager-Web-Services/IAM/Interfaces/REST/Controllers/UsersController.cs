using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FitManager_Web_Services.IAM.Domain.Model.Commands;
using FitManager_Web_Services.IAM.Domain.Model.Queries;
using FitManager_Web_Services.IAM.Application.Internal.CommandServices;
using FitManager_Web_Services.IAM.Application.Internal.QueryServices;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using FitManager_Web_Services.IAM.Interfaces.REST.Resources;
using FitManager_Web_Services.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Authorization;
using FitManager_Web_Services.Resources;
using Microsoft.Extensions.Localization;

namespace FitManager_Web_Services.IAM.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserCommandService _commandService;
        private readonly UserQueryService _queryService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UsersController(
            UserCommandService commandService,
            UserQueryService queryService,
            IStringLocalizer<SharedResource> localizer)
        {
            _commandService = commandService;
            _queryService = queryService;
            _localizer = localizer;
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(
            Summary = "Create user",
            Description = "Creates a new user in the system."
        )]
        public async Task<ActionResult> Create([FromBody] CreateUserCommand command)
        {
            var user = await _commandService.Handle(command);
            if (user == null)
            {
                return BadRequest(new
                {
                    message = _localizer["UserAlreadyExists"]
                });
            }

            var resource = UserTransformer.ToResource(user);
            return CreatedAtAction(nameof(GetById), new { id = resource.Id }, new
            {
                message = _localizer["UserCreated"],
                data = resource
            });
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation(
            Summary = "List users",
            Description = "Retrieves a list of all existing users."
        )]
        public async Task<ActionResult> GetAll()
        {
            var users = await _queryService.Handle(new GetAllUsersQuery());
            var resources = users.Select(UserTransformer.ToResource);
            return Ok(new
            {
                message = _localizer["UsersRetrieved"],
                data = resources
            });
        }

        [HttpGet("{id}")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Get user",
            Description = "Retrieves the details of a user by their ID."
        )]
        public async Task<ActionResult> GetById(int id)
        {
            var user = await _queryService.Handle(new GetUserByIdQuery(id));
            if (user == null)
            {
                return NotFound(new
                {
                    message = _localizer["UserNotFound"]
                });
            }

            var resource = UserTransformer.ToResource(user);
            return Ok(new
            {
                message = _localizer["UserRetrieved"],
                data = resource
            });
        }
    }
}