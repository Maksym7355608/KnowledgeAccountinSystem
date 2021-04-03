using KnowledgeAccountinSystem.Business.Interfaces;
using KnowledgeAccountinSystem.Business.Models;
using KnowledgeAccountinSystem.Business.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Data.Entities.Roles.Programmer)]
    public class ProgrammerController : ControllerBase
    {
        private readonly IProgrammerService service;
        private int programmerId => int.Parse(User.Claims.ElementAt(0).Value);

        public ProgrammerController(IProgrammerService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillModel>>> GetSkills()
        {
            try
            {
                return Ok(await service.GetSkillsAsync(programmerId));
            }
            catch (KASException)
            {
                return BadRequest();
            }
        }

        [HttpGet("{skillId}")]
        public async Task<ActionResult<SkillModel>> GetSkillById(int skillId)
        {
            try
            {
                return Ok(await service.GetSkillByIdAsync(programmerId, skillId));
            }
            catch (KASException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddSkill(SkillModel skill)
        {
            try
            {
                await service.AddSkillAsync(programmerId, skill);
            }
            catch (KASException)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSkill(SkillModel skill)
        {
            try
            {
                await service.EditSkillAsync(programmerId, skill);
            }
            catch (KASException)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSkill(SkillModel skill)
        {
            try
            {
                await service.DeleteSkillAsync(programmerId, skill);
            }
            catch (KASException)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
