namespace KnowledgeAccountinSystem.Data.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public SkillName Name { get; set; }
        public SkillLevel Level { get; set; }

        public int ProgrammerId { get; set; }
        public Programmer Programmer { get; set; }
    }

    public enum SkillName
    {
        dotNET = 1,
        Java,
        Python,
        JavaScript,
        C,
        Cpp,
        SQL,
        Ruby,
        PHP
    }

    public enum SkillLevel
    {
        Low = 1,
        Middle,
        Advanced
    }
}

