using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeAccountinSystem.Business.Models
{
    public class SkillModel
    {
        public int Id { get; set; }
        public Data.Entities.SkillName Name { get; set; }
        public Data.Entities.SkillLevel Level { get; set; }

        public string ProgrammerId { get; set; }
    }
}
