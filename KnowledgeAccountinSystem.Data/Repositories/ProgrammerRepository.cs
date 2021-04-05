using KnowledgeAccountinSystem.Data.Entities;
using KnowledgeAccountinSystem.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Data.Repositories
{
    public class ProgrammerRepository : IProgrammerRepository
    {
        private readonly KnowledgeAccountinSystemContext context;

        public ProgrammerRepository(KnowledgeAccountinSystemContext context)
        {
            this.context = context;
        }

        public void Add(Programmer entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddAsync(Programmer entity) => await context.Programmers.AddAsync(entity);

        public async Task DeleteByIdAsync(int id) => context.Programmers.Remove(await context.Programmers.FindAsync(id));

        public IEnumerable<Programmer> GetAll() => context.Programmers.Include(x => x.User).Include(x => x.Skills);

        public async Task<Programmer> GetByIdAsync(int id) => await context.Programmers.FindAsync(id);

        public void Update(Programmer entity) => context.Programmers.Update(entity);
    }
}
