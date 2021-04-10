using System.Collections.Generic;

namespace KnowledgeAccountinSystem.Data.Entities
{
    public class Programmer
    {
        public int Id { get; set; }
        public virtual User User { get;set; }
        public virtual IEnumerable<Skill> Skills { get; set; }

        public int? ManagerId { get; set; }
        public virtual Manager Manager { get; set; }
    }
}
