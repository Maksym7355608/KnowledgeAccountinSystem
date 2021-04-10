using System.Collections.Generic;

namespace KnowledgeAccountinSystem.Data.Entities
{
    public class Programmer
    {
        public int Id { get; set; }
        public User User { get;set; }
        public IEnumerable<Skill> Skills { get; set; }

        public int? ManagerId { get; set; }
        public Manager Manager { get; set; }
    }
}
