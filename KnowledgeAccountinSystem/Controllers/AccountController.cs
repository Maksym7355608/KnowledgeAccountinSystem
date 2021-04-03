using AutoMapper;
using KnowledgeAccountinSystem.API.Models;
using KnowledgeAccountinSystem.Business.Interfaces;
using KnowledgeAccountinSystem.Business.Models;
using KnowledgeAccountinSystem.Business.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.API.Controllers
{
    [AllowAnonymous]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService service;
        private readonly IMapper mapper;

        public AccountController(IAccountService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginModel model)
        {
            try
            {
                var userToken = await service.LogInAsync(model.Email, model.Password);
                return Ok(userToken);
            }
            catch(KASException e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                await service.RegisterAsync(new UserModel { 
                    Name= model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    Password = model.Password
                });
                return Ok();
            }
            catch (KASException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UserModel model)
        {
            try
            {
                await service.UpdateAccountAsync(model);
                return Ok();
            }catch(KASException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] UserModel model)
        {
            try
            {
                await service.DeleteAccountAsync(model);
                return Ok();
            }
            catch (KASException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("change")]
        public async Task<ActionResult> ChangeToManager([FromBody] UserModel model)
        {
            try
            {
                await service.ChangeRoleToManagerAsync(model);
                return Ok();
            }
            catch (KASException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
