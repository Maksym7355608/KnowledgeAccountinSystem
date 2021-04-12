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
    [Authorize(Roles = Data.Entities.Roles.Manager)]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService service;
        private int userId => int.Parse(User.Claims.ElementAt(0).Value);
        private int managerId => service.GetRoleId(userId);

        public ManagerController(IManagerService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProgrammerModel>> GetAllProgrammers()
        {
            return Ok(service.GetAllProgrammers());
        }

        [HttpGet("choosen")]
        public async Task<ActionResult<IEnumerable<ProgrammerModel>>> GetChoosenProgrammers()
        {
            try
            {
                return Ok(await service.GetChoosenProgrammersAsync(managerId));
            }
            catch(KASException e)
            {
                return BadRequest(e.Message);
            }            
        }

        [HttpGet("choosen/{programmerId}")]
        public async Task<ActionResult<ProgrammerModel>> GetChoosenProgrammer(int programmerId)
        {
            try
            {
                return Ok(await service.GetChoosenProgrammerAsync(managerId, programmerId));
            }
            catch (KASException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{programmerId}")]
        public async Task<ActionResult> ChooseProgrammer(int programmerId)
        {
            try
            {
                await service.ChooseProgrammerAsync(managerId, programmerId);
                return Ok();
            }
            catch (KASException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{programmerId}")]
        public async Task<ActionResult> DeleteProgrammer(int programmerId)
        {
            try
            {
                await service.DeleteProgrammerAsync(managerId, programmerId);
                return Ok();
            }
            catch (KASException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("account")]
        public async Task<ActionResult> UpdateManagerAccount([FromBody] UserModel model)
        {
            try
            {
                model.Id = userId;
                await service.UpdateAccountAsync(model);
                return Ok();
            }
            catch (KASException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("account")]
        public async Task<ActionResult> DeleteManagerAccount()
        {
            try
            {
                await service.DeleteAccountAsync(managerId);
                return Ok();
            }
            catch (KASException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
