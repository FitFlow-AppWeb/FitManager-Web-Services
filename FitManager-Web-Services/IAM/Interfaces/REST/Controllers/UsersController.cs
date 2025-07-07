using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using FitManager_Web_Services.IAM.Domain.Model.Commands;
using FitManager_Web_Services.IAM.Domain.Model.Queries;
using FitManager_Web_Services.IAM.Application.Internal.CommandServices;
using FitManager_Web_Services.IAM.Application.Internal.QueryServices;
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