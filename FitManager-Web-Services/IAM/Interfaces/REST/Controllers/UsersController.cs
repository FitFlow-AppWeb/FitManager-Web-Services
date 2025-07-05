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

namespace FitManager_Web_Services.IAM.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserCommandService _commandService;
        private readonly UserQueryService _queryService;

        public UsersController(UserCommandService commandService, UserQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(
            Summary = "Create user",
            Description = "Creates a new user in the system."
        )]
        public async Task<ActionResult<UserResource>> Create([FromBody] CreateUserCommand command)
        {
            var user = await _commandService.Handle(command);
            if (user == null)
            {
                return BadRequest("User with the same email already exists.");
            }
            var resource = UserTransformer.ToResource(user);
            return CreatedAtAction(nameof(GetById), new { id = resource.Id }, resource);
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation(
            Summary = "List users",
            Description = "Retrieves a list of all existing users."
        )]
        public async Task<ActionResult<IEnumerable<UserResource>>> GetAll()
        {
            var users = await _queryService.Handle(new GetAllUsersQuery());
            var resources = users.Select(u => UserTransformer.ToResource(u));
            return Ok(resources);
        }

        [HttpGet("{id}")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Get user",
            Description = "Retrieves the details of a user by their ID."
        )]
        public async Task<ActionResult<UserResource>> GetById(int id)
        {
            var user = await _queryService.Handle(new GetUserByIdQuery(id));
            if (user == null)
                return NotFound();
            var resource = UserTransformer.ToResource(user);
            return Ok(resource);
        }
    }
}
