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

        public async Task AddSkillAsync(int programmerId, SkillModel skill)
        {
            if (!context.ProgrammerRepository.GetAll().Select(x => x.Id).Contains(programmerId))
                throw new KASException("no programmers with same id!", HttpStatusCode.BadRequest);
            if (context.SkillRepository.GetAllByProgrammerId(programmerId).Select(x => x.Name).Contains(skill.Name))
                throw new KASException("this skill is already exists");
            if (Enum.GetValues(typeof(SkillName)).Cast<SkillName>().Contains(skill.Name) ||
                Enum.GetValues(typeof(SkillLevel)).Cast<SkillLevel>().Contains(skill.Level))
                throw new KASException("uncorrect model", HttpStatusCode.BadRequest);

            skill.ProgrammerId = programmerId;
            await context.SkillRepository.AddAsync(mapper.Map<Skill>(skill));
            context.Save();
        }

        public async Task DeleteAccountAsync(int id)
        {
            if (!context.AccountRepository.GetAll().Select(x => x.Id).Contains(id))
                throw new KASException("no users with same id!", HttpStatusCode.BadRequest);

            var user = (await context.ProgrammerRepository.GetByIdAsync(id)).User;
            await context.ProgrammerRepository.DeleteByIdAsync(id);
            await context.AccountRepository.DeleteByIdAsync(user.Id);

            await context.SaveAsync();
        }

        public async Task DeleteSkillAsync(int programmerId, int skillId)
        {
            var programmer = await context.ProgrammerRepository?.GetByIdAsync(programmerId);
            if (programmer.Equals(null))
                throw new KASException("no programmers with same id!", HttpStatusCode.BadRequest);
            if (programmer.Skills.Select(x => x.Id).Contains(skillId))
                throw new KASException("skill not found", HttpStatusCode.BadRequest);

            await context.SkillRepository.DeleteByIdAsync(skillId);
            await context.SaveAsync();
        }

        public async Task EditSkillAsync(int programmerId, SkillModel skill)
        {
            if (!context.ProgrammerRepository.GetAll().Select(x => x.Id).Contains(programmerId))
                throw new KASException("no programmers with same id!", HttpStatusCode.BadRequest);
            if (!context.SkillRepository.GetAllByProgrammerId(programmerId).Select(x => x.Name).Contains(skill.Name))
                throw new KASException("you don`t have this skill");
            if (context.SkillRepository.GetAllByProgrammerId(programmerId).Select(x => x.Name).Contains(skill.Name))
                throw new KASException("this skill is already exists");
            if (Enum.GetValues(typeof(SkillName)).Cast<SkillName>().Contains(skill.Name) ||
                Enum.GetValues(typeof(SkillLevel)).Cast<SkillLevel>().Contains(skill.Level))
                throw new KASException("uncorrect model", HttpStatusCode.BadRequest);

            skill.ProgrammerId = programmerId;
            var sk = await context.SkillRepository.GetByIdAsync(skill.Id);
            context.SkillRepository.Update(mapper.Map(skill, sk));

            await context.SaveAsync();
        }

        public int GetRoleId(int userId)
        {
            try
            {
                return context.ProgrammerRepository.GetAll().FirstOrDefault(x => x.User.Id == userId).Id;
            }
            catch (KASException)
            {
                throw new KASException("Unauthorized on this role", HttpStatusCode.Unauthorized);
            }
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


            var user = await context.AccountRepository.GetByIdAsync(model.Id);
            context.AccountRepository.Update(mapper.Map(model, user));
            await context.SaveAsync();
        }
    }
}

//skill.Name > Enum.GetValues(typeof(SkillName)).Cast<SkillName>().Max() ||
//                skill.Name < Enum.GetValues(typeof(SkillName)).Cast<SkillName>().Min() ||
//                skill.Level > Enum.GetValues(typeof(SkillLevel)).Cast<SkillLevel>().Max() ||
//                skill.Level < Enum.GetValues(typeof(SkillLevel)).Cast<SkillLevel>().Min()
