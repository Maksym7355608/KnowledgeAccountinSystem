using AutoMapper;
using KnowledgeAccountinSystem.Business.Interfaces;
using KnowledgeAccountinSystem.Business.Models;
using KnowledgeAccountinSystem.Business.Validation;
using KnowledgeAccountinSystem.Data;
using KnowledgeAccountinSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Business.Services
{
    public class ProgrammerService : IProgrammerService
    {
        private readonly IUnitOfWork context;
        private readonly IMapper mapper;

        public ProgrammerService(IUnitOfWork context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async void AddSkillAsync(int programmerId, SkillModel skill)
        {
            if (!context.ProgrammerRepository.GetAll().Select(x => x.Id).Contains(programmerId))
                throw new KASException("no programmers with same id!", HttpStatusCode.BadRequest);
            if (context.SkillRepository.GetAllByProgrammerId(programmerId).Select(x => x.Name).Contains(skill.Name))
                throw new KASException("this skill is already exists");
            skill.ProgrammerId = programmerId;
            await context.SkillRepository.AddAsync(mapper.Map<Skill>(skill));
            await context.SaveAsync();
        }

        public async Task DeleteAccountAsync(int id)
        {
            if (!context.AccountRepository.GetAll().Select(x => x.Id).Contains(id))
                throw new KASException("no users with same id!", HttpStatusCode.BadRequest);

            await context.ProgrammerRepository.DeleteByIdAsync(id);

            var user = await context.ProgrammerRepository.GetByIdAsync(id);
            await context.AccountRepository.DeleteByIdAsync(user.User.Id);

            await context.SaveAsync();
        }


        public async Task DeleteSkillAsync(int programmerId, SkillModel skill)
        {
            if (!context.ProgrammerRepository.GetAll().Select(x => x.Id).Contains(programmerId))
                throw new KASException("no programmers with same id!", HttpStatusCode.BadRequest);
            if (!context.SkillRepository.GetAllByProgrammerId(programmerId).Select(x => x.Name).Contains(skill.Name))
                throw new KASException("you don`t have this skill");
            skill.ProgrammerId = programmerId;
            await context.SkillRepository.DeleteByIdAsync(skill.Id);
            await context.SaveAsync();
        }

        public async Task EditSkillAsync(int programmerId, SkillModel skill)
        {
            if (!context.ProgrammerRepository.GetAll().Select(x => x.Id).Contains(programmerId))
                throw new KASException("no programmers with same id!", HttpStatusCode.BadRequest);
            if (!context.SkillRepository.GetAllByProgrammerId(programmerId).Select(x => x.Name).Contains(skill.Name))
                throw new KASException("you don`t have this skill");
            skill.ProgrammerId = programmerId;
            context.SkillRepository.Update(mapper.Map<Skill>(skill));

            await context.SaveAsync();
        }

        public int GetRoleId(int userId)
        {
            return context.ProgrammerRepository.GetAll().FirstOrDefault(x => x.User.Id == userId).Id;
        }

        public async Task<SkillModel> GetSkillByIdAsync(int programmerId, int skillId)
        {
            if (!context.ProgrammerRepository.GetAll().Select(x => x.Id).Contains(programmerId))
                throw new KASException("no programmers with same id!", HttpStatusCode.BadRequest);
            var a = await GetSkillsAsync(programmerId);
            return a.FirstOrDefault(x => x.Id == skillId);
        }

        public async Task<IEnumerable<SkillModel>> GetSkillsAsync(int programmerId)
        {
            if (!context.ProgrammerRepository.GetAll().Select(x => x.Id).Contains(programmerId))
                throw new KASException("no programmers with same id!", HttpStatusCode.BadRequest);

            return mapper.Map<IEnumerable<SkillModel>>(await Task.Run(() => context.SkillRepository.GetAllByProgrammerId(programmerId)));
        }

        public async Task UpdateAccountAsync(UserModel model)
        {
            if (!context.AccountRepository.GetAll().Select(x => x.Id).Contains(model.Id))
                throw new KASException("no programmers with same id!", HttpStatusCode.BadRequest);
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Surname))
                throw new KASException("model incorrect");


            var user = await context.ProgrammerRepository.GetByIdAsync(model.Id);
            user.User = mapper.Map<User>(model);

            context.ProgrammerRepository.Update(user);
            await context.SaveAsync();
        }
    }
}
