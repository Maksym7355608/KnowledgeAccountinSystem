using KnowledgeAccountinSystem.Data.Entities;
using KnowledgeAccountinSystem.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task AddAsync(Programmer entity) => await context.Programmers
            .AddAsync(entity);

        public async Task DeleteByIdAsync(int id) => context.Programmers
            .Remove(await context.Programmers
            .FindAsync(id));

        public IQueryable<Programmer> GetAll() => context.Programmers
            .Include(x => x.Skills)
            .Include(x => x.User)
            .AsNoTracking();

        public async Task<Programmer> GetByIdAsync(int id) => await context.Programmers
            .Include(x => x.Skills)
            .Include(x => x.User)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);

        public void Update(Programmer entity) => context.Programmers
            .Update(entity);
    }
}
