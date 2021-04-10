using KnowledgeAccountinSystem.Data.Entities;
using KnowledgeAccountinSystem.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Data.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private KnowledgeAccountinSystemContext context;

        public SkillRepository(KnowledgeAccountinSystemContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Skill entity) => await context.Skills
            .AddAsync(entity);

        public async Task DeleteByIdAsync(int id) => context.Skills
            .Remove(await context.Skills
            .FindAsync(id));

        public IQueryable<Skill> GetAll() => context.Skills
            .Include(x => x.Programmer)
            .AsNoTracking();

        public IQueryable<Skill> GetAllByProgrammerId(int programmerId) => context.Skills
            .Where(x => x.Programmer.Id == programmerId);

        public async Task<Skill> GetByIdAsync(int id) => await context.Skills
            .Include(x => x.Programmer)
            .SingleOrDefaultAsync(x => x.Id == id);

        public void Update(Skill entity) => context.Skills
            .Update(entity);
    }
}
