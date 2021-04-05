using AutoMapper;
using KnowledgeAccountinSystem.Business.Interfaces;
using KnowledgeAccountinSystem.Business.Models;
using KnowledgeAccountinSystem.Business.Validation;
using KnowledgeAccountinSystem.Data;
using KnowledgeAccountinSystem.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork context;
        private readonly IMapper mapper;
        private IConfiguration config;

        public AccountService(IUnitOfWork context, IMapper mapper, IConfiguration config)
        {
            this.context = context;
            this.mapper = mapper;
            this.config = config;
        }

        private async Task<UserModel> FindUserAsync(string email, string password) =>
            mapper.Map<UserModel>(await context.AccountRepository.GetUserByEmailAndPasswordAsync(email, password));

        public async Task<string> LogInAsync(string email, string password)
        {
            var user = await FindUserAsync(email, password);
            if (user == null)
                throw new KASException("Unauthorized", HttpStatusCode.Unauthorized);
            else
                return GetToken(user);

        }

        private string GetToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config["JWT:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task RegisterAsync(UserModel model)
        {
            var user = mapper.Map<User>(model);
            user.Role = Roles.Programmer;
            await context.ProgrammerRepository.AddAsync(new Programmer { User = user });
            await context.SaveAsync();
        }

        /// <summary>
        /// This method update user account
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="KASException">id is not found</exception>
        /// <exception cref="KASException">model incorrect</exception>
        /// <returns></returns>
        public async Task UpdateAccountAsync(UserModel model)
        {
            if (!context.ProgrammerRepository.GetAll().Select(x => x.Id).Contains(model.Id))
                throw new KASException("no programmers with same id!", HttpStatusCode.BadRequest);
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Surname))
                throw new KASException("model incorrect");

            context.AccountRepository.Update(mapper.Map<User>(model));
            await context.SaveAsync();
        }

        /// <summary>
        /// This method delete user account
        /// </summary>
        /// <param name="userId"></param>
        /// <exception cref="KASException">id is not found</exception>
        /// <returns></returns>
        public async Task DeleteAccountAsync(int userId)
        {
            if (!context.AccountRepository.GetAll().Select(x => x.Id).Contains(userId))
                throw new KASException("no users with same id!", HttpStatusCode.BadRequest);
            var user = await context.AccountRepository.GetByIdAsync(userId);
            switch (user.Role)
            {
                case Roles.Programmer:
                    int pId = context.ProgrammerRepository.GetAll().Where(x => x.User.Id == userId).First().Id;
                    await context.ProgrammerRepository.DeleteByIdAsync(pId);
                    break;
                case Roles.Manager:
                    int mId = context.ManagerRepository.GetAll().Where(x => x.User.Id == userId).First().Id;
                    await context.ManagerRepository.DeleteByIdAsync(mId);
                    break;
            }
            await context.AccountRepository.DeleteByIdAsync(userId);
            await context.SaveAsync();
        }

        public async Task ChangeRoleToManagerAsync(int userId)
        {
            if (!context.ProgrammerRepository.GetAll().Select(x => x.Id).Contains(userId))
                throw new KASException("no users with same id!", HttpStatusCode.BadRequest);

            var programmer = await context.ProgrammerRepository.GetByIdAsync(userId);
            var programmer_skills = programmer.Skills;

            foreach (var skill in programmer_skills)
                await context.SkillRepository.DeleteByIdAsync(skill.Id);

            var user = programmer.User;
            user.Role = Roles.Manager;
            await context.ProgrammerRepository.DeleteByIdAsync(programmer.Id);
            await context.ManagerRepository.AddAsync(new Manager { User = user });

            await context.SaveAsync();
        }
    }
}
