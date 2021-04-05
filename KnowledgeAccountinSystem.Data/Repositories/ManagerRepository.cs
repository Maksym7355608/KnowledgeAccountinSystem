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

        public void Add(Manager entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddAsync(Manager manager)
        {
            await context.Managers.AddAsync(manager);
        }

        public async Task ChooseProgrammerAsync(int id, Programmer entity)
        {
            var manager = await context.Managers.FindAsync(id);
            manager.Programmers.ToList().Add(entity);
            context.Managers.Update(manager);
        }

        public async Task DeleteByIdAsync(int id)
        {
            context.Managers.Remove(await context.Managers.FindAsync(id));
        }

        public async Task DeleteProgrammerAsync(int id, Programmer entity)
        {
            var manager = await context.Managers.FindAsync(id);
            manager.Programmers.ToList().Remove(entity);
            context.Managers.Update(manager);
        }

        public IEnumerable<Manager> GetAll()
        {
            return context.Managers.Include(x => x.User).Include(x => x.Programmers);
        }

        public async Task<Manager> GetByIdAsync(int id)
        {
            return await context.Managers.FindAsync(id);
        }

        public async Task<IEnumerable<Programmer>> GetChoosenProgrammersAsync(int id)
        {
            var res = await context.Managers.FindAsync(id);
            return res.Programmers;
        }

        public void Update(Manager entity)
        {
            context.Managers.Update(entity);
        }
    }
}
