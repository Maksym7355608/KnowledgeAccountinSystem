using System.Collections.Generic;

namespace KnowledgeAccountinSystem.Business.Models
{
    public class ProgrammerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IEnumerable<SkillModel> Skills { get; set; }
    }
}
