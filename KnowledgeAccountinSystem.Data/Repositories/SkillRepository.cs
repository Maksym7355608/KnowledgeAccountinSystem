using KnowledgeAccountinSystem.Data.Entities;
using KnowledgeAccountinSystem.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Data.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly KnowledgeAccountinSystemContext context;

        public SkillRepository(KnowledgeAccountinSystemContext context)
        {
            this.context = context;
        }

        public void Add(Skill entity)
        {
            context.Skills.Add(entity);
        }

        public async Task AddAsync(Skill entity) => await context.Skills.AddAsync(entity);

        public async Task DeleteByIdAsync(int id) => context.Skills.Remove(await context.Skills.FindAsync(id));

        public IEnumerable<Skill> GetAll() => context.Skills;

        public IEnumerable<Skill> GetAllByProgrammerId(int programmerId) => context.Skills.Where(x => x.Programmer.Id == programmerId);

        public async Task<Skill> GetByIdAsync(int id) => await context.Skills.FindAsync(id);

        public void Update(Skill entity) => context.Skills.Update(entity);
    }
}
