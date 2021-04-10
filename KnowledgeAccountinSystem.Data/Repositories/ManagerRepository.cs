using KnowledgeAccountinSystem.Data.Entities;
using KnowledgeAccountinSystem.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeAccountinSystem.Data.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly KnowledgeAccountinSystemContext context;

        public ManagerRepository(KnowledgeAccountinSystemContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Manager manager) => await context.Managers
            .AddAsync(manager);

        public async Task ChooseProgrammerAsync(int id, int programmerId)
        {
            var programmer = await GetProgrammerByIdAsync(programmerId);
            programmer.ManagerId = id;
            context.Programmers.Update(programmer);
        }

        public async Task DeleteByIdAsync(int id) => context.Managers
            .Remove(await context.Managers
            .FindAsync(id));

        public async Task DeleteProgrammerAsync(int id, int programmerId)
        {
            var programmer = await GetProgrammerByIdAsync(programmerId);
            programmer.ManagerId = default;
            context.Programmers.Update(programmer);
        }

        public IQueryable<Manager> GetAll() => context.Managers
            .Include(x => x.Programmers)
            .Include(x => x.User)
            .AsNoTracking();

        public async Task<Manager> GetByIdAsync(int id) => await context.Managers
            .Include(x => x.Programmers)
            .Include(x => x.User)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Programmer>> GetChoosenProgrammersAsync(int id) => (await GetByIdAsync(id)).Programmers
            .Select(x => GetProgrammerByIdAsync(x.Id).Result);

        public void Update(Manager entity) => context.Managers
            .Update(entity);

        private async Task<Programmer> GetProgrammerByIdAsync(int programmerId) => await context.Programmers
            .Include(x => x.Skills)
            .Include(x => x.User)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == programmerId);
    }
}
