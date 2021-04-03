using KnowledgeAccountinSystem.Business.Interfaces;
using KnowledgeAccountinSystem.Business.Models;
using KnowledgeAccountinSystem.Business.Validation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace KnowledgeAccountinSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService service;

        public StatisticController(IStatisticService service)
        {
            this.service = service;
        }

        [HttpGet("programmers/{count}")]
        public ActionResult<IEnumerable<ProgrammerModel>> GetTheMostPopularProgrammers(int count)
        {
            try
            {
                return Ok(service.GetTheMostPopularProgrammers(count));
            }
            catch (KASException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("managers/{count}")]
        public ActionResult<IEnumerable<ManagerModel>> GetTopManagers(int count)
        {
            try
            {
                return Ok(service.GetTopManagers(count));
            }
            catch (KASException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("skills/{count}")]
        public ActionResult<IEnumerable<Data.Entities.SkillName>> GetTheMostPopularSkills(int count)
        {
            try
            {
                return Ok(service.GetTheMostPopularSkills(count));
            }
            catch (KASException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
