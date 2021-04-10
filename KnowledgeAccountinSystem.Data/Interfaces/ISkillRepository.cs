using KnowledgeAccountinSystem.Data.Entities;
using System.Linq;

namespace KnowledgeAccountinSystem.Data.Interfaces
{
    public interface ISkillRepository : IRepository<Skill>
    {
        IQueryable<Skill> GetAllByProgrammerId(int programmerId);
    }
}
