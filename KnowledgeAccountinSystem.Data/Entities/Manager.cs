using System.Collections.Generic;

namespace KnowledgeAccountinSystem.Data.Entities
{
    public class Manager
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual IEnumerable<Programmer> Programmers { get; set; }

    }
}
