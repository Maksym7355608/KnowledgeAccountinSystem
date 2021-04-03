using KnowledgeAccountinSystem.Data.Entities;
using System.Collections.Generic;

namespace KnowledgeAccountinSystem.Data.Interfaces
{
    public interface ISkillRepository : IRepository<Skill>
    {
        IEnumerable<Skill> GetAllByProgrammerId(int programmerId);
    }
}
