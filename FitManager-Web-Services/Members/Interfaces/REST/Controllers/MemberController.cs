using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Application.Internal.CommandServices;
using FitManager_Web_Services.Members.Application.Internal.QueryServices;
using FitManager_Web_Services.Members.Interfaces.REST.Resources;

namespace FitManager_Web_Services.Members.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly MemberCommandService _commandService;
        private readonly MemberQueryService _queryService;

        public MemberController(MemberCommandService commandService, MemberQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        // GET: api/member
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetAll()
        {
            var members = await _queryService.GetAllMembersAsync();
            return Ok(members);
        }

        // GET: api/member/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetById(int id)
        {
            var member = await _queryService.GetMemberByIdAsync(id);
            if (member == null)
                return NotFound();
            return Ok(member);
        }

        // GET: api/member/dni/{dni}
        [HttpGet("dni/{dni}")]
        public async Task<ActionResult<Member>> GetByDni(int dni)
        {
            var member = await _queryService.GetMemberByDniAsync(dni);
            if (member == null)
                return NotFound();
            return Ok(member);
        }

        // POST: api/member
        [HttpPost]
        public async Task<ActionResult> Create(Member member)
        {
            await _commandService.AddMemberAsync(member);
            return CreatedAtAction(nameof(GetById), new { id = member.Id }, member);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateMemberResource resource) 
        {
            
            var existingMember = await _queryService.GetMemberByIdAsync(id); 
            if (existingMember == null)
            {
                return NotFound(); 
            }
            existingMember.FirstName = resource.FirstName;
            existingMember.LastName = resource.LastName;
            existingMember.Age = resource.Age;
            existingMember.PhoneNumber = resource.PhoneNumber;
            existingMember.Address = resource.Address;
            existingMember.Dni = resource.Dni;
            existingMember.Email = resource.Email;

            await _commandService.UpdateMemberAsync(existingMember); 

            return NoContent(); // HTTP 204
        }

        // DELETE: api/member/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _commandService.DeleteMemberAsync(id);
            return NoContent();
        }
    }
}
