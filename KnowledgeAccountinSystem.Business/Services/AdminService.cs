using AutoMapper;
using KnowledgeAccountinSystem.Business.Interfaces;
using KnowledgeAccountinSystem.Business.Models;
using KnowledgeAccountinSystem.Business.Validation;
using KnowledgeAccountinSystem.Data;
using KnowledgeAccountinSystem.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Business.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork context;
        private readonly IMapper mapper;

        public AdminService(IUnitOfWork context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task ChangeRoleAsync(int userId)
        {
            switch (await GetUserRoleAsync(userId))
            {
                case Roles.Programmer:
                    await ChangeRoleToManagerAsync(userId);
                    break;
                case Roles.Manager:
                    await ChangeRoleToProgrammerAsync(userId);
                    break;
                case Roles.Admin:
                    throw new UnuniqueException("admin must be one", HttpStatusCode.BadRequest);
            }
            await context.SaveAsync();
        }

        private async Task ChangeRoleToManagerAsync(int userId)
        {
            int roleId = GetRoleId(userId);
            var programmer = await context.ProgrammerRepository.GetByIdAsync(roleId);
            var programmer_skills = programmer.Skills;
            if (programmer_skills != null)
                foreach (var skill in programmer_skills)
                    await context.SkillRepository.DeleteByIdAsync(skill.Id);

            var user = await context.AccountRepository.GetByIdAsync(userId);
            user.Role = Roles.Manager;
            await context.ProgrammerRepository.DeleteByIdAsync(roleId);
            context.AccountRepository.Update(user);
            await context.ManagerRepository.AddAsync(new Manager { User = user });
        }

        private async Task ChangeRoleToProgrammerAsync(int userId)
        {
            int managerId = GetRoleId(userId);
            var manager = await context.ManagerRepository.GetByIdAsync(managerId);
            var user = await context.AccountRepository.GetByIdAsync(userId);
            var programmers = manager.Programmers;

            if(programmers != null)
                foreach (var programmer in programmers)
                    await context.ManagerRepository.DeleteProgrammerAsync(managerId, programmer.Id);

            await context.ManagerRepository.DeleteByIdAsync(managerId);
            user.Role = Roles.Programmer;
            await context.ProgrammerRepository.AddAsync(new Programmer { User = user });
        }

        private async Task<string> GetUserRoleAsync(int userId)
        {
            var user = await context.AccountRepository.GetByIdAsync(userId);
            if (user.Equals(null))
                throw new ModelException("uncorrect choosen id", HttpStatusCode.BadRequest);
            return user.Role;
        }

        private int GetRoleId(int userId)
        {
            try
            {
                return GetUserRoleAsync(userId).Result switch
                {
                    Roles.Programmer => context.ProgrammerRepository.GetAll().FirstOrDefault(x => x.User.Id == userId).Id,
                    Roles.Manager => context.ManagerRepository.GetAll().FirstOrDefault(x => x.User.Id == userId).Id,
                    _ => throw new AuthorizeException(),
                };
            }
            catch (AuthorizeException)
            {
                throw new AuthorizeException("Undefined role", HttpStatusCode.Unauthorized);
            }
        }

        public IEnumerable<UserModel> GetUsers()
        {
            return mapper.Map<IEnumerable<UserModel>>(context.AccountRepository.GetAll().AsEnumerable());
        }
    }
}
