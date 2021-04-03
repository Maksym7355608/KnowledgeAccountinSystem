using KnowledgeAccountinSystem.Data.Interfaces;
using System.Threading.Tasks;

namespace KnowledgeAccountinSystem.Data
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }
        ISkillRepository SkillRepository { get; }
        IProgrammerRepository ProgrammerRepository { get; }
        IManagerRepository ManagerRepository { get; }

        Task SaveAsync();
    }
}
