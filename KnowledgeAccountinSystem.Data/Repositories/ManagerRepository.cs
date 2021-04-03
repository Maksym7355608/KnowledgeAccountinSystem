using KnowledgeAccountinSystem.Data.Entities;
using KnowledgeAccountinSystem.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace KnowledgeAccountinSystem.Data.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly KnowledgeAccountinSystemContext context;

        public ManagerRepository(KnowledgeAccountinSystemContext context)
        {
            this.context = context;
        }

        public async Task ChooseProgrammerAsync(int id, Programmer entity)
        {
            var manager = await context.Managers.FindAsync(id);
            manager.Programmers.ToList().Add(entity);
            context.Managers.Update(manager);
        }

        public async Task DeleteProgrammerAsync(int id, Programmer entity)
        {
            var manager = await context.Managers.FindAsync(id);
            manager.Programmers.ToList().Remove(entity);
            context.Managers.Update(manager);
        }

        public IEnumerable<Manager> GetAll()
        {
            return context.Managers;
        }

        public async Task<IEnumerable<Programmer>> GetChoosenProgrammersAsync(int id)
        {
            var res = await context.Managers.FindAsync(id);
            return res.Programmers;
        }
    }
}
