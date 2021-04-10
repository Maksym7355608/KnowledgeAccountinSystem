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
        private readonly IConfiguration config;

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
    }
}
