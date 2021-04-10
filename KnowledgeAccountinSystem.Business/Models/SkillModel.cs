namespace KnowledgeAccountinSystem.Business.Models
{
    public class SkillModel
    {
        public int Id { get; set; }
        public Data.Entities.SkillName Name { get; set; }
        public Data.Entities.SkillLevel Level { get; set; }

        public int ProgrammerId { get; set; }
    }
}
