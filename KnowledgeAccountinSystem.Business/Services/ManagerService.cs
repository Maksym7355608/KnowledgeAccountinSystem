using AutoMapper;
using KnowledgeAccountinSystem.Business.Extensions;
using KnowledgeAccountinSystem.Business.Interfaces;
using KnowledgeAccountinSystem.Business.Models;
using KnowledgeAccountinSystem.Business.Validation;
using KnowledgeAccountinSystem.Data;
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
            if (context.ProgrammerRepository?.GetByIdAsync(programmerId) == null)
                throw new ModelException("uncorrect choosen id");

            await context.ManagerRepository.ChooseProgrammerAsync(id, programmerId);
            await context.SaveAsync();
        }

        public async Task DeleteAccountAsync(int id)
        {
            var user = await context.ManagerRepository.GetByIdAsync(id);
            await context.ManagerRepository.DeleteByIdAsync(id);
            await context.AccountRepository.DeleteByIdAsync(user.User.Id);

            await context.SaveAsync();
        }

        public async Task DeleteProgrammerAsync(int id, int programmerId)
        {
            IEnumerable<ProgrammerModel> choosen = await GetChoosenProgrammersAsync(id);
            var programmer = choosen.FirstOrDefault(x => x.Id == programmerId);

            if (programmer.Equals(null))
                throw new ModelException("programmer not found");

            await context.ManagerRepository.DeleteProgrammerAsync(id, programmerId);
            await context.SaveAsync();
        }

        public IEnumerable<ProgrammerModel> GetAllProgrammers()
        {
            return mapper.Map<IEnumerable<ProgrammerModel>>(context.ProgrammerRepository.GetAll().AsEnumerable());
        }

        public async Task<ProgrammerModel> GetChoosenProgrammerAsync(int id, int programmerId)
        {
            IEnumerable<ProgrammerModel> choosen = await GetChoosenProgrammersAsync(id);
            var programmer = choosen.FirstOrDefault(x => x.Id == programmerId);

            if (programmer.Equals(null))
                throw new ModelException("programmer not found");

            return mapper.Map<ProgrammerModel>(programmer);
        }

        public async Task<IEnumerable<ProgrammerModel>> GetChoosenProgrammersAsync(int id)
        {
            IEnumerable<ProgrammerModel> choosen = mapper.Map<IEnumerable<ProgrammerModel>>
                (await context.ManagerRepository?.GetChoosenProgrammersAsync(id));
            return choosen;
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

        public int GetRoleId(int userId)
        {
            int? roleId = context.ManagerRepository.GetAll().FirstOrDefault(x => x.User.Id == userId)?.Id;
            if(roleId.HasValue)
                throw new AuthorizeException("Unauthorized on this role", HttpStatusCode.Unauthorized);

            return roleId.Value;
        }
    }
}
