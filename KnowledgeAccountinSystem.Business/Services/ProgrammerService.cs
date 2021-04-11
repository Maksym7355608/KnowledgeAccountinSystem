using AutoMapper;
using KnowledgeAccountinSystem.Business.Extensions;
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

        public async Task AddSkillAsync(int programmerId, SkillModel skill)
        {
            if (skill.IsSkillExist(context, programmerId))
                throw new UnuniqueException("this skill is already exists");
            if (skill.IsSkillModelNotValid())
                throw new ModelException("uncorrect model", HttpStatusCode.BadRequest);

            skill.ProgrammerId = programmerId;
            await context.SkillRepository.AddAsync(mapper.Map<Skill>(skill));
            context.Save();
        }

        public async Task DeleteAccountAsync(int id)
        {
            var user = (await context.ProgrammerRepository.GetByIdAsync(id)).User;
            await context.ProgrammerRepository.DeleteByIdAsync(id);
            await context.AccountRepository.DeleteByIdAsync(user.Id);

            await context.SaveAsync();
        }

        public async Task DeleteSkillAsync(int programmerId, int skillId)
        {
            var programmer = await context.ProgrammerRepository?.GetByIdAsync(programmerId);
            if (programmer.Skills.Select(x => x.Id).Contains(skillId))
                throw new UnuniqueException("skill not found", HttpStatusCode.BadRequest);

            await context.SkillRepository.DeleteByIdAsync(skillId);
            await context.SaveAsync();
        }

        public async Task EditSkillAsync(int programmerId, SkillModel skill)
        {
            if (!skill.IsSkillExist(context, programmerId))
                throw new UnuniqueException("you don`t have this skill");
            if (skill.IsSkillModelNotValid())
                throw new ModelException("uncorrect model", HttpStatusCode.BadRequest);

            skill.ProgrammerId = programmerId;
            var sk = await context.SkillRepository.GetByIdAsync(skill.Id);
            context.SkillRepository.Update(mapper.Map(skill, sk));

            await context.SaveAsync();
        }

        public int GetRoleId(int userId)
        {
            int? roleId = context.ProgrammerRepository.GetAll().FirstOrDefault(x => x.User.Id == userId)?.Id;
            if (roleId.HasValue)
                throw new AuthorizeException("Unauthorized on this role", HttpStatusCode.Unauthorized);

            return roleId.Value;
        }

        public async Task<SkillModel> GetSkillByIdAsync(int programmerId, int skillId)
        {
            var skills = await GetSkillsAsync(programmerId);
            var skill = skills.FirstOrDefault(x => x.Id == skillId);

            if (skill.Equals(null))
                throw new ModelException("incorrect skill id", HttpStatusCode.BadRequest);

            return skill;
        }

        public async Task<IEnumerable<SkillModel>> GetSkillsAsync(int programmerId)
        {
            return mapper.Map<IEnumerable<SkillModel>>((await Task.Run(() =>
                context.SkillRepository.GetAllByProgrammerId(programmerId)))
                .AsEnumerable());
        }

        public async Task UpdateAccountAsync(UserModel model)
        {
            if (model.Id.IsAccountNotExist(context))
                throw new AuthorizeException("no programmers with same id!", HttpStatusCode.BadRequest);
            if (model.IsModelValid())
                throw new ModelException("uncorrect user model", HttpStatusCode.BadRequest);


            var user = await context.AccountRepository.GetByIdAsync(model.Id);
            context.AccountRepository.Update(mapper.Map(model, user));
            await context.SaveAsync();
        }
    }
}