using System.Collections.Generic;

namespace KnowledgeAccountinSystem.Business.Models
{
    public class ManagerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IEnumerable<ProgrammerModel> Programmers { get; set; }
    }
}
