using System.Collections.Generic;

namespace KnowledgeAccountinSystem.Data.Entities
{
    public class Manager
    {
        public int Id { get; set; }
        public User User { get; set; }
        public IEnumerable<Programmer> Programmers { get; set; }

    }
}
