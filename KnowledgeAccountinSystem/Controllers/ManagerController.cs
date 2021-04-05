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
        private readonly int managerId;

        public ManagerController(IManagerService service)
        {
            this.service = service;
            managerId = service.GetRoleId(int.Parse(User.Claims.ElementAt(0).Value));
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

        [HttpGet("choosen/{programmer}")]
        public async Task<ActionResult<ProgrammerModel>> GetChoosenProgrammer(ProgrammerModel programmer)
        {
            try
            {
                return Ok(await service.GetChoosenProgrammerAsync(managerId, programmer));
            }
            catch (KASException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ChooseProgrammer([FromBody] ProgrammerModel programmer)
        {
            try
            {
                await service.ChooseProgrammerAsync(managerId, programmer);
                return Ok();
            }
            catch (KASException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProgrammer([FromBody] ProgrammerModel programmer)
        {
            try
            {
                await service.DeleteProgrammerAsync(managerId, programmer);
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
