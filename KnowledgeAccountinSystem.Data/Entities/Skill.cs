namespace KnowledgeAccountinSystem.Data.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public SkillName Name { get; set; }
        public SkillLevel Level { get; set; }

        public Programmer Programmer { get; set; }
    }

    public enum SkillName
    {
        dotNET,
        Java,
        Python,
        JavaScript,
        C,
        Cpp,
        SQL
    }

    public enum SkillLevel
    {
        None,
        Low,
        Middle,
        Advanced
    }
}

