using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Members.Application.Internal.CommandServices;
using FitManager_Web_Services.Members.Application.Internal.QueryServices;
using FitManager_Web_Services.Members.Interfaces.REST.Resources;
using FitManager_Web_Services.Members.Interfaces.REST.Transform;
using FitManager_Web_Services.Members.Domain.Model.Commands;
using FitManager_Web_Services.Members.Domain.Model.Queries;

namespace FitManager_Web_Services.Members.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly MemberCommandService _memberCommandService;
        private readonly MemberQueryService _memberQueryService;

        public MemberController(MemberCommandService memberCommandService, MemberQueryService memberQueryService)
        {
            _memberCommandService = memberCommandService;
            _memberQueryService = memberQueryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember([FromBody] CreateMemberResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createCommand = CreateMemberCommandFromResourceAssembler.ToCommandFromResource(resource);
            var member = await _memberCommandService.Handle(createCommand);

            if (member == null)
            {
                return BadRequest("Could not create member due to an internal issue.");
            }

            var memberResource = MemberResourceFromEntityAssembler.ToResourceFromEntity(member);
            return Created(string.Empty, memberResource); 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberResource>>> GetAllMembers()
        {
            var getAllQuery = new GetAllMembersQuery();
            var members = await _memberQueryService.Handle(getAllQuery);
            var memberResources = MemberResourceFromEntityAssembler.ToResourceListFromEntityList(members);
            return Ok(memberResources);
        }

        
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] UpdateMemberResource resource) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateCommand = UpdateMemberCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var updatedMember = await _memberCommandService.Handle(updateCommand); 

            if (updatedMember == null)
            {
                return NotFound();
            }

            var memberResource = MemberResourceFromEntityAssembler.ToResourceFromEntity(updatedMember);
            return Ok(memberResource); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var deleteCommand = new DeleteMemberCommand(id);
            var success = await _memberCommandService.Handle(deleteCommand);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}