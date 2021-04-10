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
    public class ManagerService : IManagerService
    {
        private readonly IUnitOfWork context;
        private readonly IMapper mapper;

        public ManagerService(IUnitOfWork context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task ChooseProgrammerAsync(int id, int programmerId)
        {
            if (!context.ProgrammerRepository.GetAll().Select(x => x.Id).Contains(programmerId))
                throw new KASException("no programmers with same id!", HttpStatusCode.BadRequest);

            await context.ManagerRepository.ChooseProgrammerAsync(id, programmerId);
            await context.SaveAsync();
        }

        public async Task DeleteAccountAsync(int id)
        {
            if (!context.AccountRepository.GetAll().Select(x => x.Id).Contains(id))
                throw new KASException("no users with same id!", HttpStatusCode.BadRequest);
                        
            var user = await context.ManagerRepository.GetByIdAsync(id);
            await context.ManagerRepository.DeleteByIdAsync(id);
            await context.AccountRepository.DeleteByIdAsync(user.User.Id);

            await context.SaveAsync();
        }

        public async Task DeleteProgrammerAsync(int id, int programmerId)
        {
            if (!context.ProgrammerRepository.GetAll().Select(x => x.Id).Contains(programmerId))
                throw new KASException("no programmers with same id!", HttpStatusCode.BadRequest);

            await context.ManagerRepository.DeleteProgrammerAsync(id, programmerId);
            await context.SaveAsync();
        }

        public IEnumerable<ProgrammerModel> GetAllProgrammers()
        {
            return mapper.Map<IEnumerable<ProgrammerModel>>(context.ProgrammerRepository.GetAll().AsEnumerable());
        }

        public async Task<ProgrammerModel> GetChoosenProgrammerAsync(int id, int programmerId)
        {
            if (!context.ProgrammerRepository.GetAll().Select(x => x.Id).Contains(programmerId))
                throw new KASException("no programmers with same id!", HttpStatusCode.BadRequest);

            IEnumerable<ProgrammerModel> choosen = await GetChoosenProgrammersAsync(id) ?? null;

            return mapper.Map<ProgrammerModel>(choosen?.FirstOrDefault(x => x.Id == programmerId));
        }

        public async Task<IEnumerable<ProgrammerModel>> GetChoosenProgrammersAsync(int id)
        {
            IEnumerable<ProgrammerModel> choosen = mapper.Map<IEnumerable<ProgrammerModel>>
                (await context.ManagerRepository?.GetChoosenProgrammersAsync(id));
            return choosen ?? throw new KASException("no choosen programmers", HttpStatusCode.NotFound);
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

        public int GetRoleId(int userId)
        {
            try
            {
                return context.ManagerRepository.GetAll().FirstOrDefault(x => x.User.Id == userId).Id;
            }
            catch (KASException)
            {
                throw new KASException("Unauthorized on this role", HttpStatusCode.Unauthorized);
            }
        }
    }
}
