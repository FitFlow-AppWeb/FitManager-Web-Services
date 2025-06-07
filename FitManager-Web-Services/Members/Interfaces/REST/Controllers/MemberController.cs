using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitManager_Web_Services.Members.Domain.Model.Aggregates;
using FitManager_Web_Services.Members.Application.Internal.CommandServices;
using FitManager_Web_Services.Members.Application.Internal.QueryServices;

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

        // PUT: api/member/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Member member)
        {
            if (id != member.Id)
                return BadRequest();

            await _commandService.UpdateMemberAsync(member);
            return NoContent();
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
